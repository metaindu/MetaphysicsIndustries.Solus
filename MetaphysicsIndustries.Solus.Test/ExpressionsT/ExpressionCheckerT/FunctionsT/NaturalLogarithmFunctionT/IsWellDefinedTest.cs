
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

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.NaturalLogarithmFunctionT
{
    [TestFixture]
    public class IsWellDefinedTest
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
        public void NaturalLogarithmFunctionValueDoesNotThrow(
            float arg, float expected)
        {
            // given
            var f = NaturalLogarithmFunction.Value;
            var args = new Expression[] { new Literal(arg) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        [Ignore("Can't check the ranges of inputs yet.")]
        public void NaturalLogarithmFunctionZeroThrows()
        {
            // given
            var f = NaturalLogarithmFunction.Value;
            var args = new Expression[] { new Literal(0) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => ec.IsWellDefined(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Argument must be positive"));
        }

        [Test]
        [Ignore("Can't check the ranges of inputs yet.")]
        public void NaturalLogarithmFunctionNegativeValueThrows()
        {
            // given
            var f = NaturalLogarithmFunction.Value;
            var args = new Expression[] { new Literal(-1) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => ec.IsWellDefined(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Argument must be positive"));
        }
    }
}
