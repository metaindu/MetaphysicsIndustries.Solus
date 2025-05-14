
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
    FunctionsT.BitwiseOrOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalBitwiseOrOperationTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 1)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 3)]
        [TestCase(3, 1, 3)]
        [TestCase(1, 2, 3)]
        [TestCase(2, 2, 2)]
        [TestCase(3, 2, 3)]
        [TestCase(1, 3, 3)]
        [TestCase(2, 3, 3)]
        [TestCase(3, 3, 3)]
        [TestCase(1.1f, 0, 1)]
        [TestCase(1.9f, 0, 1)]
        [TestCase(2.5f, 0, 2)]
        [TestCase(11184810, 22369620, 33554430)]
        [TestCase(11184811, 22369620, 33554431)]
        [TestCase(11184810, 1, 11184811)]
        // Loss of precision at 33554431 (0x1ffffff), because float32 only
        // has 24-bits for the significand.
        [TestCase(16777216, 16777216, 16777217)]
        public void BitwiseOrOperationIntegerYieldsInteger(
            float a, float b, float expected)
        {
            // given
            var f = BitwiseOrOperation.Value;
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
