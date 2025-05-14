
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

using System;
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.Arctangent2FunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalArctangent2FunctionTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase((float)(-Math.PI / 3), -1.732050807568877f, 1)]
        [TestCase((float)(2 * Math.PI / 3), 1.732050807568877f, -1)]
        [TestCase((float)(-Math.PI / 3), -3.464101615137755f, 2)]
        [TestCase((float)(-Math.PI / 4), -1, 1)]
        [TestCase((float)(3*Math.PI / 4), 1, -1)]
        [TestCase((float)(-Math.PI / 6), -0.577350269189626f, 1)]
        [TestCase((float)(5 * Math.PI / 6), 0.577350269189626f, -1)]
        [TestCase(0, 0, 1)]
        [TestCase(0, 0, 2)]
        [TestCase((float)(Math.PI / 6), 0.577350269189626f, 1)]
        [TestCase((float)(-5 * Math.PI / 6), -0.577350269189626f, -1)]
        [TestCase((float)(Math.PI / 4), 1, 1)]
        [TestCase((float)(-3 * Math.PI / 4), -1, -1)]
        [TestCase((float)(Math.PI / 3), 1.732050807568877f, 1)]
        [TestCase((float)(-2 * Math.PI / 3), -1.732050807568877f, -1)]
        [TestCase(0, 0, 0)]
        public void Arctangent2FunctionValuesYieldsValue(
            float expected, float y, float x)
        {
            // given
            var f = Arctangent2Function.Value;
            var expr = new FunctionCall(f, new Expression[]
            {
                new Literal(y),
                new Literal(x)
            });
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.That(result.ToFloat(),
                Is.EqualTo(expected).Within(0.000001f));
        }
    }
}
