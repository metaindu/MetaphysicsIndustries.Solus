
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.SetsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestIsSupersetOfItself()
        {
            // expect
            Assert.That(Sets.Sets.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(MathObjects.Value));

            Assert.That(!Sets.Sets.Value.IsSupersetOf(
                Sets.Functions.RealsToReals));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(AllFunctions.Value));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Intervals.Value));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Matrices.M3x3));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(AllMatrices.Value));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Reals.Value));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Strings.Value));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Tensors.Value));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Vectors.R3));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestIsSubsetOfItself()
        {
            // expect
            Assert.That(Sets.Sets.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(Sets.Sets.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestIsNotSubsetOfAnyOtherSet()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Sets.Functions.RealsToReals));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(AllFunctions.Value));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Intervals.Value));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Matrices.M3x3));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(AllMatrices.Value));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Reals.Value));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Strings.Value));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Tensors.Value));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Vectors.R3));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(AllVectors.Value));
        }
    }
}


