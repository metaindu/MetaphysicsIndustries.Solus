
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
using System.Reflection;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Exceptions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    NewObjIlExpressionT
{
    [TestFixture]
    public class NewObjIlExpressionTest
    {
        class DummyClass{}

        [Test]
        public void CreatesInstance()
        {
            // given
            var ctor = typeof(DummyClass).GetConstructor(Type.EmptyTypes);
            var args = new IlExpression[0];
            // when
            var result = new NewObjIlExpression(ctor, args);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<NewObjIlExpression>(result);
            Assert.That(result.Constructor, Is.SameAs(ctor));
            Assert.That(result.Arguments, Is.SameAs(args));
        }

        [Test]
        public void NullCtorThrows()
        {
            // given
            var args = new IlExpression[0];
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new NewObjIlExpression(null, args));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Value cannot be null: constructor"));
        }

        [Test]
        public void NullArgumentsThrows()
        {
            // given
            var ctor = typeof(DummyClass).GetConstructor(Type.EmptyTypes);
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new NewObjIlExpression(ctor, null));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Value cannot be null: arguments"));
        }

        [Test]
        public void NullBothThrows()
        {
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new NewObjIlExpression(null, null));
            // and
            Assert.That(
                ex.Message,
                Is.EqualTo("Value cannot be null: constructor"));
        }
    }
}
