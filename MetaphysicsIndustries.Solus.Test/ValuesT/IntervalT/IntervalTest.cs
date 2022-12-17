
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
using MetaphysicsIndustries.Solus.Exceptions;
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
            Assert.That(result.LowerBound, Is.EqualTo(1));
            Assert.IsFalse(result.OpenLowerBound);
            Assert.That(result.UpperBound, Is.EqualTo(2));
            Assert.IsTrue(result.OpenUpperBound);
            Assert.IsFalse(result.IsIntegerInterval);
        }

        [Test]
        public void CreateWithNaNLowerThrows()
        {
            // expect
            var ex = Assert.Throws<ValueException>(
                () =>
                {
                    var i = new Interval(float.NaN, false, 1, false, false);
                });
            // and

            // TODO: hard-code all exception messages. don't rely on the
            //       framework to format the message, because then it isn't
            //       consistent.
            // TODO: Create some exception classes for bad types, values,
            //       and/or arguments
            Assert.That(ex.ParamName, Is.EqualTo("lowerBound"));
            Assert.That(
                ex.Message,
                Is.EqualTo("Not a number: lowerBound"));
        }

        [Test]
        public void CreateWithNaNUpperThrows()
        {
            // expect
            var ex = Assert.Throws<ValueException>(
                () =>
                {
                    var i = new Interval(1, false, float.NaN, false, false);
                });
            // and
            Assert.That(ex.ParamName, Is.EqualTo("upperBound"));
            Assert.That(
                ex.Message,
                Is.EqualTo("Not a number: upperBound"));
        }

        [Test]
        public void CreateWithInfinityLowerDoesNotThrow()
        {
            // expect
            Assert.DoesNotThrow(
                () =>
                {
                    var i = new Interval(float.NegativeInfinity, false,
                        1, false, false);
                });
        }

        [Test]
        public void CreateWithInfinityUpperDoesNotThrow()
        {
            // expect
            Assert.DoesNotThrow(
                () =>
                {
                    var i = new Interval(1, false,
                        float.PositiveInfinity, false, false);
                });
        }

        [Test]
        public void LowerGreaterThanHigherYieldsBoundsReversed()
        {
            // when
            var result = new Interval(2, false, 1, true, false);
            // then
            Assert.That(result.LowerBound, Is.EqualTo(1));
            Assert.IsTrue(result.OpenLowerBound);
            Assert.That(result.UpperBound, Is.EqualTo(2));
            Assert.IsFalse(result.OpenUpperBound);
        }

        [Test]
        public void CreateYieldsValues2()
        {
            // when
            var result = new Interval(3, true, 4, false, true);
            // then
            Assert.That(result.LowerBound, Is.EqualTo(3));
            Assert.IsTrue(result.OpenLowerBound);
            Assert.That(result.UpperBound, Is.EqualTo(4));
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
            Assert.That(interval.Length, Is.EqualTo(2));
        }

        [Test]
        public void IntegerYieldsIntegerInterval()
        {
            // when
            var result = Interval.Integer(1, 3);
            // then
            Assert.That(result.LowerBound, Is.EqualTo(1));
            Assert.IsFalse(result.OpenLowerBound);
            Assert.That(result.UpperBound, Is.EqualTo(3));
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
            Assert.That(result.LowerBound, Is.EqualTo(1));
            Assert.IsFalse(result.OpenLowerBound);
            Assert.That(result.UpperBound, Is.EqualTo(3));
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
            Assert.That(result, Is.EqualTo(2).Within(0.000001f));
        }

        [Test]
        public void CalcDeltaThreeStepsYieldsHalfLength()
        {
            // given
            var interval = new Interval(1.1f, false, 3.1f, false, false);
            // when
            var result = interval.CalcDelta(3);
            // then
            Assert.That(result, Is.EqualTo(1).Within(0.000001f));
        }

        [Test]
        public void CalcDeltaYieldsValue()
        {
            // given
            var interval = new Interval(1.1f, false, 3.1f, false, false);
            // when
            var result = interval.CalcDelta(101);
            // then
            Assert.That(result, Is.EqualTo(0.02f).Within(0.000001f));
        }

        [Test]
        public void MathObjectIsInterval()
        {
            // given
            var interval = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(interval.IsScalar(null));
            Assert.IsFalse(interval.IsVector(null));
            Assert.IsFalse(interval.IsMatrix(null));
            Assert.IsNull(interval.GetTensorRank(null));
            Assert.IsFalse(interval.IsString(null));
            Assert.IsNull(interval.GetDimension(null, 0));
            Assert.IsNull(interval.GetDimensions(null));
            Assert.IsNull(interval.GetVectorLength(null));
            Assert.IsTrue(interval.IsConcrete);
        }
    }
}
