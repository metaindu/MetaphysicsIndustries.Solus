
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

using System;
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.ArccotangentFunctionT
{
    [TestFixture]
    public class EvalArccotangentFunctionTest
    {
        [Test]
        // [TestCase(0, inf)]
        [TestCase((float)(Math.PI / 6), 1.732050807568877f)]
        [TestCase((float)(Math.PI / 4), 1)]
        [TestCase((float)(Math.PI / 3), 0.577350269189626f)]
        [TestCase((float)(Math.PI / 2), 0)]
        [TestCase((float)(3 * Math.PI / 4), -1)]
        // [TestCase((float)Math.PI, inf)]
        public void ArccotangentFunctionValueYieldsValue(
            float expected, float arg)
        {
            // given
            var f = ArccotangentFunction.Value;
            var args = new IMathObject[] { arg.ToNumber() };
            var eval = Util.CreateEvaluator();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }
    }
}
