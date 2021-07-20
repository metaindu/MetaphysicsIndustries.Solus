
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Macros;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.MacrosT.IfMacroT
{
    public class IfMacroTest
    {
        [Test]
        public void HasDefaultValues()
        {
            // given
            var result = IfMacro.Value;
            // expect
            Assert.AreEqual("if", result.Name);
            Assert.AreEqual(3, result.NumArguments);
            Assert.False(result.HasVariableNumArgs);
        }

        class MockExpression : Expression
        {
            public MockExpression(Func<SolusEnvironment, IMathObject> evalf)
            {
                EvalF = evalf;
            }

            public Func<SolusEnvironment, IMathObject> EvalF;
            public override IMathObject Eval(SolusEnvironment env)
            {
                if (EvalF != null) return EvalF(env);
                throw new System.NotImplementedException();
            }

            public override Expression Clone() =>
                throw new NotImplementedException();

            public override void AcceptVisitor(IExpressionVisitor visitor) =>
                throw new NotImplementedException();
        }

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
            // when
            var result = IfMacro.Value.Call(args, null);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.AreEqual(0, literal.Value);
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
            // when
            var result = IfMacro.Value.Call(args, null);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.AreEqual(0, literal.Value);
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
            // when
            var result = IfMacro.Value.Call(args, null);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.AreEqual(0, literal.Value);
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
            // when
            var result = IfMacro.Value.Call(args, null);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.AreEqual(0, literal.Value);
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
            // when
            var result = IfMacro.Value.Call(args, null);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.AreEqual(0, literal.Value);
            // and
            Assert.False(thenEvaled);
            Assert.True(elseEvaled);
        }
    }
}
