
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
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Exceptions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    CompareGreaterThanIlExpressionT
{
    [TestFixture]
    public class CompareGreaterThanIlExpressionTest
    {
        [Test]
        public void ConstructorCreatesInstance()
        {
            // given
            var left = new MockIlExpression();
            var right = new MockIlExpression();
            // when
            var result = new CompareGreaterThanIlExpression(left, right);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CompareGreaterThanIlExpression>(result);
            Assert.That(result.Left, Is.SameAs(left));
            Assert.That(result.Right, Is.SameAs(right));
        }

        [Test]
        public void ConstructorNullLeftThrows()
        {
            // given
            var right = new MockIlExpression();
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new CompareGreaterThanIlExpression(null, right));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Value cannot be null: left"));
        }

        [Test]
        public void ConstructorNullRightThrows()
        {
            // given
            var left = new MockIlExpression();
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new CompareGreaterThanIlExpression(left, null));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Value cannot be null: right"));
        }

        [Test]
        public void ConstructorNullBothThrows()
        {
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new CompareGreaterThanIlExpression(null, null));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Value cannot be null: left"));
        }
    }
}
