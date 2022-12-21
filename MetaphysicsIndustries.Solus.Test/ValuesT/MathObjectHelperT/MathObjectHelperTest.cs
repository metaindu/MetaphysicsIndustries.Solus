
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
using MetaphysicsIndustries.Solus.Sets;
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
            Assert.That(result.Value, Is.EqualTo(123));
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
            Assert.That(result.Value, Is.EqualTo(123.45f));
        }

        [Test]
        public void IntToNumberYieldsNumber()
        {
            // given
            const int value = 123;
            // when
            var result = value.ToNumber();
            // then
            Assert.That(result.Value, Is.EqualTo(123f));
        }

        [Test]
        public void LongToNumberYieldsNumber()
        {
            // given
            const long value = 123L;
            // when
            var result = value.ToNumber();
            // then
            Assert.That(result.Value, Is.EqualTo(123f));
        }

        [Test]
        public void ScalarGetMathTypeYieldsScalar()
        {
            // given
            var mo = new MockMathObject(true, false,
                false, 0);
            // expect
            Assert.That(mo.GetMathType2(), Is.SameAs(Reals.Value));
        }

        [Test]
        public void VectorGetMathTypeYieldsVector()
        {
            // given
            var mo = new Vector(new float[] { 1, 2, 3 });
            // expect
            Assert.That(mo.GetMathType2(),
                Is.InstanceOf<Vectors>().Or.InstanceOf<AllVectors>());
        }

        [Test]
        public void MatrixGetMathTypeYieldsMatrix()
        {
            // given
            var mo = new Matrix(new float[,] { { 1, 2 }, { 3, 4 } });
            // expect
            Assert.That(mo.GetMathType2(),
                Is.InstanceOf<Matrices>().Or.InstanceOf<AllMatrices>());
        }

        [Test]
        public void OtherGetMathTypeYieldsUnknown()
        {
            // given
            var mo = new MockMathObject(false, false,
                false, 0);
            // expect
            Assert.Throws<TypeException>(() => mo.GetMathType2());
        }

        [Test]
        public void StringValueToStringValueYieldsStringValue()
        {
            // given
            var mo = new StringValue("abc");
            // when
            var result = mo.ToStringValue();
            // then
            Assert.That(result.Value, Is.EqualTo("abc"));
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
            Assert.That(result.Value, Is.EqualTo("def"));
        }

        [Test]
        public void StringValueGetMathTypeYieldsString()
        {
            // given
            var mo = new MockMathObject(false, isString: true);
            // expect
            Assert.That(mo.GetMathType2(), Is.SameAs(Strings.Value));
        }

        [Test]
        public void ToMathObjects1DConvertsFloats()
        {
            // given
            var values = new[] {1f, 2f, 3f};
            // when
            var result = values.ToMathObjects();
            // then
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result[0].ToNumber().Value, Is.EqualTo(1));
            Assert.That(result[1].ToNumber().Value, Is.EqualTo(2));
            Assert.That(result[2].ToNumber().Value, Is.EqualTo(3));
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
            Assert.That(result.GetLength(0), Is.EqualTo(3));
            Assert.That(result.GetLength(1), Is.EqualTo(2));
            Assert.That(result[0, 0].ToNumber().Value, Is.EqualTo(1));
            Assert.That(result[0, 1].ToNumber().Value, Is.EqualTo(2));
            Assert.That(result[1, 0].ToNumber().Value, Is.EqualTo(3));
            Assert.That(result[1, 1].ToNumber().Value, Is.EqualTo(4));
            Assert.That(result[2, 0].ToNumber().Value, Is.EqualTo(5));
            Assert.That(result[2, 1].ToNumber().Value, Is.EqualTo(6));
        }

        [Test]
        public void FloatArrayToVectorYieldsVector()
        {
            // given
            var values = new float[] {1.2f, 3.4f, 5.6f};
            // when
            var result = values.ToVector();
            // then
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result[0].ToFloat(),
                Is.EqualTo(1.2f));
            Assert.That(result[1].ToFloat(),
                Is.EqualTo(3.4f));
            Assert.That(result[2].ToFloat(),
                Is.EqualTo(5.6f));
        }

        [Test]
        public void IntArrayToVectorYieldsVector()
        {
            // given
            var values = new int[] {1, 2, 3};
            // when
            var result = values.ToVector();
            // then
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result[0].ToFloat(),
                Is.EqualTo(1));
            Assert.That(result[1].ToFloat(),
                Is.EqualTo(2));
            Assert.That(result[2].ToFloat(),
                Is.EqualTo(3));
        }
    }
}
