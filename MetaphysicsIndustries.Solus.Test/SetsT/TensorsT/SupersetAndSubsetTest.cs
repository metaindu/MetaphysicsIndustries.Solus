
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
            Assert.That(!Tensors.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestTensorsIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(Tensors.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfSets()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfSets()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestTensorsIsSupersetOfSelf()
        {
            // expect
            Assert.That(Tensors.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestTensorsIsSubsetOfSelf()
        {
            // expect
            Assert.That(Tensors.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestTensorsIsSupersetOfAllVectors()
        {
            // expect
            Assert.That(Tensors.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestTensorsIsSupersetOfVectors()
        {
            // expect
            Assert.That(Tensors.Value.IsSupersetOf(Vectors.R2));
            Assert.That(Tensors.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!Tensors.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestTensorsIsSupersetOfAllMatrices()
        {
            // expect
            Assert.That(Tensors.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestTensorsIsSupersetOfMatrices()
        {
            // expect
            Assert.That(Tensors.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(Tensors.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!Tensors.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfReals()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfStrings()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfStrings()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestTensorsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(!Tensors.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestTensorsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(!Tensors.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
