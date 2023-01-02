
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Macros;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    MacrosT.IfMacroT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    [Ignore("Macros not supported currently")]
    public class CallTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        public void TrueConditionEvaluatesSecondArgButNotThirdArg()
        {
            // given
            bool thenEvaled = false;
            bool elseEvaled = false;
            var thenArg = new MockExpression(_ =>
            {
                thenEvaled = true;
                return new Number(0);
            });
            var elseArg = new MockExpression(_ =>
            {
                elseEvaled = true;
                return new Number(0);
            });
            var condition = new Literal(1);
            var args = new Expression[] {condition, thenArg, elseArg};
            var expr = new FunctionCall(new Literal(IfMacro.Value), args);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.That(literal.Value.ToFloat(), Is.EqualTo(0));
            // and
            Assert.True(thenEvaled);
            Assert.False(elseEvaled);
        }

        [Test]
        public void FalseConditionEvaluatesThirdArgButNotSecondArg()
        {
            // given
            bool thenEvaled = false;
            bool elseEvaled = false;
            var thenArg = new MockExpression(_ =>
            {
                thenEvaled = true;
                return new Number(0);
            });
            var elseArg = new MockExpression(_ =>
            {
                elseEvaled = true;
                return new Number(0);
            });
            var condition = new Literal(0);
            var args = new Expression[] {condition, thenArg, elseArg};
            var expr = new FunctionCall(new Literal(IfMacro.Value), args);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.That(literal.Value.ToFloat(), Is.EqualTo(0));
            // and
            Assert.False(thenEvaled);
            Assert.True(elseEvaled);
        }

        [Test]
        public void NanConditionEvaluatesThirdArgButNotSecondArg()
        {
            // given
            bool thenEvaled = false;
            bool elseEvaled = false;
            var thenArg = new MockExpression(_ =>
            {
                thenEvaled = true;
                return new Number(0);
            });
            var elseArg = new MockExpression(_ =>
            {
                elseEvaled = true;
                return new Number(0);
            });
            var condition = new Literal(float.NaN);
            var args = new Expression[] {condition, thenArg, elseArg};
            var expr = new FunctionCall(new Literal(IfMacro.Value), args);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.That(literal.Value.ToFloat(), Is.EqualTo(0));
            // and
            Assert.False(thenEvaled);
            Assert.True(elseEvaled);
        }

        [Test]
        public void PositiveInfinityConditionEvaluatesThirdArgButNotSecondArg()
        {
            // given
            bool thenEvaled = false;
            bool elseEvaled = false;
            var thenArg = new MockExpression(_ =>
            {
                thenEvaled = true;
                return new Number(0);
            });
            var elseArg = new MockExpression(_ =>
            {
                elseEvaled = true;
                return new Number(0);
            });
            var condition = new Literal(float.PositiveInfinity);
            var args = new Expression[] {condition, thenArg, elseArg};
            var expr = new FunctionCall(new Literal(IfMacro.Value), args);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.That(literal.Value.ToFloat(), Is.EqualTo(0));
            // and
            Assert.False(thenEvaled);
            Assert.True(elseEvaled);
        }

        [Test]
        public void NegativeInfinityConditionEvaluatesThirdArgButNotSecondArg()
        {
            // given
            bool thenEvaled = false;
            bool elseEvaled = false;
            var thenArg = new MockExpression(_ =>
            {
                thenEvaled = true;
                return new Number(0);
            });
            var elseArg = new MockExpression(_ =>
            {
                elseEvaled = true;
                return new Number(0);
            });
            var condition = new Literal(float.NegativeInfinity);
            var args = new Expression[] {condition, thenArg, elseArg};
            var expr = new FunctionCall(new Literal(IfMacro.Value), args);
            var eval = Util.CreateEvaluator<T>();
            // when
            var result = eval.Eval(expr, null);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.That(literal.Value.ToFloat(), Is.EqualTo(0));
            // and
            Assert.False(thenEvaled);
            Assert.True(elseEvaled);
        }
    }
}
