
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

using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorT.FunctionsT.
    LogicalOrOperationT
{
    [TestFixture]
    public class EvalLogicalOrOperationTest
    {
        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 1)]
        [TestCase(0, 3, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(2, 0, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 1)]
        [TestCase(3, 0, 1)]
        [TestCase(3, 1, 1)]
        [TestCase(3, 2, 1)]
        [TestCase(3, 3, 1)]
        [TestCase(0, -1, 1)]
        [TestCase(1, -1, 1)]
        [TestCase(-1, 1, 1)]
        [TestCase(-1, -1, 1)]
        [TestCase(0.1f, 0.1f, 0)]
        [TestCase(0.9f, 0.9f, 0)]
        [TestCase(1.1f, 1.1f, 1)]
        [TestCase(1.9f, 1.9f, 1)]
        [TestCase(1.5f, 1.5f, 1)]
        [TestCase(1.5f, 1.5001f, 1)]
        [TestCase(-0.1f, -0.1f, 0)]
        [TestCase(-0.9f, -0.9f, 0)]
        [TestCase(-1.1f, -1.1f, 1)]
        [TestCase(-1.9f, -1.9f, 1)]
        public void LogicalOrOperationValuesYieldValue(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(LogicalOrOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }
    }
}
