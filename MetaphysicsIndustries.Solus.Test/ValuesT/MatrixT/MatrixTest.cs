
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

namespace MetaphysicsIndustries.Solus.Test.ValuesT.MatrixT
{
    [TestFixture]
    public class MatrixTest
    {

        [Test]
        public void CreateSetsComponents()
        {
            // given
            var values = new IMathObject[,]
            {
                {1.ToNumber(), 2.ToNumber()},
                {3.ToNumber(), 4.ToNumber()}
            };
            // when
            var result = new Matrix(values);
            // then
            Assert.AreEqual(2, result.RowCount);
            Assert.AreEqual(2, result.ColumnCount);
            Assert.AreEqual(Types.Scalar, result.ComponentType);
            Assert.AreEqual(1, result[0, 0].ToNumber().Value);
            Assert.AreEqual(2, result[0, 1].ToNumber().Value);
            Assert.AreEqual(3, result[1, 0].ToNumber().Value);
            Assert.AreEqual(4, result[1, 1].ToNumber().Value);
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsFalse(result.IsVector(null));
            Assert.IsTrue(result.IsMatrix(null));
            Assert.AreEqual(2, result.GetTensorRank(null));
            Assert.IsFalse(result.IsString(null));
            Assert.AreEqual(2, result.GetDimension(null, 0));
            Assert.AreEqual(2, result.GetDimension(null, 1));
            var ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => result.GetDimension(null, -1));
            Assert.AreEqual("index", ex.ParamName);
            Assert.AreEqual("Index must not be negative\n" +
                            "Parameter name: index",
                ex.Message);
            ex = Assert.Throws<ArgumentOutOfRangeException>(
                () => result.GetDimension(null, 3));
            Assert.AreEqual("index", ex.ParamName);
            Assert.AreEqual("Matrices only have two dimensions\n" +
                            "Parameter name: index",
                ex.Message);
            Assert.AreEqual(new[] {2, 2}, result.GetDimensions(null));
        }

        [Test]
        public void CreateWithFloatsSetsComponents()
        {
            // given
            var values = new[,]
            {
                {1f, 2f},
                {3f, 4f}
            };
            // when
            var result = new Matrix(values);
            // then
            Assert.AreEqual(2, result.RowCount);
            Assert.AreEqual(2, result.ColumnCount);
            Assert.AreEqual(1, result[0, 0].ToNumber().Value);
            Assert.AreEqual(2, result[0, 1].ToNumber().Value);
            Assert.AreEqual(3, result[1, 0].ToNumber().Value);
            Assert.AreEqual(4, result[1, 1].ToNumber().Value);
        }

        [Test]
        public void RowCountAndColumnCountAreIndependent()
        {
            // given
            var values = new[,]
            {
                {1f, 2f},
                {3f, 4f},
                {5f, 6f}
            };
            // when
            var result = new Matrix(values);
            // then
            Assert.AreEqual(3, result.RowCount);
            Assert.AreEqual(2, result.ColumnCount);
            Assert.AreEqual(1, result[0, 0].ToNumber().Value);
            Assert.AreEqual(2, result[0, 1].ToNumber().Value);
            Assert.AreEqual(3, result[1, 0].ToNumber().Value);
            Assert.AreEqual(4, result[1, 1].ToNumber().Value);
            Assert.AreEqual(5, result[2, 0].ToNumber().Value);
            Assert.AreEqual(6, result[2, 1].ToNumber().Value);
        }

        [Test]
        public void DifferentComponentTypesYieldsMixed()
        {
            // given
            var values = new IMathObject[,]
            {
                {1.ToNumber(), 2.ToNumber()},
                {3.ToNumber(), "four".ToStringValue()}
            };
            // when
            var result = new Matrix(values);
            // then
            Assert.AreEqual(Types.Mixed, result.ComponentType);
        }

        [Test]
        public void TestToString()
        {
            // given
            var value = new Matrix(new IMathObject[,]
            {
                {1.ToNumber(), 2.ToNumber()},
                {3.ToNumber(), 4.ToNumber()},
                {5.ToNumber(), 6.ToNumber()}
            });
            // when
            var result = value.ToString();
            // then
            Assert.AreEqual("[1, 2; 3, 4; 5, 6]", result);
        }

        [Test]
        public void TestToStringComplex()
        {
            // given
            var value = new Matrix(new IMathObject[,]
            {
                {
                    "one".ToStringValue(),
                    new Vector(new float[] {2, 2, 2})
                },
                {
                    3.ToNumber(),
                    new Matrix(new float[,]
                    {
                        {4, 5},
                        {6, 7}
                    })
                }
            });
            // when
            var result = value.ToString();
            // then
            Assert.AreEqual("[\"one\", [2, 2, 2]; 3, [4, 5; 6, 7]]",
                result);
        }
    }
}
