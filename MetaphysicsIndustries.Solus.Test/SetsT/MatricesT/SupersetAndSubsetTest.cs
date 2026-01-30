
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.MatricesT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestMatricesIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(MathObjects.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestMatricesIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(Matrices.M2x2.IsSubsetOf(MathObjects.Value));
            Assert.True(Matrices.M3x3.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfSets()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(Sets.Sets.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfSets()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(Sets.Sets.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(AllFunctions.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(AllFunctions.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                Matrices.M2x2.IsSupersetOf(Sets.Functions.RealsToReals));
            Assert.False(
                Matrices.M3x3.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                Matrices.M2x2.IsSubsetOf(Sets.Functions.RealsToReals));
            Assert.False(
                Matrices.M3x3.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Matrices.M2x2.IsSupersetOf(VariadicFunctions.RealsToReals));
            Assert.False(
                Matrices.M3x3.IsSupersetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Matrices.M2x2.IsSubsetOf(VariadicFunctions.RealsToReals));
            Assert.False(
                Matrices.M3x3.IsSubsetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(Tensors.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestMatricesIsSubsetOfTensors()
        {
            // expect
            Assert.True(Matrices.M2x2.IsSubsetOf(Tensors.Value));
            Assert.True(Matrices.M3x3.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(AllVectors.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(AllVectors.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(Vectors.R2));
            Assert.False(Matrices.M2x2.IsSupersetOf(Vectors.R3));
            Assert.False(Matrices.M3x3.IsSupersetOf(Vectors.R2));
            Assert.False(Matrices.M3x3.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(Vectors.R2));
            Assert.False(Matrices.M2x2.IsSubsetOf(Vectors.R3));
            Assert.False(Matrices.M3x3.IsSubsetOf(Vectors.R2));
            Assert.False(Matrices.M3x3.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(AllMatrices.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestMatricesIsSubsetOfAllMatrices()
        {
            // expect
            Assert.True(Matrices.M2x2.IsSubsetOf(AllMatrices.Value));
            Assert.True(Matrices.M3x3.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestMatricesIsSupersetOfSelf()
        {
            // expect
            Assert.True(Matrices.M2x2.IsSupersetOf(Matrices.M2x2));
            Assert.False(Matrices.M2x2.IsSupersetOf(Matrices.M3x3));
            Assert.False(Matrices.M3x3.IsSupersetOf(Matrices.M2x2));
            Assert.True(Matrices.M3x3.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestMatricesIsSubsetOfSelf()
        {
            // expect
            Assert.True(Matrices.M2x2.IsSubsetOf(Matrices.M2x2));
            Assert.False(Matrices.M2x2.IsSubsetOf(Matrices.M3x3));
            Assert.False(Matrices.M3x3.IsSubsetOf(Matrices.M2x2));
            Assert.True(Matrices.M3x3.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfReals()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(Reals.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfReals()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(Reals.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(Strings.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(Strings.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(Intervals.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(Intervals.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(Booleans.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(Booleans.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(Sets.Expressions.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(Sets.Expressions.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(Literals.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(Literals.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(ComponentAccesses.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(ComponentAccesses.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(FunctionCalls.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(FunctionCalls.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                Matrices.M2x2.IsSupersetOf(IntervalExpressions.Value));
            Assert.False(
                Matrices.M3x3.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(IntervalExpressions.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(TensorExpressions.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(TensorExpressions.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(MatrixExpressions.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(MatrixExpressions.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(VectorExpressions.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(VectorExpressions.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestMatricesIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSupersetOf(VariableAccesses.Value));
            Assert.False(Matrices.M3x3.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestMatricesIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(Matrices.M2x2.IsSubsetOf(VariableAccesses.Value));
            Assert.False(Matrices.M3x3.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
