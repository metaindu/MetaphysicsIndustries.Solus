
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.StringsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestStringsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestStringsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(Strings.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                Strings.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestStringsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                Strings.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestStringsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Strings.Value.IsSupersetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestStringsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Strings.Value.IsSubsetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestStringsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(Vectors.R2));
            Assert.False(Strings.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestStringsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(Vectors.R2));
            Assert.False(Strings.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestStringsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(Strings.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestStringsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(Strings.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestStringsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestStringsIsSupersetOfSelf()
        {
            // expect
            Assert.True(Strings.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestStringsIsSubsetOfSelf()
        {
            // expect
            Assert.True(Strings.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                Strings.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(Strings.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(Strings.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
