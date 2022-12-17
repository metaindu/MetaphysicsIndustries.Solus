
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
    FunctionsT.DistFunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalDistFunctionTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 2)]
        [TestCase(1, 0, 1)]
        [TestCase(2, 0, 2)]
        [TestCase(0, -1, 1)]
        [TestCase(0, -2, 2)]
        [TestCase(-1, 0, 1)]
        [TestCase(-2, 0, 2)]
        [TestCase(1, 1, 1.414213562373095f)]
        [TestCase(-1, 1, 1.414213562373095f)]
        [TestCase(1, -1, 1.414213562373095f)]
        [TestCase(-1, -1, 1.414213562373095f)]
        [TestCase(2, 2, 2.82842712474619f)]
        [TestCase(1, 2, 2.23606797749979f)]
        [TestCase(-1, 2, 2.23606797749979f)]
        [TestCase(1, -2, 2.23606797749979f)]
        [TestCase(-1, -2, 2.23606797749979f)]
        [TestCase(2, 1, 2.23606797749979f)]
        [TestCase(-2, 1, 2.23606797749979f)]
        [TestCase(2, -1, 2.23606797749979f)]
        [TestCase(-2, -1, 2.23606797749979f)]
        [TestCase(1.234f, 5.678f, 5.810545585399017f)]
        public void DistFunctionValueYieldsValue(
            float x, float y, float expected)
        {
            // given
            var f = DistFunction.Value;
            var args = new IMathObject[] { x.ToNumber(), y.ToNumber() };
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.That(result.ToFloat(),
                Is.EqualTo(expected).Within(0.000001f));
        }
    }
}
