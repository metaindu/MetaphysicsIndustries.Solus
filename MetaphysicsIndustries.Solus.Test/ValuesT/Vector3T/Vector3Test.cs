
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

namespace MetaphysicsIndustries.Solus.Test.ValuesT.Vector3T
{
    [TestFixture]
    public class Vector3Test
    {
        [Test]
        public void CreateSetsElements()
        {
            // when
            var result = new Vector3(1, 2, 3);
            // then
            Assert.AreEqual(1, result.X);
            Assert.AreEqual(2, result.Y);
            Assert.AreEqual(3, result.Z);
            // and
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.AreEqual(1, result.GetTensorRank(null));
            Assert.IsFalse(result.IsString(null));
            Assert.IsNull(result.GetDimension(null, -1));
            Assert.AreEqual(3, result.GetDimension(null, 0));
            Assert.IsNull(result.GetDimension(null, 1));
            Assert.AreEqual(new int[1] { 3 }, result.GetDimensions(null));
            Assert.AreEqual(3, result.GetVectorLength(null));
            Assert.IsFalse(result.IsInterval(null));
            Assert.IsFalse(result.IsFunction(null));
            Assert.IsFalse(result.IsExpression(null));
            Assert.IsTrue(result.IsConcrete);
            Assert.AreEqual("", result.DocString);
        }

        [Test]
        public void NegateYieldsOppositeValues()
        {
            // given
            var v = new Vector3(1, 2, 3);
            // when
            var result = -v;
            // then
            Assert.AreEqual(-1, result.X);
            Assert.AreEqual(-2, result.Y);
            Assert.AreEqual(-3, result.Z);
        }

        [Test]
        public void MinusYieldsDifference()
        {
            // given
            var v = new Vector3(1, 2, 3);
            var u = new Vector3(4, 6, 8);
            // when
            var result = u - v;
            // then
            Assert.AreEqual(3, result.X);
            Assert.AreEqual(4, result.Y);
            Assert.AreEqual(5, result.Z);
        }

        [Test]
        public void PlusYieldsSum()
        {
            // given
            var v = new Vector3(1, 2, 3);
            var u = new Vector3(4, 6, 8);
            // when
            var result = u + v;
            // then
            Assert.AreEqual(5, result.X);
            Assert.AreEqual(8, result.Y);
            Assert.AreEqual(11, result.Z);
        }

        [Test]
        public void ScalarMultiplicationYieldsScaledVector1()
        {
            // given
            var v = new Vector3(1, 2, 3);
            // when
            var result = v * 2;
            // then
            Assert.AreEqual(2, result.X);
            Assert.AreEqual(4, result.Y);
            Assert.AreEqual(6, result.Z);
        }

        [Test]
        public void ScalarMultiplicationYieldsScaledVector2()
        {
            // given
            var v = new Vector3(1, 2, 3);
            // when
            var result = 2 * v;
            // then
            Assert.AreEqual(2, result.X);
            Assert.AreEqual(4, result.Y);
            Assert.AreEqual(6, result.Z);
        }

        [Test]
        public void ScalarDivisionYieldsScaledVector()
        {
            // given
            var v = new Vector3(1, 2, 3);
            // when
            var result = v / 2;
            // then
            Assert.AreEqual(0.5f, result.X);
            Assert.AreEqual(1, result.Y);
            Assert.AreEqual(1.5f, result.Z);
        }

        [Test]
        public void SameVectorsEqualsYieldsTrue()
        {
            // given
            var v = new Vector3(1, 2, 3);
            var u = new Vector3(1, 2, 3);
            // when
            var result = u == v;
            // then
            Assert.IsTrue(result);
        }

        [Test]
        public void DifferentVectorEqualsYieldsFalse()
        {
            // given
            var v = new Vector3(1, 2, 3);
            // expect
            Assert.IsFalse(v == new Vector3(1, 2, 2));
            Assert.IsFalse(v == new Vector3(1, 4, 3));
            Assert.IsFalse(v == new Vector3(2, 2, 3));
        }

        [Test]
        public void SameVectorsNotEqualsYieldsFalse()
        {
            // given
            var v = new Vector3(1, 2, 3);
            var u = new Vector3(1, 2, 3);
            // when
            var result = u != v;
            // then
            Assert.IsFalse(result);
        }

        [Test]
        public void DifferentVectorNotEqualsYieldsTrue()
        {
            // given
            var v = new Vector3(1, 2, 3);
            // expect
            Assert.IsTrue(v != new Vector3(1, 2, 2));
            Assert.IsTrue(v != new Vector3(1, 4, 3));
            Assert.IsTrue(v != new Vector3(2, 2, 3));
        }
    }
}
