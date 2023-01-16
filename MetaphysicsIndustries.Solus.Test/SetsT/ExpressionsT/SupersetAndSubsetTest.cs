
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.ExpressionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestExpressionsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestExpressionsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(Sets.Expressions.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSupersetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSupersetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(
                    VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSupersetOf(Vectors.R2));
            Assert.False(Sets.Expressions.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Vectors.R2));
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(Sets.Expressions.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfSelf()
        {
            // expect
            Assert.True(
                Sets.Expressions.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestExpressionsIsSubsetOfSelf()
        {
            // expect
            Assert.True(
                Sets.Expressions.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfLiterals()
        {
            // expect
            Assert.True(Sets.Expressions.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(Sets.Expressions.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfComponentAccesses()
        {
            // expect
            Assert.True(
                Sets.Expressions.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfFunctionCalls()
        {
            // expect
            Assert.True(
                Sets.Expressions.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfIntervalExpressions()
        {
            // expect
            Assert.True(
                Sets.Expressions.Value.IsSupersetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfTensorExpressions()
        {
            // expect
            Assert.True(
                Sets.Expressions.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfMatrixExpressions()
        {
            // expect
            Assert.True(
                Sets.Expressions.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfVectorExpressions()
        {
            // expect
            Assert.True(
                Sets.Expressions.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfVariableAccesses()
        {
            // expect
            Assert.True(
                Sets.Expressions.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(
                Sets.Expressions.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
