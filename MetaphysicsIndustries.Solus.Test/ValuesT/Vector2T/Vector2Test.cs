
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

using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ValuesT.Vector2T
{
    [TestFixture]
    public class Vector2Test
    {
        [Test]
        public void CreateSetsElements()
        {
            // when
            var result = new Vector2(1, 2);
            // then
            Assert.That(result.X, Is.EqualTo(1));
            Assert.That(result.Y, Is.EqualTo(2));
            // and
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.That(result.GetTensorRank(null), Is.EqualTo(1));
            Assert.IsFalse(result.IsString(null));
            Assert.IsNull(result.GetDimension(null, -1));
            Assert.That(result.GetDimension(null, 0), Is.EqualTo(2));
            Assert.IsNull(result.GetDimension(null, 1));
            Assert.That(result.GetDimensions(null),
                Is.EqualTo(new int[1] { 2 }));
            Assert.That(result.GetVectorLength(null), Is.EqualTo(2));
            Assert.IsFalse(result.IsInterval(null));
            Assert.IsFalse(result.IsFunction(null));
            Assert.IsFalse(result.IsExpression(null));
            Assert.IsTrue(result.IsConcrete);
            Assert.That(result.DocString, Is.EqualTo(""));
        }

        [Test]
        public void NegateYieldsOppositeValues()
        {
            // given
            var v = new Vector2(1, 2);
            // when
            var result = -v;
            // then
            Assert.That(result.X, Is.EqualTo(-1));
            Assert.That(result.Y, Is.EqualTo(-2));
        }

        [Test]
        public void MinusYieldsDifference()
        {
            // given
            var v = new Vector2(1, 2);
            var u = new Vector2(4, 6);
            // when
            var result = u - v;
            // then
            Assert.That(result.X, Is.EqualTo(3));
            Assert.That(result.Y, Is.EqualTo(4));
        }

        [Test]
        public void PlusYieldsSum()
        {
            // given
            var v = new Vector2(1, 2);
            var u = new Vector2(4, 6);
            // when
            var result = u + v;
            // then
            Assert.That(result.X, Is.EqualTo(5));
            Assert.That(result.Y, Is.EqualTo(8));
        }

        [Test]
        public void ScalarMultiplicationYieldsScaledVector1()
        {
            // given
            var v = new Vector2(1, 2);
            // when
            var result = v * 2;
            // then
            Assert.That(result.X, Is.EqualTo(2));
            Assert.That(result.Y, Is.EqualTo(4));
        }

        [Test]
        public void ScalarMultiplicationYieldsScaledVector2()
        {
            // given
            var v = new Vector2(1, 2);
            // when
            var result = 2 * v;
            // then
            Assert.That(result.X, Is.EqualTo(2));
            Assert.That(result.Y, Is.EqualTo(4));
        }

        [Test]
        public void ScalarDivisionYieldsScaledVector()
        {
            // given
            var v = new Vector2(1, 2);
            // when
            var result = v / 2;
            // then
            Assert.That(result.X, Is.EqualTo(0.5f));
            Assert.That(result.Y, Is.EqualTo(1));
        }

        [Test]
        public void SameVectorsEqualsYieldsTrue()
        {
            // given
            var v = new Vector2(1, 2);
            var u = new Vector2(1, 2);
            // when
            var result = u == v;
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void DifferentVectorEqualsYieldsFalse()
        {
            // given
            var v = new Vector2(1, 2);
            // expect
            Assert.IsFalse(v == new Vector2(1, 3));
            Assert.IsFalse(v == new Vector2(2, 2));
            Assert.IsFalse(v == new Vector2(2, 3));
        }

        [Test]
        public void SameVectorsNotEqualsYieldsFalse()
        {
            // given
            var v = new Vector2(1, 2);
            var u = new Vector2(1, 2);
            // when
            var result = u != v;
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void DifferentVectorNotEqualsYieldsTrue()
        {
            // given
            var v = new Vector2(1, 2);
            // expect
            Assert.IsTrue(v != new Vector2(1, 3));
            Assert.IsTrue(v != new Vector2(2, 2));
            Assert.IsTrue(v != new Vector2(2, 3));
        }
    }
}