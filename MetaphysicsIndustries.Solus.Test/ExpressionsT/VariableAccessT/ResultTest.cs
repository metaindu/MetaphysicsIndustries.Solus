
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
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.VariableAccessT
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void ResultMatchesExpr1()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();
            var mmo = new MockMathObject(isScalar: true);
            var me = new MockExpression(result: mmo);
            env.SetVariable("a", me);
            // precondition
            Assert.IsTrue(mmo.IsScalar(env));
            Assert.IsFalse(mmo.IsVector(env));
            Assert.IsFalse(mmo.IsMatrix(env));
            Assert.That(mmo.GetTensorRank(env), Is.EqualTo(0));
            Assert.IsFalse(mmo.IsString(env));
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.IsTrue(result.IsScalar(env));
            Assert.IsFalse(result.IsVector(env));
            Assert.IsFalse(result.IsMatrix(env));
            Assert.That(result.GetTensorRank(env), Is.EqualTo(0));
            Assert.IsFalse(result.IsString(env));
        }

        [Test]
        public void ResultMatchesExpr2()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();
            var mmo = new MockMathObject(isScalar: false, isMatrix: true,
                tensorRank: 2, dimensions: new[] { 3, 4 });
            var me = new MockExpression(result: mmo);
            env.SetVariable("a", me);
            // precondition
            Assert.IsFalse(mmo.IsScalar(env));
            Assert.IsFalse(mmo.IsVector(env));
            Assert.IsTrue(mmo.IsMatrix(env));
            Assert.That(mmo.GetTensorRank(env), Is.EqualTo(2));
            Assert.That(mmo.GetDimension(env, 0), Is.EqualTo(3));
            Assert.That(mmo.GetDimension(env, 1), Is.EqualTo(4));
            Assert.IsFalse(mmo.IsString(env));
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.IsFalse(result.IsScalar(env));
            Assert.IsFalse(result.IsVector(env));
            Assert.IsTrue(result.IsMatrix(env));
            Assert.That(result.GetTensorRank(env), Is.EqualTo(2));
            Assert.That(result.GetDimension(env, 0), Is.EqualTo(3));
            Assert.That(result.GetDimension(env, 1), Is.EqualTo(4));
            Assert.IsFalse(result.IsString(env));
        }

        [Test]
        public void ResultMatchesExpr3()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();
            var mmo = new MockMathObject(isScalar: false, isMatrix: false,
                tensorRank: 0, isString: true);
            var me = new MockExpression(result: mmo);
            env.SetVariable("a", me);
            // precondition
            Assert.IsFalse(mmo.IsScalar(env));
            Assert.IsFalse(mmo.IsVector(env));
            Assert.IsFalse(mmo.IsMatrix(env));
            Assert.That(mmo.GetTensorRank(env), Is.EqualTo(0));
            Assert.IsTrue(mmo.IsString(env));
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.IsFalse(result.IsScalar(env));
            Assert.IsFalse(result.IsVector(env));
            Assert.IsFalse(result.IsMatrix(env));
            Assert.That(result.GetTensorRank(env), Is.EqualTo(0));
            Assert.IsTrue(result.IsString(env));
        }

        [Test]
        public void MissingVariableYieldsNull()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();  // no "a"
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.IsNull(result.IsScalar(env));
            Assert.IsNull(result.IsVector(env));
            Assert.IsNull(result.IsMatrix(env));
            Assert.IsNull(result.GetTensorRank(env));
            Assert.IsNull(result.IsString(env));
            Assert.IsNull(result.GetDimension(env, 0));
            Assert.IsNull(result.GetDimensions(env));
            Assert.IsNull(result.GetVectorLength(env));
        }
    }
}
