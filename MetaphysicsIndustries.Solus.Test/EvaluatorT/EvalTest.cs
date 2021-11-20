
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
using System.Linq;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorT
{
    [TestFixture]
    public class EvalTest
    {
        //// Expressions
        // ComponentAccess

        [Test]
        public void ComponentAccessEvalYieldsTheIndicatedComponent()
        {
            // given
            var expr = new Literal(
                new Vector(new float[] { 1, 2, 3 }));
            var indexes = new Expression[] { new Literal(1) };
            var ca = new ComponentAccess(expr, indexes);
            var eval = new Evaluator();
            // when
            var result = eval.Eval(ca, null);
            // then
            Assert.IsFalse(result.IsVector(null));
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(2, result.ToFloat());
        }

        // IntervalExpression

        [Test]
        public void IntervalExpressionEvalYieldsConcreteInterval()
        {
            // given
            var i = new IntervalExpression(
                new Literal(1), true,
                new Literal(2), true);
            var eval = new Evaluator();
            // when
            var result = eval.Eval(i, null);
            // then
            Assert.IsTrue(result.IsConcrete);
            Assert.IsTrue(result.IsInterval(null));
            Assert.IsInstanceOf<Interval>(result);
            var interval = (Interval)result;
            Assert.AreEqual(1, interval.LowerBound);
            Assert.IsTrue(interval.OpenLowerBound);
            Assert.AreEqual(2, interval.UpperBound);
            Assert.IsTrue(interval.OpenUpperBound);
        }

        // MatrixExpression

        [Test]
        public void MatrixExpressionLiteralsYieldMatrix()
        {
            // given
            var expr = new MatrixExpression(2, 2,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new Literal(4));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Matrix>(result);
            var matrix = (Matrix)result;
            Assert.AreEqual(2, matrix.RowCount);
            Assert.AreEqual(2, matrix.ColumnCount);
            Assert.AreEqual(1.ToNumber(), matrix[0, 0]);
            Assert.AreEqual(2.ToNumber(), matrix[0, 1]);
            Assert.AreEqual(3.ToNumber(), matrix[1, 0]);
            Assert.AreEqual(4.ToNumber(), matrix[1, 1]);
        }

        [Test]
        public void MatrixExpressionLiteralsYieldMatrix2()
        {
            // given
            var expr = new MatrixExpression(2, 3,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new Literal(4),
                new Literal(5),
                new Literal(6));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Matrix>(result);
            var matrix = (Matrix)result;
            Assert.AreEqual(2, matrix.RowCount);
            Assert.AreEqual(3, matrix.ColumnCount);
            Assert.AreEqual(1.ToNumber(), matrix[0, 0]);
            Assert.AreEqual(2.ToNumber(), matrix[0, 1]);
            Assert.AreEqual(3.ToNumber(), matrix[0, 2]);
            Assert.AreEqual(4.ToNumber(), matrix[1, 0]);
            Assert.AreEqual(5.ToNumber(), matrix[1, 1]);
            Assert.AreEqual(6.ToNumber(), matrix[1, 2]);
        }

        [Test]
        public void MatrixExpressionUndefinedVariablesInExpressionThrows()
        {
            // given
            var expr = new MatrixExpression(2, 2,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new VariableAccess("a"));
            var env = new SolusEnvironment();
            var eval = new Evaluator();
            // expect
            var exc = Assert.Throws<NameException>(
                () => eval.Eval(expr, env));
            // and
            Assert.AreEqual("Variable not found: a",
                exc.Message);
        }

        [Test]
        public void MatrixExpressionDefinedVariablesInExpressionYieldValue()
        {
            // given
            var expr = new MatrixExpression(2, 2,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new VariableAccess("a"));
            var env = new SolusEnvironment();
            env.SetVariable("a", new Literal(5));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.IsInstanceOf<Matrix>(result);
            var matrix = (Matrix)result;
            Assert.AreEqual(2, matrix.RowCount);
            Assert.AreEqual(2, matrix.ColumnCount);
            Assert.AreEqual(1.ToNumber(), matrix[0, 0]);
            Assert.AreEqual(2.ToNumber(), matrix[0, 1]);
            Assert.AreEqual(3.ToNumber(), matrix[1, 0]);
            Assert.AreEqual(5.ToNumber(), matrix[1, 1]);
        }

        [Test]
        public void MatrixExpressionNestedExpressionsYieldNestedValues()
        {
            // given
            var expr = new MatrixExpression(2, 2,
                new Literal(1),
                new Literal(2),
                new Literal(3),
                new MatrixExpression(2, 2,
                    new Literal(4),
                    new Literal(5),
                    new Literal(6),
                    new Literal(7)));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Matrix>(result);
            var matrix = (Matrix)result;
            Assert.AreEqual(2, matrix.RowCount);
            Assert.AreEqual(2, matrix.ColumnCount);
            Assert.AreEqual(1.ToNumber(), matrix[0, 0]);
            Assert.AreEqual(2.ToNumber(), matrix[0, 1]);
            Assert.AreEqual(3.ToNumber(), matrix[1, 0]);
            Assert.IsInstanceOf<Matrix>(matrix[1, 1]);
            var matrix2 = (Matrix)matrix[1, 1];
            Assert.AreEqual(2, matrix2.RowCount);
            Assert.AreEqual(2, matrix2.ColumnCount);
            Assert.AreEqual(4.ToNumber(), matrix2[0, 0]);
            Assert.AreEqual(5.ToNumber(), matrix2[0, 1]);
            Assert.AreEqual(6.ToNumber(), matrix2[1, 0]);
            Assert.AreEqual(7.ToNumber(), matrix2[1, 1]);
        }

        // VectorExpression

        [Test]
        public void VectorExpressionLiteralsYieldVector()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new Literal(3));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Vector>(result);
            var vector = (Vector)result;
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(1.ToNumber(), vector[0]);
            Assert.AreEqual(2.ToNumber(), vector[1]);
            Assert.AreEqual(3.ToNumber(), vector[2]);
        }

        [Test]
        public void VectorExpressionUndefinedVariablesInExpressionThrows()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new VariableAccess("a"));
            var env = new SolusEnvironment();
            var eval = new Evaluator();
            // expect
            var exc = Assert.Throws<NameException>(() => eval.Eval(expr, env));
            // and
            Assert.AreEqual("Variable not found: a",
                exc.Message);
        }

        [Test]
        public void VectorExpressionDefinedVariablesInExpressionYieldValue()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new VariableAccess("a"));
            var env = new SolusEnvironment();
            env.SetVariable("a", new Literal(5));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.IsInstanceOf<Vector>(result);
            var vector = (Vector)result;
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(1.ToNumber(), vector[0]);
            Assert.AreEqual(2.ToNumber(), vector[1]);
            Assert.AreEqual(5.ToNumber(), vector[2]);
        }

        [Test]
        public void VectorExpressionNestedExpressionsYieldNestedValues()
        {
            // given
            var expr = new VectorExpression(3,
                new Literal(1),
                new Literal(2),
                new VectorExpression(3,
                    new Literal(3),
                    new Literal(4),
                    new Literal(5)));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Vector>(result);
            var vector = (Vector)result;
            Assert.AreEqual(3, vector.Length);
            Assert.AreEqual(1.ToNumber(), vector[0]);
            Assert.AreEqual(2.ToNumber(), vector[1]);
            Assert.IsInstanceOf<Vector>(vector[2]);
            var vector2 = (Vector)vector[2];
            Assert.AreEqual(3, vector2.Length);
            Assert.AreEqual(3.ToNumber(), vector2[0]);
            Assert.AreEqual(4.ToNumber(), vector2[1]);
            Assert.AreEqual(5.ToNumber(), vector2[2]);
        }

        [Test]
        public void FunctionCallYieldsReturnValue()
        {
            // given
            var mf = new MockFunction(new[] { Types.Scalar }, "f")
            {
                CallF = args => args.First()
            };
            var expr = new FunctionCall(mf, new Literal(5));
            var eval = new Evaluator();
            // when
            var result0 = eval.Eval(expr, null);
            // then
            Assert.IsNotNull(result0);
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsScalar(null));
            Assert.IsInstanceOf<Number>(result0);
            var result = result0.ToNumber();
            Assert.AreEqual(5, result.Value);
        }

        [Test]
        public void VariableAccessGetsLiteralFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var expr2 = new Literal(3);
            var env = new SolusEnvironment();
            env.SetVariable("a", expr2);
            var eval = new Evaluator();
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
        public void VariableAccessGetsFunctionCallFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var expr2 = new FunctionCall(CosineFunction.Value,
                new Literal(0));
            var env = new SolusEnvironment();
            env.SetVariable("a", expr2);
            var eval = new Evaluator();
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
        public void VariableAccessGetsNumberFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = 3.ToNumber();
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var eval = new Evaluator();
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
        public void VariableAccessGetsVectorFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = new Vector(new float[] { 1, 2, 3 });
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var eval = new Evaluator();
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
        public void VariableAccessGetsMatrixFromEnv()
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
            var eval = new Evaluator();
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
        public void VariableAccessGetsStringFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = "abc".ToStringValue();
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var eval = new Evaluator();
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
        public void VariableAccessGetsIntervalFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var value = new Interval(1.1f, 3.5f);
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            var eval = new Evaluator();
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
        public void VariableAccessGetsFunctionFromEnv()
        {
            // given
            var expr = new VariableAccess("a");
            var f = CosineFunction.Value;
            var env = new SolusEnvironment();
            env.SetVariable("a", f);
            var eval = new Evaluator();
            // when
            var result0 = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsFunction(null));
            Assert.IsInstanceOf<CosineFunction>(result0);
            Assert.AreSame(CosineFunction.Value, result0);
        }

        [Test]
        public void VariableAccessGetsExpressionInsideLiteralFromEnv()
        {
            var expr = new VariableAccess("a");
            var expr2 = ColorExpression.Gray;
            var expr3 = new Literal(expr2);
            var env = new SolusEnvironment();
            env.SetVariable("a", expr3);
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.IsTrue(result.IsConcrete);
            Assert.IsTrue(result.IsIsExpression(null));
            Assert.IsInstanceOf<ColorExpression>(result);
            Assert.AreSame(expr2, result);
        }

        [Test]
        public void VariableAccessMissingVariableThrows()
        {
            // given
            var expr = new VariableAccess("a");
            var env = new SolusEnvironment();
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<NameException>(
                () => eval.Eval(expr, env));
            // and
            Assert.AreEqual("Variable not found: a", ex.Message);
        }
        //// Functions
        // AbsoluteValueFunction

        [Test]
        public void AbsoluteValueFunctionPositiveYieldsPositive()
        {
            // given
            var expr = new FunctionCall(AbsoluteValueFunction.Value,
                new Literal(1));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void AbsoluteValueFunctionNegativeYieldsNegative()
        {
            // given
            var expr = new FunctionCall(AbsoluteValueFunction.Value,
                new Literal(-1));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        // AdditionOperation

        [Test]
        public void AdditionOperationCallWithNoArgsThrows()
        {
            // given
            var expr = new FunctionCall(AdditionOperation.Value,
                new Literal(1));
            var eval = new Evaluator();
            // expect
            Assert.Throws<ArgumentException>(
                () => eval.Eval(expr, null));
        }

        [Test]
        public void AdditionOperationCallWithOneArgThrows()
        {
            // given
            var expr = new FunctionCall(AdditionOperation.Value,
                new Literal(1));
            var eval = new Evaluator();
            // expect
            Assert.Throws<ArgumentException>(
                () => eval.Eval(expr, null));
        }

        [Test]
        public void AdditionOperationCallWithTwoArgsYieldsSum()
        {
            // given
            var expr = new FunctionCall(AdditionOperation.Value,
                new Literal(1), new Literal(2));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(3, result.ToNumber().Value);
        }

        [Test]
        public void AdditionOperationCallWithThreeArgsYieldsSum()
        {
            // given
            var expr = new FunctionCall(AdditionOperation.Value,
                new Literal(1), new Literal(2), new Literal(4));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(7, result.ToNumber().Value);
        }

        // ArccosecantFunction

        [Test]
        // [TestCase(0, 1/0)]
        [TestCase((float)(Math.PI / 6), 2)]
        [TestCase((float)(Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(Math.PI / 3), 1.154700538379252f)]
        [TestCase((float)(Math.PI / 2), 1)]
        [TestCase((float)(-Math.PI / 6), -2)]
        [TestCase((float)(-Math.PI / 4), -1.414213562373095f)]
        [TestCase((float)(-Math.PI / 3), -1.154700538379252f)]
        [TestCase((float)(-Math.PI / 2), -1)]
        public void ArccosecantFunctionValueYieldsValue(
            float expected, float arg)
        {
            // given
            var expr = new FunctionCall(ArccosecantFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // ArccosineFunction

        [Test]
        [TestCase(0, 1)]
        [TestCase((float)(Math.PI / 6), 0.866025403784439f)]
        [TestCase((float)(Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(Math.PI / 3), 0.5f)]
        [TestCase((float)(Math.PI / 2), 0)]
        [TestCase((float)(3 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)Math.PI, -1)]
        public void ArccosineFunctionValueYieldsValue(
            float expected, float arg)
        {
            // given
            var expr = new FunctionCall(ArccosineFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        [Test]
        public void ArccosineFunctionOutOfBoundsThrows1()
        {
            // given
            var expr = new FunctionCall(ArccosineFunction.Value,
                new Literal(-1.0001f));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument less than -1", ex.Message);
        }

        [Test]
        public void ArccosineFunctionOutOfBoundsThrows2()
        {
            // given
            var expr = new FunctionCall(ArccosineFunction.Value,
                new Literal(1.0001f));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument greater than 1", ex.Message);
        }

        // ArccotangentFunction

        [Test]
        // [TestCase(0, inf)]
        [TestCase((float)(Math.PI / 6), 1.732050807568877f)]
        [TestCase((float)(Math.PI / 4), 1)]
        [TestCase((float)(Math.PI / 3), 0.577350269189626f)]
        [TestCase((float)(Math.PI / 2), 0)]
        [TestCase((float)(3 * Math.PI / 4), -1)]
        // [TestCase((float)Math.PI, inf)]
        public void ArccotangentFunctionValueYieldsValue(
            float expected, float arg)
        {
            // given
            var expr = new FunctionCall(ArccotangentFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // ArcsecantFunction

        [Test]
        [TestCase(0, 1)]
        [TestCase((float)(Math.PI / 6), 1.154700538379252f)]
        [TestCase((float)(Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(Math.PI / 3), 2)]
        // [TestCase((float)(Math.PI / 2), 1/0)]
        [TestCase((float)(3 * Math.PI / 4), -1.414213562373095f)]
        [TestCase((float)Math.PI, -1)]
        public void ArcsecantFunctionValueYieldsValue(
            float expected, float arg)
        {
            // given
            var expr = new FunctionCall(ArcsecantFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // ArcsineFunction

        [Test]
        [TestCase(0, 0)]
        [TestCase((float)(Math.PI / 6), 0.5f)]
        [TestCase((float)(Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(Math.PI / 3), 0.866025403784439f)]
        [TestCase((float)(Math.PI / 2), 1)]
        [TestCase((float)(-Math.PI / 6), -0.5f)]
        [TestCase((float)(-Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(-Math.PI / 3), -0.866025403784439f)]
        [TestCase((float)(-Math.PI / 2), -1)]
        public void ArcsineFunctionValueYieldsValue(
            float expected, float arg)
        {
            // given
            var expr = new FunctionCall(ArcsineFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        [Test]
        public void ArcsineFunctionOutOfBoundsThrows1()
        {
            // given
            var expr = new FunctionCall(ArcsineFunction.Value,
                new Literal(-1.0001f));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument less than -1", ex.Message);
        }

        [Test]
        public void ArcsineFunctionOutOfBoundsThrows2()
        {
            // given
            var expr = new FunctionCall(ArcsineFunction.Value,
                new Literal(1.0001f));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument greater than 1", ex.Message);
        }

        // TODO: Arctangent2Function
        // ArctangentFunction

        [Test]
        // [TestCase((float)(-Math.PI / 2), -inf)]
        [TestCase((float)(-Math.PI / 3), -1.732050807568877f)]
        [TestCase((float)(-Math.PI / 4), -1)]
        [TestCase((float)(-Math.PI / 6), -0.577350269189626f)]
        [TestCase(0, 0)]
        [TestCase((float)(Math.PI / 6), 0.577350269189626f)]
        [TestCase((float)(Math.PI / 4), 1)]
        [TestCase((float)(Math.PI / 3), 1.732050807568877f)]
        // [TestCase((float)(Math.PI / 2), inf)]
        public void ArctangentFunctionValueYieldsValue(
            float expected, float arg)
        {
            // given
            var expr = new FunctionCall(ArctangentFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // BitwiseAndOperation

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 0)]
        [TestCase(0, 1, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 0)]
        [TestCase(3, 1, 1)]
        [TestCase(1, 2, 0)]
        [TestCase(2, 2, 2)]
        [TestCase(3, 2, 2)]
        [TestCase(1, 3, 1)]
        [TestCase(2, 3, 2)]
        [TestCase(3, 3, 3)]
        [TestCase(1.1f, 3, 1)]
        [TestCase(1.9f, 3, 1)]
        [TestCase(2.5f, 3, 2)]
        public void BitwiseAndOperationIntegerYieldsInteger(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(BitwiseAndOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat());
        }

        // BitwiseOrOperation

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(1, 0, 1)]
        [TestCase(0, 1, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(2, 1, 3)]
        [TestCase(3, 1, 3)]
        [TestCase(1, 2, 3)]
        [TestCase(2, 2, 2)]
        [TestCase(3, 2, 3)]
        [TestCase(1, 3, 3)]
        [TestCase(2, 3, 3)]
        [TestCase(3, 3, 3)]
        [TestCase(1.1f, 0, 1)]
        [TestCase(1.9f, 0, 1)]
        [TestCase(2.5f, 0, 2)]
        [TestCase(11184810, 22369620, 33554430)]
        [TestCase(11184811, 22369620, 33554431)]
        [TestCase(11184810, 1, 11184811)]
        // Loss of precision at 33554431 (0x1ffffff), because float32 only
        // has 24-bits for the significand.
        [TestCase(16777216, 16777216, 16777217)]
        public void BitwiseOrOperationIntegerYieldsInteger(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(BitwiseOrOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat());
        }

        // CatmullRomSpline

        // CeilingFunction

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(-1, -1)]
        public void CeilingFunctionIntegerYieldsInteger(
            float arg, float expected)
        {
            // given
            var expr = new FunctionCall(CeilingFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat());
        }

        [Test]
        [TestCase(0.5f, 1)]
        [TestCase(-0.5f, 0)]
        public void CeilingFunctionNonIntegerYieldsInteger(
            float arg, float expected)
        {
            // given
            var expr = new FunctionCall(CeilingFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat());
        }

        // CosecantFunction

        [Test]
        // [TestCase(0, 1/0)]
        [TestCase((float)(Math.PI / 6), 2)]
        [TestCase((float)(Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(Math.PI / 3), 1.154700538379252f)]
        [TestCase((float)(Math.PI / 2), 1)]
        [TestCase((float)(3 * Math.PI / 4), 1.414213562373095f)]
        // [TestCase((float)Math.PI, 1/0)]
        [TestCase((float)(5 * Math.PI / 4), -1.414213562373095f)]
        [TestCase((float)(3 * Math.PI / 2), -1)]
        [TestCase((float)(7 * Math.PI / 4), -1.414213562373095f)]
        // [TestCase((float)(2 * Math.PI), 1/0)]
        [TestCase((float)(9 * Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(5 * Math.PI / 2), 1)]
        [TestCase((float)(-Math.PI / 6), -2)]
        [TestCase((float)(-Math.PI / 4), -1.414213562373095f)]
        [TestCase((float)(-Math.PI / 3), -1.154700538379252f)]
        [TestCase((float)(-Math.PI / 2), -1)]
        [TestCase((float)(-3 * Math.PI / 4), -1.414213562373095f)]
        // [TestCase((float)-Math.PI, -1/0)]
        [TestCase((float)(-5 * Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(-3 * Math.PI / 2), 1)]
        [TestCase((float)(-7 * Math.PI / 4), 1.414213562373095f)]
        // [TestCase((float)(-2 * Math.PI), 1/0)]
        [TestCase((float)(-9 * Math.PI / 4), -1.414213562373095f)]
        [TestCase((float)(-5 * Math.PI / 2), -1)]
        public void CosecantFunctionValueYieldsValue(float arg, float expected)
        {
            // given
            var expr = new FunctionCall(CosecantFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // CosineFunction

        [Test]
        [TestCase(0, 1)]
        [TestCase((float)(Math.PI / 6), 0.866025403784439f)]
        [TestCase((float)(Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(Math.PI / 3), 0.5f)]
        [TestCase((float)(Math.PI / 2), 0)]
        [TestCase((float)(3 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)Math.PI, -1)]
        [TestCase((float)(5 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(3 * Math.PI / 2), 0)]
        [TestCase((float)(7 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(2 * Math.PI), 1)]
        [TestCase((float)(9 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(5 * Math.PI / 2), 0)]
        [TestCase((float)(-Math.PI / 6), 0.866025403784439f)]
        [TestCase((float)(-Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(-Math.PI / 3), 0.5f)]
        [TestCase((float)(-Math.PI / 2), 0)]
        [TestCase((float)-Math.PI, -1)]
        [TestCase((float)(-5 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(-3 * Math.PI / 2), 0)]
        [TestCase((float)(-7 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(-2 * Math.PI), 1)]
        [TestCase((float)(-9 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(-5 * Math.PI / 2), 0)]
        public void CosineFunctionValueYieldsValue(float arg, float expected)
        {
            // given
            var expr = new FunctionCall(CosineFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // CotangentFunction

        [Test]
        [TestCase((float)(-5 * Math.PI / 2), 0)]
        [TestCase((float)(-9 * Math.PI / 4), -1)]
        // [TestCase((float)(-2 * Math.PI), inf)]
        [TestCase((float)(-7 * Math.PI / 4), 1)]
        [TestCase((float)(-3 * Math.PI / 2), 0)]
        [TestCase((float)(-5 * Math.PI / 4), -1)]
        // [TestCase((float)-Math.PI, inf)]
        [TestCase((float)(-3 * Math.PI / 4), 1)]
        [TestCase((float)(-Math.PI / 2), 0)]
        [TestCase((float)(-Math.PI / 3), -0.577350269189626f)]
        [TestCase((float)(-Math.PI / 4), -1)]
        [TestCase((float)(-Math.PI / 6), -1.732050807568877f)]
        // [TestCase(0, inf)]
        [TestCase((float)(Math.PI / 6), 1.732050807568877f)]
        [TestCase((float)(Math.PI / 4), 1)]
        [TestCase((float)(Math.PI / 3), 0.577350269189626f)]
        [TestCase((float)(Math.PI / 2), 0)]
        [TestCase((float)(3 * Math.PI / 4), -1)]
        // [TestCase((float)Math.PI, inf)]
        [TestCase((float)(5 * Math.PI / 4), 1)]
        [TestCase((float)(3 * Math.PI / 2), 0)]
        [TestCase((float)(7 * Math.PI / 4), -1)]
        // [TestCase((float)(2 * Math.PI), inf)]
        [TestCase((float)(9 * Math.PI / 4), 1)]
        [TestCase((float)(5 * Math.PI / 2), 0)]
        public void CotangentFunctionValueYieldsValue(
            float arg, float expected)
        {
            // given
            var expr = new FunctionCall(CotangentFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // DistFunction

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 2)]
        [TestCase(1, 0, 1)]
        [TestCase(2, 0, 2)]
        [TestCase(0, -1, 1)]
        [TestCase(0, -2, 2)]
        [TestCase(-1, 0, 1)]
        [TestCase(-2, 0, 2)]
        [TestCase(1, 1, 1.414213562373095f)]
        [TestCase(-1, 1, 1.414213562373095f)]
        [TestCase(1, -1, 1.414213562373095f)]
        [TestCase(-1, -1, 1.414213562373095f)]
        [TestCase(2, 2, 2.82842712474619f)]
        [TestCase(1, 2, 2.23606797749979f)]
        [TestCase(-1, 2, 2.23606797749979f)]
        [TestCase(1, -2, 2.23606797749979f)]
        [TestCase(-1, -2, 2.23606797749979f)]
        [TestCase(2, 1, 2.23606797749979f)]
        [TestCase(-2, 1, 2.23606797749979f)]
        [TestCase(2, -1, 2.23606797749979f)]
        [TestCase(-2, -1, 2.23606797749979f)]
        [TestCase(1.234f, 5.678f, 5.810545585399017f)]
        public void DistFunctionValueYieldsValue(
            float x, float y, float expected)
        {
            // given
            var expr = new FunctionCall(DistFunction.Value,
                new Literal(x), new Literal(y));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // DistSqFunction

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 4)]
        [TestCase(1, 0, 1)]
        [TestCase(2, 0, 4)]
        [TestCase(0, -1, 1)]
        [TestCase(0, -2, 4)]
        [TestCase(-1, 0, 1)]
        [TestCase(-2, 0, 4)]
        [TestCase(1, 1, 2)]
        [TestCase(-1, 1, 2)]
        [TestCase(1, -1, 2)]
        [TestCase(-1, -1, 2)]
        [TestCase(2, 2, 8)]
        [TestCase(1, 2, 5)]
        [TestCase(-1, 2, 5)]
        [TestCase(1, -2, 5)]
        [TestCase(-1, -2, 5)]
        [TestCase(2, 1, 5)]
        [TestCase(-2, 1, 5)]
        [TestCase(2, -1, 5)]
        [TestCase(-2, -1, 5)]
        [TestCase(1.234f, 5.678f, 33.76244f)]
        public void DistSqFunctionValueYieldsValue(
            float x, float y, float expected)
        {
            // given
            var expr = new FunctionCall(DistSqFunction.Value,
                new Literal(x), new Literal(y));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // DivisionOperation

        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, -2, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 0.5f)]
        [TestCase(1, 3, 0.333333333333333f)]
        [TestCase(1, 4, 0.25f)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 0.666666666666667f)]
        [TestCase(2, 4, 0.5f)]
        [TestCase(1, -2, -0.5f)]
        [TestCase(-1, 2, -0.5f)]
        [TestCase(-1, -2, 0.5f)]
        public void DivisionOperationValueYieldsValue(
            float dividend, float divisor, float expected)
        {
            // given
            var expr = new FunctionCall(DivisionOperation.Value,
                new Literal(dividend), new Literal(divisor));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        [Test]
        public void DivisionOperationDivideByZeroThrows()
        {
            // given
            var expr = new FunctionCall(DivisionOperation.Value,
                new Literal(1), new Literal(0));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Division by zero", ex.Message);
        }

        // EqualComparisonOperation

        [Test]
        [TestCase(0, 0, 1)]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, 3, 0)]
        [TestCase(1, 0, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 0)]
        [TestCase(1, 3, 0)]
        [TestCase(2, 0, 0)]
        [TestCase(2, 1, 0)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 0)]
        [TestCase(3, 0, 0)]
        [TestCase(3, 1, 0)]
        [TestCase(3, 2, 0)]
        [TestCase(3, 3, 1)]
        [TestCase(0, -1, 0)]
        [TestCase(1, -1, 0)]
        [TestCase(-1, 1, 0)]
        [TestCase(-1, -1, 1)]
        [TestCase(1.5f, 1.5f, 1)]
        [TestCase(1.5f, 1.5001f, 0)]
        public void EqualComparisonOperationValuesYieldValue(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(EqualComparisonOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // ExponentOperation

        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(1, -1, 1)]
        [TestCase(1, -0.5f, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 0.5f, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(1, 4, 1)]
        [TestCase(2, -1, 0.5f)]
        [TestCase(2, -0.5f, 0.707106781186548f)]
        [TestCase(2, 0, 1)]
        [TestCase(2, 0.5f, 1.414213562373095f)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(2, 3, 8)]
        [TestCase(2, 4, 16)]
        [TestCase(1.234f, 5.678f, 3.299798925315966f)]
        [TestCase(-1, -1, -1)]
        [TestCase(-1, 0, 1)]
        [TestCase(-1, 1, -1)]
        [TestCase(-1, 2, 1)]
        [TestCase(-1, 3, -1)]
        [TestCase(-1, 4, 1)]
        [TestCase(-2, -1, -0.5f)]
        [TestCase(-2, 0, 1)]
        [TestCase(-2, 1, -2)]
        [TestCase(-2, 2, 4)]
        [TestCase(-2, 3, -8)]
        [TestCase(-2, 4, 16)]
        // TODO: divide by zero: infinity or throws?
        // [TestCase(0, -1, )]
        // [TestCase(0, -2, )]
        // TODO: sqrt(-1) currently NaN
        // TODO: complex numbers
        public void ExponentOperationValueYieldsValue(
            float b, float exponent, float expected)
        {
            // given
            var expr = new FunctionCall(ExponentOperation.Value,
                new Literal(b), new Literal(exponent));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // FactorialFunction

        [Test]
        public void FactorialFunctionCallWithNoArgsThrows()
        {
            // given
            var expr = new FunctionCall(FactorialFunction.Value);
            var eval = new Evaluator();
            // expect
            Assert.Throws<ArgumentException>(() =>
                eval.Eval(expr, null));
        }

        [Test]
        public void FactorialFunctionCallWithTwoArgsThrows()
        {
            // given
            var expr = new FunctionCall(FactorialFunction.Value,
                new Literal(1), new Literal(2));
            var eval = new Evaluator();
            // expect
            Assert.Throws<ArgumentException>(() =>
                eval.Eval(expr, null));
        }

        [Test]
        public void FactorialFunctionCallWithThreeArgsThrows()
        {
            // given
            var expr = new FunctionCall(FactorialFunction.Value,
                new Literal(1), new Literal(2), new Literal(4));
            var eval = new Evaluator();
            // expect
            Assert.Throws<ArgumentException>(() =>
                eval.Eval(expr, null));
        }

        [Test]
        [TestCase(0, 1)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(3, 6)]
        [TestCase(4, 24)]
        [TestCase(5, 120)]
        [TestCase(6, 720)]
        [TestCase(7, 5040)]
        [TestCase(8, 40320)]
        [TestCase(9, 362880)]
        [TestCase(10, 3628800)]
        [TestCase(11, 39916800)]
        public void FactorialFunctionValueYieldsValue(
            float arg, float expected)
        {
            // given
            var expr = new FunctionCall(FactorialFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(expected, result.ToNumber().Value);
        }

        // FloorFunction

        [Test]
        [TestCase(0, 0)]
        [TestCase(1, 1)]
        [TestCase(2, 2)]
        [TestCase(-1, -1)]
        public void FloorFunctionIntegerYieldsInteger(
            float arg, float expected)
        {
            // given
            var expr = new FunctionCall(FloorFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat());
        }

        [Test]
        [TestCase(0.5f, 0)]
        [TestCase(-0.5f, -1)]
        public void FloorFunctionNonIntegerYieldsInteger(
            float arg, float expected)
        {
            // given
            var expr = new FunctionCall(FloorFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat());
        }

        // GreaterThanComparisonOperation

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, 3, 0)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 2, 0)]
        [TestCase(1, 3, 0)]
        [TestCase(2, 0, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 0)]
        [TestCase(2, 3, 0)]
        [TestCase(3, 0, 1)]
        [TestCase(3, 1, 1)]
        [TestCase(3, 2, 1)]
        [TestCase(3, 3, 0)]
        [TestCase(0, -1, 1)]
        [TestCase(1, -1, 1)]
        [TestCase(-1, 1, 0)]
        [TestCase(-1, -1, 0)]
        [TestCase(1.5f, 1.5f, 0)]
        [TestCase(1.5f, 1.5001f, 0)]
        [TestCase(1.5001f, 1.5f, 1)]
        public void GreaterThanComparisonOperationValuesYieldValue(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(GreaterThanComparisonOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // GreaterThanOrEqualComparisonOperation

        [Test]
        [TestCase(0, 0, 1)]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, 3, 0)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 0)]
        [TestCase(1, 3, 0)]
        [TestCase(2, 0, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 0)]
        [TestCase(3, 0, 1)]
        [TestCase(3, 1, 1)]
        [TestCase(3, 2, 1)]
        [TestCase(3, 3, 1)]
        [TestCase(0, -1, 1)]
        [TestCase(1, -1, 1)]
        [TestCase(-1, 1, 0)]
        [TestCase(-1, -1, 1)]
        [TestCase(1.5f, 1.5f, 1)]
        [TestCase(1.5f, 1.5001f, 0)]
        [TestCase(1.5001f, 1.5f, 1)]
        public void GreaterThanOrEqualComparisonOperationValuesYieldValue(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(
                GreaterThanOrEqualComparisonOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // LessThanComparisonOperation

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 1)]
        [TestCase(0, 3, 1)]
        [TestCase(1, 0, 0)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(2, 0, 0)]
        [TestCase(2, 1, 0)]
        [TestCase(2, 2, 0)]
        [TestCase(2, 3, 1)]
        [TestCase(3, 0, 0)]
        [TestCase(3, 1, 0)]
        [TestCase(3, 2, 0)]
        [TestCase(3, 3, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(1, -1, 0)]
        [TestCase(-1, 1, 1)]
        [TestCase(-1, -1, 0)]
        [TestCase(1.5f, 1.5f, 0)]
        [TestCase(1.5f, 1.5001f, 1)]
        [TestCase(1.5001f, 1.5f, 0)]
        public void LessThanComparisonOperationValuesYieldValue(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(
                LessThanComparisonOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // LessThanOrEqualComparisonOperation

        [Test]
        [TestCase(0, 0, 1)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 1)]
        [TestCase(0, 3, 1)]
        [TestCase(1, 0, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(2, 0, 0)]
        [TestCase(2, 1, 0)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 1)]
        [TestCase(3, 0, 0)]
        [TestCase(3, 1, 0)]
        [TestCase(3, 2, 0)]
        [TestCase(3, 3, 1)]
        [TestCase(0, -1, 0)]
        [TestCase(1, -1, 0)]
        [TestCase(-1, 1, 1)]
        [TestCase(-1, -1, 1)]
        [TestCase(1.5f, 1.5f, 1)]
        [TestCase(1.5f, 1.5001f, 1)]
        [TestCase(1.5001f, 1.5f, 0)]
        public void LessThanOrEqualComparisonOperationValuesYieldValue(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(
                LessThanOrEqualComparisonOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // TODO LoadImageFunction
        // Log10Function

        [Test]
        [TestCase(0.00001f, -5)]
        [TestCase(0.0001f, -4)]
        [TestCase(0.001f, -3)]
        [TestCase(0.01f, -2)]
        [TestCase(0.03162277660168379f, -1.5f)]
        [TestCase(0.1f, -1)]
        [TestCase(0.3162277660168379f, -0.5f)]
        [TestCase(1, 0)]
        [TestCase(3.162277660168379f, 0.5f)]
        [TestCase(10, 1)]
        [TestCase(31.62277660168379f, 1.5f)]
        [TestCase(100, 2)]
        [TestCase(1000, 3)]
        [TestCase(10000, 4)]
        [TestCase(100000, 5)]
        [TestCase(1000000, 6)]
        [TestCase(1000000000, 9)]
        public void Log10FunctionValueYieldsValue(
            float arg, float expected)
        {
            // given
            var expr = new FunctionCall(Log10Function.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(expected, result.ToNumber().Value);
        }

        [Test]
        public void Log10FunctionZeroThrows()
        {
            // given
            var expr = new FunctionCall(Log10Function.Value,
                new Literal(0));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument must be positive",ex.Message);
        }

        [Test]
        public void Log10FunctionNegativeValueThrows()
        {
            // given
            var expr = new FunctionCall(Log10Function.Value,
                new Literal(-1));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument must be positive",ex.Message);
        }

        // Log2Function

        [Test]
        [TestCase(0.03125f, -5)]
        [TestCase(0.0625f, -4)]
        [TestCase(0.125f, -3)]
        [TestCase(0.25f, -2)]
        [TestCase(0.353553390593274f, -1.5f)]
        [TestCase(0.5f, -1)]
        [TestCase(0.707106781186548f, -0.5f)]
        [TestCase(1, 0)]
        [TestCase(1.414213562373095f, 0.5f)]
        [TestCase(2, 1)]
        [TestCase(2.82842712474619f, 1.5f)]
        [TestCase(4, 2)]
        [TestCase(8, 3)]
        [TestCase(16, 4)]
        [TestCase(32, 5)]
        [TestCase(64, 6)]
        [TestCase(128, 7)]
        public void Log2FunctionValueYieldsValue(
            float arg, float expected)
        {
            // given
            var expr = new FunctionCall(Log2Function.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(expected, result.ToNumber().Value, 0.00001f);
        }

        [Test]
        public void Log2FunctionZeroThrows()
        {
            // given
            var expr = new FunctionCall(Log2Function.Value,
                new Literal(0));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument must be positive",ex.Message);
        }

        [Test]
        public void Log2FunctionNegativeValueThrows()
        {
            // given
            var expr = new FunctionCall(Log2Function.Value,
                new Literal(-1));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument must be positive",ex.Message);
        }

        // LogarithmFunction

        [Test]
        [TestCase(0.25f, 2, -2)]
        [TestCase(0.5f, 2, -1)]
        [TestCase(1, 2, 0)]
        [TestCase(1.414213562373095f, 2, 0.5f)]
        [TestCase(2, 2, 1)]
        [TestCase(2.82842712474619f, 2, 1.5f)]
        [TestCase(4, 2, 2)]
        [TestCase(8, 2, 3)]
        [TestCase(1, 10, 0)]
        [TestCase(10, 10, 1)]
        [TestCase(100, 10, 2)]
        [TestCase(1, 3, 0)]
        [TestCase(3, 3, 1)]
        [TestCase(9, 3, 2)]
        [TestCase(27, 3, 3)]
        [TestCase(81, 3, 4)]
        [TestCase(1, 1.5f, 0)]
        [TestCase(1, 2.5f, 0)]
        [TestCase(1, 3.5f, 0)]
        [TestCase(1, 4.5f, 0)]
        [TestCase(1, 5.5f, 0)]
        [TestCase(2, 0.5f, -1)]
        [TestCase(1.21f, 1.1f, 2)]
        [TestCase(0.81f, 0.9f, 2)]
        public void LogarithmFunctionValueYieldsValue(
            float arg, float b, float expected)
        {
            // given
            var expr = new FunctionCall(LogarithmFunction.Value,
                new Literal(arg), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(expected, result.ToNumber().Value, 0.00001f);
        }

        [Test]
        public void LogarithmFunctionZeroArgThrows()
        {
            // given
            var expr = new FunctionCall(LogarithmFunction.Value,
                new Literal(0), new Literal(2));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument must be positive",ex.Message);
        }

        [Test]
        public void LogarithmFunctionZeroBaseThrows()
        {
            // given
            var expr = new FunctionCall(LogarithmFunction.Value,
                new Literal(2), new Literal(0));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Base must be positive",ex.Message);
        }

        [Test]
        public void LogarithmFunctionBaseOneThrows()
        {
            // given
            var expr = new FunctionCall(LogarithmFunction.Value,
                new Literal(2), new Literal(1));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Base must not be one",ex.Message);
        }

        [Test]
        public void LogarithmFunctionNegativeArgThrows()
        {
            // given
            var expr = new FunctionCall(LogarithmFunction.Value,
                new Literal(-1), new Literal(2));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument must be positive",ex.Message);
        }

        [Test]
        public void LogarithmFunctionNegativeBaseThrows()
        {
            // given
            var expr = new FunctionCall(LogarithmFunction.Value,
                new Literal(2), new Literal(-2));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Base must be positive",ex.Message);
        }

        // LogicalAndOperation

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, 3, 0)]
        [TestCase(1, 0, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(2, 0, 0)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 1)]
        [TestCase(3, 0, 0)]
        [TestCase(3, 1, 1)]
        [TestCase(3, 2, 1)]
        [TestCase(3, 3, 1)]
        [TestCase(0, -1, 0)]
        [TestCase(1, -1, 1)]
        [TestCase(-1, 1, 1)]
        [TestCase(-1, -1, 1)]
        [TestCase(0.1f, 0.1f, 0)]
        [TestCase(0.9f, 0.9f, 0)]
        [TestCase(1.1f, 1.1f, 1)]
        [TestCase(1.9f, 1.9f, 1)]
        [TestCase(1.5f, 1.5f, 1)]
        [TestCase(1.5f, 1.5001f, 1)]
        [TestCase(-0.1f, -0.1f, 0)]
        [TestCase(-0.9f, -0.9f, 0)]
        [TestCase(-1.1f, -1.1f, 1)]
        [TestCase(-1.9f, -1.9f, 1)]
        public void LogicalAndOperationValuesYieldValue(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(LogicalAndOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // LogicalOrOperation

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 1)]
        [TestCase(0, 3, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(2, 0, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 1)]
        [TestCase(2, 3, 1)]
        [TestCase(3, 0, 1)]
        [TestCase(3, 1, 1)]
        [TestCase(3, 2, 1)]
        [TestCase(3, 3, 1)]
        [TestCase(0, -1, 1)]
        [TestCase(1, -1, 1)]
        [TestCase(-1, 1, 1)]
        [TestCase(-1, -1, 1)]
        [TestCase(0.1f, 0.1f, 0)]
        [TestCase(0.9f, 0.9f, 0)]
        [TestCase(1.1f, 1.1f, 1)]
        [TestCase(1.9f, 1.9f, 1)]
        [TestCase(1.5f, 1.5f, 1)]
        [TestCase(1.5f, 1.5001f, 1)]
        [TestCase(-0.1f, -0.1f, 0)]
        [TestCase(-0.9f, -0.9f, 0)]
        [TestCase(-1.1f, -1.1f, 1)]
        [TestCase(-1.9f, -1.9f, 1)]
        public void LogicalOrOperationValuesYieldValue(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(LogicalOrOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // ModularDivision

        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, -2, 0)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(1, 4, 1)]
        [TestCase(2, 1, 0)]
        [TestCase(2, 2, 0)]
        [TestCase(2, 3, 2)]
        [TestCase(2, 4, 2)]
        [TestCase(1, -2, 1)]
        [TestCase(-1, 2, -1)]
        [TestCase(-1, -2, -1)]
        [TestCase(2, 8, 2)]
        [TestCase(-2, 8, -2)]
        [TestCase(2, -8, 2)]
        [TestCase(-2, -8, -2)]
        [TestCase(8, 2, 0)]
        [TestCase(-8, 2, 0)]
        [TestCase(8, -2, 0)]
        [TestCase(-8, -2, 0)]
        [TestCase(3, 8, 3)]
        [TestCase(-3, 8, -3)]
        [TestCase(3, -8, 3)]
        [TestCase(-3, -8, -3)]
        [TestCase(8, 3, 2)]
        [TestCase(-8, 3, -2)]
        [TestCase(8, -3, 2)]
        [TestCase(-8, -3, -2)]
        [TestCase(8.1f, 3.1f, 2)]
        [TestCase(8.9f, 3.9f, 2)]
        [TestCase(-8.1f, 3.1f, -2)]
        [TestCase(-8.9f, 3.9f, -2)]
        [TestCase(8.1f, -3.1f, 2)]
        [TestCase(8.9f, -3.9f, 2)]
        [TestCase(-8.1f, -3.1f, -2)]
        [TestCase(-8.9f, -3.9f, -2)]
        public void ModularDivisionValueYieldsValue(
            float dividend, float divisor, float expected)
        {
            // given
            var expr = new FunctionCall(ModularDivision.Value,
                new Literal(dividend), new Literal(divisor));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        [Test]
        public void ModularDivisionDivideByZeroThrows()
        {
            // given
            var expr = new FunctionCall(ModularDivision.Value,
                new Literal(1), new Literal(0));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Division by zero", ex.Message);
        }

        // MultiplicationOperation

        [Test]
        [TestCase(0, 1, 0)]
        [TestCase(0, 2, 0)]
        [TestCase(0, -1, 0)]
        [TestCase(0, -2, 0)]
        [TestCase(0, 0.5f, 0)]
        [TestCase(1, 1, 1)]
        [TestCase(1, 2, 2)]
        [TestCase(1, 3, 3)]
        [TestCase(1, 4, 4)]
        [TestCase(2, 1, 2)]
        [TestCase(2, 2, 4)]
        [TestCase(2, 3, 6)]
        [TestCase(2, 4, 8)]
        [TestCase(1, -2, -2)]
        [TestCase(-1, 2, -2)]
        [TestCase(-1, -2, 2)]
        [TestCase(1.5f, 1.5f, 2.25f)]
        public void MultiplicationOperationValuesYieldsValue(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(MultiplicationOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        [Test]
        public void MultiplicationOperationCallWithNoArgsThrows()
        {
            // given
            var expr = new FunctionCall(MultiplicationOperation.Value);
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<ArgumentException>(() =>
                eval.Eval(expr, null));
            // and
            Assert.IsTrue(ex.Message.StartsWith("Wrong number of arguments"));
        }

        [Test]
        public void MultiplicationOperationCallWithOneArgThrows()
        {
            // given
            var expr = new FunctionCall(MultiplicationOperation.Value,
                new Literal(2));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<ArgumentException>(() =>
                eval.Eval(expr, null));
            // and
            Assert.IsTrue(ex.Message.StartsWith("Wrong number of arguments"));
        }

        [Test]
        public void MultiplicationOperationCallWithTwoArgsYieldsProduct()
        {
            // given
            var expr = new FunctionCall(MultiplicationOperation.Value,
                new Literal(2), new Literal(3));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(6, result.ToNumber().Value);
        }

        [Test]
        public void MultiplicationOperationCallWithThreeArgsYieldsProduct()
        {
            // given
            var expr = new FunctionCall(MultiplicationOperation.Value,
                new Literal(2), new Literal(3), new Literal(5));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(30, result.ToNumber().Value);
        }

        // NaturalLogarithmFunction

        [Test]
        [TestCase(0.006737946999085f, -5)]
        [TestCase(0.018315638888734f, -4)]
        [TestCase(0.049787068367864f, -3)]
        [TestCase(0.135335283236613f, -2)]
        [TestCase(0.22313016014843f, -1.5f)]
        [TestCase(0.367879441171442f, -1)]
        [TestCase(0.606530659712633f, -0.5f)]
        [TestCase(1, 0)]
        [TestCase(1.648721270700128f, 0.5f)]
        [TestCase(2.718281828459045f, 1)]
        [TestCase(4.481689070338065f, 1.5f)]
        [TestCase(7.38905609893065f, 2)]
        [TestCase(20.085536923187668f, 3)]
        [TestCase(54.598150033144239f, 4)]
        [TestCase(148.413159102576603f, 5)]
        [TestCase(403.428793492735123f, 6)]
        [TestCase(8103.083927575384008f, 9)]
        public void NaturalLogarithmFunctionValueYieldsValue(
            float arg, float expected)
        {
            // given
            var expr = new FunctionCall(NaturalLogarithmFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(expected, result.ToNumber().Value, 0.00001f);
        }

        [Test]
        public void NaturalLogarithmFunctionZeroThrows()
        {
            // given
            var expr = new FunctionCall(NaturalLogarithmFunction.Value,
                new Literal(0));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument must be positive",ex.Message);
        }

        [Test]
        public void NaturalLogarithmFunctionNegativeValueThrows()
        {
            // given
            var expr = new FunctionCall(NaturalLogarithmFunction.Value,
                new Literal(-1));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<OperandException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument must be positive",ex.Message);
        }

        // NegationOperation

        [Test]
        [TestCase(-2, 2)]
        [TestCase(-1.5f, 1.5f)]
        [TestCase(-1, 1)]
        [TestCase(-0.5f, 0.5f)]
        [TestCase(0, 0)]
        [TestCase(0.5f, -0.5f)]
        [TestCase(1, -1)]
        [TestCase(1.5f, -1.5f)]
        [TestCase(2, -2)]
        public void NegationValuesYieldValue(float arg, float expected)
        {
            // given
            var expr = new FunctionCall(NegationOperation.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // NotEqualComparisonOperation

        [Test]
        [TestCase(0, 0, 0)]
        [TestCase(0, 1, 1)]
        [TestCase(0, 2, 1)]
        [TestCase(0, 3, 1)]
        [TestCase(1, 0, 1)]
        [TestCase(1, 1, 0)]
        [TestCase(1, 2, 1)]
        [TestCase(1, 3, 1)]
        [TestCase(2, 0, 1)]
        [TestCase(2, 1, 1)]
        [TestCase(2, 2, 0)]
        [TestCase(2, 3, 1)]
        [TestCase(3, 0, 1)]
        [TestCase(3, 1, 1)]
        [TestCase(3, 2, 1)]
        [TestCase(3, 3, 0)]
        [TestCase(0, -1, 1)]
        [TestCase(1, -1, 1)]
        [TestCase(-1, 1, 1)]
        [TestCase(-1, -1, 0)]
        [TestCase(1.5f, 1.5f, 0)]
        [TestCase(1.5f, 1.5001f, 1)]
        public void NotEqualComparisonOperationValuesYieldValue(
            float a, float b, float expected)
        {
            // given
            var expr = new FunctionCall(NotEqualComparisonOperation.Value,
                new Literal(a), new Literal(b));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // SecantFunction

        [Test]
        [TestCase(0, 1)]
        [TestCase((float)(Math.PI / 6), 1.154700538379252f)]
        [TestCase((float)(Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(Math.PI / 3), 2)]
        // [TestCase((float)(Math.PI / 2), 1/0)]
        [TestCase((float)(3 * Math.PI / 4), -1.414213562373095f)]
        [TestCase((float)Math.PI, -1)]
        [TestCase((float)(5 * Math.PI / 4), -1.414213562373095f)]
        // [TestCase((float)(3 * Math.PI / 2), 1/0)]
        [TestCase((float)(7 * Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(2 * Math.PI), 1)]
        [TestCase((float)(9 * Math.PI / 4), 1.414213562373095f)]
        // [TestCase((float)(5 * Math.PI / 2), 1/0)]
        [TestCase((float)(-Math.PI / 6), 1.154700538379252f)]
        [TestCase((float)(-Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(-Math.PI / 3), 2)]
        // [TestCase((float)(-Math.PI / 2), 1/0)]
        [TestCase((float)-Math.PI, -1)]
        [TestCase((float)(-5 * Math.PI / 4), -1.414213562373095f)]
        // [TestCase((float)(-3 * Math.PI / 2), 1/0)]
        [TestCase((float)(-7 * Math.PI / 4), 1.414213562373095f)]
        [TestCase((float)(-2 * Math.PI), 1)]
        [TestCase((float)(-9 * Math.PI / 4), 1.414213562373095f)]
        // [TestCase((float)(-5 * Math.PI / 2), 1/0)]
        public void SecantFunctionValueYieldsValue(float arg, float expected)
        {
            // given
            var expr = new FunctionCall(SecantFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // SineFunction

        [Test]
        [TestCase(0, 0)]
        [TestCase((float)(Math.PI / 6), 0.5f)]
        [TestCase((float)(Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(Math.PI / 3), 0.866025403784439f)]
        [TestCase((float)(Math.PI / 2), 1)]
        [TestCase((float)(3 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)Math.PI, 0)]
        [TestCase((float)(5 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(3 * Math.PI / 2), -1)]
        [TestCase((float)(7 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(2 * Math.PI), 0)]
        [TestCase((float)(9 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(5 * Math.PI / 2), 1)]
        [TestCase((float)(-Math.PI / 6), -0.5f)]
        [TestCase((float)(-Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(-Math.PI / 3), -0.866025403784439f)]
        [TestCase((float)(-Math.PI / 2), -1)]
        [TestCase((float)(-3 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)-Math.PI, -0)]
        [TestCase((float)(-5 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(-3 * Math.PI / 2), 1)]
        [TestCase((float)(-7 * Math.PI / 4), 0.707106781186548f)]
        [TestCase((float)(-2 * Math.PI), 0)]
        [TestCase((float)(-9 * Math.PI / 4), -0.707106781186548f)]
        [TestCase((float)(-5 * Math.PI / 2), -1)]
        public void SineFunctionValueYieldsValue(float arg, float expected)
        {
            // given
            var expr = new FunctionCall(SineFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // SizeFunction

        [Test]
        public void SizeFunctionZeroArgumentsThrows()
        {
            // given
            var expr = new FunctionCall(SizeFunction.Value);
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Wrong number of arguments given to " +
                            "size (expected 1 but got 0)",
                ex.Message);
        }

        [Test]
        public void SizeFunctionTwoArgumentsThrows()
        {
            // given
            var expr = new FunctionCall(SizeFunction.Value,
                new Literal(new Vector(new float[] { 1, 2, 3 })),
                new Literal(new Vector(new float[] { 4, 5, 6 })));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Wrong number of arguments given to " +
                            "size (expected 1 but got 2)",
                ex.Message);
        }

        [Test]
        public void SizeFunctionNonTensorThrows()
        {
            // given
            var expr = new FunctionCall(SizeFunction.Value,
                new Literal(1));
            var eval = new Evaluator();
            // expect
            var ex = Assert.Throws<ArgumentException>(
                () => eval.Eval(expr, null));
            // and
            Assert.AreEqual("Argument wrong type: expected " +
                            "Vector or Matrix or String but got Scalar",
                ex.Message);
        }

        [Test]
        public void SizeFunctionVectorYieldsLength()
        {

            // given
            var expr = new FunctionCall(SizeFunction.Value,
                new Literal(new float[] { 1, 2 }.ToVector()));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(2, result.ToFloat());
        }

        [Test]
        public void SizeFunctionMatrixYieldsRowAndColumnCounts()
        {
            // given
            var expr = new FunctionCall(SizeFunction.Value,
                new Literal(
                    new Matrix(new float[,]
                    {
                        { 1, 2, 3 },
                        { 4, 5, 6 }
                    })));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsVector(null));
            var v = result.ToVector();
            Assert.AreEqual(2, v.Length);
            Assert.AreEqual(2, v[0].ToFloat());
            Assert.AreEqual(3, v[1].ToFloat());
        }

        [Test]
        public void SizeFunctionStringYieldsLength()
        {
            // given
            var expr = new FunctionCall(SizeFunction.Value,
                new Literal("abc".ToStringValue()));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(3, result.ToFloat());
        }

        // TangentFunction

        [Test]
        // [TestCase((float)(-5 * Math.PI / 2), inf)]
        [TestCase((float)(-9 * Math.PI / 4), -1)]
        [TestCase((float)(-2 * Math.PI), 0)]
        [TestCase((float)(-7 * Math.PI / 4), 1)]
        // [TestCase((float)(-3 * Math.PI / 2), inf)]
        [TestCase((float)(-5 * Math.PI / 4), -1)]
        [TestCase((float)-Math.PI, 0)]
        [TestCase((float)(-3 * Math.PI / 4), 1)]
        // [TestCase((float)(-Math.PI / 2), -inf)]
        [TestCase((float)(-Math.PI / 3), -1.732050807568877f)]
        [TestCase((float)(-Math.PI / 4), -1)]
        [TestCase((float)(-Math.PI / 6), -0.577350269189626f)]
        [TestCase(0, 0)]
        [TestCase((float)(Math.PI / 6), 0.577350269189626f)]
        [TestCase((float)(Math.PI / 4), 1)]
        [TestCase((float)(Math.PI / 3), 1.732050807568877f)]
        // [TestCase((float)(Math.PI / 2), inf)]
        [TestCase((float)(3 * Math.PI / 4), -1)]
        [TestCase((float)Math.PI, 0)]
        [TestCase((float)(5 * Math.PI / 4), 1)]
        // [TestCase((float)(3 * Math.PI / 2), inf)]
        [TestCase((float)(7 * Math.PI / 4), -1)]
        [TestCase((float)(2 * Math.PI), 0)]
        [TestCase((float)(9 * Math.PI / 4), 1)]
        // [TestCase((float)(5 * Math.PI / 2), inf)]
        public void TangentFunctionValueYieldsValue(float arg, float expected)
        {
            // given
            var expr = new FunctionCall(TangentFunction.Value,
                new Literal(arg));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(expected, result.ToFloat(), 0.000001f);
        }

        // UnitStepFunction

        [Test]
        public void UnitStepFunctionPositiveYieldsOne()
        {
            // given
            var expr = new FunctionCall(UnitStepFunction.Value,
                new Literal(1));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void UnitStepFunctionZeroYieldsOne()
        {
            // given
            var expr = new FunctionCall(UnitStepFunction.Value,
                new Literal(0));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(1, result.ToNumber().Value);
        }

        [Test]
        public void UnitStepFunctionNegativeYieldsZero()
        {
            // given
            var expr = new FunctionCall(UnitStepFunction.Value,
                new Literal(-1));
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.AreEqual(0, result.ToNumber().Value);
        }

        // UserDefinedFunction

        [Test]
        public void UserDefinedFunctionYieldsValue()
        {
            // given
            var f = new UserDefinedFunction(
                "f",
                new[] { "a", "b", "c" },
                new FunctionCall(
                    AdditionOperation.Value,
                    new VariableAccess("a"),
                    new VariableAccess("b"),
                    new VariableAccess("c")));
            var expr = new FunctionCall(f,
                new Literal(1), new Literal(2), new Literal(4));
            var env = new SolusEnvironment();
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.AreEqual(7, result.ToNumber().Value);
        }

        [Test]
        public void UserDefinedFunctionNotAffectedByPreexistingVars()
        {
            // given
            var f = new UserDefinedFunction(
                "f",
                new[] { "a", "b", "c" },
                new FunctionCall(
                    AdditionOperation.Value,
                    new VariableAccess("a"),
                    new VariableAccess("b"),
                    new VariableAccess("c")));
            var expr = new FunctionCall(f,
                new Literal(1), new Literal(2), new Literal(4));
            var env = new SolusEnvironment();
            env.SetVariable("a", new Literal(8));
            var eval = new Evaluator();
            // precondition
            Assert.IsInstanceOf<Literal>(env.GetVariable("a"));
            Assert.AreEqual(8,
                ((Literal)env.GetVariable("a")).Value.ToNumber().Value);
            Assert.IsFalse(env.ContainsVariable("b"));
            Assert.IsFalse(env.ContainsVariable("c"));
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.AreEqual(7, result.ToNumber().Value);
            // and
            Assert.IsInstanceOf<Literal>(env.GetVariable("a"));
            Assert.AreEqual(8,
                ((Literal)env.GetVariable("a")).Value.ToNumber().Value);
            Assert.IsFalse(env.ContainsVariable("b"));
            Assert.IsFalse(env.ContainsVariable("c"));
        }
    }
}
