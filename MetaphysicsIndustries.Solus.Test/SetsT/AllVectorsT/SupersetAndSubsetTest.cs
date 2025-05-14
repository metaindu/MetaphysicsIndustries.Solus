
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
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.SetsT.AllVectorsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestAllVectorsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestAllVectorsIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(AllVectors.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfSets()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfSets()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.That(
                !AllVectors.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(
                !AllVectors.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfTensors()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestAllVectorsIsSubsetOfTensors()
        {
            // expect
            Assert.That(AllVectors.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestAllVectorsIsSupersetOfSelf()
        {
            // expect
            Assert.That(AllVectors.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestAllVectorsIsSubsetOfSelf()
        {
            // expect
            Assert.That(AllVectors.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestAllVectorsIsSupersetOfVectors()
        {
            // expect
            Assert.That(AllVectors.Value.IsSupersetOf(Vectors.R2));
            Assert.That(AllVectors.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!AllVectors.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!AllVectors.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!AllVectors.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfReals()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfStrings()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfStrings()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(
                Sets.Expressions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.That(
                !AllVectors.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !AllVectors.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !AllVectors.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(
                !AllVectors.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.That(
                !AllVectors.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.That(
                !AllVectors.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSupersetOf(
                VariableAccesses.Value));
        }

        [Test]
        public void TestAllVectorsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(!AllVectors.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
