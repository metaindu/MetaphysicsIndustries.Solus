
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
            MaximumFiniteFunction func, NascentMethod nm,
            SolusEnvironment env, VariableIdentityMap variables,
            List<Expression> arguments)
        {
            var exprs = new List<IlExpression>(8 * arguments.Count + 7);
            exprs.Add(new LoadConstantIlExpression(float.NegativeInfinity));
            var nop = new NopIlExpression();
            IlExpression first = nop;
            for (int i = arguments.Count - 1; i >= 0; i--)
            {
                var calcArg = ConvertToIlExpression(arguments[i], nm, env,
                    variables);
                var call1 = new CallIlExpression(
                    new Func<float, bool>(float.IsInfinity),
                    new DupIlExpression());
                var call2 = new CallIlExpression(
                    new Func<float, bool>(float.IsNaN),
                    new DupIlExpression());
                var pop = new PopIlExpression();
                var br1 = new BrTrueIlExpression(pop);
                var br2 = new BrTrueIlExpression(pop);
                var call3 = new CallIlExpression(
                    new Func<float, float, float>(Math.Max));
                var br3 = new BranchIlExpression(first);

                exprs.Insert(1, calcArg);
                exprs.Insert(2, call1);
                exprs.Insert(3, br1);
                exprs.Insert(4, call2);
                exprs.Insert(5, br2);
                exprs.Insert(6, call3);
                exprs.Insert(7, br3);
                exprs.Insert(8, pop);

                first = calcArg;
            }

            exprs.Add(nop);

            exprs.Add(
                new CallIlExpression(
                    new Func<float, bool>(float.IsNegativeInfinity),
                    new DupIlExpression()));
            var conv = new ConvertR4IlExpression();
            exprs.Add(new BrFalseIlExpression(conv));
            exprs.Add(new PopIlExpression());
            exprs.Add(new LoadConstantIlExpression(float.NaN));
            exprs.Add(conv);

            var seq = new IlExpressionSequence(exprs.ToArray());
            return seq;
        }
    }
}