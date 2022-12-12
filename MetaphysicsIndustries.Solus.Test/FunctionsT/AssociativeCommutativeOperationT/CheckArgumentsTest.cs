
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

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.
    AssociativeCommutativeOperationT
{
    [TestFixture]
    public class CheckArgumentsTest
    {
        [Test]
        public void TooFewArgumentsThrows()
        {
            // given
            var args = new IMathObject[] {1.ToNumber()};
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => AdditionOperation.Value.CheckArguments(args));
            Assert.AreEqual(
                "Wrong number of arguments given to + " +
                "(given 1, require at least 2)",
                ex.Message);
        }

        [Test]
        public void TwoArgumentsOk()
        {
            // given
            var args = new IMathObject[]
            {
                1.ToNumber(),
                2.ToNumber(),
            };
            var paramTypes = new Types[0];
            // expect
            Assert.DoesNotThrow(
                () => AdditionOperation.Value.CheckArguments(args));
        }

        [Test]
        public void MoreThanTwoArgumentsOk()
        {
            // given
            var args = new IMathObject[]
            {
                1.ToNumber(), 2.ToNumber(),
                3.ToNumber(), 4.ToNumber(), 5.ToNumber()
            };
            // expect
            Assert.DoesNotThrow(
                () => AdditionOperation.Value.CheckArguments(args));
        }

        [Test]
        public void WrongTypeThrows()
        {
            // given
            var args = new IMathObject[]
            {
                new MockMathObject(false, true),
                1.ToNumber(),
                2.ToNumber(),
            };
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => AdditionOperation.Value.CheckArguments(args));
            Assert.AreEqual(
                "Argument 0 wrong type: expected Scalar but got Vector",
                ex.Message);
        }
    }
}
