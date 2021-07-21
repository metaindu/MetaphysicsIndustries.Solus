using System;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ExpressionsT.StringExpressionT
{
    [TestFixture]
    public class SpringExpressionTest
    {
        [Test]
        public void CreateSetsValue()
        {
            // when
            var result = new StringExpression("abc");
            // then
            Assert.AreEqual("abc", result.Value);
        }

        [Test]
        public void CreateWithNullArgumentThrows()
        {
            // expect
            var ex = Assert.Throws<ArgumentNullException>(
                () => new StringExpression(null));
            // and
            Assert.AreEqual("value", ex.ParamName);
        }

        [Test]
        public void EvalYieldsStringValue()
        {
            // given
            var expr = new StringExpression("def");
            // when
            var result = expr.Eval(null);
            // then
            Assert.IsTrue(result.IsString);
            Assert.AreEqual(3, result.GetDimension(0));
            Assert.AreEqual("def", result.ToStringValue().Value);
        }

        [Test]
        public void CloneCreatesNewStringExpressionObject()
        {
            // given
            var expr = new StringExpression("ghi");
            // when
            var result = expr.Clone();
            // then
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<StringExpression>(result);
            var expr2 = (StringExpression) result;
            Assert.AreEqual("ghi", expr2.Value);
            Assert.AreNotSame(expr, expr2);
        }

        [Test]
        public void AcceptVisitorThrows()
        {
            // given
            var expr = new StringExpression("jkl");
            // expect
            Assert.Throws<NotImplementedException>(
                () => expr.AcceptVisitor(null));
        }

        [Test]
        public void ToStringYieldsValue()
        {
            // given
            var expr = new StringExpression("mno");
            // when
            var result = expr.ToString();
            // then
            Assert.AreEqual("mno", result);
        }
    }
}
