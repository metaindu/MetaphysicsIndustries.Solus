
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
    FunctionsT.SineFunctionT
{
    [TestFixture]
    public class EvalSineFunctionTest
    {
        [Test]
        [TestCase(0, 0)]
        [TestCase((float)(Math.PI / 6), 0.5f)]
        [TestCase((float)(Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(Math.PI / 3), 0.866025403784439f)]
        [TestCase((float)(Math.PI / 2), 1)]
        [TestCase((float)(3 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)Math.PI, 0)]
        [TestCase((float)(5 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(3 * Math.PI / 2), -1)]
        [TestCase((float)(7 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(2 * Math.PI), 0)]
        [TestCase((float)(9 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(5 * Math.PI / 2), 1)]
        [TestCase((float)(-Math.PI / 6), -0.5f)]
        [TestCase((float)(-Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(-Math.PI / 3), -0.866025403784439f)]
        [TestCase((float)(-Math.PI / 2), -1)]
        [TestCase((float)(-3 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)-Math.PI, -0)]
        [TestCase((float)(-5 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(-3 * Math.PI / 2), 1)]
        [TestCase((float)(-7 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(-2 * Math.PI), 0)]
        [TestCase((float)(-9 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(-5 * Math.PI / 2), -1)]
        public void SineFunctionValueDoesNotThrow(float arg, float expected)
        {
            // given
            var f = SineFunction.Value;
            var args = new Expression[] { new Literal(arg) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, null));
        }
    }
}
