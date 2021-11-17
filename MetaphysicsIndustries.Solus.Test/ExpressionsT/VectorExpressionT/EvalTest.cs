
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

using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.VectorExpressionT
{
    [TestFixture]
    public class EvalTest
    {
        [Test]
        public void LiteralsYieldVector()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new Literal(3));
            // when
            var result = expr.Eval(null);
            // then
            Assert.IsInstanceOf<Vector>(result);
            var vector = (Vector) result;
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(1.ToNumber(), vector[0]);
            Assert.AreEqual(2.ToNumber(), vector[1]);
            Assert.AreEqual(3.ToNumber(), vector[2]);
        }

        [Test]
        public void UndefinedVariablesInExpressionCauseException()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new VariableAccess("a"));
            var env = new SolusEnvironment();
            // expect
            var exc = Assert.Throws<NameException>(() => expr.Eval(env));
            // and
            Assert.AreEqual("Variable not found: a",
                exc.Message);
        }

        [Test]
        public void DefinedVariablesInExpressionYieldValue()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new VariableAccess("a"));
            var env = new SolusEnvironment();
            env.SetVariable("a", new Literal(5));
            // when
            var result = expr.Eval(env);
            // then
            Assert.IsInstanceOf<Vector>(result);
            var vector = (Vector) result;
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(1.ToNumber(), vector[0]);
            Assert.AreEqual(2.ToNumber(), vector[1]);
            Assert.AreEqual(5.ToNumber(), vector[2]);
        }

        [Test]
        public void NestedExpressionsYieldNestedValues()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new VectorExpression(3,
                    new Literal(3),
                    new Literal(4),
                    new Literal(5)));
            // when
            var result = expr.Eval(null);
            // then
            Assert.IsInstanceOf<Vector>(result);
            var vector = (Vector) result;
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(1.ToNumber(), vector[0]);
            Assert.AreEqual(2.ToNumber(), vector[1]);
            Assert.IsInstanceOf<Vector>(vector[2]);
            var vector2 = (Vector) vector[2];
            Assert.AreEqual(3, vector2.Length);
            Assert.AreEqual(3.ToNumber(), vector2[0]);
            Assert.AreEqual(4.ToNumber(), vector2[1]);
            Assert.AreEqual(5.ToNumber(), vector2[2]);
        }
    }
}
