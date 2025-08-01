
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

using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Transformers;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.TransformersT.
    ApplyVariablesTransformT
{
    [TestFixture]
    public class ApplyVariablesTransformTest
    {
        [Test]
        public void TransformLiteralYieldsSame()
        {
            // given
            var expr = new Literal(3);
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void TransformVariableWithMissingYieldsSame()
        {
            // given
            var expr = new VariableAccess("a");
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void TransformVariableWithPresentYieldsOther()
        {
            // given
            var expr = new VariableAccess("a");
            var expr2 = new Literal(3);
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", expr2);
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.SameAs(expr2));
        }

        [Test]
        public void TransformVariableWithNonExpressionYieldsOther()
        {
            // given
            var expr = new VariableAccess("a");
            var value = 3.ToNumber();
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", value);
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal)result;
            Assert.That(value, Is.EqualTo(literal.Value));
        }

        [Test]
        public void TransformMatrixYieldsTransformed()
        {
            // given
            var expr = new MatrixExpression(1, 2,
                new VariableAccess("a"),
                new VariableAccess("b"));
            var a = 3.ToNumber();
            var b = 4.ToNumber();
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", a);
            env.SetVariable("b", b);
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.Not.SameAs(expr));
            Assert.IsInstanceOf<MatrixExpression>(result);
            var me = (MatrixExpression)result;
            Assert.That(me.RowCount, Is.EqualTo(1));
            Assert.That(me.ColumnCount, Is.EqualTo(2));
            Assert.That(((Literal)me[0, 0]).Value.ToNumber().Value,
                Is.EqualTo(3));
            Assert.That(((Literal)me[0, 1]).Value.ToNumber().Value,
                Is.EqualTo(4));
        }

        [Test]
        public void TransformMatrixWithSomeMissingYieldsTransformed()
        {
            // given
            var expr2 = new VariableAccess("b");
            var expr = new MatrixExpression(1, 2,
                new VariableAccess("a"),
                expr2);
            var a = 3.ToNumber();
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", a);
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.Not.SameAs(expr));
            Assert.IsInstanceOf<MatrixExpression>(result);
            var me = (MatrixExpression)result;
            Assert.That(me.RowCount, Is.EqualTo(1));
            Assert.That(me.ColumnCount, Is.EqualTo(2));
            Assert.That(((Literal)me[0, 0]).Value.ToNumber().Value,
                Is.EqualTo(3));
            Assert.That(expr2, Is.SameAs(me[0, 1]));
        }

        [Test]
        public void TransformMatrixWithAllMissingYieldsSame()
        {
            // given
            var expr = new MatrixExpression(1, 2,
                new VariableAccess("a"),
                new VariableAccess("b"));
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void TransformVectorYieldsTransformed()
        {
            // given
            var expr = new VectorExpression(2,
                new VariableAccess("a"),
                new VariableAccess("b"));
            var a = 3.ToNumber();
            var b = 4.ToNumber();
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", a);
            env.SetVariable("b", b);
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.Not.SameAs(expr));
            Assert.IsInstanceOf<VectorExpression>(result);
            var ve = (VectorExpression)result;
            Assert.That(ve.Length, Is.EqualTo(2));
            Assert.That(((Literal)ve[0]).Value.ToNumber().Value,
                Is.EqualTo(3));
            Assert.That(((Literal)ve[1]).Value.ToNumber().Value,
                Is.EqualTo(4));
        }

        [Test]
        public void TransformVectorWithSomeMissingYieldsTransformed()
        {
            // given
            var expr2 = new VariableAccess("b");
            var expr = new VectorExpression(2,
                new VariableAccess("a"),
                expr2);
            var a = 3.ToNumber();
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", a);
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.Not.SameAs(expr));
            Assert.IsInstanceOf<VectorExpression>(result);
            var ve = (VectorExpression)result;
            Assert.That(ve.Length, Is.EqualTo(2));
            Assert.That(((Literal)ve[0]).Value.ToNumber().Value,
                Is.EqualTo(3));
            Assert.That(ve[1], Is.SameAs(expr2));
        }

        [Test]
        public void TransformVectorWithAllMissingYieldsSame()
        {
            // given
            var expr = new VectorExpression(2,
                new VariableAccess("a"),
                new VariableAccess("b"));
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void TransformComponentAccessWithAllMissingYieldsSame()
        {
            // given
            var expr = new ComponentAccess(
                new VariableAccess("a"),
                new Expression[]
                {
                    new VariableAccess("b"),
                    new VariableAccess("c"),
                });
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }

        [Test]
        public void TransformComponentAccessWithNoneMissingYieldsTransformed()
        {
            // given
            var expr = new ComponentAccess(
                new VariableAccess("a"),
                new Expression[]
                {
                    new VariableAccess("b"),
                    new VariableAccess("c"),
                });
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", new Matrix(new float[,]
            {
                { 1, 2 },
                { 3, 4 }
            }));
            env.SetVariable("b", 0.ToNumber());
            env.SetVariable("c", 1.ToNumber());
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.IsInstanceOf<ComponentAccess>(result);
            Assert.That(result, Is.Not.SameAs(expr));
            var ca = (ComponentAccess)result;
            Assert.IsInstanceOf<Literal>(ca.Expr);
            Assert.IsInstanceOf<Literal>(ca.Indexes[0]);
            Assert.IsInstanceOf<Literal>(ca.Indexes[1]);
        }

        [Test]
        public void TransformComponentAccessWithExprMissingYieldsTransformed()
        {
            // given
            var expr = new ComponentAccess(
                new VariableAccess("a"),
                new Expression[]
                {
                    new VariableAccess("b"),
                    new VariableAccess("c"),
                });
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("b", 0.ToNumber());
            env.SetVariable("c", 1.ToNumber());
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.IsInstanceOf<ComponentAccess>(result);
            Assert.That(result, Is.Not.SameAs(expr));
            var ca = (ComponentAccess)result;
            Assert.That(ca.Expr, Is.SameAs(expr.Expr));
            Assert.IsInstanceOf<Literal>(ca.Indexes[0]);
            Assert.IsInstanceOf<Literal>(ca.Indexes[1]);
        }

        [Test]
        public void TransformComponentAccessWithIndexMissingYieldsTransform()
        {
            // given
            var expr = new ComponentAccess(
                new VariableAccess("a"),
                new Expression[]
                {
                    new VariableAccess("b"),
                    new VariableAccess("c"),
                });
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", new Matrix(new float[,]
            {
                { 1, 2 },
                { 3, 4 }
            }));
            env.SetVariable("b", 0.ToNumber());
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.IsInstanceOf<ComponentAccess>(result);
            Assert.That(result, Is.Not.SameAs(expr));
            var ca = (ComponentAccess)result;
            Assert.IsInstanceOf<Literal>(ca.Expr);
            Assert.IsInstanceOf<Literal>(ca.Indexes[0]);
            Assert.That(ca.Indexes[1], Is.SameAs(expr.Indexes[1]));
        }

        [Test]
        public void TransformCallWithNoneMissingYieldsTransformed()
        {
            // given
            var expr = new FunctionCall(
                AdditionOperation.Value,
                new VariableAccess("a"),
                new VariableAccess("b"),
                new VariableAccess("c"));
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", 1.ToNumber());
            env.SetVariable("b", 2.ToNumber());
            env.SetVariable("c", 4.ToNumber());
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.IsInstanceOf<FunctionCall>(result);
            Assert.That(result, Is.Not.SameAs(expr));
            var fc = (FunctionCall)result;
            Assert.That(fc.Function, Is.SameAs(expr.Function));
            Assert.IsInstanceOf<Literal>(fc.Arguments[0]);
            Assert.IsInstanceOf<Literal>(fc.Arguments[1]);
            Assert.IsInstanceOf<Literal>(fc.Arguments[2]);
        }

        [Test]
        public void TransformCallWithSomeMissingYieldsTransformed()
        {
            // given
            var expr = new FunctionCall(
                AdditionOperation.Value,
                new VariableAccess("a"),
                new VariableAccess("b"),
                new VariableAccess("c"));
            var expr2 = expr.Arguments[2];
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", 1.ToNumber());
            env.SetVariable("b", 2.ToNumber());
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.IsInstanceOf<FunctionCall>(result);
            Assert.That(result, Is.Not.SameAs(expr));
            var fc = (FunctionCall)result;
            Assert.That(fc.Function, Is.SameAs(expr.Function));
            Assert.IsInstanceOf<Literal>(fc.Arguments[0]);
            Assert.IsInstanceOf<Literal>(fc.Arguments[1]);
            Assert.That(fc.Arguments[2], Is.SameAs(expr2));
        }

        [Test]
        public void TransformCallWithAllMissingYieldsSame()
        {
            // given
            var expr = new FunctionCall(
                AdditionOperation.Value,
                new VariableAccess("a"),
                new VariableAccess("b"),
                new VariableAccess("c"));
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }
        [Test]
        public void TransformIntervalWithNoneMissingYieldsTransformed()
        {
            // given
            var expr = new IntervalExpression(
                new VariableAccess("a"),
                false,
                new VariableAccess("b"),
                false);
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", 1.ToNumber());
            env.SetVariable("b", 2.ToNumber());
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.IsInstanceOf<IntervalExpression>(result);
            Assert.That(result, Is.Not.SameAs(expr));
            var interval = (IntervalExpression)result;
            Assert.IsInstanceOf<Literal>(interval.LowerBound);
            Assert.IsInstanceOf<Literal>(interval.UpperBound);
        }

        [Test]
        public void TransformIntervalWithSomeMissingYieldsTransformed()
        {
            // given
            var expr = new IntervalExpression(
                new VariableAccess("a"),
                false,
                new VariableAccess("b"),
                false);
            var expr2 = expr.UpperBound;
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            env.SetVariable("a", 1.ToNumber());
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.IsInstanceOf<IntervalExpression>(result);
            Assert.That(result, Is.Not.SameAs(expr));
            var interval = (IntervalExpression)result;
            Assert.IsInstanceOf<Literal>(interval.LowerBound);
            Assert.That(interval.UpperBound, Is.SameAs(expr2));
        }

        [Test]
        public void TransformIntervalWithAllMissingYieldsSame()
        {
            // given
            var expr = new IntervalExpression(
                new VariableAccess("a"),
                false,
                new VariableAccess("b"),
                false);
            var tf = new ApplyVariablesTransform();
            var env = new SolusEnvironment();
            // when
            var result = tf.Transform(expr, env);
            // then
            Assert.That(result, Is.SameAs(expr));
        }
    }
}
