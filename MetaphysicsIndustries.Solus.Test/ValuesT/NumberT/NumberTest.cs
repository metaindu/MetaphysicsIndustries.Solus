
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

namespace MetaphysicsIndustries.Solus.Test.ValuesT.NumberT
{
    [TestFixture]
    public class NumberTest
    {
        [Test]
        public void CreateWithoutArgYieldsDefaultValues()
        {
            // when
            var result = new Number();
            // then
            Assert.That(result.Value, Is.EqualTo(0));
            Assert.IsTrue(result.IsScalar(null));
            Assert.IsFalse(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.That(result.GetTensorRank(null), Is.EqualTo(0));
            Assert.IsFalse(result.IsString(null));
            Assert.IsNull(result.GetDimension(null, 0));
            Assert.IsNull(result.GetDimensions(null));
        }

        [Test]
        public void CreateWithArgYieldsThatValue()
        {
            // when
            var result = new Number(123);
            // then
            Assert.That(result.Value, Is.EqualTo(123));
        }

        [Test]
        public void TestToString()
        {
            // given
            var value = (123.45f).ToNumber();
            // when
            var result = value.ToString();
            // then
            Assert.That(result, Is.EqualTo("123.45"));
        }

        [Test]
        public void PiYieldsPiSymbol()
        {
            // given
            var value = ((float) Math.PI).ToNumber();
            // when
            var result = value.ToString();
            // then
            Assert.That(result, Is.EqualTo("π"));
        }

        [Test]
        public void EYieldsESymbol()
        {
            // given
            var value = ((float) Math.E).ToNumber();
            // when
            var result = value.ToString();
            // then
            Assert.That(result, Is.EqualTo("e"));
        }
    }
}
