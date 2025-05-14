
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
    FunctionsT.MinimumFiniteFunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalMinimumFiniteFunctionTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        public void AscendingYieldsMin()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new Literal(4),
                new Literal(5)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(1));
        }

        [Test]
        public void DescendingYieldsMin()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(9),
                new Literal(8),
                new Literal(7),
                new Literal(6)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(6));
        }

        [Test]
        public void NegativeAscendingYieldsMin()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(-5),
                new Literal(-4),
                new Literal(-3),
                new Literal(-2),
                new Literal(-1)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(-5));
        }

        [Test]
        public void NegativeDescendingYieldsMin()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(-6),
                new Literal(-7),
                new Literal(-8),
                new Literal(-9)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(-9));
        }

        [Test]
        public void PositiveAndNegativeYieldsMin()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(-1),
                new Literal(0),
                new Literal(1)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(-1));
        }

        [Test]
        public void NotInOrderYieldsMin()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(5),
                new Literal(9),
                new Literal(1),
                new Literal(3),
                new Literal(2)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(1));
        }

        [Test]
        public void SingleArgumentYieldsMin()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(5)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(5));
        }

        [Test]
        public void ZeroArgumentsThrows()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = Array.Empty<Expression>();
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("No arguments passed"));
        }

        [Test]
        public void NaNIsIgnored()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(1),
                new Literal(2),
                new Literal(float.NaN),
                new Literal(4),
                new Literal(5)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(1));
        }

        [Test]
        public void PositiveInfinityIsIgnored()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(1),
                new Literal(2),
                new Literal(float.PositiveInfinity),
                new Literal(4),
                new Literal(5)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(1));
        }

        [Test]
        public void NegativeInfinityIsIgnored()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(1),
                new Literal(2),
                new Literal(float.NegativeInfinity),
                new Literal(4),
                new Literal(5)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(1));
        }

        [Test]
        public void InfinitiesAndNanYieldMin()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(float.PositiveInfinity),
                new Literal(float.NegativeInfinity),
                new Literal(float.NaN),
                new Literal(1)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(1));
        }

        [Test]
        public void NoFiniteNumbersYieldsNaN1()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(float.PositiveInfinity),
                new Literal(float.NegativeInfinity),
                new Literal(float.NaN)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(float.NaN));
        }

        [Test]
        public void NoFiniteNumbersYieldsNaN2()
        {
            // given
            var f = MinimumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(float.PositiveInfinity),
                new Literal(float.NegativeInfinity)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.That(result.ToNumber().Value, Is.EqualTo(float.NaN));
        }
    }
}
