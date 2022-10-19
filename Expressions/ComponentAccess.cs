
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

        public override Expression Simplify(SolusEnvironment env)
        {
            var expr = Expr.Simplify(env);

            var canReduce = true;
            bool sameIndexes = true;
            var simplifiedIndexes = new Expression[Indexes.Count];
            var intIndexes = new int[Indexes.Count];
            int i;
            for (i = 0; i < Indexes.Count; i++)
            {
                intIndexes[i] = -1;
                var s = Indexes[i].Simplify(env);
                sameIndexes &= s == Indexes[i];
                if (s is Literal literal)
                {
                    var v = literal.Value;
                    if (v.IsIsScalar(env))
                    {
                        var n = v.ToNumber().Value;
                        if (n.IsInteger() && n >= 0)
                            intIndexes[i] = (int)n;
                        else
                            canReduce = false;
                    }
                    else
                        canReduce = false;
                }
                else
                    canReduce = false;
                simplifiedIndexes[i] = s;
            }

            if (canReduce)
            {
                if (expr is Literal eliteral2)
                {
                    var v = eliteral2.Value;
                    if (v is IVector vv)
                    {
                        if (intIndexes.Length == 1 &&
                            intIndexes[0] < vv.Length)
                            return new Literal(
                                vv.GetComponent(intIndexes[0]));
                    }
                    else if (v is IMatrix m)
                    {
                        if (intIndexes.Length == 2 &&
                            intIndexes[0] < m.RowCount &&
                            intIndexes[1] < m.ColumnCount)
                            return new Literal(
                                m.GetComponent(intIndexes[0], intIndexes[1]));
                    }
                }
                else if (expr is TensorExpression te2)
                {
                    if (te2 is IVector vv)
                    {
                        if (intIndexes.Length == 1 &&
                            intIndexes[0] < vv.Length)
                            return (Expression)vv.GetComponent(intIndexes[0]);
                    }
                    else if (te2 is IMatrix m)
                    {
                        if (intIndexes.Length == 2 &&
                            intIndexes[0] < m.RowCount &&
                            intIndexes[1] < m.ColumnCount)
                            return (Expression)m.GetComponent(
                                intIndexes[0], intIndexes[1]);
                    }
                }
            }

            if (expr == Expr && sameIndexes)
                return this;

            return new ComponentAccess(expr, simplifiedIndexes);
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
            // For now, we assume all components are scalars.
            public ResultC(ComponentAccess ca) => _ca = ca;
            private readonly ComponentAccess _ca;
            public bool? IsScalar(SolusEnvironment env) => true;
            public bool? IsVector(SolusEnvironment env) => false;
            public bool? IsMatrix(SolusEnvironment env) => false;
            public int? GetTensorRank(SolusEnvironment env) => 0;
            public bool? IsString(SolusEnvironment env) => false;
            public int? GetDimension(SolusEnvironment env, int index) => null;
            public int[] GetDimensions(SolusEnvironment env) => null;
            public int? GetVectorLength(SolusEnvironment env) => null;
            public bool? IsInterval(SolusEnvironment env) => false;
            public bool? IsFunction(SolusEnvironment env) => false;
            public bool? IsExpression(SolusEnvironment env) => false;
            public bool IsConcrete => false;
            public string DocString => "";
        }
    }
}
