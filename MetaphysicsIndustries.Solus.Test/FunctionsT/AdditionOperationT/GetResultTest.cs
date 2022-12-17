
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

using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.AdditionOperationT
{
    [TestFixture]
    public class GetResultTest
    {
        [Test]
        public void ResultMatchesFirstArg1()
        {
            // given
            var arg1 = 1.ToNumber();
            var arg2 = 1.ToNumber();
            var args = new IMathObject[] { arg1, arg2 };
            // precondition
            Assert.IsTrue(arg1.IsScalar(null));
            Assert.IsFalse(arg1.IsVector(null));
            Assert.IsFalse(arg1.IsMatrix(null));
            Assert.That(arg1.GetTensorRank(null), Is.EqualTo(0));
            Assert.IsFalse(arg1.IsString(null));
            // when
            var value = AdditionOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.IsFalse(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.That(result.GetTensorRank(null), Is.EqualTo(0));
            Assert.IsFalse(result.IsString(null));
        }

        [Test]
        public void ResultMatchesFirstArg2()
        {
            // given
            var arg1 = "abc".ToStringValue();
            var arg2 = "def".ToStringValue();
            var args = new IMathObject[] { arg1, arg2 };
            // precondition
            Assert.IsFalse(arg1.IsScalar(null));
            Assert.IsFalse(arg1.IsVector(null));
            Assert.IsFalse(arg1.IsMatrix(null));
            Assert.That(arg1.GetTensorRank(null), Is.EqualTo(0));
            Assert.IsTrue(arg1.IsString(null));
            // when
            var value = AdditionOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.IsFalse(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.That(result.GetTensorRank(null), Is.EqualTo(0));
            Assert.IsFalse(result.IsString(null));
        }
    }
}
