
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
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.DivIlExpressionT
{
    [TestFixture]
    public class DivIlExpressionTest
    {
        [Test]
        public void ConstructorCreatesInstance()
        {
            // given
            var dividend = new MockIlExpression();
            var divisor = new MockIlExpression();
            // when
            var result = new DivIlExpression(dividend, divisor);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<DivIlExpression>(result);
            Assert.AreSame(dividend, result.Dividend);
            Assert.AreSame(divisor, result.Divisor);
        }

        [Test]
        public void ConstructorNullDividendThrows()
        {
            // given
            var divisor = new MockIlExpression();
            // expect
            var ex = Assert.Throws<ArgumentNullException>(
                () => new DivIlExpression(null, divisor));
            // and
            Assert.AreEqual(
                "Value cannot be null.\nParameter name: dividend",
                ex.Message);
        }

        [Test]
        public void ConstructorNullDivisorThrows()
        {
            // given
            var dividend = new MockIlExpression();
            // expect
            var ex = Assert.Throws<ArgumentNullException>(
                () => new DivIlExpression(dividend, null));
            // and
            Assert.AreEqual(
                "Value cannot be null.\nParameter name: divisor",
                ex.Message);
        }

        [Test]
        public void ConstructorNullBothThrows()
        {
            // expect
            var ex = Assert.Throws<ArgumentNullException>(
                () => new DivIlExpression(null, null));
            // and
            Assert.AreEqual(
                "Value cannot be null.\nParameter name: dividend",
                ex.Message);
        }
    }
}
