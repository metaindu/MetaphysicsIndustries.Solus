
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
using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ComponentAccessT
{
    [TestFixture]
    public class AccessComponent2Test
    {
        static VectorExpression vector(params string[] values)
        {
            return new VectorExpression(
                values.Length,
                values.Select(
                    v => (Expression)new VariableAccess(v)).ToArray());
        }

        static MatrixExpression matrix(int columns, params string[] values)
        {
            int rows = values.Length / columns;
            var values2 = new Expression[rows * columns];
            for (var k = 0; k < rows * columns; k++)
                values2[k] = new VariableAccess(values[k]);
            return new MatrixExpression(rows, columns, values2);
        }

        static IMathObject[] mkindexes(params int[] indexes)
        {
            return indexes.Select(i => (IMathObject) i.ToNumber()).ToArray();
        }

        [Test]
        public void VectorWithIndexYieldsComponent()
        {
            // given
            var expr = vector("a", "b", "c");
            var indexes = mkindexes(1);
            // when
            var result = ComponentAccess.AccessComponent(expr, indexes);
            // then
            Assert.IsInstanceOf<VariableAccess>(result);
            Assert.AreEqual("b", ((VariableAccess)result).VariableName);
        }

        [Test]
        public void MatrixWithIndexesYieldsComponent()
        {
            // given
            var expr = matrix(2,
                "a", "b",
                "c", "d");
            var indexes = mkindexes(1,1);
            // when
            var result = ComponentAccess.AccessComponent(expr, indexes);
            // then
            Assert.IsInstanceOf<VariableAccess>(result);
            Assert.AreEqual("d", ((VariableAccess)result).VariableName);
        }

        class MockTensorExpression : TensorExpression
        {
            public MockTensorExpression(int tensorRank) =>
                TensorRank = tensorRank;

            public override int TensorRank { get; }

            public override IMathObject Eval(SolusEnvironment env) =>
                throw new NotImplementedException();
            public override Expression Clone() =>
                throw new NotImplementedException();
            public override void AcceptVisitor(IExpressionVisitor visitor) =>
                throw new NotImplementedException();
            public override IEnumerator<Expression> GetEnumerator() =>
                throw new NotImplementedException();
            public override void ApplyToAll(Modulator mod) =>
                throw new NotImplementedException();
        }

        [Test]
        public void NumberThrows()
        {
            // given
            var expr = new MockTensorExpression(0);
            var indexes = mkindexes(1);
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual("Scalars do not have components",
                ex.Message);
        }

        [Test]
        public void VectorWithTooManyIndexesThrows()
        {
            // given
            var expr = vector("a","b","c");
            var indexes = mkindexes(1, 1);
            // when
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual(
                "Number of indexes doesn't match the number " +
                "required by the expression",
                ex.Message);
        }

        [Test]
        public void MatrixWithTooFewIndexesThrows()
        {
            // given
            var expr = matrix(2,
                "a", "b",
                "c", "d");
            var indexes = mkindexes(1);
            // when
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual(
                "Number of indexes doesn't match the number " +
                "required by the expression",
                ex.Message);
        }

        [Test]
        public void VectorAsIndexThrows()
        {
            // given
            var expr = vector("a","b","c");
            var indexes = new IMathObject[]
            {
                new Vector(new float[] {4, 5, 6})
            };
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual("Indexes must be scalar", ex.Message);
        }

        [Test]
        public void MatrixAsIndexThrows()
        {
            // given
            var expr = vector("a","b","c");
            var indexes = new IMathObject[]
            {
                new Matrix(new float[,] {{1, 2}, {3, 4}})
            };
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual("Indexes must be scalar", ex.Message);
        }

        [Test]
        public void StringAsIndexThrows()
        {
            // given
            var expr = vector("a","b","c");
            var indexes = new IMathObject[] {"abc".ToStringValue()};
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual("Indexes must be scalar", ex.Message);
        }

        [Test]
        public void NegativeIndexThrows()
        {
            // given
            var expr = vector("a","b","c");
            var indexes = mkindexes(-1);
            // expect
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual(
                "Indexes must not be negative",
                ex.Message);
        }

        // TODO: check for index greater than vector dimension
        // TODO: check for index greater than matrix dimension

        [Test]
        public void HigherTensorRankObjectThrows()
        {
            // given
            var expr = new MockTensorExpression(3);
            var indexes = mkindexes(1, 2, 3);
            // expect
            var ex = Assert.Throws<NotImplementedException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual(
                "Component access is not implemented for tensor " +
                "rank greater than 2",
                ex.Message);
        }

        [Test]
        public void VectorWithTooLargeAnIndexThrows()
        {
            // given
            var expr = vector("a","b","c");
            var indexes = mkindexes(3);
            // when
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual(
                "Index exceeds the size of the vector",
                ex.Message);
        }

        [Test]
        public void MatrixWithTooLargeRowIndexThrows()
        {
            // given
            var expr = matrix(2,
                "a", "b",
                "c", "d");
            var indexes = mkindexes(2, 0);
            // when
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual(
                "Index exceeds number of rows of the matrix",
                ex.Message);
        }

        [Test]
        public void MatrixWithTooLargeColumnIndexThrows()
        {
            // given
            var expr = matrix(2,
                "a", "b",
                "c", "d");
            var indexes = mkindexes(0, 2);
            // when
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual(
                "Index exceeds number of columns of the matrix",
                ex.Message);
        }
    }
}
