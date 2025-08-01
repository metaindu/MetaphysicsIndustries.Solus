
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
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.AdditionOperationT
{
    [TestFixture]
    public class IsWellFormedTest
    {
        [Test]
        public void AdditionOperationCallWithNoArgsThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[] { new Literal(1) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.Throws<ArgumentException>(
                () => ec.IsWellFormed(expr));
        }

        [Test]
        public void AdditionOperationCallWithOneArgThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[] { new Literal(1) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.Throws<ArgumentException>(
                () => ec.IsWellFormed(expr));
        }

        [Test]
        public void AdditionOperationCallWithTwoArgsDoesNotThrow()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[] { new Literal(1), new Literal(2) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellFormed(expr));
        }

        [Test]
        public void AdditionOperationCallWithThreeArgsDoesNotThrow()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(1),
                new Literal(2),
                new Literal(4)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellFormed(expr));
        }

        [Test]
        public void WrongTypeYieldsTrue()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Literal(1),
                new Literal(2)
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(expr));
        }
    }
}
