
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
            Assert.That(result.RowCount, Is.EqualTo(2));
            Assert.That(result.ColumnCount, Is.EqualTo(2));
            Assert.That(result.ComponentType, Is.SameAs(Reals.Value));
            Assert.That(result[0, 0].ToNumber().Value, Is.EqualTo(1));
            Assert.That(result[0, 1].ToNumber().Value, Is.EqualTo(2));
            Assert.That(result[1, 0].ToNumber().Value, Is.EqualTo(3));
            Assert.That(result[1, 1].ToNumber().Value, Is.EqualTo(4));
            Assert.IsFalse(result.IsScalar(null));
            Assert.IsFalse(result.IsVector(null));
            Assert.IsTrue(result.IsMatrix(null));
            Assert.That(result.GetTensorRank(null), Is.EqualTo(2));
            Assert.IsFalse(result.IsString(null));
            Assert.That(result.GetDimension(null, 0), Is.EqualTo(2));
            Assert.That(result.GetDimension(null, 1), Is.EqualTo(2));
            Assert.IsNull(result.GetDimension(null, -1));
            Assert.IsNull(result.GetDimension(null, 3));
            Assert.That(result.GetDimensions(null), Is.EqualTo(new[] {2, 2}));
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
            Assert.That(result.RowCount, Is.EqualTo(2));
            Assert.That(result.ColumnCount, Is.EqualTo(2));
            Assert.That(result[0, 0].ToNumber().Value, Is.EqualTo(1));
            Assert.That(result[0, 1].ToNumber().Value, Is.EqualTo(2));
            Assert.That(result[1, 0].ToNumber().Value, Is.EqualTo(3));
            Assert.That(result[1, 1].ToNumber().Value, Is.EqualTo(4));
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
            Assert.That(result.RowCount, Is.EqualTo(3));
            Assert.That(result.ColumnCount, Is.EqualTo(2));
            Assert.That(result[0, 0].ToNumber().Value, Is.EqualTo(1));
            Assert.That(result[0, 1].ToNumber().Value, Is.EqualTo(2));
            Assert.That(result[1, 0].ToNumber().Value, Is.EqualTo(3));
            Assert.That(result[1, 1].ToNumber().Value, Is.EqualTo(4));
            Assert.That(result[2, 0].ToNumber().Value, Is.EqualTo(5));
            Assert.That(result[2, 1].ToNumber().Value, Is.EqualTo(6));
        }

        [Test]
        public void NonRealComponentsThrows()
        {
            // given
            var values = new IMathObject[,]
            {
                {1.ToNumber(), 2.ToNumber()},
                {3.ToNumber(), "four".ToStringValue()}
            };
            // expect
            var ex = Assert.Throws<TypeException>(
                () => new Matrix(values));
            // and
            Assert.That(ex.Message,
                Is.EqualTo("The type was incorrect: " +
                           "All components must be reals"));
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
            Assert.That(result, Is.EqualTo("[1, 2; 3, 4; 5, 6]"));
        }
    }
}
