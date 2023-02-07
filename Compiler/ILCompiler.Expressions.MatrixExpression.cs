
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(MatrixExpression expr,
            NascentMethod nm, SolusEnvironment env,
            VariableIdentityMap variables)
        {
            var arrayType = typeof(float[,]);
            var ctor = arrayType.GetConstructor(
                new[] { typeof(int), typeof(int) });
            var setMethod = arrayType.GetMethod("Set",
                new[] { typeof(int), typeof(int), typeof(float) });
            var seq = new List<IlExpression>();
            var newobj = new NewObjIlExpression(ctor,
                new LoadConstantIlExpression(expr.RowCount),
                new LoadConstantIlExpression(expr.ColumnCount));
            seq.Add(newobj);
            var dup = new DupIlExpression(newobj);
            int r, c;
            for (r = 0; r < expr.RowCount; r++)
            for (c = 0; c < expr.ColumnCount; c++)
                seq.Add(
                    new CallIlExpression(
                        setMethod,
                        dup,
                        new LoadConstantIlExpression(r),
                        new LoadConstantIlExpression(c),
                        ConvertToIlExpression(expr[r, c], nm, env,
                            variables)));
            return new IlExpressionSequence(seq);
        }
    }
}
