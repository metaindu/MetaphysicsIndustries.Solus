
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
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.SizeFunctionT
{
    [TestFixture]
    public class GetResultTest
    {
        [Test]
        public void ResultForScalarIsVector()
        {
            // given
            var args = new ISet[] { Reals.Value };
            // expect
            var value = SizeFunction.Value;
            Assert.Throws<NotImplementedException>(
                () => value.GetResultType(null, args));
        }

        [Test]
        public void ResultForVectorIsVector()
        {
            // given
            var args = new ISet[] { RealCoordinateSpace.R3 };
            // when
            var value = SizeFunction.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(RealCoordinateSpace.Get(1)));
        }

        [Test]
        public void ResultForMatrixIsVector()
        {
            // given
            var args = new ISet[] { Matrices.M2x3 };
            // when
            var value = SizeFunction.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(RealCoordinateSpace.R2));
        }
    }
}
