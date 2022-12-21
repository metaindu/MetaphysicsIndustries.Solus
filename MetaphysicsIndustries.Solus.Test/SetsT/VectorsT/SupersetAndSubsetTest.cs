
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.VectorsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestIsSupersetOfSomeSets()
        {
            // expect
            Assert.That(Vectors.R2.IsSupersetOf(Vectors.R2));
            Assert.That(!Vectors.R2.IsSupersetOf(Vectors.R3));
            Assert.That(!Vectors.R3.IsSupersetOf(Vectors.R2));
            Assert.That(Vectors.R3.IsSupersetOf(Vectors.R3));
            Assert.That(!Vectors.R2.IsSupersetOf(AllVectors.Value));
            Assert.That(!Vectors.R3.IsSupersetOf(AllVectors.Value));
            Assert.That(AllVectors.Value.IsSupersetOf(Vectors.R2));
            Assert.That(AllVectors.Value.IsSupersetOf(Vectors.R3));
            Assert.That(AllVectors.Value.IsSupersetOf(AllVectors.Value));

            Assert.That(!Vectors.R2.IsSupersetOf(Sets.Sets.Value));
            Assert.That(!AllVectors.Value.IsSupersetOf(Sets.Sets.Value));

            Assert.That(!Vectors.R2.IsSupersetOf(Sets.Functions.RealsToReals));
            Assert.That(!Vectors.R2.IsSupersetOf(AllFunctions.Value));
            Assert.That(!Vectors.R2.IsSupersetOf(Intervals.Value));
            Assert.That(!Vectors.R2.IsSupersetOf(Matrices.M2x2));
            Assert.That(!Vectors.R2.IsSupersetOf(Matrices.M3x3));
            Assert.That(!Vectors.R2.IsSupersetOf(AllMatrices.Value));
            Assert.That(!Vectors.R2.IsSupersetOf(Reals.Value));
            Assert.That(!Vectors.R2.IsSupersetOf(Strings.Value));
            Assert.That(!Vectors.R2.IsSupersetOf(Tensors.Value));

            Assert.That(!AllVectors.Value.IsSupersetOf(Sets.Functions.RealsToReals));
            Assert.That(!AllVectors.Value.IsSupersetOf(AllFunctions.Value));
            Assert.That(!AllVectors.Value.IsSupersetOf(Intervals.Value));
            Assert.That(!AllVectors.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!AllVectors.Value.IsSupersetOf(Matrices.M3x3));
            Assert.That(!AllVectors.Value.IsSupersetOf(AllMatrices.Value));
            Assert.That(!AllVectors.Value.IsSupersetOf(Reals.Value));
            Assert.That(!AllVectors.Value.IsSupersetOf(Strings.Value));
            Assert.That(!AllVectors.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestIsSubsetOfSomeSets()
        {
            // expect
            Assert.That(Vectors.R2.IsSubsetOf(Vectors.R2));
            Assert.That(!Vectors.R2.IsSubsetOf(Vectors.R3));
            Assert.That(!Vectors.R3.IsSubsetOf(Vectors.R2));
            Assert.That(Vectors.R3.IsSubsetOf(Vectors.R3));
            Assert.That(Vectors.R2.IsSubsetOf(AllVectors.Value));
            Assert.That(Vectors.R3.IsSubsetOf(AllVectors.Value));
            Assert.That(!AllVectors.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!AllVectors.Value.IsSubsetOf(Vectors.R3));

            Assert.That(Vectors.R2.IsSubsetOf(Sets.Sets.Value));
            Assert.That(AllVectors.Value.IsSubsetOf(Sets.Sets.Value));

            Assert.That(!Vectors.R2.IsSubsetOf(Sets.Functions.RealsToReals));
            Assert.That(!Vectors.R2.IsSubsetOf(AllFunctions.Value));
            Assert.That(!Vectors.R2.IsSubsetOf(Intervals.Value));
            Assert.That(!Vectors.R2.IsSubsetOf(Matrices.M2x2));
            Assert.That(!Vectors.R2.IsSubsetOf(Matrices.M3x3));
            Assert.That(!Vectors.R2.IsSubsetOf(AllMatrices.Value));
            Assert.That(!Vectors.R2.IsSubsetOf(Reals.Value));
            Assert.That(!Vectors.R2.IsSubsetOf(Strings.Value));
            Assert.That(Vectors.R2.IsSubsetOf(Tensors.Value));

            Assert.That(!AllVectors.Value.IsSubsetOf(Sets.Functions.RealsToReals));
            Assert.That(!AllVectors.Value.IsSubsetOf(AllFunctions.Value));
            Assert.That(!AllVectors.Value.IsSubsetOf(Intervals.Value));
            Assert.That(!AllVectors.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!AllVectors.Value.IsSubsetOf(Matrices.M3x3));
            Assert.That(!AllVectors.Value.IsSubsetOf(AllMatrices.Value));
            Assert.That(!AllVectors.Value.IsSubsetOf(Reals.Value));
            Assert.That(!AllVectors.Value.IsSubsetOf(Strings.Value));
            Assert.That(AllVectors.Value.IsSubsetOf(Tensors.Value));
        }
    }
}


