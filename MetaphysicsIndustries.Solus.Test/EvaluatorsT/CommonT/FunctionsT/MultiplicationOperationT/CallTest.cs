
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.MultiplicationOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalMultiplicationOperationTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, -2, 0)]
        [TestCase(0, 0.5f, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 3, 3)]
        [TestCase(1, 4, 4)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(2, 3, 6)]
        [TestCase(2, 4, 8)]
        [TestCase(1, -2, -2)]
        [TestCase(-1, 2, -2)]
        [TestCase(-1, -2, 2)]
        [TestCase(1.5f, 1.5f, 2.25f)]
        public void MultiplicationOperationValuesYieldsValue(
            float a, float b, float expected)
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.That(result.ToFloat(),
                Is.EqualTo(expected).Within(0.000001f));
        }

        [Test]
        public void MultiplicationOperationCallWithNoArgsThrows()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = Array.Empty<Expression>();
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(() =>
                eval.Eval(expr, null));
            // and
            Assert.IsTrue(ex.Message.StartsWith("Wrong number of arguments"));
        }

        [Test]
        public void MultiplicationOperationCallWithOneArgThrows()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[] { new Literal(2) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(() =>
                eval.Eval(expr, null));
            // and
            Assert.IsTrue(ex.Message.StartsWith("Wrong number of arguments"));
        }

        [Test]
        public void MultiplicationOperationCallWithTwoArgsYieldsProduct()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[] { new Literal(2), new Literal(3) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.ToNumber().Value, Is.EqualTo(6));
        }

        [Test]
        public void MultiplicationOperationCallWithThreeArgsYieldsProduct()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(2),
                new Literal(3),
                new Literal(5)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.ToNumber().Value, Is.EqualTo(30));
        }
    }
}
