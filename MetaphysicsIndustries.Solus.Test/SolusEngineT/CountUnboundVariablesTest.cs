using MetaphysicsIndustries.Solus.Expressions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.SolusEngineT
{
    [TestFixture]
    public class CountUnboundVariablesTest
    {
        [Test]
        public void NoVariablesYieldsZero()
        {
            // given
            var parser = new SolusParser();
            var expr = parser.GetExpression("1+2*(3+4)");
            var env = new SolusEnvironment();
            // when
            var result = SolusEngine.CountUnboundVariables(expr, env);
            // then
            Assert.AreEqual(0, result);
        }
        [Test]
        public void UnboundVariablesYieldsNonZero()
        {
            // given
            var parser = new SolusParser();
            var expr = parser.GetExpression("x+y");
            var env = new SolusEnvironment();
            // when
            var result = SolusEngine.CountUnboundVariables(expr, env);
            // then
            Assert.AreEqual(2, result);
        }
        [Test]
        public void BoundVariablesYieldZero()
        {
            // given
            var parser = new SolusParser();
            var expr = parser.GetExpression("x+y");
            var env = new SolusEnvironment();
            env.SetVariable("x", new Literal(1));
            env.SetVariable("y", new Literal(2));
            // when
            var result = SolusEngine.CountUnboundVariables(expr, env);
            // then
            Assert.AreEqual(0, result);
        }
        [Test]
        public void VariableDefinedWithUnboundVariablesYieldsNonZero()
        {
            // given
            var parser = new SolusParser();
            var expr = parser.GetExpression("x+y");
            var env = new SolusEnvironment();
            env.SetVariable("x", parser.GetExpression("z*w"));
            // when
            var result = SolusEngine.CountUnboundVariables(expr, env);
            // then
            Assert.AreEqual(3, result);
        }
        [Test]
        public void VariableDefinedWithboundVariablesDoesNotIncreaseCount()
        {
            // given
            var parser = new SolusParser();
            var expr = parser.GetExpression("x+y");
            var env = new SolusEnvironment();
            env.SetVariable("x", parser.GetExpression("z*w"));
            env.SetVariable("z", new Literal(1));
            env.SetVariable("w", new Literal(2));
            // when
            var result = SolusEngine.CountUnboundVariables(expr, env);
            // then
            Assert.AreEqual(1, result);
        }
        [Test]
        public void MultipleReferencesToTheSameVariableOnlyCountAsOne()
        {
            // given
            var parser = new SolusParser();
            var expr = parser.GetExpression("x+y");
            var env = new SolusEnvironment();
            env.SetVariable("x", parser.GetExpression("z+1"));
            env.SetVariable("y", parser.GetExpression("z+2"));
            // when
            var result = SolusEngine.CountUnboundVariables(expr, env);
            // then
            Assert.AreEqual(1, result);
        }
    }
}