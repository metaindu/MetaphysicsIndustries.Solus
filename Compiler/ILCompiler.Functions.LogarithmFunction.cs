
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
            LogarithmFunction func, NascentMethod nm,
            SolusEnvironment env, VariableIdentityMap variables,
            List<Expression> arguments)
        {
            var excType = typeof(OperandException);
            var ctor = excType.GetConstructor(
                new Type[] { typeof(string), typeof(Exception) });

            var arg = ConvertToIlExpression(arguments[0], nm, env,
                variables);

            var checkArgNotPos = new IfThenElseConstruct(
                new CompareGreaterThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(0f)),
                elseBlock: new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression(
                            "Argument must be positive"),
                        new LoadNullIlExpression())));

            var base_ = ConvertToIlExpression(arguments[1], nm, env,
                variables);

            var checkBaseNotPos = new IfThenElseConstruct(
                new CompareGreaterThanIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(0f)),
                elseBlock: new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Base must be positive"),
                        new LoadNullIlExpression())));
            var checkBaseNotOne = new IfThenElseConstruct(
                new CompareEqualIlExpression(
                    new DupIlExpression(),
                    new LoadConstantIlExpression(1f)),
                thenBlock: new ThrowIlExpression(
                    new NewObjIlExpression(
                        ctor,
                        new LoadStringIlExpression("Base must not be one"),
                        new LoadNullIlExpression())));

            var expr = new CallIlExpression(
                new Func<double, double, double>(Math.Log)
                // arg,
                // base_
                );
            return new IlExpressionSequence(
                arg,
                checkArgNotPos,
                base_,
                checkBaseNotPos,
                checkBaseNotOne,
                expr,
                new ConvertR4IlExpression());
        }
    }
}