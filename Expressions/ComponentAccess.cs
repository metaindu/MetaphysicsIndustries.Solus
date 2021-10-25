
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class ComponentAccess : Expression
    {
        public ComponentAccess(Expression expr, IEnumerable<Expression> indexes)
        {
            Expr = expr;
            Indexes = new ReadOnlyCollection<Expression>(indexes.ToList());
            Result = new ResultC(this);
        }

        public readonly Expression Expr;
        public readonly ReadOnlyCollection<Expression> Indexes;

        public override IMathObject Eval(SolusEnvironment env)
        {
            var value = Expr.Eval(env);
            var evaledIndexes = GetEvaledIndexes(env);
            return AccessComponent(value, evaledIndexes, env);
        }

        private IMathObject[] GetEvaledIndexes(SolusEnvironment env)
        {
            // TODO: caching, eventually, so as to not repeat so much
            // evaluation. But that is a non-starter at the moment, because
            // the contents of the env can change at any time.

            return Indexes.Select(e => e.Eval(env)).ToArray();
        }

        private static void CheckIndexes(IMathObject[] indexes,
            bool exprIsScalar, bool exprIsVector, bool exprIsMatrix,
            int exprTensorRank, bool exprIsString, int? exprLength,
            int? exprRowCount, int? exprColumnCount)
        {
            if (exprIsString)
            {
                if (1 != indexes.Length)
                    throw new IndexException(
                        "Number of indexes doesn't match the number " +
                        "required by the expression");
            }
            else
            {
                if (exprIsScalar || exprTensorRank < 1)
                    throw new OperandException(
                        "Scalars do not have components");
                if (exprTensorRank != indexes.Length)
                    throw new IndexException(
                        "Number of indexes doesn't match the number " +
                        "required by the expression");
            }

            // TODO: maybe pass the env, and then we don't have to eval the
            // indexes before-hand?
            if (indexes.Any(i => !i.IsScalar(null)))
                throw new IndexException(
                    "Indexes must be scalar");
            if (indexes.Any(i => i.ToNumber().Value < 0))
                throw new IndexException(
                    "Indexes must not be negative");

            var index0 = (int) indexes[0].ToNumber().Value;
            if (exprIsVector)
            {
                if (index0 >= exprLength.Value)
                    throw new IndexException(
                        "Index exceeds the size of the vector");
                return;
            }

            if (exprIsString)
            {
                if (index0 >= exprLength.Value)
                    throw new IndexException(
                        "Index exceeds the size of the string");
                return;
            }

            var index1 = (int) indexes[1].ToNumber().Value;
            if (exprIsMatrix)
            {
                if (index0 >= exprRowCount)
                    throw new IndexException(
                        "Index exceeds number of rows of the matrix");
                if (index1 >= exprColumnCount)
                    throw new IndexException(
                        "Index exceeds number of columns of the matrix");
                return;
            }

            throw new NotImplementedException(
                "Component access is not implemented for tensor rank " +
                "greater than 2");
        }

        public static IMathObject AccessComponent(IMathObject expr,
            IMathObject[] indexes, SolusEnvironment env)
        {
            int? length = null;
            if (expr.IsVector(env)) length = expr.ToVector().Length;
            else if (expr.IsString(env)) length = expr.ToStringValue().Length;
            int? exprRowCount = null;
            int? exprColumnCount = null;
            if (expr.GetTensorRank(env) > 1)
            {
                exprRowCount = new int?(expr.GetDimension(env, 0));
                exprColumnCount = new int?(expr.GetDimension(env, 1));
            }
            CheckIndexes(indexes, expr.IsScalar(env), expr.IsVector(env),
                expr.IsMatrix(env), expr.GetTensorRank(env),
                expr.IsString(env), length,
                exprRowCount, exprColumnCount);

            var index0 = (int) indexes[0].ToNumber().Value;
            if (expr.IsVector(env))
            {
                var v = expr.ToVector();
                return v[index0];
            }

            if (expr.IsString(env))
            {
                var sv = expr.ToStringValue();
                return sv.Value[index0].ToStringValue();
            }

            var index1 = (int) indexes[1].ToNumber().Value;
            if (expr.IsMatrix(env))
            {
                var m = expr.ToMatrix();
                return m[index0, index1];
            }

            throw new NotImplementedException(
                "Component access is not implemented for tensor rank " +
                "greater than 2");
        }

        public static Expression AccessComponent(Expression expr,
            IMathObject[] indexes, SolusEnvironment env)
        {
            int? length = null;
            if (expr.Result.IsVector(env))
                length = expr.Result.GetVectorLength(env);
            else if (expr.Result.IsString(env))
                length = expr.Result.GetStringLength(env);

            int exprTensorRank = expr.Result.GetTensorRank(env);
            int? exprRowCount = null;
            int? exprColumnCount = null;
            if (exprTensorRank == 2)
            {
                exprRowCount = expr.Result.GetDimension(env, 0);
                exprColumnCount = expr.Result.GetDimension(env, 1);
            }

            CheckIndexes(indexes, expr.Result.IsScalar(env),
                expr.Result.IsVector(env), expr.Result.IsMatrix(env),
                exprTensorRank, expr.Result.IsString(env),
                length, exprRowCount, exprColumnCount);

            var index0 = (int) indexes[0].ToNumber().Value;
            if (expr is VectorExpression ve)
                return ve[index0];

            var index1 = (int) indexes[1].ToNumber().Value;
            if (expr is MatrixExpression me)
                return me[index0, index1];

            throw new NotImplementedException(
                "Component access is not implemented for tensor rank " +
                "greater than 2");
        }

        public override Expression PreliminaryEval(SolusEnvironment env)
        {
            var expr = Expr.PreliminaryEval(env);
            var evaledIndexes =
                Indexes.Select(i => i.PreliminaryEval(env)).ToList();
            if (evaledIndexes.All(ei => ei is Literal))
            {
                var indexes2 = evaledIndexes.Select(
                    ei => ((Literal) ei).Value).ToArray();
                switch (expr)
                {
                    case Literal lit:
                        return new Literal(
                            AccessComponent(lit.Value, indexes2, env));
                    case TensorExpression te:
                        return AccessComponent(te, indexes2, env);
                }
            }

            return new ComponentAccess(expr, evaledIndexes);
        }

        public override Expression Clone()
        {
            return new ComponentAccess(Expr, Indexes);
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
            
            Expr.AcceptVisitor(visitor);
            foreach (var index in Indexes)
                index.AcceptVisitor(visitor);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(Expr);
            sb.Append("[");
            for (var i = 0; i < Indexes.Count; i++)
            {
                if (i > 0) sb.Append(", ");
                sb.Append(Indexes[i]);
            }
            sb.Append("]");
            return sb.ToString();
        }

        public override IMathObject Result { get; }

        private class ResultC : IMathObject
        {
            public ResultC(ComponentAccess ca) => _ca = ca;
            private readonly ComponentAccess _ca;
            public bool IsScalar(SolusEnvironment env)
            {
                var evaledIndexes = _ca.GetEvaledIndexes(env);
                var expr = AccessComponent(_ca.Expr, evaledIndexes, env);
                return expr.Result.IsScalar(env);
            }

            public bool IsVector(SolusEnvironment env)
            {
                var evaledIndexes = _ca.GetEvaledIndexes(env);
                var expr = AccessComponent(_ca.Expr, evaledIndexes, env);
                return expr.Result.IsVector(env);
            }

            public bool IsMatrix(SolusEnvironment env)
            {
                var evaledIndexes = _ca.GetEvaledIndexes(env);
                var expr = AccessComponent(_ca.Expr, evaledIndexes, env);
                return expr.Result.IsMatrix(env);
            }

            public int GetTensorRank(SolusEnvironment env)
            {
                var evaledIndexes = _ca.GetEvaledIndexes(env);
                var expr = AccessComponent(_ca.Expr, evaledIndexes, env);
                return expr.Result.GetTensorRank(env);
            }

            public bool IsString(SolusEnvironment env)
            {
                var evaledIndexes = _ca.GetEvaledIndexes(env);
                var expr = AccessComponent(_ca.Expr, evaledIndexes, env);
                return expr.Result.IsString(env);
            }

            public int GetDimension(SolusEnvironment env, int index)
            {
                var evaledIndexes = _ca.GetEvaledIndexes(env);
                var expr = AccessComponent(_ca.Expr, evaledIndexes, env);
                return expr.Result.GetDimension(env, index);
            }

            public int[] GetDimensions(SolusEnvironment env)
            {
                var evaledIndexes = _ca.GetEvaledIndexes(env);
                var expr = AccessComponent(_ca.Expr, evaledIndexes, env);
                return expr.Result.GetDimensions(env);
            }

            public int GetVectorLength(SolusEnvironment env)
            {
                var evaledIndexes = _ca.GetEvaledIndexes(env);
                var expr = AccessComponent(_ca.Expr, evaledIndexes, env);
                return expr.Result.GetVectorLength(env);
            }

            public int GetStringLength(SolusEnvironment env)
            {
                if (_ca.Indexes.Count != 1)
                    throw new IndexException(
                        "Wrong number of indexes for a string");
                var index = InterrogateIndexValue(_ca.Indexes[0], env);
                if (!index.IsScalar(env))
                    throw new IndexException(
                        "Wrong type of index for a string");
                var index2 = index.ToNumber().Value;
                if (index2 != index2.RoundInt())
                    throw new IndexException(
                        "Index is not an integer");
                if (index2 < 0 ||
                    index2 > _ca.Expr.Result.GetStringLength(env))
                    throw new IndexException(
                        "Index out of range");
                var evaledIndexes = new IMathObject[] { index };
                var expr = AccessComponent(_ca.Expr, evaledIndexes, env);
                return expr.Result.GetStringLength(env);
            }

            public bool IsConcrete => false;
        }

        private static IMathObject InterrogateIndexValue(Expression expr,
            SolusEnvironment env)
        {
            if (expr is Literal lit) return lit.Value;
            if (expr is VariableAccess va)
            {
                if (!env.ContainsVariable(va.VariableName))
                    throw new NameException(
                        $"Variable not found: {va.VariableName}");
                var expr2 = env.GetVariable(va.VariableName);
                // TODO: don't get stuck in an infinite loop, e.g. a:=b, b:=a
                return InterrogateIndexValue(expr2, env);
            }
            // TODO: other expression types

            throw new RequiresEvaluationException();
        }
    }
}
