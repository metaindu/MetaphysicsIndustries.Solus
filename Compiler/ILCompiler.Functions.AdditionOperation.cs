
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(
            AdditionOperation func, NascentMethod nm,
            VariableIdentityMap variables,
            List<Expression> arguments)
        {
            IlExpression expr = new RawInstructions();

            var argType = arguments[0].GetResultType(null);
            if (argType.IsSubsetOf(Reals.Value)){
                bool first = true;
                foreach (var arg in arguments)
                {
                    if (!first &&
                        arg is FunctionCall call &&
                        call.Function is Literal literal &&
                        literal.Value is Function f &&
                        f == NegationOperation.Value)
                    {
                        expr = new SubIlExpression(expr,
                            ConvertToIlExpression(call.Arguments[0], nm,
                                variables));
                    }
                    else
                    {
                        if (first)
                            expr = ConvertToIlExpression(arg, nm, variables);
                        else
                            expr = new AddIlExpression(expr,
                                ConvertToIlExpression(arg, nm, variables));
                        first = false;
                    }
                }
            }
            else if (argType is Vectors vt)
            {
                int n = vt.Dimension;
                var seq = new List<IlExpression>();
                var destLocal = nm.CreateLocal(typeof(float[]), "vectorSum");
                var stloc = new StoreLocalIlExpression(
                    destLocal,
                    ConvertToIlExpression(
                        new Literal(Vector.Zero(n)), nm, variables));
                seq.Add(stloc);
                var addendLocal =
                    nm.CreateLocal(typeof(float[]), "vectorAddend");
                foreach (var arg in arguments)
                {
                    seq.Add(
                        new StoreLocalIlExpression(
                            addendLocal,
                            ConvertToIlExpression(arg, nm, variables)));
                    // TODO: logic to choose between a loop (e.g.
                    //       WhileLoopConstruct) and a hard-coded sequence of
                    //       instructions, or something in between, like a
                    //       partially unrolled loop, or even a duff's device.
                    int i;
                    for (i = 0; i < n; i++)
                        seq.Add(new StoreElemIlExpression(
                            new LoadLocalIlExpression(destLocal),
                            new LoadConstantIlExpression(i),
                            new AddIlExpression(
                                new LoadElemIlExpression(
                                    new LoadLocalIlExpression(destLocal),
                                    new LoadConstantIlExpression(i)),
                                new LoadElemIlExpression(
                                    new LoadLocalIlExpression(addendLocal),
                                    new LoadConstantIlExpression(i)))));
                }

                seq.Add(new LoadLocalIlExpression(destLocal));

                return new IlExpressionSequence(seq);
            }
            else if (argType is Matrices mt)
            {
            }
            else
                throw new TypeException("argument at index 0",
                    $"Unsupported type for addition: {argType.DisplayName}");

            return expr;
        }
    }
}
