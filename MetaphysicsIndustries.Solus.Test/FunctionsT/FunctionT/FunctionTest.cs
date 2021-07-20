
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

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.FunctionT
{
    [TestFixture]
    public class FunctionTest
    {
        class MockFunction : Function
        {
            public MockFunction(Types[] paramTypes, string name = "")
                : base(paramTypes, name)
            {
            }

            protected override IMathObject InternalCall(SolusEnvironment env,
                IMathObject[] args)
            {
                throw new System.NotImplementedException();
            }
        }

        [Test]
        public void CreateSetsProperties()
        {
            // given
            var paramTypes = new[] {Types.Scalar};
            // when
            var f = new MockFunction(paramTypes, "func1");
            // then
            Assert.AreEqual("func1", f.Name);
            Assert.AreEqual("func1", f.DisplayName);
            Assert.IsNotNull(f.ParamTypes);
            Assert.AreEqual(1, f.ParamTypes.Count);
            Assert.AreEqual(Types.Scalar, f.ParamTypes[0]);
        }

        [Test]
        public void NullParamTypesThrows()
        {
            // expect
            var ex = Assert.Throws<ArgumentNullException>(
                () => new MockFunction(null, "func2"));
            // and
            Assert.AreEqual("paramTypes", ex.ParamName);
        }
    }
}