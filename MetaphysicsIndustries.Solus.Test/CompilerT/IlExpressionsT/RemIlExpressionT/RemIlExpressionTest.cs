
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    RemIlExpressionT
{
    [TestFixture]
    public class RemIlExpressionTest
    {
        [Test]
        public void ConstructorCreatesInstance()
        {
            // given
            var dividend = new MockIlExpression();
            var divisor = new MockIlExpression();
            // when
            var result = new RemIlExpression(dividend, divisor);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RemIlExpression>(result);
            Assert.That(result.Dividend, Is.SameAs(dividend));
            Assert.That(result.Divisor, Is.SameAs(divisor));
        }

        [Test]
        public void ConstructorNullDividendDoesNotThrow()
        {
            // given
            var divisor = new MockIlExpression();
            // expect
            var result = new RemIlExpression(null, divisor);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RemIlExpression>(result);
            Assert.IsNull(result.Dividend);
            Assert.That(result.Divisor, Is.SameAs(divisor));
        }

        [Test]
        public void ConstructorNullDivisorDoesNotThrow()
        {
            // given
            var dividend = new MockIlExpression();
            // expect
            var result = new RemIlExpression(dividend, null);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RemIlExpression>(result);
            Assert.That(result.Dividend, Is.SameAs(dividend));
            Assert.IsNull(result.Divisor);
        }

        [Test]
        public void ConstructorNullBothDoesNotThrow()
        {
            // expect
            var result = new RemIlExpression(null, null);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<RemIlExpression>(result);
            Assert.IsNull(result.Dividend);
            Assert.IsNull(result.Divisor);
        }
    }
}
