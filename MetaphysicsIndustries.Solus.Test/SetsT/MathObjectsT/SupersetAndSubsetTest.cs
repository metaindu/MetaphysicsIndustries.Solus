
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.MathObjectsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestMathObjectsIsSupersetOfSelf()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestMathObjectsIsSubsetOfSelf()
        {
            // expect
            Assert.True(MathObjects.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfSets()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfAllFunctions()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfFunctions()
        {
            // expect
            Assert.True(
                MathObjects.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                MathObjects.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfVariadicFunctions()
        {
            // expect
            Assert.True(
                MathObjects.Value.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                MathObjects.Value.IsSubsetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfTensors()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfAllVectors()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfVectors()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(Vectors.R2));
            Assert.True(MathObjects.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(Vectors.R2));
            Assert.False(MathObjects.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfAllMatrices()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfMatrices()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(Matrices.M2x2));
            Assert.True(MathObjects.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(MathObjects.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfReals()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfStrings()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfIntervals()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfBooleans()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfExpressions()
        {
            // expect
            Assert.True(
                MathObjects.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(
                MathObjects.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfLiterals()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfComponentAccesses()
        {
            // expect
            Assert.True(
                MathObjects.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                MathObjects.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfFunctionCalls()
        {
            // expect
            Assert.True(MathObjects.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(MathObjects.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfIntervalExpressions()
        {
            // expect
            Assert.True(
                MathObjects.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                MathObjects.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfTensorExpressions()
        {
            // expect
            Assert.True(
                MathObjects.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(
                MathObjects.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfMatrixExpressions()
        {
            // expect
            Assert.True(
                MathObjects.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                MathObjects.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfVectorExpressions()
        {
            // expect
            Assert.True(
                MathObjects.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                MathObjects.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfVariableAccesses()
        {
            // expect
            Assert.True(
                MathObjects.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                MathObjects.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
