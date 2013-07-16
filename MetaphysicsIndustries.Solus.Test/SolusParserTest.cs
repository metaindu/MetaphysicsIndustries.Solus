using System;
using NUnit.Framework;
using MetaphysicsIndustries.Collections;

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
        public void TestParensAndOperators()
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

        [Test]
        public void TestManyOperands()
        {
            var parser = new SolusParser();

            var expr = parser.Compile("1 + a + 2 + b + 3 + c");

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;

            Assert.IsInstanceOf<AdditionOperation>(fcall.Function);
            Assert.GreaterOrEqual(fcall.Arguments.Count, 4);
            Assert.LessOrEqual(fcall.Arguments.Count, 6);

            Set<string> varnames = new Set<string>();

            float sum = 0;
            foreach (var arg in fcall.Arguments)
            {
                Assert.That(arg is Literal || arg is VariableAccess);
                if (arg is Literal)
                {
                    sum += (arg as Literal).Value;
                }
                else
                {
                    varnames.Add((arg as VariableAccess).Variable.Name);
                }
            }

            Assert.AreEqual(6.0f, sum);
            Assert.That(varnames.Contains("a"));
            Assert.That(varnames.Contains("b"));
            Assert.That(varnames.Contains("c"));
        }
    }
}

