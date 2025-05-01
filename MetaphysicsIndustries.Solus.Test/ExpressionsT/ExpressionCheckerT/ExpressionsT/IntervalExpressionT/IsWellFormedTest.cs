
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

using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    ExpressionsT.IntervalExpressionT
{
    [TestFixture]
    public class IsWellFormedTest
    {
        [Test]
        public void SimpleOpenIntervalDoesNotThrow()
        {
            // given
            var i = new IntervalExpression(
                new Literal(1), true,
                new Literal(2), true);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellFormed(i));
        }

        [Test]
        public void NonNumberLowerBoundYieldsTrue()
        {
            // given
            var i = new IntervalExpression(
                new Literal("asdf"), true,
                new Literal(2), true);
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(i));
        }

        [Test]
        public void NonNumberUpperBoundYieldsTrue()
        {
            // given
            var i = new IntervalExpression(
                new Literal(1), true,
                new Literal("asdf"), true);
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(i));
        }
    }
}
