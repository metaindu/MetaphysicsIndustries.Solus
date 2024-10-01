
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.VariadicFunctionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    MathObjects.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(
                VariadicFunctions.RealsToReals.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfSets()
        {
            // It is a *member* of `Sets`, but not a *subset* of it.
            // Note: This is incorrect if we treat a function as a set of
            // ordered pairs. In that case, `RealsToReals` is a set of other
            // sets and thus a subset of `Sets`.
            // TODO: fix this so as to treat functions as sets.

            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    AllFunctions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsSubsetOfAllFunctions()
        {
            // expect
            Assert.True(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    AllFunctions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestVariadicFunctionsIsSupersetOfSelf()
        {
            // expect
            Assert.True(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestVariadicFunctionsIsSubsetOfSelf()
        {
            // expect
            Assert.True(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    AllVectors.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Vectors.R2));
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Vectors.R2));
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    AllMatrices.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Matrices.M2x2));
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Matrices.M2x2));
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    Sets.Expressions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    Sets.Expressions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    FunctionCalls.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    FunctionCalls.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSupersetOf(
                    VariableAccesses.Value));
        }

        [Test]
        public void TestVariadicFunctionsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                VariadicFunctions.RealsToReals.IsSubsetOf(
                    VariableAccesses.Value));
        }
    }
}
