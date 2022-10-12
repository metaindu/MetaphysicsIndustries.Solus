
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
using System.Reflection.Emit;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    LoadStringIlExpressionT
{
    [TestFixture]
    public class LoadStringIlExpressionTest
    {
        [Test]
        public void CreateSetsProperty()
        {
            // when
            var result = new LoadStringIlExpression("abc");
            // then
            Assert.IsNotNull(result);
            Assert.AreEqual("abc", result.Value);
        }

        [Test]
        public void NullArgumentThrows()
        {
            // expect
            var ex = Assert.Throws<ArgumentNullException>(
                () => new LoadStringIlExpression(null));
            // and
            Assert.AreEqual(
                "Value cannot be null.\nParameter name: value",
                ex.Message);
        }

    }
}