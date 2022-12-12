
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
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    ExpressionsT.VariableAccessT
{
    [TestFixture]
    public class CheckTest
    {
        [Test]
        public void GetsLiteralFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var expr2 = new Literal(3);
            var env = new SolusEnvironment();
            env.SetVariable("a", expr2);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        public void GetsFunctionCallFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var expr2 = new FunctionCall(CosineFunction.Value,
                new Literal(0));
            var env = new SolusEnvironment();
            env.SetVariable("a", expr2);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        public void GetsNumberFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = 3.ToNumber();
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        public void GetsVectorFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = new Vector(new float[] { 1, 2, 3 });
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        public void GetsMatrixFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = new Matrix(new float[,]
            {
                { 1, 2 },
                { 3, 4 }
            });
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        public void GetsStringFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = "abc".ToStringValue();
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        public void GetsIntervalFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = new Interval(1.1f, 3.5f);
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        public void GetsFunctionFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var f = CosineFunction.Value;
            var env = new SolusEnvironment();
            env.SetVariable("a", f);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        public void GetsExpressionInsideLiteralFromEnv()
        {
            var expr = new VariableAccess("a");
            var expr2 = ColorExpression.Gray;
            var expr3 = new Literal(expr2);
            var env = new SolusEnvironment();
            env.SetVariable("a", expr3);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.Check(expr, env));
        }

        [Test]
        public void MissingVariableThrows()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<NameException>(
                () => ec.Check(expr, env));
            // and
            Assert.AreEqual("Variable not found: a", ex.Message);
        }
    }
}
