
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
    FunctionsT.EqualComparisonOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalEqualComparisonOperationTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0, 0, true)]
        [TestCase(0, 1, false)]
        [TestCase(0, 2, false)]
        [TestCase(0, 3, false)]
        [TestCase(1, 0, false)]
        [TestCase(1, 1, true)]
        [TestCase(1, 2, false)]
        [TestCase(1, 3, false)]
        [TestCase(2, 0, false)]
        [TestCase(2, 1, false)]
        [TestCase(2, 2, true)]
        [TestCase(2, 3, false)]
        [TestCase(3, 0, false)]
        [TestCase(3, 1, false)]
        [TestCase(3, 2, false)]
        [TestCase(3, 3, true)]
        [TestCase(0, -1, false)]
        [TestCase(1, -1, false)]
        [TestCase(-1, 1, false)]
        [TestCase(-1, -1, true)]
        [TestCase(1.5f, 1.5f, true)]
        [TestCase(1.5f, 1.5001f, false)]
        public void EqualComparisonOperationValuesYieldValue(
            float a, float b, bool expected)
        {
            // given
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That((bool)(result.ToBoolean()), Is.EqualTo(expected));
        }
    }
}
