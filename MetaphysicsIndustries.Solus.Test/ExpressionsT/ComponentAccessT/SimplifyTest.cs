
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
using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ComponentAccessT
{
    [TestFixture]
    public class SimplifyTest
    {
        static Vector vector(params float[] values) => new Vector(values);

        static Literal vliteral(params float[] values) =>
            new Literal(new Vector(values));

        static VectorExpression varvector(params string[] values) =>
            new VectorExpression(values.Length,
                values.Select(
                    s => (Expression)new VariableAccess(s)).ToArray());

        static Expression[] litindexes(params float[] values) =>
            values.Select(v => (Expression) new Literal(v)).ToArray();

        static Expression[] varindexes(params string[] values) =>
            values.Select(
                v => (Expression) new VariableAccess(v)).ToArray();

        static MatrixExpression matrix(int columns, params string[] values)
        {
            int rows = values.Length / columns;
            var values2 = new Expression[rows * columns];
            for (var k = 0; k < rows * columns; k++)
                values2[k] = new VariableAccess(values[k]);
            return new MatrixExpression(rows, columns, values2);
        }

        static Expression[] mkindexes(params int[] indexes)
        {
            return indexes.Select(i => (Expression)new Literal(i)).ToArray();
        }

        private static SolusEnvironment empty = new SolusEnvironment();

        [Test]
        public void LiteralExprWithNonLiteralIndexYieldsSame()
        {
            // given
            var expr = vliteral(1, 2, 3);
            var indexes = new Expression[] {new VariableAccess("a")};
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.Simplify(empty);
            // then
            Assert.That(result, Is.SameAs(ca));
        }

        [Test]
        public void LiteralExprWithLiteralIndexYieldsLiteralComponent()
        {
            // given
            var expr = vliteral(4, 5, 6);
            var indexes = new Expression[] {new Literal(1.ToNumber())};
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.Simplify(empty);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var lit = (Literal) result;
            Assert.IsTrue(lit.Value.IsScalar(null));
            Assert.That(lit.Value.ToFloat(), Is.EqualTo(5));
        }

        [Test]
        public void VectorExprWithNonLiteralIndexYieldsComponentAccessExpr()
        {
            // given
            var expr = varvector("a", "b", "c");
            var indexes = varindexes("x");
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.Simplify(empty);
            // then
            Assert.IsInstanceOf<ComponentAccess>(result);
            var ca2 = (ComponentAccess) result;
            Assert.IsInstanceOf<VectorExpression>(ca2.Expr);
            Assert.That(((VectorExpression) ca2.Expr).Length, Is.EqualTo(3));
            Assert.That(ca2.Indexes.Count, Is.EqualTo(1));
            Assert.IsInstanceOf<VariableAccess>(ca2.Indexes[0]);
        }

        [Test]
        public void VectorExprWithLiteralIndexYieldsExprComponent()
        {
            // given
            var expr = varvector("a", "b", "c");
            var indexes = litindexes(1);
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.Simplify(empty);
            // then
            Assert.IsInstanceOf<VariableAccess>(result);
            var va = (VariableAccess) result;
            Assert.That(va.VariableName, Is.EqualTo("b"));
        }

        [Test]
        public void VarAccessWithLiteralIndexYieldsSame()
        {
            // given
            var expr = new VariableAccess("a");
            var indexes = litindexes(1);
            var ca = new ComponentAccess(expr, indexes);
            // when
            var result = ca.Simplify(empty);
            // then
            Assert.That(result, Is.SameAs(ca));
        }

        [Test]
        public void VectorWithIndexYieldsComponent()
        {
            // given
            var expr = new ComponentAccess(
                varvector("a", "b", "c"),
                mkindexes(1));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.IsInstanceOf<VariableAccess>(result);
            Assert.That(((VariableAccess)result).VariableName,
                Is.EqualTo("b"));
        }

        [Test]
        public void MatrixWithIndexesYieldsComponent()
        {
            // given
            var expr = new ComponentAccess(
                matrix(2,
                    "a", "b",
                    "c", "d"),
                mkindexes(1, 1));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.IsInstanceOf<VariableAccess>(result);
            Assert.That(((VariableAccess)result).VariableName,
                Is.EqualTo("d"));
        }

        class MockTensorExpression : TensorExpression
        {
            private readonly IMathObject _result;

            public MockTensorExpression(int tensorRank)
            {
                TensorRank = tensorRank;
                _result = new MockMathObjectF(
                    isScalarF: e => TensorRank == 0,
                    isVectorF: e => TensorRank == 1,
                    isMatrixF: e => TensorRank == 2,
                    getTensorRankF: e => TensorRank,
                    isStringF: e => false);
            }

            public override int TensorRank { get; }

            public override Expression GetComponent(int[] indexes) =>
                throw new NotImplementedException();

            public override IMathObject CustomEval(SolusEnvironment env) =>
                throw new NotImplementedException();
            public override Expression Clone() =>
                throw new NotImplementedException();
            public override void AcceptVisitor(IExpressionVisitor visitor) =>
                throw new NotImplementedException();
            public override IEnumerator<Expression> GetEnumerator() =>
                throw new NotImplementedException();
            public override void ApplyToAll(Modulator mod) =>
                throw new NotImplementedException();

            public override ISet GetResultType(SolusEnvironment env)
            {
                if (TensorRank==0)
                    return Reals.Value;
                if (TensorRank==1)
                    return AllVectors.Value;
                if (TensorRank==2)
                    return AllMatrices.Value;
                throw new NotImplementedException();
            }
        }

        [Test]
        public void NumberYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                new Literal(new Number(0)),
                mkindexes(1));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void VectorWithTooManyIndexesYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                varvector("a", "b", "c"),
                mkindexes(1, 1));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void MatrixWithTooFewIndexesYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                matrix(2,
                    "a", "b",
                    "c", "d"),
                mkindexes(1));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void VectorAsIndexYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                varvector("a", "b", "c"),
                new Expression[]
                {
                    new Literal(new Vector(new float[] { 4, 5, 6 }))
                });
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void MatrixAsIndexYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                varvector("a", "b", "c"),
                new Expression[]
                {
                    new Literal(
                        new Matrix(
                            new float[,] { { 1, 2 }, { 3, 4 } }))
                });
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void StringAsIndexYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                varvector("a", "b", "c"),
                new Expression[]
                {
                    new Literal("abc".ToStringValue())
                });
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void NegativeIndexYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                varvector("a", "b", "c"),
                mkindexes(-1));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        // TODO: check for index greater than vector dimension
        // TODO: check for index greater than matrix dimension

        [Test]
        public void HigherTensorRankObjectYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                new MockTensorExpression(3),
                mkindexes(1, 2, 3));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void VectorWithTooLargeAnIndexYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                varvector("a", "b", "c"),
                mkindexes(3));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void MatrixWithTooLargeRowIndexYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                matrix(2,
                    "a", "b",
                    "c", "d"),
                mkindexes(2, 0));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void MatrixWithTooLargeColumnIndexYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                matrix(2,
                    "a", "b",
                    "c", "d"),
                mkindexes(0, 2));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void IntervalExpressionWithLiteralIndexYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                new IntervalExpression(
                    new Literal(-1), false,
                    new Literal(1), false),
                mkindexes(0));
            var env = new SolusEnvironment();
            // when
            var result = expr.Simplify(env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }
    }
}
