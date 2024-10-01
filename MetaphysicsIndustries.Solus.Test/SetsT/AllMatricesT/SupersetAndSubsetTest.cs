
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.AllMatricesT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestAllMatricesIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestAllMatricesIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(AllMatrices.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfSets()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfSets()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSubsetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestAllMatricesIsSubsetOfTensors()
        {
            // expect
            Assert.True(AllMatrices.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(Vectors.R2));
            Assert.False(AllMatrices.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(Vectors.R2));
            Assert.False(AllMatrices.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestAllMatricesIsSupersetOfSelf()
        {
            // expect
            Assert.True(AllMatrices.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestAllMatricesIsSubsetOfSelf()
        {
            // expect
            Assert.True(AllMatrices.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestAllMatricesIsSupersetOfMatrices()
        {
            // expect
            Assert.True(AllMatrices.Value.IsSupersetOf(Matrices.M2x2));
            Assert.True(AllMatrices.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(AllMatrices.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfReals()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfReals()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(AllMatrices.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestAllMatricesIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                AllMatrices.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
