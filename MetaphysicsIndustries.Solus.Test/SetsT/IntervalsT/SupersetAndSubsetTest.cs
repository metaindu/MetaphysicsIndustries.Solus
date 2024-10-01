
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.IntervalsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestIntervalsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestIntervalsIsSubsetOfMathObjects()
        {
            // expect
            Assert.True(Intervals.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfSets()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfSets()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSupersetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfVariadicFunctions()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSubsetOf(VariadicFunctions.RealsToReals));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfTensors()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfTensors()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfVectors()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(Vectors.R2));
            Assert.False(Intervals.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfVectors()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(Vectors.R2));
            Assert.False(Intervals.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(Matrices.M2x2));
            Assert.False(Intervals.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(Matrices.M2x2));
            Assert.False(Intervals.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfReals()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfReals()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfStrings()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfStrings()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestIntervalsIsSupersetOfSelf()
        {
            // expect
            Assert.True(Intervals.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestIntervalsIsSubsetOfSelf()
        {
            // expect
            Assert.True(Intervals.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.False(Intervals.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestIntervalsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.False(
                Intervals.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestIntervalsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.False(Intervals.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
