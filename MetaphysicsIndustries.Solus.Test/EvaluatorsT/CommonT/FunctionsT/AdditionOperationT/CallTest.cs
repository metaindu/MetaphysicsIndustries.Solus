
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

using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.AdditionOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalAdditionOperationTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        public void AdditionOperationCallWithNoArgsThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[] { new Literal(1) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
        }

        [Test]
        public void AdditionOperationCallWithOneArgThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[] { new Literal(1) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
        }

        [Test]
        public void AdditionOperationCallWithTwoArgsYieldsSum()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[] { new Literal(1), new Literal(2) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.ToNumber().Value, Is.EqualTo(3));
        }

        [Test]
        public void AdditionOperationCallWithThreeArgsYieldsSum()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(1),
                new Literal(2),
                new Literal(4)
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.ToNumber().Value, Is.EqualTo(7));
        }

        [Test]
        public void AddTwoVectorsYieldsVector()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Vector3(1, 2, 3)),
                new Literal(new Vector3(4, 5, 6))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.IsIsVector(null));
            var v = result.ToVector();
            Assert.That(v.Length, Is.EqualTo(3));
            Assert.That(v[0].ToNumber().Value, Is.EqualTo(5f));
            Assert.That(v[0].ToNumber().Value, Is.EqualTo(7f));
            Assert.That(v[0].ToNumber().Value, Is.EqualTo(9f));
        }

        [Test]
        public void AddThreeVectorsYieldsVector()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Vector3(1, 2, 3)),
                new Literal(new Vector3(4, 5, 6)),
                new Literal(new Vector3(7, 8, 9))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.IsIsVector(null));
            var v = result.ToVector();
            Assert.That(v.Length, Is.EqualTo(3));
            Assert.That(v[0].ToNumber().Value, Is.EqualTo(12f));
            Assert.That(v[0].ToNumber().Value, Is.EqualTo(15f));
            Assert.That(v[0].ToNumber().Value, Is.EqualTo(18f));
        }

        [Test]
        public void VectorsOfDifferentDimensionThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Vector3(1, 2, 3)),
                new Literal(new Vector2(4, 5))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong argument type at index 1: expected Vector(3) " +
                    "but got Vector(2)"));
        }

        [Test]
        public void AddTwoMatricesYieldsMatrix()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Matrix(new float[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                })),
                new Literal(new Matrix(new float[,]
                {
                    { 5, 6 },
                    { 7, 8 }
                }))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.IsIsMatrix(null));
            var m = result.ToMatrix();
            Assert.That(m,
                Is.EqualTo(
                    new Matrix(new float[,] { { 6, 8 }, { 10, 12 } })));
        }

        [Test]
        public void AddThreeMatricesYieldsMatrix()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Matrix(new float[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                })),
                new Literal(new Matrix(new float[,]
                {
                    { 5, 6 },
                    { 7, 8 }
                })),
                new Literal(new Matrix(new float[,]
                {
                    { 9, 10 },
                    { 11, 12 }
                }))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.That(result.IsIsMatrix(null));
            var m = result.ToMatrix();
            Assert.That(m,
                Is.EqualTo(
                    new Matrix(new float[,]
                    {
                        { 15, 18 },
                        { 21, 24 }
                    })));
        }

        [Test]
        public void MatricesOfDifferentSizeThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Matrix(new float[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                })),
                new Literal(new Matrix(new float[,]
                {
                    { 5, 6 },
                    { 7, 8 },
                    { 9, 10 }
                }))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong argument type at index 1: expected " +
                    "Matrix(2, 2) but got Matrix(3, 2)"));
        }

        [Test]
        public void ScalarPlusVectorThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(123f),
                new Literal(new Vector3(1, 2, 3))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong argument type at index 1: expected Scalar but " +
                    "got Vector(3)"));
        }

        [Test]
        public void VectorPlusScalarThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Vector3(1, 2, 3)),
                new Literal(123f),
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong argument type at index 1: expected Vector(3) " +
                    "but got Scalar"));
        }

        [Test]
        public void ScalarPlusMatrixThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(123f),
                new Literal(new Matrix(new float[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                }))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong argument type at index 1: expected Scalar but " +
                    "got Matrix(2, 2)"));
        }

        [Test]
        public void MatrixPlusScalarThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Matrix(new float[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                })),
                new Literal(123f),
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong argument type at index 1: expected " +
                    "Matrix(2, 2) but got Scalar"));
        }

        [Test]
        public void VectorPlusMatrixThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Vector3(1, 2, 3)),
                new Literal(new Matrix(new float[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                }))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong argument type at index 1: expected Vector(3) " +
                    "but got Matrix(2, 2)"));
        }

        [Test]
        public void MatrixPlusVectorThrows()
        {
            // given
            var f = AdditionOperation.Value;
            var args = new Expression[]
            {
                new Literal(new Matrix(new float[,]
                {
                    { 1, 2 },
                    { 3, 4 }
                })),
                new Literal(new Vector3(1, 2, 3))
            };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex, Is.Not.Null);
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong argument type at index 1: expected " +
                    "Matrix(2, 2) but got Vector(3)"));
        }
    }
}
