
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
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(
            SizeFunction func, NascentMethod nm,
            List<Expression> arguments)
        {
            var arg = ConvertToIlExpression(arguments[0], nm);
            if (arg.ResultType == typeof(byte) ||
                arg.ResultType == typeof(sbyte) ||
                arg.ResultType == typeof(short) ||
                arg.ResultType == typeof(ushort) ||
                arg.ResultType == typeof(int) ||
                arg.ResultType == typeof(uint) ||
                arg.ResultType == typeof(long) ||
                arg.ResultType == typeof(ulong) ||
                arg.ResultType == typeof(float) ||
                arg.ResultType == typeof(double) ||
                arg.ResultType == typeof(decimal) ||
                arg.ResultType == typeof(bool))
                return new IlExpressionSequence(
                    arg,
                    new PopIlExpression(),
                    new LoadNullIlExpression());
            if (arg.ResultType == typeof(string))
                return new ConvertR4IlExpression(
                    new CallIlExpression(
                        typeof(string).GetMethod("get_Length"),
                        arg));
            if (arg.ResultType == typeof(float[]))
                return new ConvertR4IlExpression(
                    new CallIlExpression(
                        typeof(float[]).GetMethod("get_Length"),
                        arg));
            var getLength3 = typeof(float[,]).GetMethod(
                "GetLength", new Type[] { typeof(int) });
            if (arg.ResultType == typeof(float[,]))
            {
                var local = nm.CreateLocal(typeof(float[,]));
                return new IlExpressionSequence(
                    typeof(float[]),
                    new StoreLocalIlExpression(local, arg),
                    new NewArrIlExpression(typeof(float),
                        new LoadConstantIlExpression(2L)),
                    new StoreElemIlExpression(
                        new DupIlExpression(),
                        new LoadConstantIlExpression(0L),
                        new ConvertR4IlExpression(
                            new CallIlExpression(
                                getLength3,
                                new LoadLocalIlExpression(local),
                                new LoadConstantIlExpression(0L)))),
                    new StoreElemIlExpression(
                        new DupIlExpression(),
                        new LoadConstantIlExpression(1L),
                        new ConvertR4IlExpression(
                            new CallIlExpression(
                                getLength3,
                                new LoadLocalIlExpression(local),
                                new LoadConstantIlExpression(1L)))));
            }

            throw new NotImplementedException();
        }
    }
}