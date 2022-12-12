
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
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.AbsoluteValueFunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalAbsoluteValueFunctionTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        public void AbsoluteValueFunctionPositiveYieldsPositive()
        {
            // given
            var f = AbsoluteValueFunction.Value;
            var args = new IMathObject[] { 1.ToNumber() };
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void AbsoluteValueFunctionNegativeYieldsPositive()
        {
            // given
            var f = AbsoluteValueFunction.Value;
            var args = new IMathObject[] { (-1).ToNumber() };
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.AreEqual(1, result.ToNumber().Value);
        }
    }
}
