using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
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
                    new MockExpression(isResultScalarF: e => false),
                    new MockExpression(isResultScalarF: e => true),
                    new MockExpression(isResultScalarF: e => false)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.IsResultScalar(env);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsResultScalarDelegatesToComponent2()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(isResultScalarF: e => true),
                    new MockExpression(isResultScalarF: e => false),
                    new MockExpression(isResultScalarF: e => true)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.IsResultScalar(env);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsResultVectorDelegatesToComponent1()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(isResultVectorF: e => false),
                    new MockExpression(isResultVectorF: e => true),
                    new MockExpression(isResultVectorF: e => false)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.IsResultVector(env);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsResultVectorDelegatesToComponent2()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(isResultVectorF: e => true),
                    new MockExpression(isResultVectorF: e => false),
                    new MockExpression(isResultVectorF: e => true)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.IsResultVector(env);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsResultMatrixDelegatesToComponent1()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(isResultMatrixF: e => false),
                    new MockExpression(isResultMatrixF: e => true),
                    new MockExpression(isResultMatrixF: e => false)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.IsResultMatrix(env);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsResultMatrixDelegatesToComponent2()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(isResultMatrixF: e => true),
                    new MockExpression(isResultMatrixF: e => false),
                    new MockExpression(isResultMatrixF: e => true)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.IsResultMatrix(env);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void IsResultStringDelegatesToComponent1()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(isResultStringF: e => false),
                    new MockExpression(isResultStringF: e => true),
                    new MockExpression(isResultStringF: e => false)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.IsResultString(env);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void IsResultStringDelegatesToComponent2()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(isResultStringF: e => true),
                    new MockExpression(isResultStringF: e => false),
                    new MockExpression(isResultStringF: e => true)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.IsResultString(env);
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void GetResultTensorRankDelegatesToComponent1()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(getResultTensorRankF: e => 1),
                    new MockExpression(getResultTensorRankF: e => 2),
                    new MockExpression(getResultTensorRankF: e => 1)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultTensorRank(env);
            // then
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetResultTensorRankDelegatesToComponent2()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(getResultTensorRankF: e => 2),
                    new MockExpression(getResultTensorRankF: e => 1),
                    new MockExpression(getResultTensorRankF: e => 2)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultTensorRank(env);
            // then
            Assert.AreEqual(1, result);
        }

        [Test]
        public void GetResultDimensionDelegatesToComponent()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(getResultDimensionF: (e, i) => 1),
                    new MockExpression(getResultDimensionF: (e, i) => 2),
                    new MockExpression(getResultDimensionF: (e, i) => 1)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultDimension(env, 0);
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
                    new MockExpression(getResultDimensionsF: e => one),
                    new MockExpression(getResultDimensionsF: e => two),
                    new MockExpression(getResultDimensionsF: e => one)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultDimensions(env);
            // then
            Assert.AreEqual(new[] { 2, 2, 2 }, result);
        }

        [Test]
        public void GetResultVectorLengthDelegatesToComponent()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(getResultVectorLengthF: e => 1),
                    new MockExpression(getResultVectorLengthF: e => 2),
                    new MockExpression(getResultVectorLengthF: e => 3)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultVectorLength(env);
            // then
            Assert.AreEqual(2, result);
        }

        [Test]
        public void GetResultStringLengthDelegatesToComponent()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new MockExpression(getResultStringLengthF: e => 1),
                    new MockExpression(getResultStringLengthF: e => 2),
                    new MockExpression(getResultStringLengthF: e => 3)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultStringLength(env);
            // then
            Assert.AreEqual(2, result);
        }


        [Test]
        public void DelegatesToMatrixComponent()
        {
            // given
            var expr = new ComponentAccess(
                new MatrixExpression(2, 2,
                    new MockExpression(isResultScalarF: e => false),
                    new MockExpression(isResultScalarF: e => true),
                    new MockExpression(isResultScalarF: e => false),
                    new MockExpression(isResultScalarF: e => false)),
                new[] { new Literal(0), new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.IsResultScalar(env);
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void DelegatesToStringComponent()
        {
            // given
            var expr = new ComponentAccess(
                new MockExpression(
                    isResultScalarF: e => false,
                    isResultVectorF: e => false,
                    isResultMatrixF: e => false,
                    getResultTensorRankF: e => 0,
                    isResultStringF: e => true,
                    getResultStringLengthF: e => 3),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.IsResultString(env);
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
