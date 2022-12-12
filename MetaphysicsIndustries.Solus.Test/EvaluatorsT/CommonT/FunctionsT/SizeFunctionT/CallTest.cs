
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.SizeFunctionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalSizeFunctionTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        public void SizeFunctionZeroArgumentsThrows()
        {
            // given
            var f = SizeFunction.Value;
            var args = new IMathObject[0];
            var eval = Util.CreateEvaluator<T>();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Call(f, args, null));
            // and
            Assert.AreEqual("Wrong number of arguments given to " +
                            "size (expected 1 but got 0)",
                ex.Message);
        }

        [Test]
        public void SizeFunctionTwoArgumentsThrows()
        {
            // given
            var f = SizeFunction.Value;
            var args = new IMathObject[]
            {
                new Vector(new float[] { 1, 2, 3 }),
                new Vector(new float[] { 4, 5, 6 })
            };
            var eval = Util.CreateEvaluator<T>();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Call(f, args, null));
            // and
            Assert.AreEqual("Wrong number of arguments given to " +
                            "size (expected 1 but got 2)",
                ex.Message);
        }

        [Test]
        public void SizeFunctionNonTensorThrows()
        {
            // given
            var f = SizeFunction.Value;
            var args = new IMathObject[] { 1.ToNumber() };
            var eval = Util.CreateEvaluator<T>();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Call(f, args, null));
            // and
            Assert.AreEqual("Argument wrong type: expected " +
                            "Vector or Matrix or String but got Scalar",
                ex.Message);
        }

        [Test]
        public void SizeFunctionVectorYieldsLength()
        {
            // given
            var f = SizeFunction.Value;
            var args = new IMathObject[]
            {
                new float[] { 1, 2 }.ToVector()
            };
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsVector(null));
            var v = result.ToVector();
            Assert.AreEqual(1, v.Length);
            Assert.AreEqual(2, v[0].ToFloat());
        }

        [Test]
        public void SizeFunctionMatrixYieldsRowAndColumnCounts()
        {
            // given
            var f = SizeFunction.Value;
            var args = new IMathObject[]
            {
                new Matrix(new float[,]
                {
                    { 1, 2, 3 },
                    { 4, 5, 6 }
                })
            };
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsVector(null));
            var v = result.ToVector();
            Assert.AreEqual(2, v.Length);
            Assert.AreEqual(2, v[0].ToFloat());
            Assert.AreEqual(3, v[1].ToFloat());
        }

        [Test]
        public void SizeFunctionStringYieldsLength()
        {
            // given
            var f = SizeFunction.Value;
            var args = new IMathObject[] { "abc".ToStringValue() };
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Call(f, args, null);
            // then
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsVector(null));
            var v = result.ToVector();
            Assert.AreEqual(1, v.Length);
            Assert.AreEqual(3, v[0].ToFloat());
        }
    }
}
