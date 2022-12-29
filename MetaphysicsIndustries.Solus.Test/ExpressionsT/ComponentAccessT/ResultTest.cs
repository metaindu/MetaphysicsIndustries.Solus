
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

using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ComponentAccessT
{
    [TestFixture]
    public class ResultTest
    {
        [Test]
        public void VectorYieldsReal1()
        {
            // given
            var expr = new ComponentAccess(
                new VectorExpression(3,
                    new Literal(1),
                    new Literal(2),
                    new Literal(3)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.That(result, Is.SameAs(Reals.Value));
        }

        [Test]
        public void VectorYieldsReal2()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.That(result, Is.SameAs(Reals.Value));
        }

        [Test]
        public void MatrixYieldsReal1()
        {
            // given
            var expr = new ComponentAccess(
                new MatrixExpression(2, 3,
                    new Literal(1),
                    new Literal(2),
                    new Literal(3),
                    new Literal(4),
                    new Literal(5),
                    new Literal(6)),
                new[] { new Literal(1), new Literal(2) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.That(result, Is.SameAs(Reals.Value));
        }

        [Test]
        public void MatrixYieldsReal2()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(
                    new Matrix(new float[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 }
                    })),
                new[] { new Literal(1), new Literal(2) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.That(result, Is.SameAs(Reals.Value));
        }

        [Test]
        public void StringYieldsString()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(
                    new StringValue("abc")),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // when
            var result = expr.GetResultType(env);
            // then
            Assert.That(result, Is.SameAs(Strings.Value));
        }

        [Test]
        public void ScalarThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(1),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => expr.GetResultType(env));
        }

        [Test]
        public void FunctionThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(SineFunction.Value),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => expr.GetResultType(env));
            // // and
            // Assert.That(result, Is.SameAs(Strings.Value));
        }

        [Test]
        public void IntervalThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Interval(1, 5)),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => expr.GetResultType(env));
        }

        [Test]
        public void SetThrows()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(Reals.Value),
                new[] { new Literal(1) });
            var env = new SolusEnvironment();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => expr.GetResultType(env));
        }
    }
}
