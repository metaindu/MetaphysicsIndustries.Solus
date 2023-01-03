
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.StringsT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestStringsIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestStringsIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(Strings.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfSets()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfSets()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfFunctions()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestStringsIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestStringsIsNotSupersetOfTensors()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfTensors()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfVectors()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!Strings.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestStringsIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!Strings.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestStringsIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfMatrices()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!Strings.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestStringsIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!Strings.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestStringsIsNotSupersetOfReals()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestStringsIsSupersetOfSelf()
        {
            // expect
            Assert.That(Strings.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestStringsIsSubsetOfSelf()
        {
            // expect
            Assert.That(Strings.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfIntervals()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfBooleans()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfBooleans()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfExpressions()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfExpressions()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfLiterals()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfLiterals()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestStringsIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(!Strings.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestStringsIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(!Strings.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
