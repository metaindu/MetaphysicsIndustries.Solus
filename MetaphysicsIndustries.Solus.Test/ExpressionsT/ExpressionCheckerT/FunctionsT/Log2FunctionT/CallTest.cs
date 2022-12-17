
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.Log2FunctionT
{
    [TestFixture]
    public class EvalLog2FunctionTest
    {
        [Test]
        [TestCase(0.03125f, -5)]
        [TestCase(0.0625f, -4)]
        [TestCase(0.125f, -3)]
        [TestCase(0.25f, -2)]
        [TestCase(0.353553390593274f, -1.5f)]
        [TestCase(0.5f, -1)]
        [TestCase(0.707106781186548f, -0.5f)]
        [TestCase(1, 0)]
        [TestCase(1.414213562373095f, 0.5f)]
        [TestCase(2, 1)]
        [TestCase(2.82842712474619f, 1.5f)]
        [TestCase(4, 2)]
        [TestCase(8, 3)]
        [TestCase(16, 4)]
        [TestCase(32, 5)]
        [TestCase(64, 6)]
        [TestCase(128, 7)]
        public void Log2FunctionValueDoesNotThrow(
            float arg, float expected)
        {
            // given
            var f = Log2Function.Value;
            var args = new Expression[] { new Literal(arg) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, null));
        }

        [Test]
        [Ignore("Can't check the ranges of inputs yet.")]
        public void Log2FunctionZeroThrows()
        {
            // given
            var f = Log2Function.Value;
            var args = new Expression[] { new Literal(0) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => ec.Check(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Argument must be positive"));
        }

        [Test]
        [Ignore("Can't check the ranges of inputs yet.")]
        public void Log2FunctionNegativeValueThrows()
        {
            // given
            var f = Log2Function.Value;
            var args = new Expression[] { new Literal(-1) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => ec.Check(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Argument must be positive"));
        }
    }
}
