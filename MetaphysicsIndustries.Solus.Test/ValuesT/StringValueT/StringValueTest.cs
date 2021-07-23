
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
            Assert.IsFalse(result.IsScalar);
            Assert.IsFalse(result.IsVector);
            Assert.IsFalse(result.IsMatrix);
            Assert.AreEqual(0, result.TensorRank);
            Assert.IsTrue(result.IsString);
            Assert.AreEqual(0, result.GetDimension(0));
            Assert.AreEqual(new[] {0}, result.GetDimensions());
        }

        [Test]
        public void CreateWithArgYieldsThatValue()
        {
            // when
            var result = new StringValue("abc");
            // then
            Assert.AreEqual("abc", result.Value);
            Assert.AreEqual(3, result.GetDimension(0));
            Assert.AreEqual(new[] {3}, result.GetDimensions());
        }

        [Test]
        public void GetDimensionNegativeIndexThrows()
        {
            // given
            var value = new StringValue("abc");
            // expect
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => value.GetDimension(-1));
            // and
            Assert.AreEqual("index", ex.ParamName);
            Assert.AreEqual("Index must not be negative\n" +
                            "Parameter name: index",
                ex.Message);
        }

        [Test]
        public void GetDimensionIndexTooLargeThrows()
        {
            // given
            var value = new StringValue("abc");
            // expect
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => value.GetDimension(1));
            // and
            Assert.AreEqual("index", ex.ParamName);
            Assert.AreEqual("Strings only have a single dimension\n" +
                            "Parameter name: index",
                ex.Message);
        }

        [Test]
        public void TestToString()
        {
            // given
            var value = new StringValue("abc");
            // when
            var result = value.ToString();
            // then
            Assert.AreEqual("\"abc\"", result);
        }
    }
}
