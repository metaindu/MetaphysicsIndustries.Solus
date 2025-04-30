
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
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.ExpressionCheckerT.
    FunctionsT.IfOperatorT
{
    [TestFixture]
    public class IsWellDefinedTest
    {
        [Test]
        public void TrueConditionEvaluatesSecondArgButNotThirdArg()
        {
            // given
            var thenArg = new MockExpression(_ =>
            {
                return new Number(0);
            });
            var elseArg = new MockExpression(_ =>
            {
                return new Number(0);
            });
            var condition = new Literal(1);
            var args = new Expression[] {condition, thenArg, elseArg};
            var expr = new FunctionCall(new Literal(IfOperator.Value), args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void FalseConditionEvaluatesThirdArgButNotSecondArg()
        {
            // given
            var thenArg = new MockExpression(_ =>
            {
                return new Number(0);
            });
            var elseArg = new MockExpression(_ =>
            {
                return new Number(0);
            });
            var condition = new Literal(0);
            var args = new Expression[] {condition, thenArg, elseArg};
            var expr = new FunctionCall(new Literal(IfOperator.Value), args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void NanConditionEvaluatesThirdArgButNotSecondArg()
        {
            // given
            var thenArg = new MockExpression(_ =>
            {
                return new Number(0);
            });
            var elseArg = new MockExpression(_ =>
            {
                return new Number(0);
            });
            var condition = new Literal(float.NaN);
            var args = new Expression[] {condition, thenArg, elseArg};
            var expr = new FunctionCall(new Literal(IfOperator.Value), args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void PositiveInfinityConditionEvaluatesThirdArgButNotSecondArg()
        {
            // given
            var thenArg = new MockExpression(_ =>
            {
                return new Number(0);
            });
            var elseArg = new MockExpression(_ =>
            {
                return new Number(0);
            });
            var condition = new Literal(float.PositiveInfinity);
            var args = new Expression[] {condition, thenArg, elseArg};
            var expr = new FunctionCall(new Literal(IfOperator.Value), args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }

        [Test]
        public void NegativeInfinityConditionEvaluatesThirdArgButNotSecondArg()
        {
            // given
            var thenArg = new MockExpression(_ =>
            {
                return new Number(0);
            });
            var elseArg = new MockExpression(_ =>
            {
                return new Number(0);
            });
            var condition = new Literal(float.NegativeInfinity);
            var args = new Expression[] {condition, thenArg, elseArg};
            var expr = new FunctionCall(new Literal(IfOperator.Value), args);
            var ec = new ExpressionChecker();
            // expect
            Assert.DoesNotThrow(() => ec.IsWellDefined(expr, null));
        }
    }
}
