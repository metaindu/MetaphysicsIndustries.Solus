
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

using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ValuesT.IntervalT
{
    [TestFixture]
    public class ContainsTest
    {
        [Test]
        public void TypicalValueYields1()
        {
            // given
            var interval = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsTrue(interval.Contains(1.5f));
            // and
            Assert.IsFalse(interval.Contains(0));
            Assert.IsFalse(interval.Contains(3));
        }

        [Test]
        public void TypicalValueYields2()
        {
            // given
            var interval = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsTrue(interval.Contains(1.5f));
            // and
            Assert.IsFalse(interval.Contains(0));
            Assert.IsFalse(interval.Contains(3));
        }

        [Test]
        public void TypicalValueYields3()
        {
            // given
            var interval = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsTrue(interval.Contains(1.5f));
            // and
            Assert.IsFalse(interval.Contains(0));
            Assert.IsFalse(interval.Contains(3));
        }

        [Test]
        public void TypicalValueYields4()
        {
            // given
            var interval = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsTrue(interval.Contains(1.5f));
            // and
            Assert.IsFalse(interval.Contains(0));
            Assert.IsFalse(interval.Contains(3));
        }

        [Test]
        public void BoundaryOnClosedLowerYieldsTrue()
        {
            // given
            var interval = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsTrue(interval.Contains(1));
        }

        [Test]
        public void BoundaryOnOpenLowerYieldsFalse()
        {
            // given
            var interval = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsFalse(interval.Contains(1));
        }

        [Test]
        public void BoundaryOnClosedUpperYieldsTrue()
        {
            // given
            var interval = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsTrue(interval.Contains(2));
        }

        [Test]
        public void BoundaryOnOpenUpperYieldsTrue()
        {
            // given
            var interval = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsFalse(interval.Contains(2));
        }

        [Test]
        public void OutsideValuesOnDegenerateClosedClosedYieldsFalse()
        {
            // given
            var interval = new Interval(1, false, 1, false, false);
            // expect
            Assert.IsFalse(interval.Contains(0));
            Assert.IsFalse(interval.Contains(2));
        }

        [Test]
        public void OutsideValuesOnDegenerateOpenClosedYieldsFalse()
        {
            // given
            var interval = new Interval(1, true, 1, false, false);
            // expect
            Assert.IsFalse(interval.Contains(0));
            Assert.IsFalse(interval.Contains(2));
        }

        [Test]
        public void OutsideValuesOnDegenerateClosedOpenYieldsFalse()
        {
            // given
            var interval = new Interval(1, false, 1, true, false);
            // expect
            Assert.IsFalse(interval.Contains(0));
            Assert.IsFalse(interval.Contains(2));
        }

        [Test]
        public void OutsideValuesOnDegenerateOpenOpenYieldsFalse()
        {
            // given
            var interval = new Interval(1, true, 1, true, false);
            // expect
            Assert.IsFalse(interval.Contains(0));
            Assert.IsFalse(interval.Contains(2));
        }

        [Test]
        public void BoundaryOnDegenerateClosedClosedYieldsTrue()
        {
            // given
            var interval = new Interval(1, false, 1, false, false);
            // expect
            Assert.IsTrue(interval.Contains(1));
        }

        [Test]
        public void BoundaryOnDegenerateOpenClosedYieldsTrue()
        {
            // given
            var interval = new Interval(1, true, 1, false, false);
            // expect
            Assert.IsTrue(interval.Contains(1));
        }

        [Test]
        public void BoundaryOnDegenerateClosedOpenYieldsTrue()
        {
            // given
            var interval = new Interval(1, false, 1, true, false);
            // expect
            Assert.IsTrue(interval.Contains(1));
        }

        [Test]
        public void BoundaryOnDegenerateOpenOpenYieldsFalse()
        {
            // given
            var interval = new Interval(1, true, 1, true, false);
            // expect
            Assert.IsFalse(interval.Contains(1));
        }

        [Test]
        public void NonFiniteValuesYieldFalse()
        {
            // given
            var interval = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(interval.Contains(float.NaN));
            Assert.IsFalse(interval.Contains(float.NegativeInfinity));
            Assert.IsFalse(interval.Contains(float.PositiveInfinity));
        }
    }
}
