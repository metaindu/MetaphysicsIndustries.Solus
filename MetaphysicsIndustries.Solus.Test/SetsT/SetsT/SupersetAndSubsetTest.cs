
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.SetsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestSetsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestSetsIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(Sets.Sets.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestSetsIsSupersetOfSelf()
        {
            // expect
            Assert.That(Sets.Sets.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestSetsIsSubsetOfSelf()
        {
            // expect
            Assert.That(Sets.Sets.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.That(
                !Sets.Sets.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestSetsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(
                !Sets.Sets.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestSetsIsNotSupersetOfTensors()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfTensors()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfVectors()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestSetsIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestSetsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestSetsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestSetsIsNotSupersetOfReals()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfStrings()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfStrings()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(
                ComponentAccesses.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !Sets.Sets.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(
                IntervalExpressions.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(
                TensorExpressions.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(
                MatrixExpressions.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(
                VectorExpressions.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestSetsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestSetsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(!Sets.Sets.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
