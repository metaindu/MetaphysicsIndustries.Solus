
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

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.DupIlExpressionT
{
    [TestFixture]
    public class DupIlExpressionTest
    {
        [Test]
        public void ConstructorCreatesInstance()
        {
            // given
            var target = new MockIlExpression();
            // when
            var result = new DupIlExpression(target);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<DupIlExpression>(result);
            Assert.AreSame(target, result.Target);
        }

        [Test]
        public void ConstructorNullTargettThrows()
        {
            // expect
            var ex = Assert.Throws<ArgumentNullException>(
                () => new DupIlExpression(null));
            // and
            Assert.AreEqual(
                "Value cannot be null.\nParameter name: target",
                ex.Message);
        }
    }
}
