
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

using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.UserDefinedFunctionT
{
    [TestFixture]
    public class UserDefinedFunctionTest
    {
        [Test]
        public void CreateSetsProperties()
        {
            // when
            var f = new UserDefinedFunction(
                "abc",
                new[] { "x" },
                new Literal(1));
            // then
            Assert.That(f.Name,Is.EqualTo("abc"));
            Assert.That(f.Parameters.Count,Is.EqualTo(1));
            Assert.That(f.Parameters[0].Name,Is.EqualTo("x"));
            Assert.That(f.Parameters[0].Type, Is.SameAs(Reals.Value));
            Assert.That(f.Expression, Is.TypeOf<Literal>());
            Assert.That(f.Expression.As<Literal>().Value.ToNumber().Value, Is.EqualTo(1));
        }
    }
}
