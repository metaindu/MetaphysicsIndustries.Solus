
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorT.FunctionsT.
    SizeFunctionT
{
    [TestFixture]
    public class EvalSizeFunctionTest
    {
        [Test]
        public void SizeFunctionZeroArgumentsThrows()
        {
            // given
            var expr = new FunctionCall(SizeFunction.Value);
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Wrong number of arguments given to " +
                            "size (expected 1 but got 0)",
                ex.Message);
        }

        [Test]
        public void SizeFunctionTwoArgumentsThrows()
        {
            // given
            var expr = new FunctionCall(SizeFunction.Value,
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Literal(new Vector(new float[] { 4, 5, 6 })));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Wrong number of arguments given to " +
                            "size (expected 1 but got 2)",
                ex.Message);
        }

        [Test]
        public void SizeFunctionNonTensorThrows()
        {
            // given
            var expr = new FunctionCall(SizeFunction.Value,
                new Literal(1));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument wrong type: expected " +
                            "Vector or Matrix or String but got Scalar",
                ex.Message);
        }

        [Test]
        public void SizeFunctionVectorYieldsLength()
        {

            // given
            var expr = new FunctionCall(SizeFunction.Value,
                new Literal(new float[] { 1, 2 }.ToVector()));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(2, result.ToFloat());
        }

        [Test]
        public void SizeFunctionMatrixYieldsRowAndColumnCounts()
        {
            // given
            var expr = new FunctionCall(SizeFunction.Value,
                new Literal(
                    new Matrix(new float[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 }
                    })));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
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
            var expr = new FunctionCall(SizeFunction.Value,
                new Literal("abc".ToStringValue()));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(3, result.ToFloat());
        }
    }
}
