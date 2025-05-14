
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.MatrixExpressionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(MatrixExpressions.Value.IsSubsetOf(
                MathObjects.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfSets()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(
                Sets.Sets.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfSets()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(Sets.Functions
                    .RealsToReals));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSubsetOf(Sets.Functions
                    .RealsToReals));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfTensors()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfTensors()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfVectors()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(
                AllMatrices.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfReals()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfStrings()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfStrings()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(
                Intervals.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsSubsetOfExpressions()
        {
            // expect
            Assert.That(
                MatrixExpressions.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.That(!MatrixExpressions.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(IntervalExpressions
                    .Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSubsetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(
                MatrixExpressions.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsSupersetOfSelf()
        {
            // expect
            Assert.That(
                MatrixExpressions.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsSubsetOfSelf()
        {
            // expect
            Assert.That(
                MatrixExpressions.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestMatrixExpressionsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(
                !MatrixExpressions.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
