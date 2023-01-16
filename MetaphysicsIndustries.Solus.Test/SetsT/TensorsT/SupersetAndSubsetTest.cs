
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.TensorsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestTensorsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestTensorsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(Tensors.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                Tensors.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                Tensors.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Tensors.Value.IsSupersetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Tensors.Value.IsSubsetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestTensorsIsSupersetOfSelf()
        {
            // expect
            Assert.True(Tensors.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestTensorsIsSubsetOfSelf()
        {
            // expect
            Assert.True(Tensors.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestTensorsIsSupersetOfAllVectors()
        {
            // expect
            Assert.True(Tensors.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestTensorsIsSupersetOfVectors()
        {
            // expect
            Assert.True(Tensors.Value.IsSupersetOf(Vectors.R2));
            Assert.True(Tensors.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(Vectors.R2));
            Assert.False(Tensors.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestTensorsIsSupersetOfAllMatrices()
        {
            // expect
            Assert.True(Tensors.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestTensorsIsSupersetOfMatrices()
        {
            // expect
            Assert.True(Tensors.Value.IsSupersetOf(Matrices.M2x2));
            Assert.True(Tensors.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(Tensors.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                Tensors.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(Tensors.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(Tensors.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
