
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
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class ComponentAccess : Expression
    {
        public ComponentAccess(Expression expr, IEnumerable<Expression> indexes)
        {
            Expr = expr;
            Indexes = new ReadOnlyCollection<Expression>(indexes.ToList());
        }

        public readonly Expression Expr;
        public readonly ReadOnlyCollection<Expression> Indexes;

        public override IMathObject Eval(SolusEnvironment env)
        {
            var value = Expr.Eval(env);
            var evaledIndexes =
                Indexes.Select(e => e.Eval(env)).ToArray();
            return AccessComponent(value, evaledIndexes);
        }

        public static IMathObject AccessComponent(IMathObject expr,
            IMathObject[] indexes)
        {
            if (expr.TensorRank < 1)
                throw new ArgumentException(
                    "Scalars do not have components");
            if (expr.TensorRank != indexes.Length)
                throw new IndexOutOfRangeException(
                    "Number of indexes doesn't match the number " +
                    "required by the expression");
            if (indexes.Any(i => !i.IsScalar))
                throw new IndexOutOfRangeException(
                    "Indexes must be scalar");
            if (indexes.Any(i => i.ToNumber().Value < 0))
                throw new IndexOutOfRangeException(
                    "Indexes must not be negative");

            var index0 = (int) indexes[0].ToNumber().Value;
            if (expr.IsVector)
                return expr.ToVector()[index0];

            var index1 = (int) indexes[1].ToNumber().Value;
            if (expr.IsMatrix)
                return expr.ToMatrix()[index0, index1];

            throw new NotImplementedException(
                "Component access is not implemented for tensor rank " +
                "greater than 2");
        }

        public static Expression AccessComponent(TensorExpression expr,
            IMathObject[] indexes)
        {
            if (expr.TensorRank < 1)
                throw new ArgumentException(
                    "Scalars do not have components");
            if (expr.TensorRank != indexes.Length)
                throw new IndexOutOfRangeException(
                    "Number of indexes doesn't match the number " +
                    "required by the expression");
            if (indexes.Any(i => !i.IsScalar))
                throw new IndexOutOfRangeException(
                    "Indexes must be scalar");
            if (indexes.Any(i => i.ToNumber().Value < 0))
                throw new IndexOutOfRangeException(
                    "Indexes must not be negative");

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
                        return AccessComponent(te, indexes2);
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
    }
}
