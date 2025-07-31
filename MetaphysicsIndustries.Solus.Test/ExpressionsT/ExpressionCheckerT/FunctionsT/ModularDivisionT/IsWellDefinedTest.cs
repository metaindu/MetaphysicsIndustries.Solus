
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
    FunctionsT.ModularDivisionT
{
    [TestFixture]
    public class IsWellDefinedTest
    {
        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, -2, 0)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(1, 4, 1)]
        [TestCase(2, 1, 0)]
        [TestCase(2, 2, 0)]
        [TestCase(2, 3, 2)]
        [TestCase(2, 4, 2)]
        [TestCase(1, -2, 1)]
        [TestCase(-1, 2, -1)]
        [TestCase(-1, -2, -1)]
        [TestCase(2, 8, 2)]
        [TestCase(-2, 8, -2)]
        [TestCase(2, -8, 2)]
        [TestCase(-2, -8, -2)]
        [TestCase(8, 2, 0)]
        [TestCase(-8, 2, 0)]
        [TestCase(8, -2, 0)]
        [TestCase(-8, -2, 0)]
        [TestCase(3, 8, 3)]
        [TestCase(-3, 8, -3)]
        [TestCase(3, -8, 3)]
        [TestCase(-3, -8, -3)]
        [TestCase(8, 3, 2)]
        [TestCase(-8, 3, -2)]
        [TestCase(8, -3, 2)]
        [TestCase(-8, -3, -2)]
        [TestCase(8.1f, 3.1f, 2)]
        [TestCase(8.9f, 3.9f, 2)]
        [TestCase(-8.1f, 3.1f, -2)]
        [TestCase(-8.9f, 3.9f, -2)]
        [TestCase(8.1f, -3.1f, 2)]
        [TestCase(8.9f, -3.9f, 2)]
        [TestCase(-8.1f, -3.1f, -2)]
        [TestCase(-8.9f, -3.9f, -2)]
        public void ModularDivisionValueDoesNotThrow(
            float dividend, float divisor, float expected)
        {
            // given
            var f = ModularDivision.Value;
            var args = new Expression[]
            {
                new Literal(dividend),
                new Literal(divisor)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        [Ignore("Can't check the ranges of inputs yet.")]
        public void ModularDivisionDivideByZeroThrows()
        {
            // given
            var f = ModularDivision.Value;
            var args = new Expression[] { new Literal(1), new Literal(0) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => ec.IsWellDefined(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("Division by zero"));
        }
    }
}