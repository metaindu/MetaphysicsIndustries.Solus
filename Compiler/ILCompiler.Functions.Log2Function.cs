
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(
            Log2Function func, NascentMethod nm,
            List<Expression> arguments)
        {
            var arg = ConvertToIlExpression(arguments[0], nm);

            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });

            var checkNotPos = new IfThenElseConstruct(
                new CompareGreaterThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(0f)),
                elseBlock: new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression(
                            "Argument must be positive"),
                        new LoadNullIlExpression())));

            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Log),
                // arg,
                new LoadConstantIlExpression(2f));

            return new IlExpressionSequence(
                arg,
                checkNotPos,
                expr,
                new ConvertR4IlExpression());
        }
    }
}