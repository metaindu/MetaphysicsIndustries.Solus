
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Sets;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.VariableAccessT
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void ResultMatchesExpr1()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();
            var me = new MockExpression(result: Reals.Value);
            env.SetVariable("a", me);
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.That(result, Is.SameAs(Reals.Value));
        }

        [Test]
        public void ResultMatchesExpr2()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();
            var me = new MockExpression(result: Matrices.M3x4);
            env.SetVariable("a", me);
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.That(result, Is.SameAs(Matrices.M3x4));
        }

        [Test]
        public void ResultMatchesExpr3()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();
            var me = new MockExpression(result: Strings.Value);
            env.SetVariable("a", me);
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.That(result, Is.SameAs(Strings.Value));
        }

        [Test]
        public void MissingVariableThrows()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();  // no "a"
            // expect
            var ex = Assert.Throws<NameException>(
                () => expr.GetResultType(env));
            // and
            Assert.That(ex.Message, Is.EqualTo("Variable not found: a"));
        }
    }
}
