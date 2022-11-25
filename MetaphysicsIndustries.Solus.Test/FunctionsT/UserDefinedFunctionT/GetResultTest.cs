
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
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.UserDefinedFunctionT
{
    [TestFixture]
    public class GetResultTest
    {
        [Test]
        public void ResultMatchesDefinedFunction1()
        {
            // given
            var expr = new Literal(1.ToNumber());
            var f = new UserDefinedFunction("f", new string[0], expr);
            // precondition
            Assert.IsTrue(expr.GetResultType(null).IsScalar(null));
            Assert.IsFalse(expr.GetResultType(null).IsVector(null));
            Assert.IsFalse(expr.GetResultType(null).IsMatrix(null));
            Assert.AreEqual(0, expr.GetResultType(null).GetTensorRank(null));
            Assert.IsFalse(expr.GetResultType(null).IsString(null));
            // when
            var result = f.GetResult(new IMathObject[0]);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.IsFalse(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.AreEqual(0, result.GetTensorRank(null));
            Assert.IsFalse(result.IsString(null));
        }

        [Test]
        public void ResultMatchesDefinedFunction2()
        {
            // given
            var expr = new Literal(new Vector(new float[] { 1, 2, 3 }));
            var f = new UserDefinedFunction("f", new string[0], expr);
            // precondition
            Assert.IsFalse(expr.GetResultType(null).IsScalar(null));
            Assert.IsTrue(expr.GetResultType(null).IsVector(null));
            Assert.IsFalse(expr.GetResultType(null).IsMatrix(null));
            Assert.AreEqual(1, expr.GetResultType(null).GetTensorRank(null));
            Assert.IsFalse(expr.GetResultType(null).IsString(null));
            // when
            var result = f.GetResult(new IMathObject[0]);
            // then
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.AreEqual(1, result.GetTensorRank(null));
            Assert.IsFalse(result.IsString(null));
        }
    }
}
