
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

using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.BasicEvaluatorT.
    FunctionsT.DivisionOperationT
{
    [TestFixture]
    public class EvalDivisionOperationTest
    {
        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, -2, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 0.5f)]
        [TestCase(1, 3, 0.333333333333333f)]
        [TestCase(1, 4, 0.25f)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 0.666666666666667f)]
        [TestCase(2, 4, 0.5f)]
        [TestCase(1, -2, -0.5f)]
        [TestCase(-1, 2, -0.5f)]
        [TestCase(-1, -2, 0.5f)]
        public void DivisionOperationValueYieldsValue(
            float dividend, float divisor, float expected)
        {
            // given
            var f = DivisionOperation.Value;
            var args = new IMathObject[]
            {
                dividend.ToNumber(),
                divisor.ToNumber()
            };
            var eval = new BasicEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        [Test]
        public void DivisionOperationDivideByZeroThrows()
        {
            // given
            var f = DivisionOperation.Value;
            var args = new IMathObject[] { 1.ToNumber(), 0.ToNumber() };
            var eval = new BasicEvaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Call(f, args, null));
            // and
            Assert.AreEqual("Division by zero", ex.Message);
        }
    }
}
