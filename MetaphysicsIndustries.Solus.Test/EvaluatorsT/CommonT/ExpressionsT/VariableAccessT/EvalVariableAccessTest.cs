
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

using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    ExpressionsT.VariableAccessT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalVariableAccessTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        public void GetsLiteralFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var expr2 = new Literal(3);
            var env = new SolusEnvironment();
            env.SetVariable("a", expr2);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result0 = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsScalar(null));
            Assert.IsInstanceOf<Number>(result0);
            var result = result0.ToNumber();
            Assert.That(result.Value, Is.EqualTo(3));
        }

        [Test]
        public void GetsFunctionCallFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var expr2 = new FunctionCall(CosineFunction.Value,
                new Literal(0));
            var env = new SolusEnvironment();
            env.SetVariable("a", expr2);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result0 = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsScalar(null));
            Assert.IsInstanceOf<Number>(result0);
            var result = result0.ToNumber();
            Assert.That(result.Value, Is.EqualTo(1));
        }

        [Test]
        public void GetsNumberFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = 3.ToNumber();
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result0 = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsScalar(null));
            Assert.IsInstanceOf<Number>(result0);
            var result = result0.ToNumber();
            Assert.That(result.Value, Is.EqualTo(3));
        }

        [Test]
        public void GetsVectorFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = new Vector(new float[] { 1, 2, 3 });
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result0 = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsVector(null));
            Assert.IsInstanceOf<Vector>(result0);
            var result = (Vector)result0;
            Assert.That(result.Length, Is.EqualTo(3));
            Assert.That(result[0].ToNumber().Value, Is.EqualTo(1));
            Assert.That(result[1].ToNumber().Value, Is.EqualTo(2));
            Assert.That(result[2].ToNumber().Value, Is.EqualTo(3));
        }

        [Test]
        public void GetsMatrixFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = new Matrix(new float[,]
            {
                { 1, 2 },
                { 3, 4 }
            });
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result0 = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsMatrix(null));
            Assert.IsInstanceOf<Matrix>(result0);
            var result = (Matrix)result0;
            Assert.That(result.RowCount, Is.EqualTo(2));
            Assert.That(result.ColumnCount, Is.EqualTo(2));
            Assert.That(result[0, 0].ToNumber().Value, Is.EqualTo(1));
            Assert.That(result[0, 1].ToNumber().Value, Is.EqualTo(2));
            Assert.That(result[1, 0].ToNumber().Value, Is.EqualTo(3));
            Assert.That(result[1, 1].ToNumber().Value, Is.EqualTo(4));
        }

        [Test]
        public void GetsStringFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = "abc".ToStringValue();
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result0 = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsString(null));
            Assert.IsInstanceOf<StringValue>(result0);
            var result = (StringValue)result0;
            Assert.That(result.Value, Is.EqualTo("abc"));
        }

        [Test]
        public void GetsIntervalFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = new Interval(1.1f, 3.5f);
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result0 = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsInterval(null));
            Assert.IsInstanceOf<Interval>(result0);
            var result = (Interval)result0;
            Assert.That(result.LowerBound, Is.EqualTo(1.1f));
            Assert.That(result.UpperBound, Is.EqualTo(3.5f));
        }

        [Test]
        [Ignore("Not currently supported")]
        public void GetsFunctionFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var f = CosineFunction.Value;
            var env = new SolusEnvironment();
            env.SetVariable("a", f);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result0 = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsFunction(null));
            Assert.IsInstanceOf<CosineFunction>(result0);
            Assert.That(result0, Is.SameAs(CosineFunction.Value));
        }

        [Test]
        [Ignore("Not currently supported")]
        public void GetsExpressionInsideLiteralFromEnv()
        {
            var expr = new VariableAccess("a");
            var expr2 = ColorExpression.Gray;
            var expr3 = new Literal(expr2);
            var env = new SolusEnvironment();
            env.SetVariable("a", expr3);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result.IsConcrete);
            Assert.IsTrue(result.IsIsExpression(null));
            Assert.IsInstanceOf<ColorExpression>(result);
            Assert.That(result, Is.SameAs(expr2));
        }

        [Test]
        public void MissingVariableThrows()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();
            var eval = Util.CreateEvaluator<T>();
            // expect
            var ex = Assert.Throws<NameException>(
                () => eval.Eval(expr, env));
            // and
            Assert.That(ex.Message, Is.EqualTo("Variable not found: a"));
        }
    }
}
