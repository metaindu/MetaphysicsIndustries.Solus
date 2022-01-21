
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

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.MultiplicationOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
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
            var args = new IMathObject[] { a.ToNumber(), b.ToNumber() };
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        [Test]
        public void MultiplicationOperationCallWithNoArgsThrows()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new IMathObject[0];
            var eval = Util.CreateEvaluator<T>();
            // expect
            var ex = Assert.Throws<ArgumentException>(() =>
                eval.Call(f, args, null));
            // and
            Assert.IsTrue(ex.Message.StartsWith("Wrong number of arguments"));
        }

        [Test]
        public void MultiplicationOperationCallWithOneArgThrows()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new IMathObject[] { 2.ToNumber() };
            var eval = Util.CreateEvaluator<T>();
            // expect
            var ex = Assert.Throws<ArgumentException>(() =>
                eval.Call(f, args, null));
            // and
            Assert.IsTrue(ex.Message.StartsWith("Wrong number of arguments"));
        }

        [Test]
        public void MultiplicationOperationCallWithTwoArgsYieldsProduct()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new IMathObject[] { 2.ToNumber(), 3.ToNumber() };
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.AreEqual(6, result.ToNumber().Value);
        }

        [Test]
        public void MultiplicationOperationCallWithThreeArgsYieldsProduct()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new IMathObject[]
            {
                2.ToNumber(),
                3.ToNumber(),
                5.ToNumber()
            };
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.AreEqual(30, result.ToNumber().Value);
        }
    }
}
