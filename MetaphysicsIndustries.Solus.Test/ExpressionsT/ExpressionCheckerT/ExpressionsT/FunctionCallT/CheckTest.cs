
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
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

using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    ExpressionsT.FunctionCallT
{
    [TestFixture]
    public class CheckTest
    {
        [Test]
        public void FunctionCallWithNumberArgDoesNotThrow()
        {
            // given
            var mf = new MockFunction(new[] { Types.Scalar }, "f")
            {
                CallF = args => args.First()
            };
            var expr = new FunctionCall(mf, new Literal(5));
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, null));
        }

        [Test]
        public void FunctionCallWithVariableTargetDoesNotThrow()
        {
            // given
            var mf = new MockFunction(new[] { Types.Scalar }, "f")
            {
                CallF = args => args.First()
            };
            var expr = new FunctionCall(new VariableAccess("f"),
                new Expression[] { new Literal(5) });
            var ec = new ExpressionChecker();
            var env = new SolusEnvironment();
            env.SetVariable("f", mf);
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        [Ignore("IfMacro currently returns a Literal, which confuses " +
                "MultiplicationOperation")]
        public void RecursiveFunctionDoesNotThrow()
        {
            // given
            // factorial
            // f(x) := if( x<=1, 1, x * f(x-1) )
            var udf = new UserDefinedFunction("f", new[] { "x" },
                new FunctionCall(
                    new VariableAccess("if"),
                    new FunctionCall(
                        LessThanOrEqualComparisonOperation.Value,
                        new VariableAccess("x"),
                        new Literal(1)),
                    new Literal(1),
                    new FunctionCall(
                        MultiplicationOperation.Value,
                        new VariableAccess("x"),
                        new FunctionCall(
                            new VariableAccess("f"),
                            new FunctionCall(
                                AdditionOperation.Value,
                                new VariableAccess("x"),
                                new Literal(-1))))));
            var env = new SolusEnvironment();
            env.SetVariable("f", udf);
            var ec = new ExpressionChecker();
            var expr = new FunctionCall(
                new VariableAccess("f"),
                new Literal(2));
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }
    }
}
