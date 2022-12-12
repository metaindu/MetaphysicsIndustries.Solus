
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
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(
            Literal expr, NascentMethod nm,
            VariableIdentityMap variables)
        {
            // TODO: work out how to use `variables` in place of null `env`

            if (expr.Value.IsIsScalar(null))
            {
                var value = expr.Value.ToFloat();
                return new LoadConstantIlExpression(value);
            }

            if (expr.Value.IsIsVector(null))
            {
                var v = expr.Value.ToVector();
                var seq = new List<IlExpression>();
                var newarr = new NewArrIlExpression(
                    typeof(float),
                    new LoadConstantIlExpression(v.Length));
                seq.Add(newarr);
                int i;
                // for (i = 0; i < v.Length; i++)
                //     seq.Add();
                for (i = 0; i < v.Length; i++)
                {
                    seq.Add(
                        new StoreElemIlExpression(
                            array_: new DupIlExpression(newarr),
                            index: new LoadConstantIlExpression(i),
                            value: new LoadConstantIlExpression(
                                v[i].ToNumber().Value)));
                }

                return new IlExpressionSequence(typeof(float[]), seq);
            }

            if (expr.Value.IsIsMatrix(null))
            {
                var m = expr.Value.ToMatrix();
                var arrayType = typeof(float[,]);
                var ctor = arrayType.GetConstructor(
                    new[] { typeof(int), typeof(int) });
                var setMethod = arrayType.GetMethod("Set",
                    new[] { typeof(int), typeof(int), typeof(float) });
                var seq = new List<IlExpression>();
                seq.Add(new NewObjIlExpression(ctor,
                    new LoadConstantIlExpression(m.RowCount),
                    new LoadConstantIlExpression(m.ColumnCount)));
                var dup = new DupIlExpression();
                int r, c;
                for (r = 0; r < m.RowCount; r++)
                for (c = 0; c < m.ColumnCount; c++)
                    seq.Add(
                        new CallIlExpression(
                            setMethod,
                            dup,
                            new LoadConstantIlExpression(r),
                            new LoadConstantIlExpression(c),
                            new LoadConstantIlExpression(m[r, c].ToFloat())));

                return new IlExpressionSequence(typeof(float[,]), seq);
            }

            if (expr.Value.IsIsString(null))
                return new LoadStringIlExpression(
                    expr.Value.ToStringValue().Value);

            if (expr.Value.IsIsInterval(null))
            {
                var ii = expr.Value.ToInterval();
                var stuple = typeof(STuple<float, bool, float, bool>);
                var ctor = stuple.GetConstructor(
                    new[]
                    {
                        typeof(float),
                        typeof(bool),
                        typeof(float),
                        typeof(bool)
                    });
                var rv = new NewObjIlExpression(ctor,
                    new LoadConstantIlExpression(ii.LowerBound),
                    new LoadConstantIlExpression(ii.OpenLowerBound),
                    new LoadConstantIlExpression(ii.UpperBound),
                    new LoadConstantIlExpression(ii.OpenUpperBound));
                return rv;
            }

            throw new NotImplementedException(
                "currently only implemented for numbers, vectors, " +
                " matrices, and strings.");
        }
    }
}
