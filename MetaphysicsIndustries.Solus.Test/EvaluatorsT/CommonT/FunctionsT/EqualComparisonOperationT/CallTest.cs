
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

using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    FunctionsT.EqualComparisonOperationT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalEqualComparisonOperationTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        [TestCase(0, 0, true)]
        [TestCase(0, 1, false)]
        [TestCase(0, 2, false)]
        [TestCase(0, 3, false)]
        [TestCase(1, 0, false)]
        [TestCase(1, 1, true)]
        [TestCase(1, 2, false)]
        [TestCase(1, 3, false)]
        [TestCase(2, 0, false)]
        [TestCase(2, 1, false)]
        [TestCase(2, 2, true)]
        [TestCase(2, 3, false)]
        [TestCase(3, 0, false)]
        [TestCase(3, 1, false)]
        [TestCase(3, 2, false)]
        [TestCase(3, 3, true)]
        [TestCase(0, -1, false)]
        [TestCase(1, -1, false)]
        [TestCase(-1, 1, false)]
        [TestCase(-1, -1, true)]
        [TestCase(1.5f, 1.5f, true)]
        [TestCase(1.5f, 1.5001f, false)]
        public void EqualComparisonOperationRealsYieldValue(
            float a, float b, bool expected)
        {
            // given
            var f = EqualComparisonOperation.Value;
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
        [TestCase(false, false, true)]
        [TestCase(false, true, false)]
        [TestCase(true, false, false)]
        [TestCase(true, true, true)]
        public void EqualComparisonOperationBooleansYieldsValue(
            bool a, bool b, bool expected)
        {
            // given
            var f = EqualComparisonOperation.Value;
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
        [TestCase("abc", "abc", true)]
        [TestCase("abc", "def", false)]
        [TestCase("true", "false", false)]
        [TestCase("", "", true)]
        public void EqualComparisonOperationStringsYieldValue(
            string a, string b, bool expected)
        {
            // given
            var f = EqualComparisonOperation.Value;
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
        public void EqualComparisonOperationMatricesYieldsValue1()
        {
            // given
            var a = new Matrix(new float[,] { { 1, 2 }, { 3, 4 } });
            var b = new Matrix(new float[,] { { 1, 2 }, { 3, 4 } });
            var f = EqualComparisonOperation.Value;
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
        public void EqualComparisonOperationMatricesYieldsValue2()
        {
            // given
            var a = new Matrix(new float[,] { { 1, 2 }, { 3, 4 } });
            var b = new Matrix(new float[,] { { 1, 2 }, { 3, 5 } });
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        [Test]
        public void EqualComparisonOperationMatricesYieldsValue3()
        {
            // given
            var a = new Matrix(new float[,] { { 1, 2 }, { 3, 4 } });
            var b = new Matrix(new float[,]
            {
                { 1, 2 },
                { 3, 4 },
                { 5, 6 }
            });
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        [Test]
        public void EqualComparisonOperationMatricesYieldsValue4()
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
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        [Test]
        public void EqualComparisonOperationMatricesYieldsValue5()
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
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        [Test]
        public void EqualComparisonOperationVectorsYieldsValue1()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = new Vector(new float[] { 1, 2, 3 });
            var f = EqualComparisonOperation.Value;
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
        public void EqualComparisonOperationVectorsYieldsValue2()
        {
            // given
            var a = new Vector(new float[] { 1, 2 });
            var b = new Vector2(1, 2);
            var f = EqualComparisonOperation.Value;
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
        public void EqualComparisonOperationVectorsYieldsValue3()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = new Vector3(1, 2, 3);
            var f = EqualComparisonOperation.Value;
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
        public void EqualComparisonOperationVectorsYieldsValue4()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = new Vector(new float[] { 1, 2, 4 });
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        [Test]
        public void EqualComparisonOperationVectorsYieldsValue5()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = new Vector(new float[] { 1, 2, 3, 4 });
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        [Test]
        public void EqualComparisonOperationVectorsYieldsValue6()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = new Vector2(1, 2);
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        [Test]
        public void EqualComparisonOperationDifferentTypesYieldsFalse1()
        {
            // given
            var a = new Vector(new float[] { 1, 2, 3 });
            var b = 123f;
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        [Test]
        public void EqualComparisonOperationDifferentTypesYieldsFalse2()
        {
            // given
            var a = "123";
            var b = 123f;
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        [Test]
        public void EqualComparisonOperationDifferentTypesYieldsFalse3()
        {
            // given
            var a = "true";
            var b = true;
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        [Test]
        public void EqualComparisonOperationIntervalThrows()
        {
            // given
            var a = new Interval(1, 3);
            var b = new Interval(1, 3);
            var f = EqualComparisonOperation.Value;
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
        public void DifferentTypesWithIntervalYieldsFalse()
        {
            // given
            var a = 123f;
            var b = new Interval(1, 3);
            var f = EqualComparisonOperation.Value;
            var args = new Expression[] { new Literal(a), new Literal(b) };
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(f, args);
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsBoolean(null));
            Assert.That(!result.ToBoolean());
        }

        // [Test]
        // public void EqualComparisonOperationExpressionThrows()
        // {
        //     // given
        //     var a = new Literal(123f);
        //     var b = new Literal(123f);
        //     var f = EqualComparisonOperation.Value;
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
        // public void DifferentTypesWithExpressionYieldsFalse()
        // {
        //     // given
        //     var a = 123f;
        //     var b = new Literal(123f);
        //     var f = EqualComparisonOperation.Value;
        //     var args = new Expression[] { new Literal(a), new Literal(b) };
        //     var eval = Util.CreateEvaluator<T>();
        //     var expr = new FunctionCall(f, args);
        //     // when
        //     var result = eval.Eval(expr, null);
        //     // then
        //     Assert.IsTrue(result.IsBoolean(null));
        //     Assert.That(!result.ToBoolean());
        // }
    }
}
