
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
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.ArcsecantFunctionT
{
    [TestFixture]
    public class EvalArcsecantFunctionTest
    {
        [Test]
        [TestCase(0, 1)]
        [TestCase((float)(Math.PI / 6), 1.154700538379252f)]
        [TestCase((float)(Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(Math.PI / 3), 2)]
        // [TestCase((float)(Math.PI / 2), 1/0)]
        [TestCase((float)(3 * Math.PI / 4), -1.414213562373095f)]
        [TestCase((float)Math.PI, -1)]
        public void ArcsecantFunctionValueDoesNotThrow(
            float expected, float arg)
        {
            // given
            var f = ArcsecantFunction.Value;
            var args = new Expression[] { new Literal(arg) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, null));
        }
    }
}
