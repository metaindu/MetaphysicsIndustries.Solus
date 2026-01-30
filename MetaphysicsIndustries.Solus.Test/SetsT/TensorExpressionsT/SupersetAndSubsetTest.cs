
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.TensorExpressionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestTensorExpressionsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestTensorExpressionsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(
                TensorExpressions.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSupersetOf(Vectors.R2));
            Assert.False(TensorExpressions.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSubsetOf(Vectors.R2));
            Assert.False(TensorExpressions.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(TensorExpressions.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(TensorExpressions.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsSubsetOfExpressions()
        {
            // expect
            Assert.True(
                TensorExpressions.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(TensorExpressions.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsSupersetOfSelf()
        {
            // expect
            Assert.True(
                TensorExpressions.Value.IsSupersetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsSubsetOfSelf()
        {
            // expect
            Assert.True(
                TensorExpressions.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsSupersetOfMatrixExpressions()
        {
            // expect
            Assert.True(
                TensorExpressions.Value.IsSupersetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsSupersetOfVectorExpressions()
        {
            // expect
            Assert.True(
                TensorExpressions.Value.IsSupersetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestTensorExpressionsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                TensorExpressions.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
