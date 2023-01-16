
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.VectorExpressionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestVectorExpressionsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestVectorExpressionsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(
                VectorExpressions.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSubsetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSubsetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSupersetOf(Vectors.R2));
            Assert.False(VectorExpressions.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSubsetOf(Vectors.R2));
            Assert.False(VectorExpressions.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(VectorExpressions.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(VectorExpressions.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsSubsetOfExpressions()
        {
            // expect
            Assert.True(
                VectorExpressions.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(VectorExpressions.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSubsetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsSubsetOfTensorExpressions()
        {
            // expect
            Assert.True(
                VectorExpressions.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsSupersetOfSelf()
        {
            // expect
            Assert.True(
                VectorExpressions.Value.IsSupersetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsSubsetOfSelf()
        {
            // expect
            Assert.True(
                VectorExpressions.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                VectorExpressions.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
