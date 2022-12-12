
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

namespace MetaphysicsIndustries.Solus.Test.ValuesT.IntervalT
{
    [TestFixture]
    public class EmptyDegenerateTest
    {
        [Test]
        public void EqualBoundsBothClosedIsDegenerateNotEmpty()
        {
            // given
            var i = new Interval(1, false, 1, false, false);
            // expect
            Assert.IsTrue(i.IsDegenerate);
            Assert.IsFalse(i.IsEmpty);
        }

        [Test]
        public void EqualBoundsSomeOpenIsEmptyNotDegenerate2()
        {
            // given
            var i = new Interval(1, true, 1, false, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsTrue(i.IsEmpty);
        }

        [Test]
        public void EqualBoundsSomeOpenIsEmptyNotDegenerate3()
        {
            // given
            var i = new Interval(1, false, 1, true, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsTrue(i.IsEmpty);
        }

        [Test]
        public void EqualBoundsSomeOpenIsEmptyNotDegenerate4()
        {
            // given
            var i = new Interval(1, true, 1, true, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsTrue(i.IsEmpty);
        }

        [Test]
        public void NonEqualBoundsNeitherEmptyNorDegenerate1()
        {
            // given
            var i = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsFalse(i.IsEmpty);
        }

        [Test]
        public void NonEqualBoundsNeitherEmptyNorDegenerate2()
        {
            // given
            var i = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsFalse(i.IsEmpty);
        }

        [Test]
        public void NonEqualBoundsNeitherEmptyNorDegenerate3()
        {
            // given
            var i = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsFalse(i.IsEmpty);
        }

        [Test]
        public void NonEqualBoundsNeitherEmptyNorDegenerate4()
        {
            // given
            var i = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsFalse(i.IsEmpty);
        }
    }
}
