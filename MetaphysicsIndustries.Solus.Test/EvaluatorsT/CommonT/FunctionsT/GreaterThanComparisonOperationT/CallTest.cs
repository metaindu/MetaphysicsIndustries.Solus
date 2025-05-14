
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
    FunctionsT.GreaterThanComparisonOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalGreaterThanComparisonOperationTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0, 0, false)]
        [TestCase(0, 1, false)]
        [TestCase(0, 2, false)]
        [TestCase(0, 3, false)]
        [TestCase(1, 0, true)]
        [TestCase(1, 1, false)]
        [TestCase(1, 2, false)]
        [TestCase(1, 3, false)]
        [TestCase(2, 0, true)]
        [TestCase(2, 1, true)]
        [TestCase(2, 2, false)]
        [TestCase(2, 3, false)]
        [TestCase(3, 0, true)]
        [TestCase(3, 1, true)]
        [TestCase(3, 2, true)]
        [TestCase(3, 3, false)]
        [TestCase(0, -1, true)]
        [TestCase(1, -1, true)]
        [TestCase(-1, 1, false)]
        [TestCase(-1, -1, false)]
        [TestCase(1.5f, 1.5f, false)]
        [TestCase(1.5f, 1.5001f, false)]
        [TestCase(1.5001f, 1.5f, true)]
        public void GreaterThanComparisonOperationValuesYieldValue(
            float a, float b, bool expected)
        {
            // given
            var f = GreaterThanComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That((bool)result.ToBoolean(), Is.EqualTo(expected));
        }
    }
}
