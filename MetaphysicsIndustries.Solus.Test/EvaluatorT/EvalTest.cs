
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
        public void VariableAccessGetsValueFromVariable()
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
        // ArccosineFunction
        // ArccotangentFunction
        // ArcsecantFunction
        // ArcsineFunction
        // Arctangent2Function
        // ArctangentFunction
        // AssociativeCommutativeOperation
        // BitwiseAndOperation
        // BitwiseOrOperation
        // CatmullRomSpline
        // CeilingFunction
        // CosecantFunction
        // CosineFunction
        // CotangentFunction
        // DistFunction
        // DistSqFunction
        // DivisionOperation
        // EqualComparisonOperation
        // ExponentOperation
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
        // GreaterThanComparisonOperation
        // GreaterThanOrEqualComparisonOperation
        // LessThanComparisonOperation
        // LessThanOrEqualComparisonOperation
        // LoadImageFunction
        // Log10Function
        // Log2Function
        // LogarithmFunction
        // LogicalAndOperation
        // LogicalOrOperation
        // ModularDivision
        // MultiplicationOperation
        // NaturalLogarithmFunction
        // NegationOperation
        // NotEqualComparisonOperation
        // SecantFunction
        // SineFunction
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
    }
}
