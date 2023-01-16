
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.UserDefinedFunctionT
{
    [TestFixture]
    public class FunctionTypeTest
    {
        [Test]
        public void NullaryFunctionType1()
        {
            // given
            var expr = new Literal(1.ToNumber());
            var f = new UserDefinedFunction("f", Array.Empty<string>(), expr);
            // when
            var result = f.FunctionType;
            // then
            Assert.That(result, Is.SameAs(Sets.Functions.Get(Reals.Value)));
        }

        [Test]
        public void NullaryStringStillSaysReals()
        {
            // given
            var expr = new Literal("abc");
            var f = new UserDefinedFunction("f", Array.Empty<string>(), expr);
            // when
            var result = f.FunctionType;
            // then
            Assert.That(result, Is.SameAs(Sets.Functions.Get(Reals.Value)));
        }

        [Test]
        public void NullaryBooleanStillSaysReals()
        {
            // given
            var expr = new Literal(true);
            var f = new UserDefinedFunction("f", Array.Empty<string>(), expr);
            // when
            var result = f.FunctionType;
            // then
            Assert.That(result, Is.SameAs(Sets.Functions.Get(Reals.Value)));
        }

        [Test]
        public void UnaryFunctionType3()
        {
            // given
            var expr = new VariableAccess("a");
            var f = new UserDefinedFunction("f", new[] { "a" }, expr);
            // when
            var result = f.FunctionType;
            // then
            Assert.That(result, Is.SameAs(Sets.Functions.RealsToReals));
        }
    }
}
