
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.ExponentOperationT
{
    [TestFixture]
    public class IsWellDefinedTest
    {
        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(1, -1, 1)]
        [TestCase(1, -0.5f, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 0.5f, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(1, 4, 1)]
        [TestCase(2, -1, 0.5f)]
        [TestCase(2, -0.5f, 0.707106781186548f)]
        [TestCase(2, 0, 1)]
        [TestCase(2, 0.5f, 1.414213562373095f)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(2, 3, 8)]
        [TestCase(2, 4, 16)]
        [TestCase(1.234f, 5.678f, 3.299798925315966f)]
        [TestCase(-1, -1, -1)]
        [TestCase(-1, 0, 1)]
        [TestCase(-1, 1, -1)]
        [TestCase(-1, 2, 1)]
        [TestCase(-1, 3, -1)]
        [TestCase(-1, 4, 1)]
        [TestCase(-2, -1, -0.5f)]
        [TestCase(-2, 0, 1)]
        [TestCase(-2, 1, -2)]
        [TestCase(-2, 2, 4)]
        [TestCase(-2, 3, -8)]
        [TestCase(-2, 4, 16)]
        // TODO: divide by zero: infinity or throws?
        // [TestCase(0, -1, )]
        // [TestCase(0, -2, )]
        // TODO: sqrt(-1) currently NaN
        // TODO: complex numbers
        public void ExponentOperationValueDoesNotThrow(
            float b, float exponent, float expected)
        {
            // given
            var f = ExponentOperation.Value;
            var args = new Expression[]
            {
                new Literal(b),
                new Literal(exponent)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }
    }
}
