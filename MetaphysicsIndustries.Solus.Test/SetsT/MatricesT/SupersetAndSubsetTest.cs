
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.MatricesT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestIsSupersetOfSomeSets()
        {
            // expect
            Assert.That(Matrices.M2x2.IsSupersetOf(Matrices.M2x2));
            Assert.That(Matrices.M3x3.IsSupersetOf(Matrices.M3x3));
            Assert.That(!Matrices.M2x2.IsSupersetOf(Matrices.M3x3));
            Assert.That(!Matrices.M3x3.IsSupersetOf(Matrices.M2x2));
            Assert.That(!Matrices.M2x2.IsSupersetOf(AllMatrices.Value));
            Assert.That(!Matrices.M3x3.IsSupersetOf(AllMatrices.Value));
            Assert.That(AllMatrices.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(AllMatrices.Value.IsSupersetOf(Matrices.M3x3));
            Assert.That(AllMatrices.Value.IsSupersetOf(AllMatrices.Value));

            Assert.That(!Matrices.M2x2.IsSupersetOf(Tensors.Value));
            Assert.That(!AllMatrices.Value.IsSupersetOf(Tensors.Value));

            Assert.That(!Matrices.M2x2.IsSupersetOf(Sets.Sets.Value));
            Assert.That(!AllMatrices.Value.IsSupersetOf(Sets.Sets.Value));

            Assert.That(!Matrices.M2x2.IsSupersetOf(Sets.Functions.RealsToReals));
            Assert.That(!Matrices.M2x2.IsSupersetOf(AllFunctions.Value));
            Assert.That(!Matrices.M2x2.IsSupersetOf(Intervals.Value));
            Assert.That(!Matrices.M2x2.IsSupersetOf(Reals.Value));
            Assert.That(!Matrices.M2x2.IsSupersetOf(Strings.Value));
            Assert.That(!Matrices.M2x2.IsSupersetOf(Vectors.R2));
            Assert.That(!Matrices.M2x2.IsSupersetOf(Vectors.R3));
            Assert.That(!Matrices.M2x2.IsSupersetOf(AllVectors.Value));

            Assert.That(!AllMatrices.Value.IsSupersetOf(Sets.Functions.RealsToReals));
            Assert.That(!AllMatrices.Value.IsSupersetOf(AllFunctions.Value));
            Assert.That(!AllMatrices.Value.IsSupersetOf(Intervals.Value));
            Assert.That(!AllMatrices.Value.IsSupersetOf(Reals.Value));
            Assert.That(!AllMatrices.Value.IsSupersetOf(Strings.Value));
            Assert.That(!AllMatrices.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!AllMatrices.Value.IsSupersetOf(Vectors.R3));
            Assert.That(!AllMatrices.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestIsSubsetOfSomeSets()
        {
            // expect
            Assert.That(Matrices.M2x2.IsSubsetOf(Matrices.M2x2));
            Assert.That(Matrices.M3x3.IsSubsetOf(Matrices.M3x3));
            Assert.That(!Matrices.M2x2.IsSubsetOf(Matrices.M3x3));
            Assert.That(!Matrices.M3x3.IsSubsetOf(Matrices.M2x2));
            Assert.That(Matrices.M2x2.IsSubsetOf(AllMatrices.Value));
            Assert.That(Matrices.M3x3.IsSubsetOf(AllMatrices.Value));
            Assert.That(!AllMatrices.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!AllMatrices.Value.IsSubsetOf(Matrices.M3x3));
            Assert.That(AllMatrices.Value.IsSubsetOf(AllMatrices.Value));

            Assert.That(Matrices.M2x2.IsSubsetOf(Tensors.Value));
            Assert.That(AllMatrices.Value.IsSubsetOf(Tensors.Value));

            Assert.That(Matrices.M2x2.IsSubsetOf(Sets.Sets.Value));
            Assert.That(AllMatrices.Value.IsSubsetOf(Sets.Sets.Value));

            Assert.That(!Matrices.M2x2.IsSubsetOf(Sets.Functions.RealsToReals));
            Assert.That(!Matrices.M2x2.IsSubsetOf(AllFunctions.Value));
            Assert.That(!Matrices.M2x2.IsSubsetOf(Intervals.Value));
            Assert.That(!Matrices.M2x2.IsSubsetOf(Reals.Value));
            Assert.That(!Matrices.M2x2.IsSubsetOf(Strings.Value));
            Assert.That(!Matrices.M2x2.IsSubsetOf(Vectors.R2));
            Assert.That(!Matrices.M2x2.IsSubsetOf(Vectors.R3));
            Assert.That(!Matrices.M2x2.IsSubsetOf(AllVectors.Value));

            Assert.That(!AllMatrices.Value.IsSubsetOf(Sets.Functions.RealsToReals));
            Assert.That(!AllMatrices.Value.IsSubsetOf(AllFunctions.Value));
            Assert.That(!AllMatrices.Value.IsSubsetOf(Intervals.Value));
            Assert.That(!AllMatrices.Value.IsSubsetOf(Reals.Value));
            Assert.That(!AllMatrices.Value.IsSubsetOf(Strings.Value));
            Assert.That(!AllMatrices.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!AllMatrices.Value.IsSubsetOf(Vectors.R3));
            Assert.That(!AllMatrices.Value.IsSubsetOf(AllVectors.Value));
        }
    }
}


