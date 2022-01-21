
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
            Assert.AreEqual(3, result.Value);
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
            Assert.AreEqual(1, result.Value);
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
            Assert.AreEqual(3, result.Value);
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
            Assert.AreEqual(3, result.Length);
            Assert.AreEqual(1, result[0].ToNumber().Value);
            Assert.AreEqual(2, result[1].ToNumber().Value);
            Assert.AreEqual(3, result[2].ToNumber().Value);
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
            Assert.AreEqual(2, result.RowCount);
            Assert.AreEqual(2, result.ColumnCount);
            Assert.AreEqual(1, result[0, 0].ToNumber().Value);
            Assert.AreEqual(2, result[0, 1].ToNumber().Value);
            Assert.AreEqual(3, result[1, 0].ToNumber().Value);
            Assert.AreEqual(4, result[1, 1].ToNumber().Value);
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
            Assert.AreEqual("abc", result.Value);
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
            Assert.AreEqual(1.1f, result.LowerBound);
            Assert.AreEqual(3.5f, result.UpperBound);
        }

        [Test]
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
            Assert.AreSame(CosineFunction.Value, result0);
        }

        [Test]
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
            Assert.AreSame(expr2, result);
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
            Assert.AreEqual("Variable not found: a", ex.Message);
        }
    }
}
