
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

using System;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ValuesT.MathObjectHelperT
{
    [TestFixture]
    public class MathObjectHelperTest
    {
        [Test]
        public void NumberToNumberYieldsNumber()
        {
            // given
            var mo = new Number(123);
            // when
            var result = mo.ToNumber();
            // then
            Assert.AreEqual(123, result.Value);
        }

        [Test]
        public void NonNumberToNumberThrows()
        {
            // given
            var mo = new MockMathObject();
            // expect
            Assert.Throws<InvalidCastException>(() => mo.ToNumber());
        }

        [Test]
        public void FloatToNumberThrows()
        {
            // given
            const float value = 123.45f;
            // when
            var result = value.ToNumber();
            // then
            Assert.AreEqual(123.45f, result.Value);
        }

        [Test]
        public void IntToNumberThrows()
        {
            // given
            const int value = 123;
            // when
            var result = value.ToNumber();
            // then
            Assert.AreEqual(123f, result.Value);
        }

        [Test]
        public void LongToNumberThrows()
        {
            // given
            const long value = 123L;
            // when
            var result = value.ToNumber();
            // then
            Assert.AreEqual(123f, result.Value);
        }

        [Test]
        public void ScalarGetMathTypeYieldsScalar()
        {
            // given
            var mo = new MockMathObject(true, false,
                false, 0);
            // expect
            Assert.AreEqual(Types.Scalar, mo.GetMathType());
        }

        [Test]
        public void VectorGetMathTypeYieldsScalar()
        {
            // given
            var mo = new MockMathObject(false, true,
                false, 1);
            // expect
            Assert.AreEqual(Types.Vector, mo.GetMathType());
        }

        [Test]
        public void MatrixGetMathTypeYieldsScalar()
        {
            // given
            var mo = new MockMathObject(false, false,
                true, 2);
            // expect
            Assert.AreEqual(Types.Matrix, mo.GetMathType());
        }

        [Test]
        public void OtherGetMathTypeYieldsUnknown()
        {
            // given
            var mo = new MockMathObject(false, false,
                false, 0);
            // expect
            Assert.AreEqual(Types.Unknown, mo.GetMathType());
        }
    }
}
