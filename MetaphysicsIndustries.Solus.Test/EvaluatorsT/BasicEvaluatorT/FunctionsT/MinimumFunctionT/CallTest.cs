
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.BasicEvaluatorT.
    FunctionsT.MinimumFunctionT
{
    [TestFixture]
    public class EvalMinimumFunctionTest
    {
        [Test]
        public void AscendingYieldsMin()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(1),
                new Number(2),
                new Number(3),
                new Number(4),
                new Number(5)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void DescendingYieldsMin()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(9),
                new Number(8),
                new Number(7),
                new Number(6)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(6, result.ToNumber().Value);
        }

        [Test]
        public void NegativeAscendingYieldsMin()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(-5),
                new Number(-4),
                new Number(-3),
                new Number(-2),
                new Number(-1)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(-5, result.ToNumber().Value);
        }

        [Test]
        public void NegativeDescendingYieldsMin()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(-6),
                new Number(-7),
                new Number(-8),
                new Number(-9)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(-9, result.ToNumber().Value);
        }

        [Test]
        public void PositiveAndNegativeYieldsMin()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(-1),
                new Number(0),
                new Number(1)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(-1, result.ToNumber().Value);
        }

        [Test]
        public void NotInOrderYieldsMin()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(5),
                new Number(9),
                new Number(1),
                new Number(3),
                new Number(2)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void SingleArgumentYieldsMin()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(5)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(5, result.ToNumber().Value);
        }

        [Test]
        public void ZeroArgumentsThrows()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[0];
            var eval = new BasicEvaluator();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Call(f, args, null));
            // and
            Assert.AreEqual("No arguments passed", ex.Message);
        }

        [Test]
        public void NaNYieldsNaN()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(1),
                new Number(2),
                new Number(float.NaN),
                new Number(4),
                new Number(5)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(float.NaN, result.ToNumber().Value);
        }

        [Test]
        public void PositiveInfinityIsIgnored()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(1),
                new Number(2),
                new Number(float.PositiveInfinity),
                new Number(4),
                new Number(5)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void NegativeInfinityYieldsNegativeInfinity()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(1),
                new Number(2),
                new Number(float.NegativeInfinity),
                new Number(4),
                new Number(5)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(float.NegativeInfinity, result.ToNumber().Value);
        }

        [Test]
        public void InfinitiesAndNanYieldNan()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[]
            {
                new Number(float.PositiveInfinity),
                new Number(float.NegativeInfinity),
                new Number(float.NaN)
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(float.NaN, result.ToNumber().Value);
        }
    }
}
