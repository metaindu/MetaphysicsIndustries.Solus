
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.FunctionCallsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestFunctionCallsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestFunctionCallsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(FunctionCalls.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSupersetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSubsetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(Vectors.R2));
            Assert.False(FunctionCalls.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(Vectors.R2));
            Assert.False(FunctionCalls.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(FunctionCalls.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(FunctionCalls.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestFunctionCallsIsSubsetOfExpressions()
        {
            // expect
            Assert.True(
                FunctionCalls.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(FunctionCalls.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestFunctionCallsIsSupersetOfSelf()
        {
            // expect
            Assert.True(
                FunctionCalls.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestFunctionCallsIsSubsetOfSelf()
        {
            // expect
            Assert.True(FunctionCalls.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestFunctionCallsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                FunctionCalls.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
