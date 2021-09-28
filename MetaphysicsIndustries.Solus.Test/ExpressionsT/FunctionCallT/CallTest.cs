using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.FunctionCallT
{
    [TestFixture]
    public class CallTest
    {
        [Test]
        public void ArgumentsAreEvaluated()
        {
            // given
            var calls = new List<int>();
            var f = new MockFunction(0, calls,
                new[] {Types.Scalar, Types.Scalar});
            var a1 = new MockExpression(1, calls);
            var a2 = new MockExpression(2, calls);
            var expr = new FunctionCall(f, a1, a2);
            // when
            var result = expr.Eval(null);
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Number>(result);
            var number = (Number) result;
            Assert.AreEqual(0, number.Value);
            // and
            Assert.AreEqual(3,calls.Count);
            // arguments are evaluated first
            Assert.AreEqual(1,calls[0]);
            Assert.AreEqual(2,calls[1]);
            // then the actual function is called
            Assert.AreEqual(0,calls[2]);
        }

        public class MockFunction : Function
        {
            public MockFunction(int value, List<int> calls, Types[] paramTypes,
                string name = "f") 
                : base(paramTypes, name)
            {
                _value = value;
                _calls = calls;
            }
            private readonly int _value;
            private readonly List<int> _calls;
            protected override IMathObject InternalCall(SolusEnvironment env, IMathObject[] args)
            {
                _calls.Add(_value);
                return new Number(_value);
            }
        }

        public class MockExpression : Expression
        {
            public MockExpression(int value, List<int> calls)
            {
                _value = value;
                _calls = calls;
            }
            private readonly int _value;
            private readonly List<int> _calls;
            public override IMathObject Eval(SolusEnvironment env)
            {
                _calls.Add(_value);
                return new Number(_value);
            }
            public override Expression Clone() =>
                throw new System.NotImplementedException();
            public override void AcceptVisitor(IExpressionVisitor visitor) =>
                throw new System.NotImplementedException();
        }
    }
}