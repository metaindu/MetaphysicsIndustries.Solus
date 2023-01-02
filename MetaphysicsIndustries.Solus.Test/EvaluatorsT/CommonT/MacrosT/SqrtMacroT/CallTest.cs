
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
using MetaphysicsIndustries.Solus.Macros;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    MacrosT.SqrtMacroT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class CallTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [Ignore("Macros not supported currently")]
        public void CallYieldsExponentOperation()
        {
            // given
            var expr = new FunctionCall(new Literal(SqrtMacro.Value),
                new Literal(2));
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<FunctionCall>(result);
            var fc = (FunctionCall)result;
            Assert.IsInstanceOf<Literal>(fc.Function);
            var target = (Literal)fc.Function;
            Assert.That(target.Value, Is.SameAs(ExponentOperation.Value));
            Assert.That(fc.Arguments.Count,
                Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(fc.Arguments[0]);
            Assert.That(((Literal)fc.Arguments[0]).Value.ToNumber().Value,
                Is.EqualTo(2f));
            Assert.IsInstanceOf<Literal>(fc.Arguments[1]);
            Assert.That(((Literal)fc.Arguments[1]).Value.ToNumber().Value,
                Is.EqualTo(0.5f));
        }
    }
}
