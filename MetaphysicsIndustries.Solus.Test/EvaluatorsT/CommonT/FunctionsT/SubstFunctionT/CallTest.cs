
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.SubstFunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    // TODO: [TestFixture(typeof(CompilingEvaluator))]
    public class CallTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        public void UnmatchedVariableYieldsSame()
        {
            // given
            var original = new VariableAccess("x");
            var expr = new FunctionCall(
                new Literal(SubstFunction.Value),
                original,
                new VariableAccess("y"),
                new Literal(1));
            var eval = Util.CreateEvaluator<T>();
            var env = new SolusEnvironment();
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.That(result, Is.SameAs(original));
            Assert.IsInstanceOf<VariableAccess>(result);
            Assert.That(((VariableAccess)result).VariableName,
                Is.EqualTo("x"));
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
            var eval = Util.CreateEvaluator<T>();
            var env = new SolusEnvironment();
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.IsInstanceOf<Literal>(result);
            Assert.That(((Literal)result).Value.ToNumber().Value,
                Is.EqualTo(1));
        }
    }
}
