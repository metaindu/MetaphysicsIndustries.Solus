
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
        public void FloatToNumberYieldsNumber()
        {
            // given
            const float value = 123.45f;
            // when
            var result = value.ToNumber();
            // then
            Assert.AreEqual(123.45f, result.Value);
        }

        [Test]
        public void IntToNumberYieldsNumber()
        {
            // given
            const int value = 123;
            // when
            var result = value.ToNumber();
            // then
            Assert.AreEqual(123f, result.Value);
        }

        [Test]
        public void LongToNumberYieldsNumber()
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
        public void VectorGetMathTypeYieldsVector()
        {
            // given
            var mo = new MockMathObject(false, true,
                false, 1);
            // expect
            Assert.AreEqual(Types.Vector, mo.GetMathType());
        }

        [Test]
        public void MatrixGetMathTypeYieldsMatrix()
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

        [Test]
        public void StringValueToStringValueYieldsStringValue()
        {
            // given
            var mo = new StringValue("abc");
            // when
            var result = mo.ToStringValue();
            // then
            Assert.AreEqual("abc", result.Value);
        }

        [Test]
        public void NonStringValueToStringValueThrows()
        {
            // given
            var mo = new Number(123);
            // expect
            Assert.Throws<InvalidCastException>(() => mo.ToStringValue());
        }

        [Test]
        public void FloatToStringValueYieldsStringValue()
        {
            // given
            const string value = "def";
            // when
            var result = value.ToStringValue();
            // then
            Assert.AreEqual("def", result.Value);
        }

        [Test]
        public void StringValueGetMathTypeYieldsString()
        {
            // given
            var mo = new MockMathObject(false, isString: true);
            // expect
            Assert.AreEqual(Types.String, mo.GetMathType());
        }

        [Test]
        public void ToMathObjects1DConvertsFloats()
        {
            // given
            var values = new[] {1f, 2f, 3f};
            // when
            var result = values.ToMathObjects();
            // then
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(1, result[0].ToNumber().Value);
            Assert.AreEqual(2, result[1].ToNumber().Value);
            Assert.AreEqual(3, result[2].ToNumber().Value);
        }

        [Test]
        public void ToMathObjects2DConvertsFloats()
        {
            // given
            var values = new[,]
            {
                {1f, 2f},
                {3f, 4f},
                {5f, 6f}
            };
            // when
            var result = values.ToMathObjects();
            // then
            Assert.AreEqual(3, result.GetLength(0));
            Assert.AreEqual(2, result.GetLength(1));
            Assert.AreEqual(1, result[0, 0].ToNumber().Value);
            Assert.AreEqual(2, result[0, 1].ToNumber().Value);
            Assert.AreEqual(3, result[1, 0].ToNumber().Value);
            Assert.AreEqual(4, result[1, 1].ToNumber().Value);
            Assert.AreEqual(5, result[2, 0].ToNumber().Value);
            Assert.AreEqual(6, result[2, 1].ToNumber().Value);
        }

        [Test]
        public void FloatArrayToVectorYieldsVector()
        {
            // given
            var values = new float[] {1.2f, 3.4f, 5.6f};
            // when
            var result = values.ToVector();
            // then
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(1.2f,result[0].ToFloat());
            Assert.AreEqual(3.4f,result[1].ToFloat());
            Assert.AreEqual(5.6f,result[2].ToFloat());
        }

        [Test]
        public void IntArrayToVectorYieldsVector()
        {
            // given
            var values = new int[] {1, 2, 3};
            // when
            var result = values.ToVector();
            // then
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(1,result[0].ToFloat());
            Assert.AreEqual(2,result[1].ToFloat());
            Assert.AreEqual(3,result[2].ToFloat());
        }
    }
}
