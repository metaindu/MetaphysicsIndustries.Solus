
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.EvaluatorT.ExpressionsT.
    MatrixExpressionT
{
    [TestFixture]
    public class EvalMatrixExpressionTest
    {
        [Test]
        public void LiteralsYieldMatrix()
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
        public void LiteralsYieldMatrix2()
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
        public void UndefinedVariablesInExpressionCauseException()
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
        public void DefinedVariablesInExpressionYieldValue()
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
        public void NestedExpressionsYieldNestedValues()
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
    }
}
