
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.MinimumFunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalMinimumFunctionTest<T>
        where T : IEvaluator, new()
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(1));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(6));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(-5));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(-9));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(-1));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(1));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(5));
        }

        [Test]
        public void ZeroArgumentsThrows()
        {
            // given
            var f = MinimumFunction.Value;
            var args = new IMathObject[0];
            var eval = Util.CreateEvaluator<T>();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Call(f, args, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("No arguments passed"));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(float.NaN));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(1));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value,
                Is.EqualTo(float.NegativeInfinity));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(float.NaN));
        }
    }
}
