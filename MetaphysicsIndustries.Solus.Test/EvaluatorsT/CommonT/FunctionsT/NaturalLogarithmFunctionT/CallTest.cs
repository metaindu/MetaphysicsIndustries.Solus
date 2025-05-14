
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

using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.NaturalLogarithmFunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalNaturalLogarithmFunctionTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0.006737946999085f, -5)]
        [TestCase(0.018315638888734f, -4)]
        [TestCase(0.049787068367864f, -3)]
        [TestCase(0.135335283236613f, -2)]
        [TestCase(0.22313016014843f, -1.5f)]
        [TestCase(0.367879441171442f, -1)]
        [TestCase(0.606530659712633f, -0.5f)]
        [TestCase(1, 0)]
        [TestCase(1.648721270700128f, 0.5f)]
        [TestCase(2.718281828459045f, 1)]
        [TestCase(4.481689070338065f, 1.5f)]
        [TestCase(7.38905609893065f, 2)]
        [TestCase(20.085536923187668f, 3)]
        [TestCase(54.598150033144239f, 4)]
        [TestCase(148.413159102576603f, 5)]
        [TestCase(403.428793492735123f, 6)]
        [TestCase(8103.083927575384008f, 9)]
        public void NaturalLogarithmFunctionValueYieldsValue(
            float arg, float expected)
        {
            // given
            var f = NaturalLogarithmFunction.Value;
            var args = new Expression[] { new Literal(arg) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.ToNumber().Value,
                Is.EqualTo(expected).Within(0.00001f));
        }

        [Test]
        public void NaturalLogarithmFunctionZeroThrows()
        {
            // given
            var f = NaturalLogarithmFunction.Value;
            var args = new Expression[] { new Literal(0) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Argument must be positive"));
        }

        [Test]
        public void NaturalLogarithmFunctionNegativeValueThrows()
        {
            // given
            var f = NaturalLogarithmFunction.Value;
            var args = new Expression[] { new Literal(-1) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Argument must be positive"));
        }
    }
}
