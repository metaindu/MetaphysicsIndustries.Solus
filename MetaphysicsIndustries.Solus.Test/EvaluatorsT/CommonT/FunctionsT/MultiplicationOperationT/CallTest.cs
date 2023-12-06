
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.MultiplicationOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalMultiplicationOperationTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, -2, 0)]
        [TestCase(0, 0.5f, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 3, 3)]
        [TestCase(1, 4, 4)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(2, 3, 6)]
        [TestCase(2, 4, 8)]
        [TestCase(1, -2, -2)]
        [TestCase(-1, 2, -2)]
        [TestCase(-1, -2, 2)]
        [TestCase(1.5f, 1.5f, 2.25f)]
        public void MultiplicationOperationValuesYieldsValue(
            float a, float b, float expected)
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.That(result.ToFloat(),
                Is.EqualTo(expected).Within(0.000001f));
        }

        [Test]
        public void MultiplicationOperationCallWithNoArgsThrows()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = Array.Empty<Expression>();
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(() =>
                eval.Eval(expr, null));
            // and
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Does.StartWith("Wrong number of arguments"));
        }

        [Test]
        public void MultiplicationOperationCallWithOneArgThrows()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[] { new Literal(2) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(() =>
                eval.Eval(expr, null));
            // and
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message, Does.StartWith("Wrong number of arguments"));
        }

        [Test]
        public void MultiplicationOperationCallWithTwoArgsYieldsProduct()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[] { new Literal(2), new Literal(3) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.ToNumber().Value, Is.EqualTo(6));
        }

        [Test]
        public void MultiplicationOperationCallWithThreeArgsYieldsProduct()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(2),
                new Literal(3),
                new Literal(5)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.ToNumber().Value, Is.EqualTo(30));
        }

        [Test]
        public void ScalarAndVectorYieldsVector()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(2),
                new Literal(Vector2.One),
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result, Is.EqualTo(new Vector2(2, 2)));
        }

        [Test]
        public void VectorAndScalarYieldsVector()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(Vector2.One),
                new Literal(2),
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result, Is.EqualTo(new Vector(new float[] { 2, 2 })));
        }

        [Test]
        public void MatrixAndScalarYieldsMatrix()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(Matrix.Identity3),
                new Literal(2),
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result,
                Is.EqualTo(new Matrix(
                    new float[,] { { 2, 0, 0 }, { 0, 2, 0 }, { 0, 0, 2 } })));
        }

        [Test]
        public void ScalarAndMatrixYieldsMatrix()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(2),
                new Literal(Matrix.Identity3),
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result,
                Is.EqualTo(
                    Matrix.M33(
                        2, 0, 0,
                        0, 2, 0,
                        0, 0, 2)));
        }

        [Test]
        public void MatrixAndVectorYieldsVector()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(
                    Matrix.M33(
                        2, 0, 0,
                        0, 3, 0,
                        0, 0, 5)),
                new Literal(new Vector3(7, 11, 13))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result, Is.EqualTo(new Vector3(14, 33, 65)));
        }

        [Test]
        public void MatrixAndMatrixYieldsMatrix()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(
                    Matrix.M22(
                        2, 3,
                        5, 7)),
                new Literal(
                    Matrix.M22(
                        11, 13,
                        17, 19)),
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result,
                Is.EqualTo(
                    Matrix.M22(
                        73, 83,
                        174, 198)));
        }

        // VectorAndMatrix ?
        // VectorAndVector ?

        [Test]
        public void ScalarAndVectorAndScalarYieldsVector()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(2),
                new Literal(new Vector3(3, 5, 7)),
                new Literal(11),
            };

            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result, Is.EqualTo(new Vector3(66, 110, 154)));
        }

        [Test]
        public void ScalarAndMatrixAndScalarYieldsMatrix()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(2),
                new Literal(
                    Matrix.M22(
                        3, 5,
                        7, 11)),
                new Literal(13),
            };

            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result,
                Is.EqualTo(
                    Matrix.M22(
                        78, 130,
                        182, 286)));
        }

        [Test]
        public void MatrixAndScalarAndMatrixYieldsMatrix()
        {
            // given
            var f = MultiplicationOperation.Value;
            var args = new Expression[]
            {
                new Literal(
                    Matrix.M22(
                        2, 3,
                        5, 7)),
                new Literal(23),
                new Literal(
                    Matrix.M22(
                        11, 13,
                        17, 19)),
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result,
                Is.EqualTo(
                    Matrix.M22(
                        1679, 1909,
                        4002, 4554)));
        }
    }
}
