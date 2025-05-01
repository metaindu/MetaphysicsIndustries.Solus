
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

using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.IfOperatorT
{
    public class IfOperatorTest
    {
        [Test]
        public void HasDefaultValues()
        {
            // given
            var result = IfOperator.Value;
            // expect
            Assert.That(result.Name, Is.EqualTo("if"));
            Assert.That(result.Parameters, Is.Not.Null);
            Assert.That(result.Parameters.Count,Is.EqualTo(3));
            Assert.That(result.Parameters[0].Type,
                Is.SameAs(Sets.Expressions.Value));
            Assert.That(result.Parameters[1].Type,
                Is.SameAs(Sets.Expressions.Value));
            Assert.That(result.Parameters[2].Type,
                Is.SameAs(Sets.Expressions.Value));
        }
    }
}
