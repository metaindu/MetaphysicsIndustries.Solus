
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.VectorExpressionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestVectorExpressionsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestVectorExpressionsIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(VectorExpressions.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfSets()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSupersetOf(
                Sets.Sets.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfSets()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(Sets.Functions
                    .RealsToReals));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSubsetOf(Sets.Functions
                    .RealsToReals));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfTensors()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfTensors()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfVectors()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!VectorExpressions.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(
                AllMatrices.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!VectorExpressions.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfReals()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfStrings()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfStrings()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSupersetOf(
                Intervals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsSubsetOfExpressions()
        {
            // expect
            Assert.That(
                VectorExpressions.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.That(!VectorExpressions.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(
                    ComponentAccesses.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(IntervalExpressions
                    .Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSubsetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(
                    TensorExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(
                VectorExpressions.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(
                    MatrixExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsSupersetOfSelf()
        {
            // expect
            Assert.That(
                VectorExpressions.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsSubsetOfSelf()
        {
            // expect
            Assert.That(
                VectorExpressions.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestVectorExpressionsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(
                !VectorExpressions.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
