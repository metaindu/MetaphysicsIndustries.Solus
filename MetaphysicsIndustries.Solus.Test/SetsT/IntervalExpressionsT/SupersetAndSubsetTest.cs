
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.IntervalExpressionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(
                IntervalExpressions.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(IntervalExpressions.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(IntervalExpressions.Value.IsSupersetOf(Vectors.R2));
            Assert.False(IntervalExpressions.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(IntervalExpressions.Value.IsSubsetOf(Vectors.R2));
            Assert.False(IntervalExpressions.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(IntervalExpressions.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(IntervalExpressions.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(IntervalExpressions.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(IntervalExpressions.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(IntervalExpressions.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(
                    Sets.Expressions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsSubsetOfExpressions()
        {
            // expect
            Assert.True(
                IntervalExpressions.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsSupersetOfSelf()
        {
            // expect
            Assert.True(
                IntervalExpressions.Value.IsSupersetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsSubsetOfSelf()
        {
            // expect
            Assert.True(
                IntervalExpressions.Value.IsSubsetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSupersetOf(
                    VariableAccesses.Value));
        }

        [Test]
        public void TestIntervalExpressionsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                IntervalExpressions.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
