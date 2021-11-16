
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

using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ValuesT.IntervalT
{
    [TestFixture]
    public class IntervalTest
    {
        [Test]
        public void CreateYieldsValues1()
        {
            // when
            var result = new Interval(1, false, 2, true, false);
            // then
            Assert.AreEqual(1, result.LowerBound);
            Assert.IsFalse(result.OpenLowerBound);
            Assert.AreEqual(2, result.UpperBound);
            Assert.IsTrue(result.OpenUpperBound);
            Assert.IsFalse(result.IsIntegerInterval);
        }

        [Test]
        public void CreateYieldsValues2()
        {
            // when
            var result = new Interval(3, true, 4, false, true);
            // then
            Assert.AreEqual(3, result.LowerBound);
            Assert.IsTrue(result.OpenLowerBound);
            Assert.AreEqual(4, result.UpperBound);
            Assert.IsFalse(result.OpenUpperBound);
            Assert.IsTrue(result.IsIntegerInterval);
        }

        // TODO: what if LowerBound > UpperBound?
        // TODO: what if LowerBound == UpperBound?

        [Test]
        public void LengthYieldsDifferenceBetweenBounds()
        {
            // given
            var interval = new Interval(1, false, 3, false, false);
            // expect
            Assert.AreEqual(2, interval.Length);
        }

        [Test]
        public void IntegerYieldsIntegerInterval()
        {
            // when
            var result = Interval.Integer(1, 3);
            // then
            Assert.AreEqual(1, result.LowerBound);
            Assert.IsFalse(result.OpenLowerBound);
            Assert.AreEqual(3, result.UpperBound);
            Assert.IsFalse(result.OpenUpperBound);
            Assert.IsTrue(result.IsIntegerInterval);
        }

        [Test]
        public void RoundYieldsIntegerInterval()
        {
            // given
            var interval = new Interval(1.1f, false, 3.1f, false, false);
            // when
            var result = interval.Round();
            // then
            Assert.AreEqual(1, result.LowerBound);
            Assert.IsFalse(result.OpenLowerBound);
            Assert.AreEqual(3, result.UpperBound);
            Assert.IsFalse(result.OpenUpperBound);
            Assert.IsTrue(result.IsIntegerInterval);
        }

        // TODO: CalcDelta 1
        // TODO: CalcDelta 0
        // TODO: CalcDelta -1

        [Test]
        public void CalcDeltaTwoStepsYieldsLength()
        {
            // given
            var interval = new Interval(1.1f, false, 3.1f, false, false);
            // when
            var result = interval.CalcDelta(2);
            // then
            Assert.AreEqual(2, result, 0.000001f);
        }

        [Test]
        public void CalcDeltaThreeStepsYieldsHalfLength()
        {
            // given
            var interval = new Interval(1.1f, false, 3.1f, false, false);
            // when
            var result = interval.CalcDelta(3);
            // then
            Assert.AreEqual(1, result, 0.000001f);
        }

        [Test]
        public void CalcDeltaYieldsValue()
        {
            // given
            var interval = new Interval(1.1f, false, 3.1f, false, false);
            // when
            var result = interval.CalcDelta(101);
            // then
            Assert.AreEqual(0.02f, result, 0.000001f);
        }
    }
}
