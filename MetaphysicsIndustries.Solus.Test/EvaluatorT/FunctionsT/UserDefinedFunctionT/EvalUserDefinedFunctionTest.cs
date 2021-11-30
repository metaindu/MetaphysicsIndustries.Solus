
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

using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorT.FunctionsT.
    UserDefinedFunctionT
{
    [TestFixture]
    public class EvalUserDefinedFunctionTest
    {
        [Test]
        public void UserDefinedFunctionYieldsValue()
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
            var expr = new FunctionCall(f,
                new Literal(1), new Literal(2), new Literal(4));
            var env = new SolusEnvironment();
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.AreEqual(7, result.ToNumber().Value);
        }

        [Test]
        public void UserDefinedFunctionNotAffectedByPreexistingVars()
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
            var expr = new FunctionCall(f,
                new Literal(1), new Literal(2), new Literal(4));
            var env = new SolusEnvironment();
            env.SetVariable("a", new Literal(8));
            var eval = new Evaluator();
            // precondition
            Assert.IsInstanceOf<Literal>(env.GetVariable("a"));
            Assert.AreEqual(8,
                ((Literal)env.GetVariable("a")).Value.ToNumber().Value);
            Assert.IsFalse(env.ContainsVariable("b"));
            Assert.IsFalse(env.ContainsVariable("c"));
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.AreEqual(7, result.ToNumber().Value);
            // and
            Assert.IsInstanceOf<Literal>(env.GetVariable("a"));
            Assert.AreEqual(8,
                ((Literal)env.GetVariable("a")).Value.ToNumber().Value);
            Assert.IsFalse(env.ContainsVariable("b"));
            Assert.IsFalse(env.ContainsVariable("c"));
        }
    }
}
