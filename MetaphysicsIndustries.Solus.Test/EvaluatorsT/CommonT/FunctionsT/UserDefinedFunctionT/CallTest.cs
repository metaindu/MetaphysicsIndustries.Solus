
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.UserDefinedFunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalUserDefinedFunctionTest<T>
        where T : IEvaluator, new()
    {
        // TODO: check args

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
            var args = new IMathObject[]
            {
                new Number(1),
                new Number(2),
                new Number(4)
            };
            var env = new SolusEnvironment();
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, env);
            // then
            Assert.That(result.ToNumber().Value, Is.EqualTo(7));
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
            var args = new IMathObject[]
            {
                new Number(1),
                new Number(2),
                new Number(4)
            };
            var env = new SolusEnvironment();
            env.SetVariable("a", new Literal(8));
            var eval = Util.CreateEvaluator<T>();
            // precondition
            Assert.IsInstanceOf<Literal>(env.GetVariable("a"));
            Assert.That(((Literal)env.GetVariable("a")).Value.ToNumber().Value,
                Is.EqualTo(8));
            Assert.IsFalse(env.ContainsVariable("b"));
            Assert.IsFalse(env.ContainsVariable("c"));
            // when
            var result = eval.Call(f, args, env);
            // then
            Assert.That(result.ToNumber().Value, Is.EqualTo(7));
            // and
            Assert.IsInstanceOf<Literal>(env.GetVariable("a"));
            Assert.That(((Literal)env.GetVariable("a")).Value.ToNumber().Value,
                Is.EqualTo(8));
            Assert.IsFalse(env.ContainsVariable("b"));
            Assert.IsFalse(env.ContainsVariable("c"));
        }
    }
}
