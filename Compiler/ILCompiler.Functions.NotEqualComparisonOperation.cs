
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(
            NotEqualComparisonOperation func,
            NascentMethod nm,
            VariableIdentityMap variables,
            List<Expression> arguments)
        {
            var a = ConvertToIlExpression(arguments[0], nm, variables);
            var b = ConvertToIlExpression(arguments[1], nm, variables);
            var seq = new List<IlExpression>();
            if (a.ResultType != b.ResultType)
            {
                seq.Add(a);
                seq.Add(b);
                seq.Add(new PopIlExpression());
                seq.Add(new PopIlExpression());
                seq.Add(new LoadConstantIlExpression(true));
            }
            else if (a.ResultType == typeof(float) ||
                     a.ResultType == typeof(double) ||
                     a.ResultType == typeof(bool) ||
                     a.ResultType == typeof(byte) ||
                     a.ResultType == typeof(sbyte) ||
                     a.ResultType == typeof(short) ||
                     a.ResultType == typeof(ushort) ||
                     a.ResultType == typeof(int) ||
                     a.ResultType == typeof(uint) ||
                     a.ResultType == typeof(long) ||
                     a.ResultType == typeof(ulong) ||
                     a.ResultType == typeof(string))
            {
                seq.Add(
                    new CompareEqualIlExpression(
                        new LoadConstantIlExpression(0),
                        new CompareEqualIlExpression(a, b)));
            }
            else if (a.ResultType == typeof(float[]))
            {
                // TODO: opportunity for parallelism here
                var aa = nm.CreateLocal(typeof(float[]), "a");
                seq.Add(a);
                seq.Add(new StoreLocalIlExpression(aa, null));
                var bb = nm.CreateLocal(typeof(float[]), "b");
                seq.Add(b);
                seq.Add(new StoreLocalIlExpression(bb, null));
                var len = typeof(float[]).GetMethod("get_Length");
                seq.Add(
                    new CompareEqualIlExpression(
                        new CallIlExpression(
                            len,
                            new LoadLocalIlExpression(aa)),
                        new CallIlExpression(
                            len,
                            new LoadLocalIlExpression(bb))));
                var returnTrue = new NopIlExpression();
                var nop = new NopIlExpression();
                seq.Add(new BrFalseIlExpression(returnTrue));
                var counter = nm.CreateLocal(typeof(int), "i");
                seq.Add(
                    new StoreLocalIlExpression(counter,
                        new CallIlExpression(len,
                            new LoadLocalIlExpression(aa))));
                seq.Add(
                    new WhileLoopConstruct(
                        new CompareGreaterThanIlExpression(
                            new LoadLocalIlExpression(counter),
                            new LoadConstantIlExpression(0)),
                        new IlExpressionSequence(
                            new StoreLocalIlExpression(
                                counter,
                                new SubIlExpression(
                                    new LoadLocalIlExpression(counter),
                                    new LoadConstantIlExpression(1))),
                            new CompareEqualIlExpression(
                                new LoadElemIlExpression(
                                    new LoadLocalIlExpression(aa),
                                    new LoadLocalIlExpression(counter)),
                                new LoadElemIlExpression(
                                    new LoadLocalIlExpression(bb),
                                    new LoadLocalIlExpression(counter))),
                            new BrFalseIlExpression(returnTrue))));
                seq.Add(new LoadConstantIlExpression(false));
                seq.Add(new BranchIlExpression(nop));
                seq.Add(returnTrue);
                seq.Add(new LoadConstantIlExpression(true));
                seq.Add(nop);
            }
            else if (a.ResultType == typeof(float[,]))
            {
                // TODO: opportunity for parallelism here
                var aa = nm.CreateLocal(typeof(float[,]), "a");
                seq.Add(new StoreLocalIlExpression(aa, a));
                var bb = nm.CreateLocal(typeof(float[,]), "b");
                seq.Add(new StoreLocalIlExpression(bb, b));
                var len = typeof(float[,]).GetMethod(
                    "GetLength",
                    new[] { typeof(int) });
                seq.Add(
                    new CompareEqualIlExpression(
                        new CallIlExpression(
                            len,
                            new LoadLocalIlExpression(aa),
                            new LoadConstantIlExpression(0)),
                        new CallIlExpression(
                            len,
                            new LoadLocalIlExpression(bb),
                            new LoadConstantIlExpression(0))));
                var returnTrue = new NopIlExpression();
                var nop = new NopIlExpression();
                seq.Add(new BrFalseIlExpression(returnTrue));
                seq.Add(
                    new CompareEqualIlExpression(
                        new CallIlExpression(
                            len,
                            new LoadLocalIlExpression(aa),
                            new LoadConstantIlExpression(1)),
                        new CallIlExpression(
                            len,
                            new LoadLocalIlExpression(bb),
                            new LoadConstantIlExpression(1))));
                seq.Add(new BrFalseIlExpression(returnTrue));
                var r = nm.CreateLocal(typeof(int), "r");
                var c = nm.CreateLocal(typeof(int), "c");
                seq.Add(
                    new StoreLocalIlExpression(r,
                        new CallIlExpression(len,
                            new LoadLocalIlExpression(aa),
                            new LoadConstantIlExpression(0))));
                // TODO: hard-code row count and column count
                // TODO: loop unrolling
                // TODO: parallelize
                var get = typeof(float[,]).GetMethod(
                    "Get", new[] { typeof(int), typeof(int) });
                seq.Add(
                    new WhileLoopConstruct(
                        new CompareGreaterThanIlExpression(
                            new LoadLocalIlExpression(r),
                            new LoadConstantIlExpression(0)),
                        new IlExpressionSequence(
                            new StoreLocalIlExpression(
                                r,
                                new SubIlExpression(
                                    new LoadLocalIlExpression(r),
                                    new LoadConstantIlExpression(1))),
                            new StoreLocalIlExpression(c,
                                new CallIlExpression(len,
                                    new LoadLocalIlExpression(aa),
                                    new LoadConstantIlExpression(1))),
                            new WhileLoopConstruct(
                                new CompareGreaterThanIlExpression(
                                    new LoadLocalIlExpression(c),
                                    new LoadConstantIlExpression(0)),
                                new IlExpressionSequence(
                                    new StoreLocalIlExpression(
                                        c,
                                        new SubIlExpression(
                                            new LoadLocalIlExpression(c),
                                            new LoadConstantIlExpression(1))),
                                    new CompareEqualIlExpression(
                                        new CallIlExpression(
                                            get,
                                            new LoadLocalIlExpression(aa),
                                            new LoadLocalIlExpression(r),
                                            new LoadLocalIlExpression(c)),
                                        new CallIlExpression(
                                            get,
                                            new LoadLocalIlExpression(bb),
                                            new LoadLocalIlExpression(r),
                                            new LoadLocalIlExpression(c))),
                                    new BrFalseIlExpression(returnTrue))))));
                seq.Add(new LoadConstantIlExpression(false));
                seq.Add(new BranchIlExpression(nop));
                seq.Add(returnTrue);
                seq.Add(new LoadConstantIlExpression(true));
                seq.Add(nop);
            }
            else
            {
                seq.Add(a);
                seq.Add(b);
                seq.Add(new PopIlExpression());
                seq.Add(new PopIlExpression());
                var ctor = typeof(TypeException).GetConstructor(
                    new Type[] { typeof(string), typeof(string) });
                seq.Add(
                    new ThrowIlExpression(
                        new NewObjIlExpression(
                            ctor,
                            new LoadNullIlExpression(),
                            new LoadStringIlExpression(
                                "Type not supported for equality " +
                                // TODO: don't hard-code the type name
                                "comparisons: Interval"))));
            }

            return new IlExpressionSequence(seq);
        }
    }
}
