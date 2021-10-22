
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
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ComponentAccessT
{
    [TestFixture]
    public class ResultAttributesTest
    {
        [Test]
        public void IsResultScalarDelegatesToComponent1()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isScalarF: e => false)),
                    new MockExpression(result: new MockEnvMathObject(
                        isScalarF: e => true)),
                    new MockExpression(result: new MockEnvMathObject(
                        isScalarF: e => false))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.IsScalar(env);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsResultScalarDelegatesToComponent2()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isScalarF: e => true)),
                    new MockExpression(result: new MockEnvMathObject(
                        isScalarF: e => false)),
                    new MockExpression(result: new MockEnvMathObject(
                        isScalarF: e => true))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.IsScalar(env);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsResultVectorDelegatesToComponent1()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isVectorF: e => false)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isVectorF: e => true)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isVectorF: e => false))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.IsVector(env);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsResultVectorDelegatesToComponent2()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isVectorF: e => true)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isVectorF: e => false)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isVectorF: e => true))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.IsVector(env);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsResultMatrixDelegatesToComponent1()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isMatrixF: e => false)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isMatrixF: e => true)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isMatrixF: e => false))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.IsMatrix(env);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsResultMatrixDelegatesToComponent2()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isMatrixF: e => true)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isMatrixF: e => false)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isMatrixF: e => true))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.IsMatrix(env);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsResultStringDelegatesToComponent1()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isStringF: e => false)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isStringF: e => true)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isStringF: e => false))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.IsString(env);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsResultStringDelegatesToComponent2()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isStringF: e => true)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isStringF: e => false)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isStringF: e => true))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.IsString(env);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void GetResultTensorRankDelegatesToComponent1()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getTensorRankF: e => 1)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getTensorRankF: e => 2)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getTensorRankF: e => 1))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.GetTensorRank(env);
            // then
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetResultTensorRankDelegatesToComponent2()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getTensorRankF: e => 2)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getTensorRankF: e => 1)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getTensorRankF: e => 2))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.GetTensorRank(env);
            // then
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetResultDimensionDelegatesToComponent()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getDimensionF: (e, i) => 1)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getDimensionF: (e, i) => 2)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getDimensionF: (e, i) => 1))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.GetDimension(env, 0);
            // then
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetResultDimensionsDelegatesToComponent()
        {
            // given
            var one = new[] { 1, 1, 1 };
            var two = new[] { 2, 2, 2 };
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getDimensionsF: e => one)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getDimensionsF: e => two)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getDimensionsF: e => one))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.GetDimensions(env);
            // then
            Assert.AreEqual(new[] { 2, 2, 2 }, result);
        }

        [Test]
        public void GetResultVectorLengthDelegatesToComponent()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getVectorLengthF: e => 1)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getVectorLengthF: e => 2)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getVectorLengthF: e => 3))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.GetVectorLength(env);
            // then
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetResultStringLengthDelegatesToComponent()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getStringLengthF: e => 1)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getStringLengthF: e => 2)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            getStringLengthF: e => 3))),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.GetStringLength(env);
            // then
            Assert.AreEqual(2, result);
        }


        [Test]
        public void DelegatesToMatrixComponent()
        {
            // given
            var expr = new ComponentAccess(
                new MatrixExpression(2, 2,
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isScalarF: e => false)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isScalarF: e => true)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isScalarF: e => false)),
                    new MockExpression(
                        result: new MockEnvMathObject(
                            isScalarF: e => false))),
                new[] { new Literal(0), new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.IsScalar(env);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void DelegatesToStringComponent()
        {
            // given
            var expr = new ComponentAccess(
                new MockExpression(
                    result: new MockEnvMathObject(
                        isScalarF: e => false,
                        isVectorF: e => false,
                        isMatrixF: e => false,
                        getTensorRankF: e => 0,
                        isStringF: e => true,
                        getStringLengthF: e => 3)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.Result.IsString(env);
            // then
            Assert.IsTrue(result);
        }
        // scalar?
        // vector
        // matrix
        // mock tensor?
        // Literal with string value

    }
}
