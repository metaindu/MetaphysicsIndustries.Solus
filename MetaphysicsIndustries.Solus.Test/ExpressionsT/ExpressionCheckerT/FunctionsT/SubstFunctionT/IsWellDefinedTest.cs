
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

using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.SubstFunctionT
{
    [TestFixture]
    public class IsWellDefinedTest
    {
        [Test]
        public void UnmatchedVariableDoesNotThrow()
        {
            // given
            var original = new VariableAccess("x");
            var expr = new FunctionCall(
                new Literal(SubstFunction.Value),
                original,
                new VariableAccess("y"),
                new Literal(1));
            var ec = new ExpressionChecker();
            var env = new SolusEnvironment();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, env));
        }

        [Test]
        public void MatchVariableGetsReplaced()
        {
            // given
            var expr = new FunctionCall(
                new Literal(SubstFunction.Value),
                new VariableAccess("x"),
                new VariableAccess("x"),
                new Literal(1));
            var ec = new ExpressionChecker();
            var env = new SolusEnvironment();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, env));
        }
    }
}
