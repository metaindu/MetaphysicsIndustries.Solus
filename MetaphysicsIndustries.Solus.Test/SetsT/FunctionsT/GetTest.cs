
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

using MetaphysicsIndustries.Solus.Sets;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.SetsT.FunctionsT
{
    [TestFixture]
    public class GetTest
    {
        [Test]
        public void SubsequentInvocationsGiveTheSameObject1()
        {
            //given
            var a = Sets.Functions.Get(Reals.Value, Reals.Value);
            // when
            var b = Sets.Functions.Get(Reals.Value, Reals.Value);
            // then
            Assert.That(object.ReferenceEquals(a, b));
        }

        [Test]
        public void SubsequentInvocationsGiveTheSameObject2()
        {
            //given
            var a = Sets.Functions.Get(Reals.Value, Reals.Value);
            // when
            var b = Sets.Functions.RealsToReals;
            // then
            Assert.That(object.ReferenceEquals(a, b));
        }

        [Test]
        public void SubsequentInvocationsGiveTheSameObject3()
        {
            //given
            var a = Sets.Functions.RealsToReals;
            // when
            var b = Sets.Functions.Get(Reals.Value, Reals.Value);
            // then
            Assert.That(object.ReferenceEquals(a, b));
        }

        [Test]
        public void SubsequentInvocationsGiveTheSameObject4()
        {
            //given
            var a = Sets.Functions.RealsToReals;
            // when
            var b = Sets.Functions.RealsToReals;
            // then
            Assert.That(object.ReferenceEquals(a, b));
        }
    }
}
