
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
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
            var args = Array.Empty<IMathObject>();
            var parameters = Array.Empty<Parameter>();
            // expect
            Assert.DoesNotThrow(() =>
                Function.CheckArguments(args, parameters, "displayName"));
        }

        [Test]
        public void TooManyArgsThrows()
        {
            // given
            var args = new IMathObject[] {0.ToNumber()};
            var parameters = Array.Empty<Parameter>();
            // expect
            var ex = Assert.Throws<ArgumentException>(() =>
                Function.CheckArguments(args, parameters, "displayName"));
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong number of arguments given to displayName " +
                    "(expected 0 but got 1)"));
        }

        [Test]
        public void TooFewArgsThrows()
        {
            // given
            var args = Array.Empty<IMathObject>();
            var parameters = new[] { new Parameter("", Reals.Value) };
            // expect
            var ex = Assert.Throws<ArgumentException>(() =>
                Function.CheckArguments(args, parameters, "displayName"));
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong number of arguments given to displayName " +
                    "(expected 1 but got 0)"));
        }

        [Test]
        public void WrongTypeThrows()
        {
            // given
            var args = new IMathObject[] { new Vector3(1, 2, 3) };
            var parameters = new[] { new Parameter("", Reals.Value) };
            // expect
            var ex = Assert.Throws<TypeException>(() =>
                Function.CheckArguments(args, parameters, "displayName"));
            Assert.That(ex.Message,
                Is.EqualTo(
                    "The type was incorrect: Argument 0 wrong type: " +
                    "expected Scalar but got Vector"));
        }
    }
}
