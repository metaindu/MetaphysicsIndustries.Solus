
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.MathObjectsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestMathObjectsIsSupersetOfSelf()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestMathObjectsIsSubsetOfSelf()
        {
            // expect
            Assert.That(MathObjects.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfSets()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfSets()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfAllFunctions()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfFunctions()
        {
            // expect
            Assert.That(
                MathObjects.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(
                !MathObjects.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfTensors()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfTensors()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfAllVectors()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfVectors()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(Vectors.R2));
            Assert.That(MathObjects.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!MathObjects.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfAllMatrices()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfMatrices()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(MathObjects.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!MathObjects.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfReals()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfStrings()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfStrings()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfIntervals()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfBooleans()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfExpressions()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(
                Sets.Expressions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfLiterals()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfComponentAccesses()
        {
            // expect
            Assert.That(
                MathObjects.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(
                ComponentAccesses.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                MathObjects.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !MathObjects.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(
                MathObjects.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(
                TensorExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfMatrixExpressions()
        {
            // expect
            Assert.That(
                MathObjects.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(
                MatrixExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfVectorExpressions()
        {
            // expect
            Assert.That(
                MathObjects.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(
                VectorExpressions.Value));
        }

        [Test]
        public void TestMathObjectsIsSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(MathObjects.Value.IsSupersetOf(
                VariableAccesses.Value));
        }

        [Test]
        public void TestMathObjectsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(!MathObjects.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
