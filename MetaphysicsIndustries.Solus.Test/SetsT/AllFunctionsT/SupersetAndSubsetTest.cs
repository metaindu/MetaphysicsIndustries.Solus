
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.AllFunctionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestAllFunctionsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestAllFunctionsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(AllFunctions.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestAllFunctionsIsSupersetOfSelf()
        {
            // expect
            Assert.True(AllFunctions.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestAllFunctionsIsSubsetOfSelf()
        {
            // expect
            Assert.True(AllFunctions.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestAllFunctionsIsSupersetOfFunctions()
        {
            // expect
            Assert.True(
                AllFunctions.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestAllFunctionsIsSupersetOfVariadicFunctions()
        {
            // expect
            Assert.True(
                AllFunctions.Value.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSubsetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(Vectors.R2));
            Assert.False(AllFunctions.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(Vectors.R2));
            Assert.False(AllFunctions.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(AllFunctions.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(AllFunctions.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(AllFunctions.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestAllFunctionsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                AllFunctions.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
