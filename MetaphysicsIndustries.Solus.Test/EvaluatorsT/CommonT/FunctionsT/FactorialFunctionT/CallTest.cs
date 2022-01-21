
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
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.FactorialFunctionT
{
    [TestFixture]
    public class EvalFactorialFunctionTest
    {
        [Test]
        public void FactorialFunctionCallWithNoArgsThrows()
        {
            // given
            var f = FactorialFunction.Value;
            var args = new IMathObject[0];
            var eval = Util.CreateEvaluator();
            // expect
            Assert.Throws<ArgumentException>(() =>
                eval.Call(f, args, null));
        }

        [Test]
        public void FactorialFunctionCallWithTwoArgsThrows()
        {
            // given
            var f = FactorialFunction.Value;
            var args = new IMathObject[] { 1.ToNumber(), 2.ToNumber() };
            var eval = Util.CreateEvaluator();
            // expect
            Assert.Throws<ArgumentException>(() =>
                eval.Call(f, args, null));
        }

        [Test]
        public void FactorialFunctionCallWithThreeArgsThrows()
        {
            // given
            var f = FactorialFunction.Value;
            var args = new IMathObject[]
            {
                1.ToNumber(),
                2.ToNumber(),
                4.ToNumber()
            };
            var eval = Util.CreateEvaluator();
            // expect
            Assert.Throws<ArgumentException>(() =>
                eval.Call(f, args, null));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(4, 24)]
        [TestCase(5, 120)]
        [TestCase(6, 720)]
        [TestCase(7, 5040)]
        [TestCase(8, 40320)]
        [TestCase(9, 362880)]
        [TestCase(10, 3628800)]
        [TestCase(11, 39916800)]
        public void FactorialFunctionValueYieldsValue(
            float arg, float expected)
        {
            // given
            var f = FactorialFunction.Value;
            var args = new IMathObject[] { arg.ToNumber() };
            var eval = Util.CreateEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.AreEqual(expected, result.ToNumber().Value);
        }
    }
}
