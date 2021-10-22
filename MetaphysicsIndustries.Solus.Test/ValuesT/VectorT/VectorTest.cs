
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
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(Types.Scalar, result.ComponentType);
            Assert.AreEqual(1, result[0].ToNumber().Value);
            Assert.AreEqual(2, result[1].ToNumber().Value);
            Assert.AreEqual(3, result[2].ToNumber().Value);
            Assert.IsFalse(result.IsScalar());
            Assert.IsTrue(result.IsVector());
            Assert.IsFalse(result.IsMatrix());
            Assert.AreEqual(1, result.GetTensorRank());
            Assert.IsFalse(result.IsString());
            Assert.AreEqual(3, result.GetDimension(0));
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => result.GetDimension(-1));
            Assert.AreEqual("index", ex.ParamName);
            Assert.AreEqual("Index must not be negative\n" +
                            "Parameter name: index",
                ex.Message);
            ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => result.GetDimension(2));
            Assert.AreEqual("index", ex.ParamName);
            Assert.AreEqual("Vectors only have a single dimension\n" +
                            "Parameter name: index",
                ex.Message);
            Assert.AreEqual(new[] {3}, result.GetDimensions());
        }

        [Test]
        public void CreateWithFloatsSetsComponents()
        {
            // given
            var values = new[] {1f, 2f, 3f};
            // when
            var result = new Vector(values);
            // then
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(1, result[0].ToNumber().Value);
            Assert.AreEqual(2, result[1].ToNumber().Value);
            Assert.AreEqual(3, result[2].ToNumber().Value);
        }

        [Test]
        public void DifferentComponentTypesYieldsMixed()
        {
            // given
            var values = new IMathObject[]
            {
                1.ToNumber(),
                "abc".ToStringValue()
            };
            // when
            var result = new Vector(values);
            // then
            Assert.AreEqual(Types.Mixed, result.ComponentType);
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
            Assert.AreEqual("[1, 2, 3]", result);
        }

        [Test]
        public void ComplexVectorYieldsComplexStringRepresentation()
        {
            // given
            var v = new Vector(new IMathObject[]
            {
                1.ToNumber(),
                "two".ToStringValue(),
                new Vector(new IMathObject[]
                {
                    "t".ToStringValue(),
                    "h".ToStringValue(),
                    "r".ToStringValue(),
                    "e".ToStringValue(),
                    "e".ToStringValue(),
                })
            });
            // when
            var result = v.ToString();
            // then
            Assert.AreEqual(
                "[1, \"two\", [\"t\", \"h\", \"r\", \"e\", \"e\"]]",
                result);
        }
    }
}
