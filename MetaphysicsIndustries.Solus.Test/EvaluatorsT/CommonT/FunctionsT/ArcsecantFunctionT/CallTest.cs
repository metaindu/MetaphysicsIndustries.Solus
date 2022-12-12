
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

using System;
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.ArcsecantFunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalArcsecantFunctionTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0, 1)]
        [TestCase((float)(Math.PI / 6), 1.154700538379252f)]
        [TestCase((float)(Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(Math.PI / 3), 2)]
        // [TestCase((float)(Math.PI / 2), 1/0)]
        [TestCase((float)(3 * Math.PI / 4), -1.414213562373095f)]
        [TestCase((float)Math.PI, -1)]
        public void ArcsecantFunctionValueYieldsValue(
            float expected, float arg)
        {
            // given
            var f = ArcsecantFunction.Value;
            var args = new IMathObject[] { arg.ToNumber() };
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }
    }
}
