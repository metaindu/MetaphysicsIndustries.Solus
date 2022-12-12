
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    ExpressionsT.MatrixExpressionT
{
    [TestFixture]
    public class CheckTest
    {
        [Test]
        public void LiteralsYieldMatrix()
        {
            // given
            var expr = new MatrixExpression(2, 2,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new Literal(4));
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, null));
        }

        [Test]
        public void LiteralsYieldMatrix2()
        {
            // given
            var expr = new MatrixExpression(2, 3,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new Literal(4),
                new Literal(5),
                new Literal(6));
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, null));
        }

        [Test]
        public void UndefinedVariablesInExpressionCauseException()
        {
            // given
            var expr = new MatrixExpression(2, 2,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new VariableAccess("a"));
            var env = new SolusEnvironment();
            var ec = new ExpressionChecker();
            // expect
            var exc = Assert.Throws<NameException>(
                () => ec.Check(expr, env));
            // and
            Assert.AreEqual("Variable not found: a",
                exc.Message);
        }

        [Test]
        public void DefinedVariablesInExpressionYieldValue()
        {
            // given
            var expr = new MatrixExpression(2, 2,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new VariableAccess("a"));
            var env = new SolusEnvironment();
            env.SetVariable("a", new Literal(5));
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        public void NestedExpressionsYieldNestedValues()
        {
            // given
            var expr = new MatrixExpression(2, 2,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new MatrixExpression(2, 2,
                    new Literal(4),
                    new Literal(5),
                    new Literal(6),
                    new Literal(7)));
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, null));
        }
    }
}
