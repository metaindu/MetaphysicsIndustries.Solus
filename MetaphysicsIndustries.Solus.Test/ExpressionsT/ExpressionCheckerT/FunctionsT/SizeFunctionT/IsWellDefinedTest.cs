
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
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.SizeFunctionT
{
    [TestFixture]
    public class IsWellDefinedTest
    {
        [Test]
        public void SizeFunctionZeroArgumentsThrows()
        {
            // given
            var f = SizeFunction.Value;
            var args = new Expression[0];
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => ec.IsWellDefined(expr, null));
            // and
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong number of arguments given to " +
                    "size (expected 1 but got 0)"));
        }

        [Test]
        public void SizeFunctionTwoArgumentsThrows()
        {
            // given
            var f = SizeFunction.Value;
            var args = new Expression[]
            {
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Literal(new Vector(new float[] { 4, 5, 6 }))
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => ec.IsWellDefined(expr, null));
            // and
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Wrong number of arguments given to " +
                    "size (expected 1 but got 2)"));
        }

        [Test]
        public void SizeFunctionNonTensorThrows()
        {
            // given
            var f = SizeFunction.Value;
            var args = new Expression[] { new Literal(1) };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => ec.IsWellDefined(expr, null));
            // and
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Argument wrong type: expected " +
                    "Vector or Matrix or String but got Scalar"));
        }

        [Test]
        public void SizeFunctionVectorDoesNotThrow()
        {

            // given
            var f = SizeFunction.Value;
            var args = new Expression[]
            {
                new Literal(new float[] { 1, 2 }.ToVector())
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void SizeFunctionMatrixDoesNotThrow()
        {
            // given
            var f = SizeFunction.Value;
            var args = new Expression[]
            {
                new Literal(
                    new Matrix(new float[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 }
                    }))
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void SizeFunctionStringDoesNotThrow()
        {
            // given
            var f = SizeFunction.Value;
            var args = new Expression[]
            {
                new Literal("abc".ToStringValue())
            };
            var expr = new FunctionCall(f, args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }
    }
}
