
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
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.TangentFunctionT
{
    [TestFixture]
    public class IsWellFormedTest
    {
        [Test]
        // [TestCase((float)(-5 * Math.PI / 2), inf)]
        [TestCase((float)(-9 * Math.PI / 4), -1)]
        [TestCase((float)(-2 * Math.PI), 0)]
        [TestCase((float)(-7 * Math.PI / 4), 1)]
        // [TestCase((float)(-3 * Math.PI / 2), inf)]
        [TestCase((float)(-5 * Math.PI / 4), -1)]
        [TestCase((float)-Math.PI, 0)]
        [TestCase((float)(-3 * Math.PI / 4), 1)]
        // [TestCase((float)(-Math.PI / 2), -inf)]
        [TestCase((float)(-Math.PI / 3), -1.732050807568877f)]
        [TestCase((float)(-Math.PI / 4), -1)]
        [TestCase((float)(-Math.PI / 6), -0.577350269189626f)]
        [TestCase(0, 0)]
        [TestCase((float)(Math.PI / 6), 0.577350269189626f)]
        [TestCase((float)(Math.PI / 4), 1)]
        [TestCase((float)(Math.PI / 3), 1.732050807568877f)]
        // [TestCase((float)(Math.PI / 2), inf)]
        [TestCase((float)(3 * Math.PI / 4), -1)]
        [TestCase((float)Math.PI, 0)]
        [TestCase((float)(5 * Math.PI / 4), 1)]
        // [TestCase((float)(3 * Math.PI / 2), inf)]
        [TestCase((float)(7 * Math.PI / 4), -1)]
        [TestCase((float)(2 * Math.PI), 0)]
        [TestCase((float)(9 * Math.PI / 4), 1)]
        // [TestCase((float)(5 * Math.PI / 2), inf)]
        public void TangentFunctionValueDoesNotThrow(float arg, float expected)
        {
            // given
            var f = TangentFunction.Value;
            var args = new Expression[] { new Literal(arg) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellFormed(expr));
        }
    }
}
