
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
    public class EqualsTest
    {
        [Test]
        public void IdenticalIntervalsAreEqual1()
        {
            // given
            var i1 = new Interval(1, true, 2, true, true);
            var i2 = new Interval(1, true, 2, true, true);
            // expect
            Assert.IsTrue(i1.Equals(i2));
            Assert.IsTrue(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void IdenticalIntervalsAreEqual2()
        {
            // given
            var i1 = new Interval(1, true, 2, true, false);
            var i2 = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsTrue(i1.Equals(i2));
            Assert.IsTrue(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void IdenticalIntervalsAreEqual3()
        {
            // given
            var i1 = new Interval(1, true, 2, false, true);
            var i2 = new Interval(1, true, 2, false, true);
            // expect
            Assert.IsTrue(i1.Equals(i2));
            Assert.IsTrue(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void IdenticalIntervalsAreEqual4()
        {
            // given
            var i1 = new Interval(1, true, 2, false, false);
            var i2 = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsTrue(i1.Equals(i2));
            Assert.IsTrue(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void IdenticalIntervalsAreEqual5()
        {
            // given
            var i1 = new Interval(1, false, 2, true, true);
            var i2 = new Interval(1, false, 2, true, true);
            // expect
            Assert.IsTrue(i1.Equals(i2));
            Assert.IsTrue(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void IdenticalIntervalsAreEqual6()
        {
            // given
            var i1 = new Interval(1, false, 2, true, false);
            var i2 = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsTrue(i1.Equals(i2));
            Assert.IsTrue(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void IdenticalIntervalsAreEqual7()
        {
            // given
            var i1 = new Interval(1, false, 2, false, true);
            var i2 = new Interval(1, false, 2, false, true);
            // expect
            Assert.IsTrue(i1.Equals(i2));
            Assert.IsTrue(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void IdenticalIntervalsAreEqual8()
        {
            // given
            var i1 = new Interval(1, false, 2, false, false);
            var i2 = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsTrue(i1.Equals(i2));
            Assert.IsTrue(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual1()
        {
            // given
            var i1 = new Interval(1, true, 2, true, true);
            var i2 = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual2()
        {
            // given
            var i1 = new Interval(1, true, 2, true, true);
            var i2 = new Interval(1, true, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual3()
        {
            // given
            var i1 = new Interval(1, true, 2, true, true);
            var i2 = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual4()
        {
            // given
            var i1 = new Interval(1, true, 2, true, true);
            var i2 = new Interval(1, false, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual5()
        {
            // given
            var i1 = new Interval(1, true, 2, true, true);
            var i2 = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual6()
        {
            // given
            var i1 = new Interval(1, true, 2, true, true);
            var i2 = new Interval(1, false, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual7()
        {
            // given
            var i1 = new Interval(1, true, 2, true, true);
            var i2 = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual8()
        {
            // given
            var i1 = new Interval(1, true, 2, true, false);
            var i2 = new Interval(1, true, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual9()
        {
            // given
            var i1 = new Interval(1, true, 2, true, false);
            var i2 = new Interval(1, true, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual10()
        {
            // given
            var i1 = new Interval(1, true, 2, true, false);
            var i2 = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual11()
        {
            // given
            var i1 = new Interval(1, true, 2, true, false);
            var i2 = new Interval(1, false, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual12()
        {
            // given
            var i1 = new Interval(1, true, 2, true, false);
            var i2 = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual13()
        {
            // given
            var i1 = new Interval(1, true, 2, true, false);
            var i2 = new Interval(1, false, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual14()
        {
            // given
            var i1 = new Interval(1, true, 2, true, false);
            var i2 = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual15()
        {
            // given
            var i1 = new Interval(1, true, 2, false, true);
            var i2 = new Interval(1, true, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual16()
        {
            // given
            var i1 = new Interval(1, true, 2, false, true);
            var i2 = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual17()
        {
            // given
            var i1 = new Interval(1, true, 2, false, true);
            var i2 = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual18()
        {
            // given
            var i1 = new Interval(1, true, 2, false, true);
            var i2 = new Interval(1, false, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual19()
        {
            // given
            var i1 = new Interval(1, true, 2, false, true);
            var i2 = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual20()
        {
            // given
            var i1 = new Interval(1, true, 2, false, true);
            var i2 = new Interval(1, false, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual21()
        {
            // given
            var i1 = new Interval(1, true, 2, false, true);
            var i2 = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual22()
        {
            // given
            var i1 = new Interval(1, true, 2, false, false);
            var i2 = new Interval(1, true, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual23()
        {
            // given
            var i1 = new Interval(1, true, 2, false, false);
            var i2 = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual24()
        {
            // given
            var i1 = new Interval(1, true, 2, false, false);
            var i2 = new Interval(1, true, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual25()
        {
            // given
            var i1 = new Interval(1, true, 2, false, false);
            var i2 = new Interval(1, false, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual26()
        {
            // given
            var i1 = new Interval(1, true, 2, false, false);
            var i2 = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual27()
        {
            // given
            var i1 = new Interval(1, true, 2, false, false);
            var i2 = new Interval(1, false, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual28()
        {
            // given
            var i1 = new Interval(1, true, 2, false, false);
            var i2 = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual29()
        {
            // given
            var i1 = new Interval(1, false, 2, true, true);
            var i2 = new Interval(1, true, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual30()
        {
            // given
            var i1 = new Interval(1, false, 2, true, true);
            var i2 = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual31()
        {
            // given
            var i1 = new Interval(1, false, 2, true, true);
            var i2 = new Interval(1, true, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual32()
        {
            // given
            var i1 = new Interval(1, false, 2, true, true);
            var i2 = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual33()
        {
            // given
            var i1 = new Interval(1, false, 2, true, true);
            var i2 = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual34()
        {
            // given
            var i1 = new Interval(1, false, 2, true, true);
            var i2 = new Interval(1, false, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual35()
        {
            // given
            var i1 = new Interval(1, false, 2, true, true);
            var i2 = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual36()
        {
            // given
            var i1 = new Interval(1, false, 2, true, false);
            var i2 = new Interval(1, true, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual37()
        {
            // given
            var i1 = new Interval(1, false, 2, true, false);
            var i2 = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual38()
        {
            // given
            var i1 = new Interval(1, false, 2, true, false);
            var i2 = new Interval(1, true, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual39()
        {
            // given
            var i1 = new Interval(1, false, 2, true, false);
            var i2 = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual40()
        {
            // given
            var i1 = new Interval(1, false, 2, true, false);
            var i2 = new Interval(1, false, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual41()
        {
            // given
            var i1 = new Interval(1, false, 2, true, false);
            var i2 = new Interval(1, false, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual42()
        {
            // given
            var i1 = new Interval(1, false, 2, true, false);
            var i2 = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual43()
        {
            // given
            var i1 = new Interval(1, false, 2, false, true);
            var i2 = new Interval(1, true, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual44()
        {
            // given
            var i1 = new Interval(1, false, 2, false, true);
            var i2 = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual45()
        {
            // given
            var i1 = new Interval(1, false, 2, false, true);
            var i2 = new Interval(1, true, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual46()
        {
            // given
            var i1 = new Interval(1, false, 2, false, true);
            var i2 = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual47()
        {
            // given
            var i1 = new Interval(1, false, 2, false, true);
            var i2 = new Interval(1, false, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual48()
        {
            // given
            var i1 = new Interval(1, false, 2, false, true);
            var i2 = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual49()
        {
            // given
            var i1 = new Interval(1, false, 2, false, true);
            var i2 = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual50()
        {
            // given
            var i1 = new Interval(1, false, 2, false, false);
            var i2 = new Interval(1, true, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual51()
        {
            // given
            var i1 = new Interval(1, false, 2, false, false);
            var i2 = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual52()
        {
            // given
            var i1 = new Interval(1, false, 2, false, false);
            var i2 = new Interval(1, true, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual53()
        {
            // given
            var i1 = new Interval(1, false, 2, false, false);
            var i2 = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual54()
        {
            // given
            var i1 = new Interval(1, false, 2, false, false);
            var i2 = new Interval(1, false, 2, true, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual55()
        {
            // given
            var i1 = new Interval(1, false, 2, false, false);
            var i2 = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual56()
        {
            // given
            var i1 = new Interval(1, false, 2, false, false);
            var i2 = new Interval(1, false, 2, false, true);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
            Assert.That(i2.GetHashCode(), Is.Not.EqualTo(i1.GetHashCode()));
        }

        [Test]
        public void DifferentIntervalsAreNotEqual57()
        {
            // given
            var i1 = new Interval(1, false, 2, false, false);
            var i2 = new Interval(1, false, 3, false, false);
            // expect
            Assert.IsFalse(i1.Equals(i2));
            Assert.IsFalse(i1.Equals((object)i2));
        }
    }
}