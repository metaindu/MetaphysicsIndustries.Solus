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
