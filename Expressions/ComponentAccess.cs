
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
            return AccessComponent(value, evaledIndexes);
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

            if (indexes.Any(i => !i.IsScalar()))
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
            IMathObject[] indexes)
        {
            int? length = null;
            if (expr.IsVector()) length = expr.ToVector().Length;
            else if (expr.IsString()) length = expr.ToStringValue().Length;
            CheckIndexes(indexes, expr.IsScalar(), expr.IsVector(), expr.IsMatrix(),
                expr.GetTensorRank(), expr.IsString(),
                length,
                expr.GetTensorRank() > 1 ? new int?(expr.GetDimension(0)) : null,
                expr.GetTensorRank() > 1 ? new int?(expr.GetDimension(1)) : null);

            var index0 = (int) indexes[0].ToNumber().Value;
            if (expr.IsVector())
            {
                var v = expr.ToVector();
                return v[index0];
            }

            if (expr.IsString())
            {
                var sv = expr.ToStringValue();
                return sv.Value[index0].ToStringValue();
            }

            var index1 = (int) indexes[1].ToNumber().Value;
            if (expr.IsMatrix())
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
                            AccessComponent(lit.Value, indexes2));
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

        public override IEnvMathObject Result { get; }

        private class ResultC : IEnvMathObject
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
                var evaledIndexes = _ca.GetEvaledIndexes(env);
                var expr = AccessComponent(_ca.Expr, evaledIndexes, env);
                return expr.Result.GetStringLength(env);
            }
        }
    }
}
