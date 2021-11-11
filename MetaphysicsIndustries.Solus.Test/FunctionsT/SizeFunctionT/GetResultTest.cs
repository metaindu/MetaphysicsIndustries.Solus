
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
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

using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.SizeFunctionT
{
    [TestFixture]
    public class GetResultTest
    {
        [Test]
        public void ResultForScalarIsVector()
        {
            // given
            var arg1 = 1.ToNumber();
            var args = new IMathObject[] { arg1 };
            // precondition
            Assert.IsTrue(arg1.IsScalar(null));
            Assert.IsFalse(arg1.IsVector(null));
            Assert.IsFalse(arg1.IsMatrix(null));
            Assert.AreEqual(0, arg1.GetTensorRank(null));
            Assert.IsFalse(arg1.IsString(null));
            // when
            var result = SizeFunction.Value.GetResult(args);
            // then
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.AreEqual(1, result.GetTensorRank(null));
            Assert.IsFalse(result.IsString(null));
            // and
            Assert.AreEqual(0, result.GetVectorLength(null));
        }

        [Test]
        public void ResultForVectorIsVector()
        {
            // given
            var arg1 = new Vector(new float[] { 1, 2, 3 });
            var args = new IMathObject[] { arg1 };
            // precondition
            Assert.IsFalse(arg1.IsScalar(null));
            Assert.IsTrue(arg1.IsVector(null));
            Assert.IsFalse(arg1.IsMatrix(null));
            Assert.AreEqual(1, arg1.GetTensorRank(null));
            Assert.IsFalse(arg1.IsString(null));
            // when
            var result = SizeFunction.Value.GetResult(args);
            // then
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.AreEqual(1, result.GetTensorRank(null));
            Assert.IsFalse(result.IsString(null));
            // and
            Assert.AreEqual(1, result.GetVectorLength(null));
        }

        [Test]
        public void ResultForMatrixIsVector()
        {
            // given
            var arg1 = new Matrix(new float[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            var args = new IMathObject[] { arg1 };
            // precondition
            Assert.IsFalse(arg1.IsScalar(null));
            Assert.IsFalse(arg1.IsVector(null));
            Assert.IsTrue(arg1.IsMatrix(null));
            Assert.AreEqual(2, arg1.GetTensorRank(null));
            Assert.IsFalse(arg1.IsString(null));
            // when
            var result = SizeFunction.Value.GetResult(args);
            // then
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.AreEqual(1, result.GetTensorRank(null));
            Assert.IsFalse(result.IsString(null));
            // and
            Assert.AreEqual(2, result.GetVectorLength(null));
        }
    }
}