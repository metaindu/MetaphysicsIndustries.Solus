
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.RealsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestRealsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestRealsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(Reals.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                Reals.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestRealsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestRealsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Reals.Value.IsSupersetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestRealsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Reals.Value.IsSubsetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestRealsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(Vectors.R2));
            Assert.False(Reals.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestRealsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(Vectors.R2));
            Assert.False(Reals.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestRealsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(Reals.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestRealsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(Reals.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestRealsIsSupersetOfSelf()
        {
            // expect
            Assert.True(Reals.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestRealsIsSubsetOfSelf()
        {
            // expect
            Assert.True(Reals.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestRealsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(Reals.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestRealsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(Reals.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
