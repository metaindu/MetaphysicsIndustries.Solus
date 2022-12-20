
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.UserDefinedFunctionT
{
    [TestFixture]
    public class GetResultTest
    {
        [Test]
        public void ResultMatchesDefinedFunction1()
        {
            // given
            var expr = new Literal(1.ToNumber());
            var f = new UserDefinedFunction("f", new string[0], expr);
            // precondition
            Assert.That(expr.GetResultType(null), Is.SameAs(Reals.Value));
            // when
            var result = f.GetResultType(null, Array.Empty<ISet>());
            // then
            Assert.That(result, Is.SameAs(Reals.Value));
        }

        [Test]
        public void ResultMatchesDefinedFunction2()
        {
            // given
            var expr = new Literal(
                new Vector(new float[] { 1, 2, 3 }));
            var f = new UserDefinedFunction(
                "f", Array.Empty<string>(), expr);
            // precondition
            Assert.That(expr.GetResultType(null),
                Is.SameAs(Vectors.R3));
            // when
            var result = f.GetResultType(null, Array.Empty<ISet>());
            // then
            Assert.That(result, Is.SameAs(Vectors.R3));
        }
    }
}
