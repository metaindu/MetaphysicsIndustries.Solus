
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    ExpressionsT.VectorExpressionT
{
    [TestFixture]
    public class IsWellDefinedTest
    {
        [Test]
        public void LiteralsAreAllowed()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new Literal(3));
            var ec = new ExpressionChecker();
            // when
            Assert.DoesNotThrow(
                () => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void UndefinedVariablesInExpressionThrows()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new VariableAccess("a"));
            var env = new SolusEnvironment();
            var ec = new ExpressionChecker();
            // expect
            var exc = Assert.Throws<NameException>(
                () => ec.IsWellDefined(expr, env));
            // and
            Assert.That(exc.Message,
                Is.EqualTo("Variable not found: a"));
        }

        [Test]
        public void DefinedVariablesInExpressionDoesNotThrow()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new VariableAccess("a"));
            var env = new SolusEnvironment();
            env.SetVariable("a", new Literal(5));
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, env));
        }

        [Test]
        public void NestedExpressionsThrows()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new VectorExpression(3,
                    new Literal(3),
                    new Literal(4),
                    new Literal(5)));
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => ec.IsWellDefined(expr, null));
            // and
            Assert.That(ex.Message,
                Is.EqualTo("All components must be reals"));
        }

        [Test]
        public void NonNumberComponentThrows()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new Literal("abc"));
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => ec.IsWellDefined(expr, null));
            // and
            Assert.That(ex.Message,
                Is.EqualTo("All components must be reals"));
        }
    }
}
