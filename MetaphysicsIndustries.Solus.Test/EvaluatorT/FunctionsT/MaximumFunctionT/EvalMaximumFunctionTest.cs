
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorT.FunctionsT.
    MaximumFunctionT
{
    [TestFixture]
    public class EvalMaximumFunctionTest
    {
        [Test]
        public void AscendingYieldsMax()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new Literal(4),
                new Literal(5));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(5, result.ToNumber().Value);
        }

        [Test]
        public void DescendingYieldsMax()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(9),
                new Literal(8),
                new Literal(7),
                new Literal(6));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(9, result.ToNumber().Value);
        }

        [Test]
        public void NegativeAscendingYieldsMax()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(-5),
                new Literal(-4),
                new Literal(-3),
                new Literal(-2),
                new Literal(-1));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(-1, result.ToNumber().Value);
        }

        [Test]
        public void NegativeDescendingYieldsMax()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(-6),
                new Literal(-7),
                new Literal(-8),
                new Literal(-9));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(-6, result.ToNumber().Value);
        }

        [Test]
        public void PositiveAndNegativeYieldsMax()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(-1),
                new Literal(0),
                new Literal(1));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void NotInOrderYieldsMax()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(5),
                new Literal(9),
                new Literal(1),
                new Literal(3),
                new Literal(2));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(9, result.ToNumber().Value);
        }

        [Test]
        public void SingleArgumentYieldsMax()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(5));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(5, result.ToNumber().Value);
        }

        [Test]
        public void ZeroArgumentsThrows()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value);
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("No arguments passed", ex.Message);
        }

        [Test]
        public void NaNYieldsNaN()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(1),
                new Literal(2),
                new Literal(float.NaN),
                new Literal(4),
                new Literal(5));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(float.NaN, result.ToNumber().Value);
        }

        [Test]
        public void PositiveInfinityYieldsPositiveInfinity()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(1),
                new Literal(2),
                new Literal(float.PositiveInfinity),
                new Literal(4),
                new Literal(5));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(float.PositiveInfinity, result.ToNumber().Value);
        }

        [Test]
        public void NegativeInfinityIsIgnored()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(1),
                new Literal(2),
                new Literal(float.NegativeInfinity),
                new Literal(4),
                new Literal(5));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(5, result.ToNumber().Value);
        }

        [Test]
        public void InfinitiesAndNanYieldNan()
        {
            // given
            var expr = new FunctionCall(
                MaximumFunction.Value,
                new Literal(float.PositiveInfinity),
                new Literal(float.NegativeInfinity),
                new Literal(float.NaN));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(float.NaN, result.ToNumber().Value);
        }
    }
}