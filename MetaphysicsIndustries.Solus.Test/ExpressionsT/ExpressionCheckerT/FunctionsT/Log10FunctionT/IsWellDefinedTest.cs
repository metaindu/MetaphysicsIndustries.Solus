
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
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.Log10FunctionT
{
    [TestFixture]
    public class IsWellDefinedTest
    {
        [Test]
        [TestCase(0.00001f, -5)]
        [TestCase(0.0001f, -4)]
        [TestCase(0.001f, -3)]
        [TestCase(0.01f, -2)]
        [TestCase(0.03162277660168379f, -1.5f)]
        [TestCase(0.1f, -1)]
        [TestCase(0.3162277660168379f, -0.5f)]
        [TestCase(1, 0)]
        [TestCase(3.162277660168379f, 0.5f)]
        [TestCase(10, 1)]
        [TestCase(31.62277660168379f, 1.5f)]
        [TestCase(100, 2)]
        [TestCase(1000, 3)]
        [TestCase(10000, 4)]
        [TestCase(100000, 5)]
        [TestCase(1000000, 6)]
        [TestCase(1000000000, 9)]
        public void Log10FunctionValueDoesNotThrow(
            float arg, float expected)
        {
            // given
            var f = Log10Function.Value;
            var args = new Expression[] { new Literal(arg) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        [Ignore("Can't check the ranges of inputs yet.")]
        public void Log10FunctionZeroThrows()
        {
            // given
            var f = Log10Function.Value;
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
        public void Log10FunctionNegativeValueThrows()
        {
            // given
            var f = Log10Function.Value;
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
