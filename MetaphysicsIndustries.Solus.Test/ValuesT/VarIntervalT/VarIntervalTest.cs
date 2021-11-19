
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

namespace MetaphysicsIndustries.Solus.Test.ValuesT.VarIntervalT
{
    [TestFixture]
    public class VarIntervalTest
    {
        [Test]
        public void CreateSetsValues()
        {
            // given
            var varname = "a";
            var interval = new Interval(1.1f, true, 3.1f, true, false);
            // when
            var result = new VarInterval(varname, interval);
            // then
            Assert.AreEqual("a", result.Variable);
            Assert.AreEqual(interval, result.Interval);
        }

        [Test]
        public void CreateSetsValues2()
        {
            // given
            var varname = "a";
            // when
            var result = new VarInterval(varname, 1.1f, 3.1f);
            // then
            Assert.AreEqual("a", result.Variable);
            Assert.AreEqual(1.1f, result.Interval.LowerBound);
            Assert.IsFalse(result.Interval.OpenLowerBound);
            Assert.AreEqual(3.1f, result.Interval.UpperBound);
            Assert.IsFalse(result.Interval.OpenUpperBound);
            Assert.IsFalse(result.Interval.IsIntegerInterval);
        }

        [Test]
        public void ToString1()
        {
            // given
            var vi = new VarInterval(
                "a",
                new Interval(1.1f, true, 3.1f, true, false));
            // when
            var result = vi.ToString();
            // then
            Assert.AreEqual("1.1 < a < 3.1", result);
        }

        [Test]
        public void ToString2()
        {
            // given
            var vi = new VarInterval(
                "a",
                new Interval(1.1f, true, 3.1f, false, false));
            // when
            var result = vi.ToString();
            // then
            Assert.AreEqual("1.1 < a <= 3.1", result);
        }

        [Test]
        public void ToString3()
        {
            // given
            var vi = new VarInterval(
                "a",
                new Interval(1.1f, false, 3.1f, true, false));
            // when
            var result = vi.ToString();
            // then
            Assert.AreEqual("1.1 <= a < 3.1", result);
        }

        [Test]
        public void ToString4()
        {
            // given
            var vi = new VarInterval(
                "a",
                new Interval(1.1f, false, 3.1f, false, false));
            // when
            var result = vi.ToString();
            // then
            Assert.AreEqual("1.1 <= a <= 3.1", result);
        }

        // TODO: string representation doesn't show integer
        // TODO: integer interval bounds should be rounded (?)
        [Test]
        public void ToString5()
        {
            // given
            var vi = new VarInterval(
                "a",
                new Interval(1.1f, false, 3.1f, false, true));
            // when
            var result = vi.ToString();
            // then
            Assert.AreEqual("1.1 <= a <= 3.1", result);
        }
    }
}
