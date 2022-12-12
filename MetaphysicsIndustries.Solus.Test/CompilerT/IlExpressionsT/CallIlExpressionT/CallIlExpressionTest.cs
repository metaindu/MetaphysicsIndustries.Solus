
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
using System.Reflection;
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Exceptions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.CompilerT.IlExpressionsT.
    CallIlExpressionT
{
    [TestFixture]
    public class CallIlExpressionTest
    {
        public static void DummyMethod(int i, float f, bool b)
        {
        }

        [Test]
        public void ConstructorCreatesInstanceDelegate()
        {
            // given
            var method = new Action<int, float, bool>(DummyMethod);
            var args = new IlExpression[0];
            // when
            var result = new CallIlExpression(method, args);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CallIlExpression>(result);
            Assert.AreSame(method.Method, result.Method);
            Assert.AreSame(args, result.Args);
        }

        [Test]
        public void NullMethodThrowsDelegate()
        {
            // given
            var args = new IlExpression[0];
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new CallIlExpression((Delegate)null, args));
            // and
            Assert.AreEqual(
                "Value cannot be null: method",
                ex.Message);
        }

        [Test]
        public void NullArgsThrowsDelegate()
        {
            // given
            var method = new Action<int, float, bool>(DummyMethod);
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new CallIlExpression(method, null));
            // and
            Assert.AreEqual(
                "Value cannot be null: args",
                ex.Message);
        }

        [Test]
        public void NullBothThrowsDelegate()
        {
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new CallIlExpression((Delegate)null, null));
            // and
            Assert.AreEqual(
                "Value cannot be null: method",
                ex.Message);
        }

        [Test]
        public void ConstructorCreatesInstanceMethodInfo()
        {
            // given
            var method = typeof(CallIlExpressionTest).GetMethod(
                "DummyMethod",
                new Type[] { typeof(int), typeof(float), typeof(bool) });
            var args = new IlExpression[0];
            // when
            var result = new CallIlExpression(method, args);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<CallIlExpression>(result);
            Assert.AreSame(method, result.Method);
            Assert.AreSame(args, result.Args);
        }

        [Test]
        public void NullMethodThrowsMethodInfo()
        {
            // given
            var args = new IlExpression[0];
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new CallIlExpression((MethodInfo)null, args));
            // and
            Assert.AreEqual(
                "Value cannot be null: method",
                ex.Message);
        }

        [Test]
        public void NullArgsThrowsMethodInfo()
        {
            // given
            var method = typeof(CallIlExpressionTest).GetMethod(
                "DummyMethod",
                new Type[] { typeof(int), typeof(float), typeof(bool) });
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new CallIlExpression(method, null));
            // and
            Assert.AreEqual(
                "Value cannot be null: args",
                ex.Message);
        }

        [Test]
        public void NullBothThrowsMethodInfo()
        {
            // expect
            var ex = Assert.Throws<ValueException>(
                () => new CallIlExpression((MethodInfo)null, null));
            // and
            Assert.AreEqual(
                "Value cannot be null: method",
                ex.Message);
        }
    }
}
