
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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

using MetaphysicsIndustries.Solus.Sets;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.SetsT.FunctionsT
{
    [TestFixture]
    public class DocStringTest
    {
        [Test]
        public void FormatDocStringNoParametersYieldsDocString()
        {
            // given
            var parameterTypes = new ISet[] { };
            // when
            var result = Sets.Functions.FormatDocString(
                Reals.Value, parameterTypes);
            // then
            Assert.That(result,
                Is.EqualTo(
                    "The set of all functions taking no arguments and " +
                    "returning Scalar"));
        }

        [Test]
        public void FormatDocStringOneParameterYieldsDocString()
        {
            // given
            var parameterTypes = new ISet[] { Reals.Value };
            // when
            var result = Sets.Functions.FormatDocString(
                Reals.Value, parameterTypes);
            // then
            Assert.That(result,
                Is.EqualTo(
                    "The set of all functions taking Scalar and " +
                    "returning Scalar"));
        }

        [Test]
        public void FormatDocStringTwoParameterYieldsDocString()
        {
            // given
            var parameterTypes = new ISet[] { Reals.Value, Reals.Value };
            // when
            var result = Sets.Functions.FormatDocString(
                Reals.Value, parameterTypes);
            // then
            Assert.That(result,
                Is.EqualTo(
                    "The set of all functions taking Scalar and Scalar and " +
                    "returning Scalar"));
        }

        [Test]
        public void FormatDocStringThreeParameterYieldsDocString()
        {
            // given
            var parameterTypes = new ISet[]
                { Reals.Value, Reals.Value, Reals.Value };
            // when
            var result = Sets.Functions.FormatDocString(
                Reals.Value, parameterTypes);
            // then
            Assert.That(result,
                Is.EqualTo(
                    "The set of all functions taking Scalar, Scalar and " +
                    "Scalar and returning Scalar"));
        }

        [Test]
        public void FormatDocStringFourParameterYieldsDocString()
        {
            // given
            var parameterTypes = new ISet[]
                { Reals.Value, Reals.Value, Reals.Value, Reals.Value };
            // when
            var result = Sets.Functions.FormatDocString(
                Reals.Value, parameterTypes);
            // then
            Assert.That(result,
                Is.EqualTo(
                    "The set of all functions taking Scalar, Scalar, " +
                    "Scalar and Scalar and returning Scalar"));
        }
    }
}
