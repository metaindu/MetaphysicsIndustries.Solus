
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.LiteralsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestLiteralsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestLiteralsIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(Literals.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfSets()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfSets()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.That(
                !Literals.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(
                !Literals.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfTensors()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfTensors()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfVectors()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!Literals.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!Literals.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!Literals.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!Literals.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfReals()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfStrings()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfStrings()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestLiteralsIsSubsetOfExpressions()
        {
            // expect
            Assert.That(Literals.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestLiteralsIsSupersetOfSelf()
        {
            // expect
            Assert.That(Literals.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestLiteralsIsSubsetOfSelf()
        {
            // expect
            Assert.That(Literals.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(
                !Literals.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestLiteralsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(!Literals.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestLiteralsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(!Literals.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
