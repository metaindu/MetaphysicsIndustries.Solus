
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

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.AdditionOperationT
{
    [TestFixture]
    public class AdditionOperationTest
    {
        [Test]
        public void ValueExists()
        {
            // expect
            Assert.IsNotNull(AdditionOperation.Value);
        }

        [Test]
        public void NameIsSet()
        {
            // expect
            Assert.AreEqual("+", AdditionOperation.Value.Name);
        }

        [Test]
        public void PrecedenceIsSet()
        {
            // expect
            Assert.AreEqual(OperationPrecedence.Addition,
                AdditionOperation.Value.Precedence);
        }

        [Test]
        public void IdentityValueIsSet()
        {
            // expect
            Assert.AreEqual(0, AdditionOperation.Value.IdentityValue);
        }
    }
}
