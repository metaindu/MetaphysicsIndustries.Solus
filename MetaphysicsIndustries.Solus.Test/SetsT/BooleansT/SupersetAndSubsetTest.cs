
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

namespace MetaphysicsIndustries.Solus.Test.SetsT.BooleansT
{
    [TestFixture]
    public class SupersetAndSubsetTest
    {
        [Test]
        public void TestBooleansIsNotSupersetOfMathObjects()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(MathObjects.Value));
        }

        [Test]
        public void TestBooleansIsSubsetOfMathObjects()
        {
            // expect
            Assert.That(Booleans.Value.IsSubsetOf(MathObjects.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfSets()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfSets()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(Sets.Sets.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfAllFunctions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(AllFunctions.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfAllFunctions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(AllFunctions.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfFunctions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfFunctions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(Sets.Functions.RealsToReals));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfTensors()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(Tensors.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfTensors()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(Tensors.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfAllVectors()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(AllVectors.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfAllVectors()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(AllVectors.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfVectors()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(Vectors.R2));
            Assert.That(!Booleans.Value.IsSupersetOf(Vectors.R3));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfVectors()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(Vectors.R2));
            Assert.That(!Booleans.Value.IsSubsetOf(Vectors.R3));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfAllMatrices()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(AllMatrices.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfAllMatrices()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(AllMatrices.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfMatrices()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(Matrices.M2x2));
            Assert.That(!Booleans.Value.IsSupersetOf(Matrices.M3x3));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfMatrices()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(Matrices.M2x2));
            Assert.That(!Booleans.Value.IsSubsetOf(Matrices.M3x3));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfReals()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(Reals.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfReals()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(Reals.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfStrings()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(Strings.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfStrings()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(Strings.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfIntervals()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(Intervals.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfIntervals()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(Intervals.Value));
        }

        [Test]
        public void TestBooleansIsSupersetOfSelf()
        {
            // expect
            Assert.That(Booleans.Value.IsSupersetOf(Booleans.Value));
        }

        [Test]
        public void TestBooleansIsSubsetOfSelf()
        {
            // expect
            Assert.That(Booleans.Value.IsSubsetOf(Booleans.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfExpressions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfExpressions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(Sets.Expressions.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfLiterals()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(Literals.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfLiterals()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(Literals.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfComponentAccesses()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfComponentAccesses()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(ComponentAccesses.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfFunctionCalls()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfFunctionCalls()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(FunctionCalls.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfIntervalExpressions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfIntervalExpressions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(IntervalExpressions.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfTensorExpressions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfTensorExpressions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(TensorExpressions.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfMatrixExpressions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfMatrixExpressions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(MatrixExpressions.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfVectorExpressions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfVectorExpressions()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(VectorExpressions.Value));
        }

        [Test]
        public void TestBooleansIsNotSupersetOfVariableAccesses()
        {
            // expect
            Assert.That(!Booleans.Value.IsSupersetOf(VariableAccesses.Value));
        }

        [Test]
        public void TestBooleansIsNotSubsetOfVariableAccesses()
        {
            // expect
            Assert.That(!Booleans.Value.IsSubsetOf(VariableAccesses.Value));
        }
    }
}
