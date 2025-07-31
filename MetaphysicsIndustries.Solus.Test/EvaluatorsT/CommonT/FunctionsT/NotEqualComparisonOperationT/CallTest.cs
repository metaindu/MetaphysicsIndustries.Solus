
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

using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.NotEqualComparisonOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalNotEqualComparisonOperationTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0, 0, false)]
        [TestCase(0, 1, true)]
        [TestCase(0, 2, true)]
        [TestCase(0, 3, true)]
        [TestCase(1, 0, true)]
        [TestCase(1, 1, false)]
        [TestCase(1, 2, true)]
        [TestCase(1, 3, true)]
        [TestCase(2, 0, true)]
        [TestCase(2, 1, true)]
        [TestCase(2, 2, false)]
        [TestCase(2, 3, true)]
        [TestCase(3, 0, true)]
        [TestCase(3, 1, true)]
        [TestCase(3, 2, true)]
        [TestCase(3, 3, false)]
        [TestCase(0, -1, true)]
        [TestCase(1, -1, true)]
        [TestCase(-1, 1, true)]
        [TestCase(-1, -1, false)]
        [TestCase(1.5f, 1.5f, false)]
        [TestCase(1.5f, 1.5001f, true)]
        public void NotEqualComparisonOperationValuesYieldValue(
            float a, float b, bool expected)
        {
            // given
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That((bool)result.ToBoolean(), Is.EqualTo(expected));
        }

        [Test]
        [TestCase(false, false, false)]
        [TestCase(false, true, true)]
        [TestCase(true, false, true)]
        [TestCase(true, true, false)]
        public void NotEqualComparisonOperationBooleansYieldsValue(
            bool a, bool b, bool expected)
        {
            // given
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That((bool)(result.ToBoolean()), Is.EqualTo(expected));
        }

        [Test]
        [TestCase("abc", "abc", false)]
        [TestCase("abc", "def", true)]
        [TestCase("true", "false", true)]
        [TestCase("", "", false)]
        public void NotEqualComparisonOperationStringsYieldValue(
            string a, string b, bool expected)
        {
            // given
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That((bool)result.ToBoolean(), Is.EqualTo(expected));
        }

        [Test]
        public void NotEqualComparisonOperationMatricesYieldsValue1()
        {
            // given
            var a = new Matrix(new float[,] { { 1, 2 }, { 3, 4 } });
            var b = new Matrix(new float[,] { { 1, 2 }, { 3, 4 } });
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.IsFalse(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationMatricesYieldsValue2()
        {
            // given
            var a = new Matrix(new float[,] { { 1, 2 }, { 3, 4 } });
            var b = new Matrix(new float[,] { { 1, 2 }, { 3, 5 } });
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationMatricesYieldsValue3()
        {
            // given
            var a = new Matrix(new float[,] { { 1, 2 }, { 3, 4 } });
            var b = new Matrix(new float[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 }
            });
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationMatricesYieldsValue4()
        {
            // given
            var a = new Matrix(new float[,]
            {
                { 1, 2, 3 },
                { 4, 5, 6 }
            });
            var b = new Matrix(new float[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 }
            });
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationMatricesYieldsValue5()
        {
            // given
            var a = new Matrix(new float[,]
            {
                { 1, 2 },
                { 3, 4 }
            });
            var b = new Matrix(new float[,]
            {
                { 1, 3 },
                { 2, 4 }
            });
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationVectorsYieldsValue1()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = new Vector(new float[] { 1, 2, 3 });
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.IsFalse(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationVectorsYieldsValue2()
        {
            // given
            var a = new Vector(new float[] { 1, 2 });
            var b = new Vector2(1, 2);
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.IsFalse(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationVectorsYieldsValue3()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = new Vector3(1, 2, 3);
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.IsFalse(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationVectorsYieldsValue4()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = new Vector(new float[] { 1, 2, 4 });
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationVectorsYieldsValue5()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = new Vector(new float[] { 1, 2, 3, 4 });
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationVectorsYieldsValue6()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = new Vector2(1, 2);
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationDifferentTypesYieldsTrue1()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = 123f;
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationDifferentTypesYieldsTrue2()
        {
            // given
            var a = "123";
            var b = 123f;
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationDifferentTypesYieldsTrue3()
        {
            // given
            var a = "true";
            var b = true;
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        [Test]
        public void NotEqualComparisonOperationIntervalThrows()
        {
            // given
            var a = new Interval(1, 3);
            var b = new Interval(1, 3);
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // expect
            var ex = Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message,
                Is.EqualTo(
                    "Type not supported for equality comparisons: Interval"));
        }

        [Test]
        public void DifferentTypesWithIntervalYieldsTrue()
        {
            // given
            var a = 123f;
            var b = new Interval(1, 3);
            var f = NotEqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(result.ToBoolean());
        }

        // [Test]
        // public void NotEqualComparisonOperationExpressionThrows()
        // {
        //     // given
        //     var a = new Literal(123f);
        //     var b = new Literal(123f);
        //     var f = NotEqualComparisonOperation.Value;
        //     var args = new Expression[] { new Literal(a), new Literal(b) };
        //     var eval = Util.CreateEvaluator<T>();
        //     var expr = new FunctionCall(f, args);
        //     // expect
        //     var ex = Assert.Throws<TypeException>(
        //         () => eval.Eval(expr, null));
        //     // and
        //     Assert.That(ex.Message,
        //         Is.EqualTo(
        //             "Type not supported for equality comparisons: " +
        //             "Literal"));
        // }

        // [Test]
        // public void DifferentTypesWithExpressionYieldsTrue()
        // {
        //     // given
        //     var a = 123f;
        //     var b = new Literal(123f);
        //     var f = NotEqualComparisonOperation.Value;
        //     var args = new Expression[] { new Literal(a), new Literal(b) };
        //     var eval = Util.CreateEvaluator<T>();
        //     var expr = new FunctionCall(f, args);
        //     // when
        //     var result = eval.Eval(expr, null);
        //     // then
        //     Assert.IsTrue(result.IsBoolean(null));
        //     Assert.That(result.ToBoolean());
        // }
    }
}
