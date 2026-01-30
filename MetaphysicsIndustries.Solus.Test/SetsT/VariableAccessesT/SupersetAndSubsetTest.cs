
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.VariableAccessesT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestVariableAccessesIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestVariableAccessesIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(VariableAccesses.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfSets()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfSets()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSubsetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSubsetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSupersetOf(Vectors.R2));
            Assert.False(VariableAccesses.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSubsetOf(Vectors.R2));
            Assert.False(VariableAccesses.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(VariableAccesses.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(VariableAccesses.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfReals()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfReals()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestVariableAccessesIsSubsetOfExpressions()
        {
            // expect
            Assert.True(
                VariableAccesses.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(VariableAccesses.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestVariableAccessesIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                VariableAccesses.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestVariableAccessesIsSupersetOfSelf()
        {
            // expect
            Assert.True(
                VariableAccesses.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestVariableAccessesIsSubsetOfSelf()
        {
            // expect
            Assert.True(
                VariableAccesses.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
