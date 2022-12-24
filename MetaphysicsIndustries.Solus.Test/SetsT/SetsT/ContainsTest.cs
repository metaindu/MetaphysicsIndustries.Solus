
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

using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.SetsT.SetsT
{
    [TestFixture]
    public class ContainsTest
    {
        [Test]
        public void ContainsOtherSets()
        {
            // expect
            Assert.That(
                Sets.Sets.Value.Contains(Sets.Functions.RealsToReals));
            Assert.That(Sets.Sets.Value.Contains(Vectors.R3));
            Assert.That(Sets.Sets.Value.Contains(MathObjects.Value));
        }

        [Test]
        public void ContainsItself()
        {
            // expect
            Assert.That(Sets.Sets.Value.Contains(Sets.Sets.Value));
        }

        [Test]
        public void DoesNotContainOtherObjects()
        {
            // expect
            Assert.That(!Sets.Sets.Value.Contains(1.ToNumber()));
            Assert.That(!Sets.Sets.Value.Contains("abc".ToStringValue()));
            Assert.That(!Sets.Sets.Value.Contains(Vector3.Zero));
            Assert.That(!Sets.Sets.Value.Contains(SineFunction.Value));
            Assert.That(!Sets.Sets.Value.Contains(Matrix.Identity2));
            Assert.That(!Sets.Sets.Value.Contains(SineFunction.Value));
            Assert.That(!Sets.Sets.Value.Contains(Literal.Zero));
        }

        [Test]
        public void ContainsIntervals()
        {
            // expect
            Assert.That(Sets.Sets.Value.Contains(
                new Interval(1, 5)));
            Assert.That(Sets.Sets.Value.Contains(
                Interval.Integer(1, 5)));
        }
    }
}