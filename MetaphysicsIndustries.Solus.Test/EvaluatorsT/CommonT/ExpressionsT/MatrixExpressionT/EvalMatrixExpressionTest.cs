
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    ExpressionsT.MatrixExpressionT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalMatrixExpressionTest<T>
        where T : IEvaluator, new()
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Matrix>(result);
            var matrix = (Matrix)result;
            Assert.That(matrix.RowCount, Is.EqualTo(2));
            Assert.That(matrix.ColumnCount, Is.EqualTo(2));
            Assert.That(matrix[0, 0], Is.EqualTo(1.ToNumber()));
            Assert.That(matrix[0, 1], Is.EqualTo(2.ToNumber()));
            Assert.That(matrix[1, 0], Is.EqualTo(3.ToNumber()));
            Assert.That(matrix[1, 1], Is.EqualTo(4.ToNumber()));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Matrix>(result);
            var matrix = (Matrix)result;
            Assert.That(matrix.RowCount, Is.EqualTo(2));
            Assert.That(matrix.ColumnCount, Is.EqualTo(3));
            Assert.That(matrix[0, 0], Is.EqualTo(1.ToNumber()));
            Assert.That(matrix[0, 1], Is.EqualTo(2.ToNumber()));
            Assert.That(matrix[0, 2], Is.EqualTo(3.ToNumber()));
            Assert.That(matrix[1, 0], Is.EqualTo(4.ToNumber()));
            Assert.That(matrix[1, 1], Is.EqualTo(5.ToNumber()));
            Assert.That(matrix[1, 2], Is.EqualTo(6.ToNumber()));
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
            var eval = Util.CreateEvaluator<T>();
            // expect
            var exc = Assert.Throws<NameException>(
                () => eval.Eval(expr, env));
            // and
            Assert.That(exc.Message,
                Is.EqualTo("Variable not found: a"));
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
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.IsInstanceOf<Matrix>(result);
            var matrix = (Matrix)result;
            Assert.That(matrix.RowCount, Is.EqualTo(2));
            Assert.That(matrix.ColumnCount, Is.EqualTo(2));
            Assert.That(matrix[0, 0], Is.EqualTo(1.ToNumber()));
            Assert.That(matrix[0, 1], Is.EqualTo(2.ToNumber()));
            Assert.That(matrix[1, 0], Is.EqualTo(3.ToNumber()));
            Assert.That(matrix[1, 1], Is.EqualTo(5.ToNumber()));
        }

        [Test]
        public void NestedExpressionsThrows()
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
            var eval = Util.CreateEvaluator<T>();
            // expect
            var ex = Assert.Throws<TypeException>(
                () => eval.Eval(expr, null));
            // and
            Assert.That(ex.Message,
                Is.EqualTo("All components must be reals"));
        }
    }
}
