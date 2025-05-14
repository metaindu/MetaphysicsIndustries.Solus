/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Transformers;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Evaluators
{
    public partial class BasicEvaluator
    {
        public IMathObject CallFunction(AbsoluteValueFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return Math.Abs(args[0].ToNumber().Value).ToNumber();
        }

        public IMathObject CallFunction(AdditionOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            // TODO: vector
            // TODO: matrix
            // TODO: string?
            float sum = 0;
            foreach (var arg in args)
            {
                sum += arg.ToNumber().Value;
            }

            return sum.ToNumber();
        }

        public IMathObject CallFunction(ArccosecantFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return
                ((float)Math.Asin(1 / args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(ArccosineFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            if (arg0 < -1)
                throw new OperandException(
                    "Argument less than -1");
            if (arg0 > 1)
                throw new OperandException(
                    "Argument greater than 1");
            return ((float)Math.Acos(arg0)).ToNumber();
        }

        public IMathObject CallFunction(ArccotangentFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            return ((float)(Math.Atan2(1, x))).ToNumber();
        }

        public IMathObject CallFunction(ArcsecantFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return ((float)Math.Acos(1 / args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(ArcsineFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            if (arg0 < -1)
                throw new OperandException(
                    "Argument less than -1");
            if (arg0 > 1)
                throw new OperandException(
                    "Argument greater than 1");
            return ((float)Math.Asin(arg0)).ToNumber();
        }

        public IMathObject CallFunction(Arctangent2Function f,
            IMathObject[] args, SolusEnvironment env)
        {
            return ((float)Math.Atan2(args[0].ToNumber().Value,
                args[1].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(ArctangentFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return ((float)Math.Atan(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(BitwiseAndOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            // TODO: BitwiseAndOperation is a BinaryOperation, but
            //       BitwiseOrOperation is an AssociativeCommutativeOperation.
            //       probably make this work on more than two arguments.
            return new Number((long)args[0].ToNumber().Value &
                              (long)args[1].ToNumber().Value);
        }

        public IMathObject CallFunction(BitwiseOrOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            long value = 0;

            foreach (var arg in args)
            {
                value |= (long)arg.ToNumber().Value;
            }

            return value.ToNumber();
        }

        public IMathObject CallFunction(CeilingFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return ((float)Math.Ceiling(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(CosecantFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return
                ((float)(1 / Math.Sin(args[0].ToNumber().Value))).ToNumber();
        }

        public IMathObject CallFunction(CosineFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            return ((float)Math.Cos(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(CotangentFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return
                ((float)(1 / Math.Tan(args[0].ToNumber().Value))).ToNumber();
        }

        public Expression CallFunction(DeriveOperator f, IMathObject[] args,
            SolusEnvironment env)
        {
            var derive = new DerivativeTransformer();
            var expr = (Expression)args[0];
            var v = ((VariableAccess)args[1]).VariableName;
            return derive.Transform(expr, new VariableTransformArgs(v));
        }

        public IMathObject CallFunction(DistFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            var arg1 = args[1].ToNumber().Value;
            return ((float)Math.Sqrt(arg0 * arg0 + arg1 * arg1)).ToNumber();
        }

        public IMathObject CallFunction(DistSqFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            var arg1 = args[1].ToNumber().Value;
            return (arg0 * arg0 + arg1 * arg1).ToNumber();
        }

        public IMathObject CallFunction(DivisionOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            if (y == 0)
                throw new OperandException("Division by zero");
            return (x / y).ToNumber();
        }

        public IMathObject CallFunction(EqualComparisonOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var type1 = args[0].GetMathType();
            var type2 = args[1].GetMathType();
            if (type1 != type2)
                return false.ToBoolean();

            if (type1 == Reals.Value)
            {
                var x = args[0].ToNumber().Value;
                var y = args[1].ToNumber().Value;
                return (x == y).ToBoolean();
            }
            else if (type1 == Strings.Value)
            {

                var x = args[0].ToStringValue().Value;
                var y = args[1].ToStringValue().Value;
                return (x == y).ToBoolean();
            }
            else if (type1 == Booleans.Value)
            {

                var x = args[0].ToBoolean().Value;
                var y = args[1].ToBoolean().Value;
                return (x == y).ToBoolean();
            }
            else if (type1.IsSubsetOf(AllVectors.Value))
            {
                var x = args[0].ToVector();
                var y = args[1].ToVector();
                if (x.Length != y.Length)
                    return false.ToBoolean();
                int i;
                var argArray = new IMathObject[2];
                for (i = 0; i < x.Length; i++)
                {
                    argArray[0] = x[i];
                    argArray[1] = y[i];
                    if (!CallFunction(f, argArray, env).ToBoolean())
                        return false.ToBoolean();
                }

                return true.ToBoolean();
            }
            else if (type1.IsSubsetOf(AllMatrices.Value))
            {
                var x = args[0].ToMatrix();
                var y = args[1].ToMatrix();
                if (x.RowCount != y.RowCount)
                    return false.ToBoolean();
                if (x.ColumnCount != y.ColumnCount)
                    return false.ToBoolean();
                int r, c;
                var argArray = new IMathObject[2];
                for (r = 0; r < x.RowCount; r++)
                for (c = 0; c < x.ColumnCount; c++)
                {
                    argArray[0] = x[r, c];
                    argArray[1] = y[r, c];
                    if (!CallFunction(f, argArray, env).ToBoolean())
                        return false.ToBoolean();
                }

                return true.ToBoolean();
            }

            throw new TypeException(
                null,
                $"Type not supported for equality comparisons: " +
                $"{type1.DisplayName}");
        }

        public IMathObject CallFunction(ExponentOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return ((float)Math.Pow(x, y)).ToNumber();
        }

        public IMathObject CallFunction(FactorialFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            // TODO: check args
            var n = args[0].ToNumber().Value;
            if (n <= 1) return 1.ToNumber();
            var product = 1f;
            while (n > 0)
            {
                product *= n;
                n--;
            }

            return product.ToNumber();
        }

        public IMathObject CallFunction(FloorFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            return ((float)Math.Floor(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(GreaterThanComparisonOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return (x > y).ToBoolean();
        }

        public IMathObject CallFunction(
            GreaterThanOrEqualComparisonOperation f, IMathObject[] args,
            SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return (x >= y).ToBoolean();
        }

        public Expression CallFunction(IfOperator ff, IMathObject[] args,
            SolusEnvironment env)
        {
            var eval = new BasicEvaluator();
            var value = eval.Eval((Expression)args[0], env).ToNumber().Value;
            if (value == 0 ||
                float.IsNaN(value) ||
                float.IsInfinity(value))
                return new Literal(eval.Eval((Expression)args[2], env));
            return new Literal(eval.Eval((Expression)args[1], env));
        }

        public Expression CallFunction(IsWellDefinedFunction ff, IMathObject[] args,
            SolusEnvironment env)
        {
            var ec = new ExpressionChecker();
            var expr = (Expression)args[0];
            return new Literal(ec.IsWellDefined(expr, env, throws:false));
        }

        public Expression CallFunction(IsWellFormedFunction ff, IMathObject[] args,
            SolusEnvironment env)
        {
            var ec = new ExpressionChecker();
            var expr = (Expression)args[0];
            return new Literal(ec.IsWellFormed(expr, throws:false));
        }

        public IMathObject CallFunction(LessThanComparisonOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return (x < y).ToBoolean();
        }

        public IMathObject CallFunction(LessThanOrEqualComparisonOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            return (x <= y).ToBoolean();
        }

        public IMathObject CallFunction(LoadImageFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public IMathObject CallFunction(Log10Function f, IMathObject[] args,
            SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            if (arg0 <= 0)
                throw new OperandException(
                    "Argument must be positive");
            return ((float)Math.Log10(arg0)).ToNumber();
        }

        public IMathObject CallFunction(Log2Function f, IMathObject[] args,
            SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            if (arg0 <= 0)
                throw new OperandException(
                    "Argument must be positive");
            return ((float)Math.Log(arg0, 2)).ToNumber();
        }

        public IMathObject CallFunction(LogarithmFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            var arg1 = args[1].ToNumber().Value;
            if (arg0 <= 0)
                throw new OperandException("Argument must be positive");
            if (arg1 <= 0)
                throw new OperandException("Base must be positive");
            if (arg1 <= 1 && arg1 >= 1)
                throw new OperandException("Base must not be one");

            var rv = (float)Math.Log(arg0, arg1);
            return rv.ToNumber();
        }

        public IMathObject CallFunction(LogicalAndOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToBoolean().Value;
            var y = args[1].ToBoolean().Value;
            return (x && y).ToBoolean();
        }

        public IMathObject CallFunction(LogicalOrOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var x = args[0].ToBoolean().Value;
            var y = args[1].ToBoolean().Value;
            return (x || y).ToBoolean();
        }

        public IMathObject CallFunction(MaximumFiniteFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            int i;
            float current = float.NaN;
            bool first = true;
            for (i = 0; i < args.Length; i++)
            {
                var value = args[i].ToNumber().Value;
                if (!float.IsInfinity(value) &&
                    !float.IsNaN(value))
                {
                    if (first)
                    {
                        current = value;
                        first = false;
                    }
                    else
                        current = Math.Max(current, value);
                }
            }

            return current.ToNumber();
        }

        public IMathObject CallFunction(MaximumFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            int i;
            var current = args[0].ToNumber().Value;
            for (i = 1; i < args.Length; i++)
                current = Math.Max(
                    current,
                    args[i].ToNumber().Value);
            return current.ToNumber();
        }

        public IMathObject CallFunction(MinimumFiniteFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            int i;
            float current = float.NaN;
            bool first = true;
            for (i = 0; i < args.Length; i++)
            {
                var value = args[i].ToNumber().Value;
                if (!float.IsInfinity(value) &&
                    !float.IsNaN(value))
                {
                    if (first)
                    {
                        current = value;
                        first = false;
                    }
                    else
                        current = Math.Min(current, value);
                }
            }

            return current.ToNumber();
        }

        public IMathObject CallFunction(MinimumFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            int i;
            var current = args[0].ToNumber().Value;
            for (i = 1; i < args.Length; i++)
                current = Math.Min(
                    current,
                    args[i].ToNumber().Value);
            return current.ToNumber();
        }

        public IMathObject CallFunction(ModularDivision f, IMathObject[] args,
            SolusEnvironment env)
        {
            var x = args[0].ToNumber().Value;
            var y = args[1].ToNumber().Value;
            if (y == 0)
                throw new OperandException("Division by zero");
            return ((long)x % (long)y).ToNumber();
        }

        public IMathObject CallFunction(MultiplicationOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            float value = 1;
            int i;
            for (i = 0; i < args.Length; i++)
            {
                value *= args[i].ToNumber().Value;
            }

            return value.ToNumber();
        }

        public IMathObject CallFunction(NaturalLogarithmFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            var arg0 = args[0].ToNumber().Value;
            if (arg0 <= 0)
                throw new OperandException(
                    "Argument must be positive");
            return ((float)Math.Log(arg0)).ToNumber();
        }

        public IMathObject CallFunction(NegationOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            return (-args[0].ToNumber().Value).ToNumber();
        }

        public IMathObject CallFunction(NotEqualComparisonOperation f,
            IMathObject[] args, SolusEnvironment env)
        {
            var type1 = args[0].GetMathType();
            var type2 = args[1].GetMathType();
            if (type1 != type2)
                return true.ToBoolean();

            if (type1 == Reals.Value)
            {
                var x = args[0].ToNumber().Value;
                var y = args[1].ToNumber().Value;
                return (x != y).ToBoolean();
            }
            else if (type1 == Strings.Value)
            {

                var x = args[0].ToStringValue().Value;
                var y = args[1].ToStringValue().Value;
                return (x != y).ToBoolean();
            }
            else if (type1 == Booleans.Value)
            {

                var x = args[0].ToBoolean().Value;
                var y = args[1].ToBoolean().Value;
                return (x != y).ToBoolean();
            }
            else if (type1.IsSubsetOf(AllVectors.Value))
            {
                var x = args[0].ToVector();
                var y = args[1].ToVector();
                if (x.Length != y.Length)
                    return true.ToBoolean();
                int i;
                var argArray = new IMathObject[2];
                for (i = 0; i < x.Length; i++)
                {
                    argArray[0] = x[i];
                    argArray[1] = y[i];
                    if (CallFunction(f, argArray, env).ToBoolean())
                        return true.ToBoolean();
                }

                return false.ToBoolean();
            }
            else if (type1.IsSubsetOf(AllMatrices.Value))
            {
                var x = args[0].ToMatrix();
                var y = args[1].ToMatrix();
                if (x.RowCount != y.RowCount)
                    return true.ToBoolean();
                if (x.ColumnCount != y.ColumnCount)
                    return true.ToBoolean();
                int r, c;
                var argArray = new IMathObject[2];
                for (r = 0; r < x.RowCount; r++)
                for (c = 0; c < x.ColumnCount; c++)
                {
                    argArray[0] = x[r, c];
                    argArray[1] = y[r, c];
                    if (CallFunction(f, argArray, env).ToBoolean())
                        return true.ToBoolean();
                }

                return false.ToBoolean();
            }

            throw new TypeException(
                null,
                $"Type not supported for equality comparisons: " +
                $"{type1.DisplayName}");
        }

        public IMathObject CallFunction(ParseExprFunction ff,
            IMathObject[] args, SolusEnvironment env)
        {
            var s = args[0].ToStringValue();
            return ParseExprFunction.Value.ParseExpr(s.Value);
        }

        public IMathObject CallFunction(RandFunction mm, IMathObject[] args,
            SolusEnvironment env)
        {
            return ((float)RandFunction.Source.NextDouble()).ToNumber();
        }

        public IMathObject CallFunction(SecantFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            return
                ((float)(1 / Math.Cos(args[0].ToNumber().Value))).ToNumber();
        }

        public IMathObject CallFunction(SineFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            return ((float)Math.Sin(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(SizeFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            // TODO: this function should be able to operate on things that
            // aren't IMathObject, namely TensorExpression
            var arg = args[0];
            if (arg.IsIsString(env) || arg.IsIsVector(env) ||
                arg.IsIsMatrix(env))
                return arg.GetDimensions(env).ToVector();

            return null;
        }

        public IMathObject CallFunction(SqrtFunction f, IMathObject[] args,
            SolusEnvironment env)
        {
            return CallFunction(ExponentOperation.Value,
                new[] { args[0], 0.5f.ToNumber() }, env);
        }

        public IMathObject CallFunction(SubstFunction ff, IMathObject[] args,
            SolusEnvironment env)
        {
            var subst = new SubstTransformer();
            var cleanup = new CleanUpTransformer();
            var var = ((VariableAccess)args[1]).VariableName;
            return cleanup.CleanUp(
                subst.Subst((Expression)args[0], var, (Expression)args[2]));
        }

        public IMathObject CallFunction(TangentFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            return ((float)Math.Tan(args[0].ToNumber().Value)).ToNumber();
        }

        public IMathObject CallFunction(UnitStepFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            if (args[0].ToNumber().Value >= 0)
                return new Number(1);
            return new Number(0);
        }

        private SolusEnvironment _udfParentEnvCache;
        private SolusEnvironment _udfChildCache;

        public IMathObject CallFunction(UserDefinedFunction f,
            IMathObject[] args, SolusEnvironment env)
        {
            if (_udfChildCache == null ||
                env != _udfParentEnvCache)
            {
                _udfChildCache = env.CreateChildEnvironment();
                _udfParentEnvCache = env;
            }

            var env2 = _udfChildCache;

            int i;
            for (i = 0; i < f.Parameters.Count; i++)
            {
                env2.SetVariable(f.Parameters[i].Name, args[i]);
            }

            return Eval(f.Expression, env2);
        }
    }
}
