
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

using System;
using MetaphysicsIndustries.Solus;
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.MaximumFiniteFunctionT
{
    [TestFixture]
    public class IsWellDefinedTest
    {
        [Test]
        public void AscendingDoesNotThrow()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new Literal(4),
                new Literal(5)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void DescendingDoesNotThrow()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(9),
                new Literal(8),
                new Literal(7),
                new Literal(6)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void NegativeAscendingDoesNotThrow()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(-5),
                new Literal(-4),
                new Literal(-3),
                new Literal(-2),
                new Literal(-1)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void NegativeDescendingDoesNotThrow()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(-6),
                new Literal(-7),
                new Literal(-8),
                new Literal(-9)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void PositiveAndNegativeDoesNotThrow()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(-1),
                new Literal(0),
                new Literal(1)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void NotInOrderDoesNotThrow()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(5),
                new Literal(9),
                new Literal(1),
                new Literal(3),
                new Literal(2)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void SingleArgumentDoesNotThrow()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(5)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void ZeroArgumentsThrows()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[0];
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => ec.IsWellDefined(expr, null));
            // and
            Assert.That(ex.Message, Is.EqualTo("No arguments passed"));
        }

        [Test]
        public void NaNIsIgnored()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(1),
                new Literal(2),
                new Literal(float.NaN),
                new Literal(4),
                new Literal(5)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void PositiveInfinityIsIgnored()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(1),
                new Literal(2),
                new Literal(float.PositiveInfinity),
                new Literal(4),
                new Literal(5)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void NegativeInfinityIsIgnored()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(1),
                new Literal(2),
                new Literal(float.NegativeInfinity),
                new Literal(4),
                new Literal(5)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void InfinitiesAndNanYieldMax()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(float.PositiveInfinity),
                new Literal(float.NegativeInfinity),
                new Literal(float.NaN),
                new Literal(1)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void NoFiniteNumbersDoesNotThrow()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(float.PositiveInfinity),
                new Literal(float.NegativeInfinity),
                new Literal(float.NaN)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void NoFiniteNumbersDoesNotThrow2()
        {
            // given
            var f = MaximumFiniteFunction.Value;
            var args = new Expression[]
            {
                new Literal(float.PositiveInfinity),
                new Literal(float.NegativeInfinity)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }
    }
}
