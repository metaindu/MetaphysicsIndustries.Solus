
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.FunctionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestIsSupersetOfSomeSets()
        {
            // given
            var f1 = Sets.Functions.Get(Strings.Value, Reals.Value);
            // expect
            Assert.That(f1.IsSupersetOf(f1));
            Assert.That(!f1.IsSupersetOf(Sets.Functions.RealsToReals));
            Assert.That(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    Sets.Functions.RealsToReals));
            Assert.That(!Sets.Functions.RealsToReals.IsSupersetOf(f1));
            Assert.That(!f1.IsSupersetOf(AllFunctions.Value));

            Assert.That(!f1.IsSupersetOf(Sets.Sets.Value));

            Assert.That(!f1.IsSupersetOf(Intervals.Value));
            Assert.That(!f1.IsSupersetOf(Matrices.M2x2));
            Assert.That(!f1.IsSupersetOf(Matrices.M3x3));
            Assert.That(!f1.IsSupersetOf(AllMatrices.Value));
            Assert.That(!f1.IsSupersetOf(Reals.Value));
            Assert.That(!f1.IsSupersetOf(Strings.Value));
            Assert.That(!f1.IsSupersetOf(Tensors.Value));
            Assert.That(!f1.IsSupersetOf(Vectors.R2));
            Assert.That(!f1.IsSupersetOf(Vectors.R3));
            Assert.That(!f1.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestIsSubsetOfSomeSets()
        {
            // given
            var f1 = Sets.Functions.Get(Strings.Value, Reals.Value);
            // expect
            Assert.That(f1.IsSubsetOf(f1));
            Assert.That(!f1.IsSubsetOf(Sets.Functions.RealsToReals));
            Assert.That(
                Sets.Functions.RealsToReals.IsSubsetOf(
                    Sets.Functions.RealsToReals));
            Assert.That(!Sets.Functions.RealsToReals.IsSubsetOf(f1));
            Assert.That(f1.IsSubsetOf(AllFunctions.Value));

            Assert.That(f1.IsSubsetOf(Sets.Sets.Value));

            Assert.That(!f1.IsSubsetOf(Intervals.Value));
            Assert.That(!f1.IsSubsetOf(Matrices.M2x2));
            Assert.That(!f1.IsSubsetOf(Matrices.M3x3));
            Assert.That(!f1.IsSubsetOf(AllMatrices.Value));
            Assert.That(!f1.IsSubsetOf(Reals.Value));
            Assert.That(!f1.IsSubsetOf(Strings.Value));
            Assert.That(!f1.IsSubsetOf(Tensors.Value));
            Assert.That(!f1.IsSubsetOf(Vectors.R2));
            Assert.That(!f1.IsSubsetOf(Vectors.R3));
            Assert.That(!f1.IsSubsetOf(AllVectors.Value));
        }
    }
}
