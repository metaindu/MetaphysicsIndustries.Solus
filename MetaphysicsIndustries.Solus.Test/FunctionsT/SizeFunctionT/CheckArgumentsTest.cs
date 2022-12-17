
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

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.SizeFunctionT
{
    [TestFixture]
    public class CheckArgumentsTest
    {
        [Test]
        public void CheckArgumentsZeroArgumentsThrows()
        {
            // given
            var args = new IMathObject[0];
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => SizeFunction.Value.CheckArguments(args));
            // and
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong number of arguments given to " +
                    "size (expected 1 but got 0)"));
        }

        [Test]
        public void CheckArgumentsTwoArgumentsThrows()
        {
            // given
            var args = new IMathObject[]
            {
                new Vector(new float[] {1, 2, 3}),
                new Vector(new float[] {4, 5, 6})
            };
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => SizeFunction.Value.CheckArguments(args));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo(
                    "Wrong number of arguments given to " +
                    "size (expected 1 but got 2)"));
        }

        [Test]
        public void CheckArgumentsNonTensorThrows()
        {
            // given
            var args = new IMathObject[] {1.ToNumber()};
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => SizeFunction.Value.CheckArguments(args));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo(
                    "Argument wrong type: expected " +
                    "Vector or Matrix or String but got Scalar"));
        }

        [Test]
        public void CheckArgumentsVectorIsAllowed()
        {
            // given
            var args = new IMathObject[]
            {
                new Vector(new float[] {1, 2})
            };
            // expect
            Assert.DoesNotThrow(
                () => SizeFunction.Value.CheckArguments(args));
        }

        [Test]
        public void CheckArgumentsMatrixIsAllowed()
        {
            // given
            var args = new IMathObject[]
            {
                new Matrix(new float[,]
                {
                    {1, 2},
                    {3, 4}
                })
            };
            // expect
            Assert.DoesNotThrow(
                () => SizeFunction.Value.CheckArguments(args));
        }

        [Test]
        public void CheckArgumentsStringIsAllowed()
        {
            // given
            var args = new IMathObject[] {"abc".ToStringValue()};
            // expect
            Assert.DoesNotThrow(
                () => SizeFunction.Value.CheckArguments(args));
        }
    }
}
