
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
    FunctionsT.LogarithmFunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalLogarithmFunctionTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0.25f, 2, -2)]
        [TestCase(0.5f, 2, -1)]
        [TestCase(1, 2, 0)]
        [TestCase(1.414213562373095f, 2, 0.5f)]
        [TestCase(2, 2, 1)]
        [TestCase(2.82842712474619f, 2, 1.5f)]
        [TestCase(4, 2, 2)]
        [TestCase(8, 2, 3)]
        [TestCase(1, 10, 0)]
        [TestCase(10, 10, 1)]
        [TestCase(100, 10, 2)]
        [TestCase(1, 3, 0)]
        [TestCase(3, 3, 1)]
        [TestCase(9, 3, 2)]
        [TestCase(27, 3, 3)]
        [TestCase(81, 3, 4)]
        [TestCase(1, 1.5f, 0)]
        [TestCase(1, 2.5f, 0)]
        [TestCase(1, 3.5f, 0)]
        [TestCase(1, 4.5f, 0)]
        [TestCase(1, 5.5f, 0)]
        [TestCase(2, 0.5f, -1)]
        [TestCase(1.21f, 1.1f, 2)]
        [TestCase(0.81f, 0.9f, 2)]
        public void LogarithmFunctionValueYieldsValue(
            float arg, float b, float expected)
        {
            // given
            var f = LogarithmFunction.Value;
            var args = new Expression[] { new Literal(arg), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.ToNumber().Value,
                Is.EqualTo(expected).Within(0.00001f));
        }

        [Test]
        public void LogarithmFunctionZeroArgThrows()
        {
            // given
            var f = LogarithmFunction.Value;
            var args = new Expression[] { new Literal(0), new Literal(2) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Argument must be positive"));
        }

        [Test]
        public void LogarithmFunctionZeroBaseThrows()
        {
            // given
            var f = LogarithmFunction.Value;
            var args = new Expression[] { new Literal(2), new Literal(0) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Base must be positive"));
        }

        [Test]
        public void LogarithmFunctionBaseOneThrows()
        {
            // given
            var f = LogarithmFunction.Value;
            var args = new Expression[] { new Literal(2), new Literal(1) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Base must not be one"));
        }

        [Test]
        public void LogarithmFunctionNegativeArgThrows()
        {
            // given
            var f = LogarithmFunction.Value;
            var args = new Expression[] { new Literal(-1), new Literal(2) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Argument must be positive"));
        }

        [Test]
        public void LogarithmFunctionNegativeBaseThrows()
        {
            // given
            var f = LogarithmFunction.Value;
            var args = new Expression[] { new Literal(2), new Literal((-2)) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Base must be positive"));
        }
    }
}
