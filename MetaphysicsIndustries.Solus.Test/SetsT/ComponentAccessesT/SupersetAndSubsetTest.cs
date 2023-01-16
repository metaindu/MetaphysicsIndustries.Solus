
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.ComponentAccessesT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestComponentAccessesIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestComponentAccessesIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(
                ComponentAccesses.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfSets()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfSets()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSupersetOf(Vectors.R2));
            Assert.False(ComponentAccesses.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Vectors.R2));
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(ComponentAccesses.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfReals()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfReals()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsSubsetOfExpressions()
        {
            // expect
            Assert.True(
                ComponentAccesses.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(ComponentAccesses.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestComponentAccessesIsSupersetOfSelf()
        {
            // expect
            Assert.True(
                ComponentAccesses.Value.IsSupersetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestComponentAccessesIsSubsetOfSelf()
        {
            // expect
            Assert.True(
                ComponentAccesses.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(
                    VectorExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestComponentAccessesIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                ComponentAccesses.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
