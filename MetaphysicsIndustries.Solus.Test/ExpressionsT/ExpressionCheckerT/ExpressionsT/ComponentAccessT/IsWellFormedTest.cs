
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    ExpressionsT.ComponentAccessT
{
    [TestFixture]
    public class IsWellFormedTest
    {
        [Test]
        public void VectorWithValidIndexDoesNotThrow()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(
                    new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(1) });
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellFormed(expr));
        }

        [Test]
        public void MatrixWithValidIndexesDoesNotThrow()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(
                    new Matrix(
                        new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(1), new Literal(1) });
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellFormed(expr));
        }

        [Test]
        public void NumberThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(1),
                new Expression[] { new Literal(1) });
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(ex.Message,
                Is.EqualTo(
                    "The expression should result in a type with " +
                    "components"));
        }

        [Test]
        public void VectorWithTooManyIndexesThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(1), new Literal(1) });
            var ec = new ExpressionChecker();
            // when
            var ex = Assert.Throws<TypeException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Wrong number of indexes for the expression"));
        }

        [Test]
        public void MatrixWithTooFewIndexesThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Matrix(
                    new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(1) });
            var ec = new ExpressionChecker();
            // when
            var ex = Assert.Throws<TypeException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Wrong number of indexes for the expression"));
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
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(ex.Message,
                Is.EqualTo("Index must be a real number: index 0"));
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
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(ex.Message,
                Is.EqualTo("Index must be a real number: index 0"));
        }

        [Test]
        public void StringAsIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal("abc".ToStringValue()) });
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(ex.Message,
                Is.EqualTo("Index must be a real number: index 0"));
        }

        [Test]
        public void NegativeIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(-1) });
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Indexes must not be negative"));
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
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<NotImplementedException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Not implemented for high-rank tensors"));
        }

        [Test]
        public void StringWithIndexDoesNotThrow()
        {
            // given
            var expr = new ComponentAccess(
                new Literal("abc".ToStringValue()),
                new Expression[] { new Literal(1) });
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellFormed(expr));
        }

        [Test]
        public void StringWithTooManyIndexesThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal("abc".ToStringValue()),
                new Expression[] { new Literal(1), new Literal(1) });
            var ec = new ExpressionChecker();
            // when
            var ex = Assert.Throws<TypeException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Wrong number of indexes for the expression"));
        }

        [Test]
        public void VectorWithTooLargeAnIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(3) });
            var ec = new ExpressionChecker();
            // when
            var ex = Assert.Throws<IndexException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Index exceeds the size of the vector"));
        }

        [Test]
        // [Ignore("Can't check index against string length yet")]
        public void StringWithTooLargeAnIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal("abc".ToStringValue()),
                new Expression[] { new Literal(3) });
            var ec = new ExpressionChecker();
            // when
            var ex = Assert.Throws<IndexException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Index exceeds the size of the string"));
        }

        [Test]
        public void MatrixWithTooLargeRowIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(2), new Literal(0) });
            var ec = new ExpressionChecker();
            // when
            var ex = Assert.Throws<IndexException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Index exceeds number of rows of the matrix"));
        }

        [Test]
        public void MatrixWithTooLargeColumnIndexThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(0), new Literal(2) });
            var ec = new ExpressionChecker();
            // when
            var ex = Assert.Throws<IndexException>(
                () => ec.IsWellFormed(expr));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Index exceeds number of columns of the matrix"));
        }
    }
}
