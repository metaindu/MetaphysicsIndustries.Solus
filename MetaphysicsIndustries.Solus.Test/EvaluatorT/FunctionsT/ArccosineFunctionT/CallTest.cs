
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorT.FunctionsT.
    ArccosineFunctionT
{
    [TestFixture]
    public class EvalArccosineFunctionTest
    {
        [Test]
        [TestCase(0, 1)]
        [TestCase((float)(Math.PI / 6), 0.866025403784439f)]
        [TestCase((float)(Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(Math.PI / 3), 0.5f)]
        [TestCase((float)(Math.PI / 2), 0)]
        [TestCase((float)(3 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)Math.PI, -1)]
        public void ArccosineFunctionValueYieldsValue(
            float expected, float arg)
        {
            // given
            var f = ArccosineFunction.Value;
            var args = new IMathObject[] { arg.ToNumber() };
            var eval = new Evaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        [Test]
        public void ArccosineFunctionOutOfBoundsThrows1()
        {
            // given
            var f = ArccosineFunction.Value;
            var args = new IMathObject[] { (-1.0001f).ToNumber() };
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Call(f, args, null));
            // and
            Assert.AreEqual("Argument less than -1", ex.Message);
        }

        [Test]
        public void ArccosineFunctionOutOfBoundsThrows2()
        {
            // given
            var f = ArccosineFunction.Value;
            var args = new IMathObject[] { 1.0001f.ToNumber() };
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Call(f, args, null));
            // and
            Assert.AreEqual("Argument greater than 1", ex.Message);
        }
    }
}
