
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
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.FunctionT
{
    [TestFixture]
    public class FunctionTest
    {
        [Test]
        public void CreateSetsProperties()
        {
            // given
            var paramTypes = new[] { new Parameter("", Reals.Value) };
            // when
            var f = new MockFunction(paramTypes, "func1");
            // then
            Assert.That(f.Name, Is.EqualTo("func1"));
            Assert.That(f.DisplayName, Is.EqualTo("func1"));
            Assert.IsNotNull(f.Parameters);
            Assert.That(f.Parameters.Count, Is.EqualTo(1));
            Assert.That(f.Parameters[0].Type, Is.SameAs(Reals.Value));
        }

        [Test]
        public void NullParamTypesThrows()
        {
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new MockFunction(null, "func2"));
            // and
            Assert.That(ex.ParamName, Is.EqualTo("paramTypes"));
        }
    }
}
