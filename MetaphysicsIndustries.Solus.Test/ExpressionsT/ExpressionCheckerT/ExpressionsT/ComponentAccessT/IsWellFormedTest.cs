
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
        public void NumberYieldsTrue()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(1),
                new Expression[] { new Literal(1) });
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        public void VectorWithTooManyIndexesYieldsTrue()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(1), new Literal(1) });
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        public void MatrixWithTooFewIndexesYieldsTrue()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Matrix(
                    new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(1) });
            var ec = new ExpressionChecker();
            // when
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        public void VectorAsIndexYieldsTrue()
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
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        public void MatrixAsIndexYieldsTrue()
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
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        public void StringAsIndexYieldsTrue()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal("abc".ToStringValue()) });
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        public void NegativeIndexYieldsTrue()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(-1) });
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(expr));
        }

        // TODO: check for index greater than vector dimension
        // TODO: check for index greater than matrix dimension

        [Test]
        public void HigherTensorRankObjectYieldsTrue()
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
            Assert.That(ec.IsWellFormed(expr));
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
        public void StringWithTooManyIndexesYieldsTrue()
        {
            // given
            var expr = new ComponentAccess(
                new Literal("abc".ToStringValue()),
                new Expression[] { new Literal(1), new Literal(1) });
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        public void VectorWithTooLargeAnIndexYieldsTrue()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Expression[] { new Literal(3) });
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        // [Ignore("Can't check index against string length yet")]
        public void StringWithTooLargeAnIndexYieldsTrue()
        {
            // given
            var expr = new ComponentAccess(
                new Literal("abc".ToStringValue()),
                new Expression[] { new Literal(3) });
            var ec = new ExpressionChecker();
            // expect
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        public void MatrixWithTooLargeRowIndexYieldsTrue()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(2), new Literal(0) });
            var ec = new ExpressionChecker();
            // when
            Assert.That(ec.IsWellFormed(expr));
        }

        [Test]
        public void MatrixWithTooLargeColumnIndexYieldsTrue()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Matrix(new float[,] { { 1, 2 }, { 3, 4 } })),
                new Expression[] { new Literal(0), new Literal(2) });
            var ec = new ExpressionChecker();
            // when
            Assert.That(ec.IsWellFormed(expr));
        }
    }
}
