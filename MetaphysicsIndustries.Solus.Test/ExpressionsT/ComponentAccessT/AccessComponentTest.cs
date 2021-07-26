
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ComponentAccessT
{
    [TestFixture]
    public class AccessComponentTest
    {
        [Test]
        public void VectorWithIndexYieldsComponent()
        {
            // given
            var expr = new Vector(new float[] {1, 2, 3});
            var indexes = new IMathObject[] {1.ToNumber()};
            // when
            var result = ComponentAccess.AccessComponent(expr, indexes);
            // then
            Assert.IsFalse(result.IsVector);
            Assert.IsTrue(result.IsScalar);
            Assert.AreEqual(2, result.ToFloat());
        }

        [Test]
        public void MatrixWithIndexesYieldsComponent()
        {
            // given
            var expr = new Matrix(new float[,] {{1, 2}, {3, 4}});
            var indexes = new IMathObject[] {1.ToNumber(), 1.ToNumber()};
            // when
            var result = ComponentAccess.AccessComponent(expr, indexes);
            // then
            Assert.IsFalse(result.IsVector);
            Assert.IsTrue(result.IsScalar);
            Assert.AreEqual(4, result.ToFloat());
        }

        [Test]
        public void NumberThrows()
        {
            // given
            var expr = 1.ToNumber();
            var indexes = new IMathObject[] {1.ToNumber()};
            // expect
            var ex = Assert.Throws<OperandException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual("Scalars do not have components",
                ex.Message);
        }

        [Test]
        public void VectorWithTooManyIndexesThrows()
        {
            // given
            var expr = new Vector(new float[] {1, 2, 3});
            var indexes = new IMathObject[] {1.ToNumber(), 1.ToNumber()};
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
            var expr = new Matrix(new float[,] {{1, 2}, {3, 4}});
            var indexes = new IMathObject[] {1.ToNumber()};
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
            var expr = new Vector(new float[] {1, 2, 3});
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
            var expr = new Vector(new float[] {1, 2, 3});
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
            var expr = new Vector(new float[] {1, 2, 3});
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
            var expr = new Vector(new float[] {1, 2, 3});
            var indexes = new IMathObject[] {(-1).ToNumber()};
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
            var expr = new MockMathObject(tensorRank: 3);
            var indexes = new IMathObject[]
            {
                1.ToNumber(),
                2.ToNumber(),
                3.ToNumber()
            };
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
        public void StringWithIndexYieldsComponent()
        {
            // given
            var expr = "abc".ToStringValue();
            var indexes = new IMathObject[] {1.ToNumber()};
            // when
            var result = ComponentAccess.AccessComponent(expr, indexes);
            // then
            Assert.IsFalse(result.IsVector);
            Assert.IsFalse(result.IsScalar);
            Assert.IsTrue(result.IsString);
            Assert.AreEqual("b", result.ToStringValue().Value);
        }

        [Test]
        public void StringWithTooManyIndexesThrows()
        {
            // given
            var expr = "abc".ToStringValue();
            var indexes = new IMathObject[] {1.ToNumber(), 1.ToNumber()};
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
        public void VectorWithTooLargeAnIndexThrows()
        {
            // given
            var expr = new Vector(new float[] {1, 2, 3});
            var indexes = new IMathObject[] {3.ToNumber()};
            // when
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual(
                "Index exceeds the size of the vector",
                ex.Message);
        }

        [Test]
        public void StringWithTooLargeAnIndexThrows()
        {
            // given
            var expr = "abc".ToStringValue();
            var indexes = new IMathObject[] {3.ToNumber()};
            // when
            var ex = Assert.Throws<IndexException>(
                () => ComponentAccess.AccessComponent(expr, indexes));
            // and
            Assert.AreEqual(
                "Index exceeds the size of the string",
                ex.Message);
        }

        [Test]
        public void MatrixWithTooLargeRowIndexThrows()
        {
            // given
            var expr = new Matrix(new float[,] {{1, 2}, {3, 4}});
            var indexes = new IMathObject[] {2.ToNumber(), 0.ToNumber()};
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
            var expr = new Matrix(new float[,] {{1, 2}, {3, 4}});
            var indexes = new IMathObject[] {0.ToNumber(), 2.ToNumber()};
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
