
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
    public class EvalTest
    {
        [Test]
        public void EvalYieldsTheIndicatedComponent()
        {
            // given
            var expr = new Literal(
                new Vector(new float[] {1, 2, 3}));
            var indexes = new Expression[] {new Literal(1)};
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.Eval(null);
            // then
            Assert.IsFalse(result.IsVector(null));
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(2, result.ToFloat());
        }

        [Test]
        public void VectorWithIndexYieldsComponent()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(
                    new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(1) });
            // when
            var result = expr.Eval(null);
            // then
            Assert.IsFalse(result.IsVector(null));
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(2, result.ToFloat());
        }

        [Test]
        public void MatrixWithIndexesYieldsComponent()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(
                    new Matrix(
                        new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(1), new Literal(1) });
            // when
            var result = expr.Eval(null);
            // then
            Assert.IsFalse(result.IsVector(null));
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(4, result.ToFloat());
        }

        [Test]
        public void NumberThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(1),
                new Expression[] { new Literal(1) });
            // expect
            var ex = Assert.Throws<OperandException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual(
                "Unable to get components from expression, " +
                "or the expression does not have components",
                ex.Message);
        }

        [Test]
        public void VectorWithTooManyIndexesThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(1), new Literal(1) });
            // when
            var ex = Assert.Throws<OperandException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual(
                "Wrong number of indexes for the expression",
                ex.Message);
        }

        [Test]
        public void MatrixWithTooFewIndexesThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Matrix(
                    new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(1) });
            // when
            var ex = Assert.Throws<OperandException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual(
                "Wrong number of indexes for the expression",
                ex.Message);
        }

        [Test]
        public void VectorAsIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[]
                {
                    new Literal(
                        new Vector(new float[] { 4, 5, 6 }))
                });
            var ex = Assert.Throws<IndexException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual("Indexes must be scalar", ex.Message);
        }

        [Test]
        public void MatrixAsIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[]
                {
                    new Literal(
                        new Matrix(new float[,] { { 1, 2 }, { 3, 4 } }))
                });
            var ex = Assert.Throws<IndexException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual("Indexes must be scalar", ex.Message);
        }

        [Test]
        public void StringAsIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal("abc".ToStringValue()) });
            var ex = Assert.Throws<IndexException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual("Indexes must be scalar", ex.Message);
        }

        [Test]
        public void NegativeIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(-1) });
            // expect
            var ex = Assert.Throws<IndexException>(
                () => expr.Eval(null));
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
            var expr = new ComponentAccess(
                new Literal(
                    new MockMathObject(
                        isScalar: false,
                        tensorRank: 3,
                        isConcrete: true)),
                new Expression[]
                {
                    new Literal(1),
                    new Literal(2),
                    new Literal(3)
                });
            // expect
            var ex = Assert.Throws<OperandException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual(
                "Unable to get components from expression, " +
                "or the expression does not have components",
                ex.Message);
        }

        [Test]
        public void StringWithIndexYieldsComponent()
        {
            // given
            var expr = new ComponentAccess(
                new Literal("abc".ToStringValue()),
                new Expression[] { new Literal(1) });
            // when
            var result = expr.Eval(null);
            // then
            Assert.IsFalse(result.IsVector(null));
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsString(null));
            Assert.AreEqual("b", result.ToStringValue().Value);
        }

        [Test]
        public void StringWithTooManyIndexesThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal("abc".ToStringValue()),
                new Expression[] { new Literal(1), new Literal(1) });
            // when
            var ex = Assert.Throws<OperandException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual(
                "Wrong number of indexes for the expression",
                ex.Message);
        }

        [Test]
        public void VectorWithTooLargeAnIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(3) });
            // when
            var ex = Assert.Throws<IndexException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual(
                "Index exceeds the size of the vector",
                ex.Message);
        }

        [Test]
        // [Ignore("Can't check index against string length yet")]
        public void StringWithTooLargeAnIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal("abc".ToStringValue()),
                new Expression[] { new Literal(3) });
            // when
            var ex = Assert.Throws<IndexException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual(
                "Index exceeds the size of the string",
                ex.Message);
        }

        [Test]
        public void MatrixWithTooLargeRowIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(2), new Literal(0) });
            // when
            var ex = Assert.Throws<IndexException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual(
                "Index exceeds number of rows of the matrix",
                ex.Message);
        }

        [Test]
        public void MatrixWithTooLargeColumnIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(0), new Literal(2) });
            // when
            var ex = Assert.Throws<IndexException>(
                () => expr.Eval(null));
            // and
            Assert.AreEqual(
                "Index exceeds number of columns of the matrix",
                ex.Message);
        }
    }
}
