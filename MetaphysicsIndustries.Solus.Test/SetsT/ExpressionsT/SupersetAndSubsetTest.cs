
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.ExpressionsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestExpressionsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestExpressionsIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(Sets.Expressions.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfSets()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfSets()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(
                AllFunctions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSupersetOf(
                    Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSubsetOf(Sets.Functions
                    .RealsToReals));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfTensors()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfTensors()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(
                AllVectors.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfVectors()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfReals()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfStrings()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfStrings()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestExpressionsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfSelf()
        {
            // expect
            Assert.That(
                Sets.Expressions.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestExpressionsIsSubsetOfSelf()
        {
            // expect
            Assert.That(
                Sets.Expressions.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfLiterals()
        {
            // expect
            Assert.That(Sets.Expressions.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.That(!Sets.Expressions.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfComponentAccesses()
        {
            // expect
            Assert.That(
                Sets.Expressions.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(
                Sets.Expressions.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                Sets.Expressions.Value.IsSupersetOf(
                    IntervalExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(
                Sets.Expressions.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfMatrixExpressions()
        {
            // expect
            Assert.That(
                Sets.Expressions.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfVectorExpressions()
        {
            // expect
            Assert.That(
                Sets.Expressions.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestExpressionsIsSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(
                Sets.Expressions.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestExpressionsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(
                !Sets.Expressions.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
