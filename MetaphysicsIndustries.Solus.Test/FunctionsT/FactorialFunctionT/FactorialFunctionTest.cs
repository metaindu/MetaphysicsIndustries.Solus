
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

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.FactorialFunctionT
{
    [TestFixture]
    public class FactorialFunctionTest
    {
        [Test]
        public void ValueExists()
        {
            // expect
            Assert.IsNotNull(FactorialFunction.Value);
        }

        [Test]
        public void NameIsSet()
        {
            // expect
            Assert.AreEqual("Factorial", FactorialFunction.Value.Name);
        }

        [Test]
        public void CallWithNoArgsThrows()
        {
            // given
            var args = new IMathObject[] { };
            // expect
            Assert.Throws<ArgumentException>(() =>
                FactorialFunction.Value.Call(null, args));
        }

        [Test]
        public void CallWithTwoArgsThrows()
        {
            // given
            var args = new IMathObject[] { 1.ToNumber(), 2.ToNumber() };
            // expect
            Assert.Throws<ArgumentException>(() =>
                FactorialFunction.Value.Call(null, args));
        }

        [Test]
        public void CallWithThreeArgsThrows()
        {
            // given
            var args = new IMathObject[] { 1.ToNumber(), 2.ToNumber(),
                4.ToNumber() };
            // expect
            Assert.Throws<ArgumentException>(() =>
                FactorialFunction.Value.Call(null, args));
        }

        // TODO: parametrize

        [Test]
        public void ZeroYieldsOne()
        {
            // given
            var args = new IMathObject[] { 0.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void OneYieldsOne()
        {
            // given
            var args = new IMathObject[] { 1.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void TwoYieldsTwo()
        {
            // given
            var args = new IMathObject[] { 2.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(2, result.ToNumber().Value);
        }

        [Test]
        public void ThreeYieldsSix()
        {
            // given
            var args = new IMathObject[] { 3.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(6, result.ToNumber().Value);
        }

        [Test]
        public void FourYieldsValue()
        {
            // given
            var args = new IMathObject[] { 4.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(24, result.ToNumber().Value);
        }

        [Test]
        public void FiveYieldsValue()
        {
            // given
            var args = new IMathObject[] { 5.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(120, result.ToNumber().Value);
        }

        [Test]
        public void SixYieldsValue()
        {
            // given
            var args = new IMathObject[] { 6.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(720, result.ToNumber().Value);
        }

        [Test]
        public void SevenYieldsValue()
        {
            // given
            var args = new IMathObject[] { 7.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(5040, result.ToNumber().Value);
        }

        [Test]
        public void EightYieldsValue()
        {
            // given
            var args = new IMathObject[] { 8.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(40320, result.ToNumber().Value);
        }

        [Test]
        public void NineYieldsValue()
        {
            // given
            var args = new IMathObject[] { 9.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(362880, result.ToNumber().Value);
        }

        [Test]
        public void TenYieldsValue()
        {
            // given
            var args = new IMathObject[] { 10.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(3628800, result.ToNumber().Value);
        }

        [Test]
        public void ElevenYieldsValue()
        {
            // given
            var args = new IMathObject[] { 11.ToNumber() };
            // when
            var result = FactorialFunction.Value.Call(null, args);
            // then
            Assert.AreEqual(39916800, result.ToNumber().Value);
        }
    }
}
