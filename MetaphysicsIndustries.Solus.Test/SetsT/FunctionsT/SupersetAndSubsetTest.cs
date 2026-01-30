
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.FunctionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestFunctionsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestFunctionsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(
                Sets.Functions.RealsToReals.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestFunctionsIsSubsetOfAllFunctions()
        {
            // expect
            Assert.True(
                Sets.Functions.RealsToReals.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestFunctionsIsSupersetOfSelf()
        {
            // expect
            Assert.True(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestFunctionsIsSubsetOfSelf()
        {
            // expect
            Assert.True(
                Sets.Functions.RealsToReals.IsSubsetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Vectors.R2));
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(Sets.Functions.RealsToReals.IsSubsetOf(Vectors.R2));
            Assert.False(Sets.Functions.RealsToReals.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Matrices.M2x2));
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(Matrices.M2x2));
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(Sets.Functions.RealsToReals.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    Sets.Expressions.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(
                    Sets.Expressions.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    FunctionCalls.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestFunctionsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSupersetOf(
                    VariableAccesses.Value));
        }

        [Test]
        public void TestFunctionsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                Sets.Functions.RealsToReals.IsSubsetOf(
                    VariableAccesses.Value));
        }
    }
}
