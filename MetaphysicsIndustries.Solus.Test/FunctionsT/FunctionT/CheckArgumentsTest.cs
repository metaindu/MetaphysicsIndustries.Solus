
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

using System;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.FunctionT
{
    [TestFixture]
    public class CheckArgumentsTest
    {
        [Test]
        public void TestZeroArgumentsZeroParamsOk()
        {
            // given
            var args = new IMathObject[0];
            var paramTypes = new Types[0];
            // expect
            Assert.DoesNotThrow(() =>
                Function.CheckArguments(args, paramTypes, "displayName"));
        }

        [Test]
        public void TooManyArgsThrows()
        {
            // given
            var args = new IMathObject[] {0.ToNumber()};
            var paramTypes = new Types[0];
            // expect
            var ex = Assert.Throws<ArgumentException>(() =>
                Function.CheckArguments(args, paramTypes, "displayName"));
            Assert.AreEqual(
                "Wrong number of arguments given to displayName " +
                "(expected 0 but got 1)",
                ex.Message);
        }

        [Test]
        public void TooFewArgsThrows()
        {
            // given
            var args = new IMathObject[0];
            var paramTypes = new[] {Types.Scalar};
            // expect
            var ex = Assert.Throws<ArgumentException>(() =>
                Function.CheckArguments(args, paramTypes, "displayName"));
            Assert.AreEqual(
                "Wrong number of arguments given to displayName " +
                "(expected 1 but got 0)",
                ex.Message);
        }

        [Test]
        public void WrongTypeThrows()
        {
            // given
            var args = new IMathObject[] {new MockMathObject(false, true)};
            var paramTypes = new[] {Types.Scalar};
            // expect
            var ex = Assert.Throws<ArgumentException>(() =>
                Function.CheckArguments(args, paramTypes, "displayName"));
            Assert.AreEqual(
                $"Argument 0 wrong type: expected Scalar but got Vector",
                ex.Message);
        }
    }
}
