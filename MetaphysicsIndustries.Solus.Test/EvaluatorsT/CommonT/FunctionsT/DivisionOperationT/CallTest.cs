
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

using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.DivisionOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalDivisionOperationTest<T>
        where T : IEvaluator, new()
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
            var args = new Expression[]
            {
                new Literal(dividend),
                new Literal(divisor)
            };
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
        public void DivisionOperationDivideByZeroThrows()
        {
            // given
            var f = DivisionOperation.Value;
            var args = new Expression[] { new Literal(1), new Literal(0) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Division by zero"));
        }
    }
}
