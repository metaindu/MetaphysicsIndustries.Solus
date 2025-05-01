
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
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.UserDefinedFunctionT
{
    [TestFixture]
    public class IsWellFormedTest
    {
        [Test]
        public void UserDefinedFunctionDoesNotThrow()
        {
            // given
            var f = new UserDefinedFunction(
                "f",
                new[] { "a", "b", "c" },
                new FunctionCall(
                    AdditionOperation.Value,
                    new VariableAccess("a"),
                    new VariableAccess("b"),
                    new VariableAccess("c")));
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
        public void ParameterTypeIsNotChecked()
        {
            // given
            var udf = new UserDefinedFunction("f",
                new Parameter[]
                {
                    new Parameter("x", Reals.Value)
                },
                new VariableAccess("x"));
            var args = new Expression[] { new Literal("abc".ToStringValue()) };
            var expr = new FunctionCall(udf, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        public void SubtypeOfParameterTypeIsAllowed()
        {
            // given
            var udf = new UserDefinedFunction("f",
                new []
                {
                    new Parameter("x", AllVectors.Value)
                },
                new VariableAccess("x"));
            var args = new Expression[] { new Literal(new Vector2(1, 2)) };
            var expr = new FunctionCall(udf, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellFormed(expr));
            // and given
            var args2 = new Expression[]
            {
                new Literal(new Vector3(1, 2, 3))
            };
            var expr2 = new FunctionCall(udf, args2);
            // expect
            Assert.DoesNotThrow(() => ec.IsWellFormed(expr2));
        }
    }
}
