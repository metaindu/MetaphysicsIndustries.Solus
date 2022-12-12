
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
            ArcsineFunction func, NascentMethod nm,
            VariableIdentityMap variables,
            List<Expression> arguments)
        {
            var arg = ConvertToIlExpression(arguments[0], nm, variables);

            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });

            var checkLess = new IfThenElseConstruct(
                new CompareLessThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(-1f)),
                new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Argument less than -1"),
                        new LoadNullIlExpression())));
            var checkGreater = new IfThenElseConstruct(
                new CompareGreaterThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(1f)),
                new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Argument greater than 1"),
                        new LoadNullIlExpression())));
            var expr = new CallIlExpression(
                new Func<double, double>(Math.Asin));

            return new IlExpressionSequence(
                arg,
                checkLess,
                checkGreater,
                expr,
                new ConvertR4IlExpression());
        }
    }
}