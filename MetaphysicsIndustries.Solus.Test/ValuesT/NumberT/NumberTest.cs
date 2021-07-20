
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
            Assert.AreEqual(0, result.Value);
            Assert.IsTrue(result.IsScalar);
            Assert.IsFalse(result.IsVector);
            Assert.IsFalse(result.IsMatrix);
            Assert.AreEqual(0, result.TensorRank);
            Assert.Throws<InvalidOperationException>(
                () => result.GetDimension(0));
            Assert.Throws<InvalidOperationException>(
                () => result.GetDimensions());
        }

        [Test]
        public void CreateWithArgYieldsThatValue()
        {
            // when
            var result = new Number(123);
            // then
            Assert.AreEqual(123, result.Value);
        }
    }
}
