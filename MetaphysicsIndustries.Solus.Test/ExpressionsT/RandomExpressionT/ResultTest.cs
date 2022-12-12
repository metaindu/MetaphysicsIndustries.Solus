
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

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.RandomExpressionT
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void ResultIsScalar()
        {
            // given
            var expr = new RandomExpression();
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.IsTrue(result.IsScalar(env));
            Assert.IsFalse(result.IsVector(env));
            Assert.IsFalse(result.IsMatrix(env));
            Assert.AreEqual(0, result.GetTensorRank(env));
            Assert.IsFalse(result.IsString(env));
            Assert.IsNull(result.GetDimension(env, 0));
            Assert.IsNull(result.GetDimensions(env));
            Assert.IsNull(result.GetVectorLength(env));
        }
    }
}
