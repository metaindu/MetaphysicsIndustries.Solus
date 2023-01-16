
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
        public void TestVectorsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(MathObjects.Value));
            Assert.False(Vectors.R3.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestVectorsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(Vectors.R2.IsSubsetOf(MathObjects.Value));
            Assert.True(Vectors.R3.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(Sets.Sets.Value));
            Assert.False(Vectors.R3.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(Sets.Sets.Value));
            Assert.False(Vectors.R3.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(AllFunctions.Value));
            Assert.False(Vectors.R3.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(AllFunctions.Value));
            Assert.False(Vectors.R3.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                Vectors.R2.IsSupersetOf(Sets.Functions.RealsToReals));
            Assert.False(
                Vectors.R3.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(Sets.Functions.RealsToReals));
            Assert.False(Vectors.R3.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Vectors.R2.IsSupersetOf(VariadicFunctions.RealsToReals));
            Assert.False(
                Vectors.R3.IsSupersetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Vectors.R2.IsSubsetOf(VariadicFunctions.RealsToReals));
            Assert.False(
                Vectors.R3.IsSubsetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(Tensors.Value));
            Assert.False(Vectors.R3.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestVectorsIsSubsetOfTensors()
        {
            // expect
            Assert.True(Vectors.R2.IsSubsetOf(Tensors.Value));
            Assert.True(Vectors.R3.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(AllVectors.Value));
            Assert.False(Vectors.R3.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestVectorsIsSubsetOfAllVectors()
        {
            // expect
            Assert.True(Vectors.R2.IsSubsetOf(AllVectors.Value));
            Assert.True(Vectors.R3.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestVectorsIsSupersetOfSelf()
        {
            // expect
            Assert.True(Vectors.R2.IsSupersetOf(Vectors.R2));
            Assert.False(Vectors.R2.IsSupersetOf(Vectors.R3));
            Assert.False(Vectors.R3.IsSupersetOf(Vectors.R2));
            Assert.True(Vectors.R3.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestVectorsIsSubsetOfSelf()
        {
            // expect
            Assert.True(Vectors.R2.IsSubsetOf(Vectors.R2));
            Assert.False(Vectors.R2.IsSubsetOf(Vectors.R3));
            Assert.False(Vectors.R3.IsSubsetOf(Vectors.R2));
            Assert.True(Vectors.R3.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(AllMatrices.Value));
            Assert.False(Vectors.R3.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(AllMatrices.Value));
            Assert.False(Vectors.R3.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(Matrices.M2x2));
            Assert.False(Vectors.R2.IsSupersetOf(Matrices.M3x3));
            Assert.False(Vectors.R3.IsSupersetOf(Matrices.M2x2));
            Assert.False(Vectors.R3.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(Matrices.M2x2));
            Assert.False(Vectors.R2.IsSubsetOf(Matrices.M3x3));
            Assert.False(Vectors.R3.IsSubsetOf(Matrices.M2x2));
            Assert.False(Vectors.R3.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(Reals.Value));
            Assert.False(Vectors.R3.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(Reals.Value));
            Assert.False(Vectors.R3.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(Strings.Value));
            Assert.False(Vectors.R3.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(Strings.Value));
            Assert.False(Vectors.R3.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(Intervals.Value));
            Assert.False(Vectors.R3.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(Intervals.Value));
            Assert.False(Vectors.R3.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(Booleans.Value));
            Assert.False(Vectors.R3.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(Booleans.Value));
            Assert.False(Vectors.R3.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(Sets.Expressions.Value));
            Assert.False(Vectors.R3.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(Sets.Expressions.Value));
            Assert.False(Vectors.R3.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(Literals.Value));
            Assert.False(Vectors.R3.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(Literals.Value));
            Assert.False(Vectors.R3.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(ComponentAccesses.Value));
            Assert.False(Vectors.R3.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(ComponentAccesses.Value));
            Assert.False(Vectors.R3.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(FunctionCalls.Value));
            Assert.False(Vectors.R3.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(FunctionCalls.Value));
            Assert.False(Vectors.R3.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(IntervalExpressions.Value));
            Assert.False(Vectors.R3.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(IntervalExpressions.Value));
            Assert.False(Vectors.R3.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(TensorExpressions.Value));
            Assert.False(Vectors.R3.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(TensorExpressions.Value));
            Assert.False(Vectors.R3.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(MatrixExpressions.Value));
            Assert.False(Vectors.R3.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(MatrixExpressions.Value));
            Assert.False(Vectors.R3.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(VectorExpressions.Value));
            Assert.False(Vectors.R3.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(VectorExpressions.Value));
            Assert.False(Vectors.R3.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestVectorsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(Vectors.R2.IsSupersetOf(VariableAccesses.Value));
            Assert.False(Vectors.R3.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestVectorsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(Vectors.R2.IsSubsetOf(VariableAccesses.Value));
            Assert.False(Vectors.R3.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
