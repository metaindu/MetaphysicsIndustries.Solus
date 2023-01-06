
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.ComponentAccessesT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestComponentAccessesIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestComponentAccessesIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(ComponentAccesses.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfSets()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(
                Sets.Sets.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfSets()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfFunctions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(Sets.Functions
                    .RealsToReals));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSubsetOf(Sets.Functions
                    .RealsToReals));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfTensors()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfTensors()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfVectors()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(
                AllMatrices.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfMatrices()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfReals()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfStrings()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfStrings()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfIntervals()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(
                Intervals.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfBooleans()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfBooleans()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfExpressions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsSubsetOfExpressions()
        {
            // expect
            Assert.That(
                ComponentAccesses.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfLiterals()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfLiterals()
        {
            // expect
            Assert.That(!ComponentAccesses.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestComponentAccessesIsSupersetOfSelf()
        {
            // expect
            Assert.That(
                ComponentAccesses.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestComponentAccessesIsSubsetOfSelf()
        {
            // expect
            Assert.That(
                ComponentAccesses.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(IntervalExpressions
                    .Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSubsetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(
                !ComponentAccesses.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
