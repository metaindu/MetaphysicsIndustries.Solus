
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorT.FunctionsT.
    FactorialFunctionT
{
    [TestFixture]
    public class EvalFactorialFunctionTest
    {
        [Test]
        public void FactorialFunctionCallWithNoArgsThrows()
        {
            // given
            var expr = new FunctionCall(FactorialFunction.Value);
            var eval = new Evaluator();
            // expect
            Assert.Throws<ArgumentException>(() =>
                eval.Eval(expr, null));
        }

        [Test]
        public void FactorialFunctionCallWithTwoArgsThrows()
        {
            // given
            var expr = new FunctionCall(FactorialFunction.Value,
                new Literal(1), new Literal(2));
            var eval = new Evaluator();
            // expect
            Assert.Throws<ArgumentException>(() =>
                eval.Eval(expr, null));
        }

        [Test]
        public void FactorialFunctionCallWithThreeArgsThrows()
        {
            // given
            var expr = new FunctionCall(FactorialFunction.Value,
                new Literal(1), new Literal(2), new Literal(4));
            var eval = new Evaluator();
            // expect
            Assert.Throws<ArgumentException>(() =>
                eval.Eval(expr, null));
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
            var expr = new FunctionCall(FactorialFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(expected, result.ToNumber().Value);
        }
    }
}
