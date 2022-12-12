
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
using MetaphysicsIndustries.Solus.Exceptions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    BrTrueIlExpressionT
{
    [TestFixture]
    public class BrTrueIlExpressionTest
    {
        [Test]
        public void ConstructorCreatesInstance()
        {
            // given
            var target = new MockIlExpression();
            // when
            var result = new BrTrueIlExpression(target);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BrTrueIlExpression>(result);
            Assert.AreSame(target, result.Target);
        }

        [Test]
        public void ConstructorNullTargetThrows()
        {
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new BrTrueIlExpression(null));
            // and
            Assert.AreEqual(
                "Value cannot be null: target",
                ex.Message);
        }
    }
}
