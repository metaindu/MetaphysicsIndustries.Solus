
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

using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.FunctionCallT
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void ResultMatchesFunction1()
        {
            // given
            var paramTypes = new[] { Types.Scalar };
            var f = new MockFunction(paramTypes, "f");
            var mmo = new MockMathObject(isScalar: true);
            f.GetResultF = args => mmo;
            var expr = new FunctionCall(f);
            var env = new SolusEnvironment();
            // precondition
            Assert.IsTrue(mmo.IsScalar(env));
            Assert.IsFalse(mmo.IsVector(env));
            Assert.IsFalse(mmo.IsMatrix(env));
            Assert.AreEqual(0, mmo.GetTensorRank(env));
            Assert.IsFalse(mmo.IsString(env));
            Assert.IsFalse(mmo.IsInterval(env));
            Assert.IsFalse(mmo.IsFunction(env));
            Assert.IsFalse(mmo.IsExpression(env));
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.IsTrue(result.IsScalar(env));
            Assert.IsFalse(result.IsVector(env));
            Assert.IsFalse(result.IsMatrix(env));
            Assert.AreEqual(0, result.GetTensorRank(env));
            Assert.IsFalse(result.IsString(env));
            Assert.IsFalse(result.IsInterval(env));
            Assert.IsFalse(result.IsFunction(env));
            Assert.IsFalse(result.IsExpression(env));
        }

        [Test]
        public void ResultMatchesFunction2()
        {
            // given
            var paramTypes = new[] { Types.Scalar };
            var f = new MockFunction(paramTypes, "f");
            var mmo = new MockMathObject(isScalar: false, isMatrix: true,
                tensorRank: 2, dimensions: new[] { 3, 4 });
            f.GetResultF = args => mmo;
            var expr = new FunctionCall(f);
            var env = new SolusEnvironment();
            // precondition
            Assert.IsFalse(mmo.IsScalar(env));
            Assert.IsFalse(mmo.IsVector(env));
            Assert.IsTrue(mmo.IsMatrix(env));
            Assert.AreEqual(2, mmo.GetTensorRank(env));
            Assert.AreEqual(3, mmo.GetDimension(env, 0));
            Assert.AreEqual(4, mmo.GetDimension(env, 1));
            Assert.IsFalse(mmo.IsString(env));
            Assert.IsFalse(mmo.IsInterval(env));
            Assert.IsFalse(mmo.IsFunction(env));
            Assert.IsFalse(mmo.IsExpression(env));
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.IsFalse(result.IsScalar(env));
            Assert.IsFalse(result.IsVector(env));
            Assert.IsTrue(result.IsMatrix(env));
            Assert.AreEqual(2, result.GetTensorRank(env));
            Assert.AreEqual(3, result.GetDimension(env, 0));
            Assert.AreEqual(4, result.GetDimension(env, 1));
            Assert.IsFalse(result.IsString(env));
            Assert.IsFalse(result.IsInterval(env));
            Assert.IsFalse(result.IsFunction(env));
            Assert.IsFalse(result.IsExpression(env));
        }
    }
}
