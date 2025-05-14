
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

using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.SetsT.BooleansT
{
    [TestFixture]
    public class ContainsTest
    {
        [Test]
        public void ContainsBooleans()
        {
            // expect
            Assert.That(Booleans.Value.Contains(new Boolean(true)));
            Assert.That(Booleans.Value.Contains(new Boolean(false)));
        }

        [Test]
        public void DoesNotContainNonBooleans()
        {
            Assert.That(!Booleans.Value.Contains(1.ToNumber()));
            Assert.That(!Booleans.Value.Contains("abc".ToStringValue()));
            Assert.That(!Booleans.Value.Contains(new Vector2(1, 2)));
            Assert.That(!Booleans.Value.Contains(Booleans.Value));
            Assert.That(!Booleans.Value.Contains(MathObjects.Value));
        }
    }
}