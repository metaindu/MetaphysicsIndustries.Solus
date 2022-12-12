
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
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(
            FactorialFunction func, NascentMethod nm,
            VariableIdentityMap variables,
            List<Expression> arguments)
        {
            var product = nm.CreateLocal(typeof(float), "product");
            var n = nm.CreateLocal(typeof(float), "n");
            // n = ...
            var initN = new StoreLocalIlExpression(
                n, ConvertToIlExpression(arguments[0], nm, variables));
            // product = 1
            var initProduct = new StoreLocalIlExpression(
                product, new LoadConstantIlExpression(1f));
            // while (n > 1)
            // {
            //     product = product * n;
            //     n = n + 1;
            // }
            var loop = new WhileLoopConstruct(
                new CompareGreaterThanIlExpression(
                    new LoadLocalIlExpression(n),
                    new LoadConstantIlExpression(1f)),
                new IlExpressionSequence(
                    new StoreLocalIlExpression(product,
                        new MulIlExpression(
                            new LoadLocalIlExpression(product),
                            new LoadLocalIlExpression(n))),
                    new StoreLocalIlExpression(n,
                        new SubIlExpression(
                            new LoadLocalIlExpression(n),
                            new LoadConstantIlExpression(1f)))));
            // return product;
            return new IlExpressionSequence(
                initN,
                initProduct,
                loop,
                new LoadLocalIlExpression(product));
        }
    }
}
