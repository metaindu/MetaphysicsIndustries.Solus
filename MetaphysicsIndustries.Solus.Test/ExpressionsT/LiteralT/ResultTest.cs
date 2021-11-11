
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

using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.LiteralT
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void ResultMatchesValue1()
        {
            // given
            var value = 1.ToNumber();
            var expr = new Literal(value);
            var env = new SolusEnvironment();
            // precondition
            Assert.IsTrue(value.IsScalar(env));
            Assert.IsFalse(value.IsVector(env));
            Assert.IsFalse(value.IsMatrix(env));
            Assert.AreEqual(0, value.GetTensorRank(env));
            Assert.IsFalse(value.IsString(env));
            // when
            var result = expr.Result;
            // then
            Assert.IsTrue(result.IsScalar(env));
            Assert.IsFalse(result.IsVector(env));
            Assert.IsFalse(result.IsMatrix(env));
            Assert.AreEqual(0, result.GetTensorRank(env));
            Assert.IsFalse(result.IsString(env));
        }

        [Test]
        public void ResultMatchesValue2()
        {
            // given
            var value = new Matrix(
                new float[,]
                {
                    { 1, 2, 3, 4 },
                    { 5, 6, 7, 8 },
                    { 9, 10, 11, 12 }

                });
            var expr = new Literal(value);
            var env = new SolusEnvironment();
            // precondition
            Assert.IsFalse(value.IsScalar(env));
            Assert.IsFalse(value.IsVector(env));
            Assert.IsTrue(value.IsMatrix(env));
            Assert.AreEqual(2, value.GetTensorRank(env));
            Assert.AreEqual(3, value.GetDimension(env, 0));
            Assert.AreEqual(4, value.GetDimension(env, 1));
            Assert.IsFalse(value.IsString(env));
            // when
            var result = expr.Result;
            // then
            Assert.IsFalse(result.IsScalar(env));
            Assert.IsFalse(result.IsVector(env));
            Assert.IsTrue(result.IsMatrix(env));
            Assert.AreEqual(2, result.GetTensorRank(env));
            Assert.AreEqual(3, result.GetDimension(env, 0));
            Assert.AreEqual(4, result.GetDimension(env, 1));
            Assert.IsFalse(result.IsString(env));
        }

        [Test]
        public void ResultMatchesValue3()
        {
            // given
            var value = "asdf".ToStringValue();
            var expr = new Literal(value);
            var env = new SolusEnvironment();
            // precondition
            Assert.IsFalse(value.IsScalar(env));
            Assert.IsFalse(value.IsVector(env));
            Assert.IsFalse(value.IsMatrix(env));
            Assert.AreEqual(0, value.GetTensorRank(env));
            Assert.IsTrue(value.IsString(env));
            // when
            var result = expr.Result;
            // then
            Assert.IsFalse(result.IsScalar(env));
            Assert.IsFalse(result.IsVector(env));
            Assert.IsFalse(result.IsMatrix(env));
            Assert.AreEqual(0, result.GetTensorRank(env));
            Assert.IsTrue(result.IsString(env));
        }
    }
}