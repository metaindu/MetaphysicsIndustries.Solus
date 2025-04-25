
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

using System;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Macros;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Evaluators
{
    public partial class BasicEvaluator
    {
        public IMathObject Eval(ColorExpression expr,
            SolusEnvironment env)
        {
            return new Number(0xFFFFFF & expr.Color.ToArgb());
        }

        private int[] _componentAccessIndexesCache;

        // Warning: Not thread-safe
        public IMathObject Eval(ComponentAccess expr, SolusEnvironment env)
        {
            var value = Eval(expr.Expr, env);
            // TODO: there are some situations where we could work with a
            //       result of Expr.Eval that is not a concrete value. for
            //       example, "[a,2][1]" should evaluate to "2", even though
            //       "[a,2]" with an unbound variable would not evaluate to a
            //       concrete value.
            switch (value)
            {
                case IVector v:
                case StringValue s:
                    if (expr.Indexes.Count != 1)
                        throw new OperandException(
                            "Wrong number of indexes for the expression");
                    break;
                case IMatrix m:
                    if (expr.Indexes.Count != 2)
                        throw new OperandException(
                            "Wrong number of indexes for the expression");
                    break;
                default:
                    throw new OperandException(
                        "Unable to get components from expression, " +
                        "or the expression does not have components");
            }

            if (_componentAccessIndexesCache == null ||
                _componentAccessIndexesCache.Length < expr.Indexes.Count)
                _componentAccessIndexesCache = new int[expr.Indexes.Count];
            int i;
            for (i = 0; i < expr.Indexes.Count; i++)
            {
                var si = Eval(expr.Indexes[i], env);
                if (!(si is Number))
                    throw new IndexException(
                        "Indexes must be scalar");
                var vi = si.ToNumber().Value;
                if (!vi.IsInteger())
                    throw new IndexException(
                        "Indexes must be integers");
                if (vi < 0)
                    throw new IndexException(
                        "Indexes must not be negative");
                _componentAccessIndexesCache[i] = (int)vi;
            }

            switch (value)
            {
                case IVector v:
                    if (_componentAccessIndexesCache[0] >= v.Length)
                        throw new IndexException(
                            "Index exceeds the size of the vector");
                    return v.GetComponent(_componentAccessIndexesCache[0]);
                case StringValue s:
                    var index = _componentAccessIndexesCache[0];
                    if (index >= s.Length)
                        throw new IndexException(
                            "Index exceeds the size of the string");
                    return s.Value[index].ToStringValue();
                case IMatrix m:
                    if (_componentAccessIndexesCache[0] >= m.RowCount)
                        throw new IndexException(
                            "Index exceeds number of rows of the matrix");
                    if (_componentAccessIndexesCache[1] >= m.ColumnCount)
                        throw new IndexException(
                            "Index exceeds number of columns of the matrix");
                    return m.GetComponent(_componentAccessIndexesCache[0],
                        _componentAccessIndexesCache[1]);
            }

            throw new OperandException("Unknown");
        }

        public IMathObject Eval(DerivativeOfVariable expr,
            SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public IMathObject Eval(FunctionCall expr, SolusEnvironment env)
        {
            var f0 = Eval(expr.Function, env);
            if (!f0.IsIsFunction(env) &&
                !(f0 is Macro))
                throw new OperandException(
                    "Call target is not a function or macro");

            int i;
            if (f0 is Macro macro)
            {
                var args = new Expression[expr.Arguments.Count];
                for (i = 0; i < expr.Arguments.Count; i++)
                    args[i] = expr.Arguments[i];
                return Call(macro, args, env);
            }

            var f = (Function)f0;

            var evaluatedArgs = new IMathObject[expr.Arguments.Count];
            if (f.IsVariadic &&
                f.VariadicParameterType != null &&
                f.VariadicParameterType.IsSubsetOf(Sets.Expressions.Value))
            {
                for (i = 0; i < expr.Arguments.Count; i++)
                    evaluatedArgs[i] = expr.Arguments[i];
            }
            else
            {
                for (i = 0; i < expr.Arguments.Count; i++)
                {
                    if (!f.IsVariadic &&
                        f.Parameters[i].Type.IsSubsetOf(Sets.Expressions.Value))
                    {
                        evaluatedArgs[i] = expr.Arguments[i];
                    }
                    else
                        evaluatedArgs[i] = Eval(expr.Arguments[i], env);
                }
            }

            return Call(f, evaluatedArgs, env);
        }

        public IMathObject Eval(IntervalExpression expr, SolusEnvironment env)
        {
            var lower = Eval(expr.LowerBound, env);
            if (!lower.IsIsScalar(env))
                throw new OperandException("Lower bound is not a scalar");
            var upper = Eval(expr.UpperBound, env);
            if (!upper.IsIsScalar(env))
                throw new OperandException("Upper bound is not a scalar");
            return new Interval(lower.ToNumber().Value, expr.OpenLowerBound,
                upper.ToNumber().Value, expr.OpenUpperBound, false);
        }

        public IMathObject Eval(Literal expr, SolusEnvironment env)
        {
            return expr.Value;
        }

        public IMathObject Eval(MatrixExpression expr, SolusEnvironment env)
        {
            var values = new IMathObject[expr.RowCount, expr.ColumnCount];
            for (int r = 0; r < expr.RowCount; r++)
            for (int c = 0; c < expr.ColumnCount; c++)
                values[r, c] = Eval(expr[r, c], env);
            return new Matrix(values);
        }

        public IMathObject Eval(RandomExpression expr, SolusEnvironment env)
        {
            return ((float)RandomExpression.Source.NextDouble()).ToNumber();
        }

        public IMathObject Eval(VariableAccess expr, SolusEnvironment env)
        {
            var var = expr.VariableName;

            if (env.ContainsVariable(var))
            {
                var target = env.GetVariable(var);
                if (target.IsIsExpression(env))
                    target = Eval((Expression)target, env);
                return target;
            }

            throw new NameException(
                $"Variable not found: {expr.VariableName}");
        }

        public IMathObject Eval(VectorExpression expr, SolusEnvironment env)
        {
            var value0 = Eval(expr[0], env);
            IMathObject value1 = null;
            if (expr.Length > 1)
                value1 = Eval(expr[1], env);
            if (expr.Length == 2 &&
                value0.IsConcrete &&
                value0.IsIsScalar(null) &&
                value1.IsConcrete &&
                value1.IsIsScalar(null))
                return new Vector2(
                    value0.ToNumber().Value,
                    value1.ToNumber().Value);
            IMathObject value2 = null;
            if (expr.Length > 2)
                value2 = Eval(expr[2], env);
            if (expr.Length == 3 &&
                value0.IsConcrete &&
                value0.IsIsScalar(null) &&
                value1.IsConcrete &&
                value1.IsIsScalar(null) &&
                value2.IsConcrete &&
                value2.IsIsScalar(null))
                return new Vector3(
                    value0.ToNumber().Value,
                    ((Number)value1).Value,
                    value2.ToNumber().Value);

            var values = new IMathObject[expr.Length];
            values[0] = value0;
            if (expr.Length > 1) values[1] = value1;
            if (expr.Length > 2) values[2] = value2;
            for (int i = 3; i < expr.Length; i++)
                values[i] = Eval(expr[i], env);
            // Vector will take ownership of array
            return new Vector(values); // TODO: don't box here
        }
    }
}
