
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

namespace MetaphysicsIndustries.Solus.Test.ValuesT.VectorT
{
    [TestFixture]
    public class VectorTest
    {
        [Test]
        public void CreateSetsComponents()
        {
            // given
            IMathObject[] values = new IMathObject[] {1.ToNumber(),
                2.ToNumber(), 3.ToNumber()};
            // when
            var result = new Vector(values);
            // then
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result.ComponentType, Is.SameAs(Reals.Value));
            Assert.That(result[0].ToNumber().Value, Is.EqualTo(1));
            Assert.That(result[1].ToNumber().Value, Is.EqualTo(2));
            Assert.That(result[2].ToNumber().Value, Is.EqualTo(3));
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsTrue(result.IsVector(null));
            Assert.IsFalse(result.IsMatrix(null));
            Assert.That(result.GetTensorRank(null), Is.EqualTo(1));
            Assert.IsFalse(result.IsString(null));
            Assert.That(result.GetDimension(null, 0), Is.EqualTo(3));
            Assert.IsNull(result.GetDimension(null, -1));
            Assert.IsNull(result.GetDimension(null, 2));
            Assert.That(result.GetDimensions(null), Is.EqualTo(new[] {3}));
        }

        [Test]
        public void CreateWithFloatsSetsComponents()
        {
            // given
            var values = new[] {1f, 2f, 3f};
            // when
            var result = new Vector(values);
            // then
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result[0].ToNumber().Value, Is.EqualTo(1));
            Assert.That(result[1].ToNumber().Value, Is.EqualTo(2));
            Assert.That(result[2].ToNumber().Value, Is.EqualTo(3));
        }

        [Test]
        public void NonRealComponentsThrows()
        {
            // given
            var values = new IMathObject[]
            {
                1.ToNumber(),
                "abc".ToStringValue()
            };
            // expect
            var ex = Assert.Throws<TypeException>(
                () => new Vector(values));
            // and
            Assert.That(ex.Message,
                Is.EqualTo("The type was incorrect: " +
                           "All components must be reals"));
        }

        [Test]
        public void ToStringProducesSimpleStringRepresentation()
        {
            // given
            var v = new Vector(new IMathObject[]
                {
                    1.ToNumber(), 2.ToNumber(), 3.ToNumber()
                });
            // when
            var result = v.ToString();
            // then
            Assert.That(result, Is.EqualTo("[1, 2, 3]"));
        }

        [Test]
        public void VectorIsEqualToVector2()
        {
            // given
            var a = new Vector(new float[] { 2, 3 });
            var b = new Vector2(2, 3);
            // expect
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.That(a.Equals(b));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.That(b.Equals(a));
#pragma warning disable NUnit2021
            Assert.That(a, Is.EqualTo(b));
            Assert.That(b, Is.EqualTo(a));
#pragma warning restore NUnit2021
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            Assert.That(b.GetHashCode(), Is.EqualTo(a.GetHashCode()));
        }

        [Test]
        public void VectorIsEqualToVector3()
        {
            // given
            var a = new Vector(new float[] { 5, 7, 11 });
            var b = new Vector3(5, 7, 11);
            // expect
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.That(a.Equals(b));
            // ReSharper disable once SuspiciousTypeConversion.Global
            Assert.That(b.Equals(a));
#pragma warning disable NUnit2021
            Assert.That(a, Is.EqualTo(b));
            Assert.That(b, Is.EqualTo(a));
#pragma warning restore NUnit2021
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
            Assert.That(b.GetHashCode(), Is.EqualTo(a.GetHashCode()));
        }
    }
}
