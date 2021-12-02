
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

            if (_evaledIndexesCache == null ||
                _evaledIndexesCache.Length < Indexes.Count)
                _evaledIndexesCache = new IMathObject[Indexes.Count];
            int i;
            for (i = 0; i < Indexes.Count; i++)
                _evaledIndexesCache[i] = Indexes[i].Eval(env);
            return AccessComponent(value, _evaledIndexesCache, env);
        }

        private IMathObject[] _evaledIndexesCache;

        private static void CheckIndexes(IMathObject[] indexes,
            bool? exprIsScalar, bool? exprIsVector, bool? exprIsMatrix,
            int? exprTensorRank, bool? exprIsString, int? exprLength,
            int? exprRowCount, int? exprColumnCount)
        {
            if (exprIsString.HasValue && exprIsString.Value)
            {
                if (1 != indexes.Length)
                    throw new IndexException(
                        "Number of indexes doesn't match the number " +
                        "required by the expression");
            }
            else
            {
                if ((exprIsScalar.HasValue && exprIsScalar.Value) ||
                    exprTensorRank < 1)
                    throw new OperandException(
                        "Scalars do not have components");
                if (exprTensorRank != indexes.Length)
                    throw new IndexException(
                        "Number of indexes doesn't match the number " +
                        "required by the expression");
            }

            // TODO: maybe pass the env, and then we don't have to eval the
            // indexes before-hand?
            int i;
            for (i = 0; i < indexes.Length; i++)
            {
                if (!indexes[i].IsIsScalar(null))
                    throw new IndexException(
                        "Indexes must be scalar");
                if (indexes[i].ToNumber().Value < 0)
                    throw new IndexException(
                        "Indexes must not be negative");
            }

            var index0 = (int) indexes[0].ToNumber().Value;
            if (exprIsVector.HasValue && exprIsVector.Value)
            {
                if (index0 >= exprLength.Value)
                    throw new IndexException(
                        "Index exceeds the size of the vector");
                return;
            }

            if (exprIsString.HasValue && exprIsString.Value)
            {
                // TODO: check index for string
                // if (!exprLength.HasValue)
                //     throw new OperandException(
                //         "Expression does not have a length");
                // if (index0 >= exprLength.Value)
                //     throw new IndexException(
                //         "Index exceeds the size of the string");
                return;
            }

            var index1 = (int) indexes[1].ToNumber().Value;
            if (exprIsMatrix.HasValue && exprIsMatrix.Value)
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
            if (expr.IsIsVector(env))
                length = expr.ToVector().Length;
            else if (expr.IsIsString(env))
                length = expr.ToStringValue().Length;
            var exprRowCount = expr.GetDimension(env, 0);
            var exprColumnCount = expr.GetDimension(env, 1);
            CheckIndexes(indexes, expr.IsScalar(env), expr.IsVector(env),
                expr.IsMatrix(env), expr.GetTensorRank(env),
                expr.IsString(env), length,
                exprRowCount, exprColumnCount);

            var index0 = (int) indexes[0].ToNumber().Value;
            if (expr.IsIsVector(env))
            {
                var v = expr.ToVector();
                return v[index0];
            }

            if (expr.IsIsString(env))
            {
                var sv = expr.ToStringValue();
                return sv.Value[index0].ToStringValue();
            }

            var index1 = (int) indexes[1].ToNumber().Value;
            if (expr.IsIsMatrix(env))
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
            if (expr.Result.IsIsVector(env))
                length = expr.Result.GetVectorLength(env);

            int? exprTensorRank = expr.Result.GetTensorRank(env);
            int? exprRowCount = null;
            int? exprColumnCount = null;
            if (exprTensorRank.HasValue && exprTensorRank.Value == 2)
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

        public override Expression Simplify(SolusEnvironment env)
        {
            var expr = Expr.Simplify(env);
            var evaledIndexes =
                Indexes.Select(i => i.Simplify(env)).ToList();
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
            // TODO: really, we can only interrogate the component if the
            // object is known to have components all of a particular type,
            // e.g. 3-vector in R^3. otherwise, we have to evaluate the
            // indexes.
            public ResultC(ComponentAccess ca) => _ca = ca;
            private readonly ComponentAccess _ca;
            public bool? IsScalar(SolusEnvironment env) => null;
            public bool? IsVector(SolusEnvironment env) => null;
            public bool? IsMatrix(SolusEnvironment env) => null;
            public int? GetTensorRank(SolusEnvironment env) => null;
            public bool? IsString(SolusEnvironment env) => null;
            public int? GetDimension(SolusEnvironment env, int index) => null;
            public int[] GetDimensions(SolusEnvironment env) => null;
            public int? GetVectorLength(SolusEnvironment env) => null;
            public bool? IsInterval(SolusEnvironment env) => null;
            public bool? IsFunction(SolusEnvironment env) => null;
            public bool? IsExpression(SolusEnvironment env) => null;
            public bool IsConcrete => false;
            public string DocString => "";
        }
    }
}
