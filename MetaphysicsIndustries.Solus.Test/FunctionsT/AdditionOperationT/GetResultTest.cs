
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

using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.AdditionOperationT
{
    [TestFixture]
    public class GetResultTest
    {
        [Test]
        public void ResultIsReal()
        {
            // given
            var args = new ISet[] { Reals.Value, Reals.Value };
            // when
            var value = AdditionOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Reals.Value));
        }

        [Test]
        public void ResultIsString()
        {
            // given
            var args = new ISet[] { Strings.Value, Strings.Value };
            // when
            var value = AdditionOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Strings.Value));
        }

        [Test]
        public void ResultIsVector1()
        {
            // given
            var args = new ISet[] { Vectors.R2, Vectors.R2 };
            // when
            var value = AdditionOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Vectors.R2));
        }

        [Test]
        public void ResultIsVector2()
        {
            // given
            var args = new ISet[] { Vectors.R3, Vectors.R3 };
            // when
            var value = AdditionOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Vectors.R3));
        }

        [Test]
        public void ResultIsMatrix1()
        {
            // given
            var args = new ISet[] { Matrices.M2x2, Matrices.M2x2 };
            // when
            var value = AdditionOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Matrices.M2x2));
        }

        [Test]
        public void ResultIsMatrix2()
        {
            // given
            var args = new ISet[] { Matrices.M3x3, Matrices.M3x3 };
            // when
            var value = AdditionOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Matrices.M3x3));
        }
    }
}
