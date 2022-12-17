
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
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    ConvertI4IlExpressionT
{
    [TestFixture]
    public class ConvertI4IlExpressionTest
    {
        [Test]
        public void ConstructorCreatesInstance()
        {
            // given
            var argument = new MockIlExpression();
            // when
            var result = new ConvertI4IlExpression(argument);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConvertI4IlExpression>(result);
            Assert.That(result.Argument, Is.SameAs(argument));
        }

        [Test]
        public void ConstructorNullArgumentDoesNotDoesNotThrow()
        {
            // when
            var result = new ConvertI4IlExpression(null);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ConvertI4IlExpression>(result);
            Assert.IsNull(result.Argument);
        }
    }
}
