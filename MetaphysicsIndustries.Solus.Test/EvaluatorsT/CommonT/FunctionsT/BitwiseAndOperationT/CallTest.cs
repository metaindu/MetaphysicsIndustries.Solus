
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
    FunctionsT.BitwiseAndOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalBitwiseAndOperationTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 0)]
        [TestCase(0, 1, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 0)]
        [TestCase(3, 1, 1)]
        [TestCase(1, 2, 0)]
        [TestCase(2, 2, 2)]
        [TestCase(3, 2, 2)]
        [TestCase(1, 3, 1)]
        [TestCase(2, 3, 2)]
        [TestCase(3, 3, 3)]
        [TestCase(1.1f, 3, 1)]
        [TestCase(1.9f, 3, 1)]
        [TestCase(2.5f, 3, 2)]
        public void BitwiseAndOperationIntegerYieldsInteger(
            float a, float b, float expected)
        {
            // given
            var f = BitwiseAndOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.That(result.ToFloat(), Is.EqualTo(expected));
        }
    }
}
