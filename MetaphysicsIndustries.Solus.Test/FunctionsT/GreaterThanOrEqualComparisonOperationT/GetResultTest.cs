
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

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.
    GreaterThanOrEqualComparisonOperationT
{
    [TestFixture]
    public class GetResultTest
    {
        [Test]
        public void ResultIsScalar()
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
            var result =
                GreaterThanOrEqualComparisonOperation.Value.GetResult(args);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.IsFalse(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.AreEqual(0, result.GetTensorRank(null));
            Assert.IsFalse(result.IsString(null));
        }
    }
}
