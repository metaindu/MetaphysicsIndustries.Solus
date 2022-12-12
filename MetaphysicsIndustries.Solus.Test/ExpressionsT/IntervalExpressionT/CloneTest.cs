
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
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.IntervalExpressionT
{
    [TestFixture]
    public class CloneTest
    {
        [Test]
        public void CloneCreatesNewDuplicateObject()
        {
            // given
            var i = new IntervalExpression(
                new Literal(1), false,
                new Literal(2), false);
            // when
            var result0 = i.Clone();
            // then
            Assert.IsInstanceOf<IntervalExpression>(result0);
            var result = (IntervalExpression)result0;
            Assert.AreNotSame(i, result);

            // shallow, for now
            Assert.AreSame(i.LowerBound, result.LowerBound);
            Assert.AreEqual(i.OpenLowerBound, result.OpenLowerBound);

            // shallow, for now
            Assert.AreSame(i.UpperBound, result.UpperBound);
            Assert.AreEqual(i.OpenUpperBound, result.OpenUpperBound);
        }
    }
}
