using System;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test
{
    [TestFixture]
    public class SolusParserTest
    {
        [Test]
        public void TestNormal()
        {
            SolusParser parser = new SolusParser();
            VariableTable vars = new VariableTable();

            var expr = parser.Compile("2 + 2");
            var value = expr.Eval(vars).Value;

            Assert.AreEqual(4.0f, value);
        }

        [Test]
        public void TestParens()
        {
            var parser = new SolusParser();

            var expr = parser.Compile("a * (2+c)");

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<MultiplicationOperation>(fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[0]);
            Assert.AreEqual("a", (fcall.Arguments[0] as VariableAccess).Variable.Name);
            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[1]);
            var fcall2 = (FunctionCall)fcall.Arguments[1];
            Assert.IsInstanceOf<AdditionOperation>(fcall2.Function);
            Assert.AreEqual(2, fcall2.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall2.Arguments[0]);
            Assert.AreEqual(2.0f, (fcall2.Arguments[0] as Literal).Value);
            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[1]);
            Assert.AreEqual("c", (fcall2.Arguments[1] as VariableAccess).Variable.Name);
        }
    }
}

