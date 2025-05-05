
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.Arctangent2FunctionT
{
    [TestFixture]
    public class IsWellDefinedTest
    {
        [Test]
        public void UndefinedVariableThrows()
        {
            // given
            var expr = new FunctionCall(
                Arctangent2Function.Value,
                new Literal(1),
                new VariableAccess("x"));
            var env = new SolusEnvironment();
            var ec = new ExpressionChecker();
            // expect
            var exc = Assert.Throws<NameException>(
                () => ec.IsWellDefined(expr, env));
            // and
            Assert.That(exc.Message,
                Is.EqualTo("Variable not found: x"));
        }

        [Test]
        public void DefinedVariableYieldsTrue()
        {
            // given
            var expr = new FunctionCall(
                Arctangent2Function.Value,
                new Literal(1),
                new VariableAccess("x"));
            var env = new SolusEnvironment();
            env.SetVariable("x", 1.ToNumber());
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellDefined(expr, env));
        }

        [Test]
        public void TooFewArgsThrows()
        {
            // given
            var expr = new FunctionCall(
                Arctangent2Function.Value,
                new Literal(1));
            var env = new SolusEnvironment();
            var ec = new ExpressionChecker();
            // expect
            Assert.Throws<ArgumentException>(
                () => ec.IsWellDefined(expr, env));
        }

        [Test]
        public void TooManyArgsThrows()
        {
            // given
            var expr = new FunctionCall(
                Arctangent2Function.Value,
                new Literal(1),
                new Literal(2),
                new Literal(3)
            );
            var env = new SolusEnvironment();
            var ec = new ExpressionChecker();
            // expect
            Assert.Throws<ArgumentException>(
                () => ec.IsWellDefined(expr, env));
        }
    }
}
