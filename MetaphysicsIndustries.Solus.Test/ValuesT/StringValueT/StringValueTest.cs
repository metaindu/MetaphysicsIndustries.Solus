
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;


namespace MetaphysicsIndustries.Solus.Test.ValuesT.StringValueT
{
    [TestFixture]
    public class StringValueTest
    {
        [Test]
        public void CreateWithoutArgYieldsDefaultValues()
        {
            // when
            var result = new StringValue();
            // then
            Assert.IsNull(result.Value);
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsFalse(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.That(result.GetTensorRank(null), Is.EqualTo(0));
            Assert.IsTrue(result.IsString(null));
            Assert.That(result.GetDimension(null, 0), Is.EqualTo(0));
            Assert.That(result.GetDimensions(null), Is.EqualTo(new[] {0}));
        }

        [Test]
        public void CreateWithArgYieldsThatValue()
        {
            // when
            var result = new StringValue("abc");
            // then
            Assert.That(result.Value, Is.EqualTo("abc"));
            Assert.That(result.GetDimension(null, 0), Is.EqualTo(3));
            Assert.That(result.GetDimensions(null), Is.EqualTo(new[] {3}));
        }

        [Test]
        public void GetDimensionNegativeIndexThrows()
        {
            // given
            var value = new StringValue("abc");
            // expect
            Assert.IsNull(value.GetDimension(null, -1));
        }

        [Test]
        public void GetDimensionIndexTooLargeYieldsNull()
        {
            // given
            var value = new StringValue("abc");
            // expect
            Assert.IsNull(value.GetDimension(null, 1));
        }

        [Test]
        public void TestToString()
        {
            // given
            var value = new StringValue("abc");
            // when
            var result = value.ToString();
            // then
            Assert.That(result, Is.EqualTo("\"abc\""));
        }
    }
}
