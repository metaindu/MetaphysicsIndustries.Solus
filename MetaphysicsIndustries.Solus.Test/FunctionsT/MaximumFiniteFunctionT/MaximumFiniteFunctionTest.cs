
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
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.MaximumFiniteFunctionT
{
    [TestFixture]
    public class MaximumFiniteFunctionTest
    {
        [Test]
        public void AscendingYieldsMax()
        {
            // given
            var args = new IMathObject[]
            {
                1.ToNumber(),
                2.ToNumber(),
                3.ToNumber(),
                4.ToNumber(),
                5.ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(5, result.ToNumber().Value);
        }

        [Test]
        public void DescendingYieldsMax()
        {
            // given
            var args = new IMathObject[]
            {
                9.ToNumber(),
                8.ToNumber(),
                7.ToNumber(),
                6.ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(9, result.ToNumber().Value);
        }

        [Test]
        public void NegativeAscendingYieldsMax()
        {
            // given
            var args = new IMathObject[]
            {
                (-5).ToNumber(),
                (-4).ToNumber(),
                (-3).ToNumber(),
                (-2).ToNumber(),
                (-1).ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(-1, result.ToNumber().Value);
        }

        [Test]
        public void NegativeDescendingYieldsMax()
        {
            // given
            var args = new IMathObject[]
            {
                (-6).ToNumber(),
                (-7).ToNumber(),
                (-8).ToNumber(),
                (-9).ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(-6, result.ToNumber().Value);
        }

        [Test]
        public void PositiveAndNegativeYieldsMax()
        {
            // given
            var args = new IMathObject[]
            {
                (-1).ToNumber(),
                0.ToNumber(),
                1.ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void NotInOrderYieldsMax()
        {
            // given
            var args = new IMathObject[]
            {
                5.ToNumber(),
                9.ToNumber(),
                1.ToNumber(),
                3.ToNumber(),
                2.ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(9, result.ToNumber().Value);
        }

        [Test]
        public void SingleArgumentYieldsMax()
        {
            // given
            var args = new IMathObject[]
            {
                5.ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(5, result.ToNumber().Value);
        }

        [Test]
        public void ZeroArgumentsThrows()
        {
            // given
            var args = new IMathObject[0];
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => MaximumFiniteFunction.Value.Call(null, args));
            // and
            Assert.AreEqual("No arguments passed", ex.Message);
        }

        [Test]
        public void NaNIsIgnored()
        {
            // given
            var args = new IMathObject[]
            {
                1.ToNumber(),
                2.ToNumber(),
                float.NaN.ToNumber(),
                4.ToNumber(),
                5.ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(5, result.ToNumber().Value);
        }

        [Test]
        public void PositiveInfinityIsIgnored()
        {
            // given
            var args = new IMathObject[]
            {
                1.ToNumber(),
                2.ToNumber(),
                float.PositiveInfinity.ToNumber(),
                4.ToNumber(),
                5.ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(5, result.ToNumber().Value);
        }

        [Test]
        public void NegativeInfinityIsIgnored()
        {
            // given
            var args = new IMathObject[]
            {
                1.ToNumber(),
                2.ToNumber(),
                float.NegativeInfinity.ToNumber(),
                4.ToNumber(),
                5.ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(5, result.ToNumber().Value);
        }

        [Test]
        public void InfinitiesAndNanYieldMax()
        {
            // given
            var args = new IMathObject[]
            {
                float.PositiveInfinity.ToNumber(),
                float.NegativeInfinity.ToNumber(),
                float.NaN.ToNumber(),
                1.ToNumber()
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void NoFiniteNumbersYieldsNaN1()
        {
            // given
            var args = new IMathObject[]
            {
                float.PositiveInfinity.ToNumber(),
                float.NegativeInfinity.ToNumber(),
                float.NaN.ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(float.NaN, result.ToNumber().Value);
        }

        [Test]
        public void NoFiniteNumbersYieldsNaN2()
        {
            // given
            var args = new IMathObject[]
            {
                float.PositiveInfinity.ToNumber(),
                float.NegativeInfinity.ToNumber(),
            };
            // when
            var result = MaximumFiniteFunction.Value.Call(null, args);
            // then
            Assert.IsInstanceOf<Number>(result);
            Assert.AreEqual(float.NaN, result.ToNumber().Value);
        }
    }
}
