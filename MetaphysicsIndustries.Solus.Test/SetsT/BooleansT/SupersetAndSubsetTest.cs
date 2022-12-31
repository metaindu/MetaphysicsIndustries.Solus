
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

using MetaphysicsIndustries.Solus.Sets;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.SetsT.BooleansT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestIsSupersetOfSomeSets()
        {
            // expect
            Assert.That(Booleans.Value.IsSupersetOf(Booleans.Value));

            Assert.That(!Booleans.Value.IsSupersetOf(Sets.Sets.Value));

            Assert.That(!Booleans.Value.IsSupersetOf(Reals.Value));
            Assert.That(!Booleans.Value.IsSupersetOf(Sets.Functions.RealsToReals));
            Assert.That(!Booleans.Value.IsSupersetOf(AllFunctions.Value));
            Assert.That(!Booleans.Value.IsSupersetOf(Intervals.Value));
            Assert.That(!Booleans.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!Booleans.Value.IsSupersetOf(Matrices.M3x3));
            Assert.That(!Booleans.Value.IsSupersetOf(AllMatrices.Value));
            Assert.That(!Booleans.Value.IsSupersetOf(Strings.Value));
            Assert.That(!Booleans.Value.IsSupersetOf(Tensors.Value));
            Assert.That(!Booleans.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!Booleans.Value.IsSupersetOf(Vectors.R3));
            Assert.That(!Booleans.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestIsSubsetOfSomeSets()
        {
            // expect
            Assert.That(Booleans.Value.IsSubsetOf(Booleans.Value));

            Assert.That(Booleans.Value.IsSubsetOf(Sets.Sets.Value));

            Assert.That(!Booleans.Value.IsSubsetOf(Reals.Value));
            Assert.That(!Booleans.Value.IsSubsetOf(Sets.Functions.RealsToReals));
            Assert.That(!Booleans.Value.IsSubsetOf(AllFunctions.Value));
            Assert.That(!Booleans.Value.IsSubsetOf(Intervals.Value));
            Assert.That(!Booleans.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!Booleans.Value.IsSubsetOf(Matrices.M3x3));
            Assert.That(!Booleans.Value.IsSubsetOf(AllMatrices.Value));
            Assert.That(!Booleans.Value.IsSubsetOf(Strings.Value));
            Assert.That(!Booleans.Value.IsSubsetOf(Tensors.Value));
            Assert.That(!Booleans.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!Booleans.Value.IsSubsetOf(Vectors.R3));
            Assert.That(!Booleans.Value.IsSubsetOf(AllVectors.Value));
        }
    }
}


