
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

using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.EvaluatorT.FunctionsT.
    ExponentOperationT
{
    [TestFixture]
    public class EvalExponentOperationTest
    {
        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(1, -1, 1)]
        [TestCase(1, -0.5f, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 0.5f, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(1, 4, 1)]
        [TestCase(2, -1, 0.5f)]
        [TestCase(2, -0.5f, 0.707106781186548f)]
        [TestCase(2, 0, 1)]
        [TestCase(2, 0.5f, 1.414213562373095f)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(2, 3, 8)]
        [TestCase(2, 4, 16)]
        [TestCase(1.234f, 5.678f, 3.299798925315966f)]
        [TestCase(-1, -1, -1)]
        [TestCase(-1, 0, 1)]
        [TestCase(-1, 1, -1)]
        [TestCase(-1, 2, 1)]
        [TestCase(-1, 3, -1)]
        [TestCase(-1, 4, 1)]
        [TestCase(-2, -1, -0.5f)]
        [TestCase(-2, 0, 1)]
        [TestCase(-2, 1, -2)]
        [TestCase(-2, 2, 4)]
        [TestCase(-2, 3, -8)]
        [TestCase(-2, 4, 16)]
        // TODO: divide by zero: infinity or throws?
        // [TestCase(0, -1, )]
        // [TestCase(0, -2, )]
        // TODO: sqrt(-1) currently NaN
        // TODO: complex numbers
        public void ExponentOperationValueYieldsValue(
            float b, float exponent, float expected)
        {
            // given
            var f = ExponentOperation.Value;
            var args = new IMathObject[] { b.ToNumber(), exponent.ToNumber() };
            var eval = new Evaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }
    }
}
