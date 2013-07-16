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

        [Test]
        public void TestFunctionCalls()
        {
            var parser = new SolusParser();

            var expr = parser.Compile("sin(pi)", cleanup:false);

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<SineFunction>(fcall.Function);
            Assert.AreEqual(1, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.AreEqual(Math.PI, (fcall.Arguments[0] as Literal).Value, 0.0001f);
            Assert.AreEqual(0f, fcall.Eval(new VariableTable()).Value, 0.0001f);


            expr = parser.Compile("derive(x, x)", cleanup:false);

            Assert.IsInstanceOf<Literal>(expr);
            Assert.AreEqual(1f, (expr as Literal).Value);


            expr = parser.Compile("if(1, 2, 3)", cleanup:false);

            Assert.IsInstanceOf<FunctionCall>(expr);
            fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<IfFunction>(fcall.Function);;
            Assert.AreEqual(3, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[2]);
            Assert.AreEqual(1, (fcall.Arguments[0] as Literal).Value);
            Assert.AreEqual(2, (fcall.Arguments[1] as Literal).Value);
            Assert.AreEqual(3, (fcall.Arguments[2] as Literal).Value);
        }

        public class CustomAsdfFunction : Function
        {
            public CustomAsdfFunction()
                : base("adsf")
            {
                Types.Clear();
                Types.Add(typeof(Expression));
                Types.Add(typeof(Expression));
            }

            protected override Literal InternalCall(VariableTable varTable, Literal[] args)
            {
                return new Literal(3);
            }
        }

        [Test]
        public void TestCustomFunctionCalls()
        {
            var parser = new SolusParser();
            var func = new CustomAsdfFunction();
            parser.AddFunction(new SolusParser.ExFunction {
                Token = "asdf",
                Converter = SolusParser.BasicFunctionConverter(func),
                NumArguments = 2,
                HasVariableNumArgs = false,
            });


            var expr = parser.Compile("asdf(1, 2)", cleanup:false);

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (expr as FunctionCall);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.AreEqual(1f, (fcall.Arguments[0] as Literal).Value);
            Assert.AreEqual(2f, (fcall.Arguments[1] as Literal).Value);
            Assert.AreEqual(3f, expr.Eval(new VariableTable()).Value);


            expr = parser.Compile("asdf(4, 5)");

            Assert.IsInstanceOf<Literal>(expr);
            Assert.AreEqual(3f, (expr as Literal).Value);


            Assert.Throws<SolusParseException>(() => expr = parser.Compile("asdf(6, 7, 8)", cleanup:false));


        }

        class CountArgsFunction : Function
        {
            public CountArgsFunction()
                : base("count")
            {
            }

            protected override Literal InternalCall(VariableTable varTable, Literal[] args)
            {
                return new Literal(args.Length);
            }

            protected override void CheckArguments(Expression[] args)
            {
            }
        }

        [Test]
        public void TestVarArgFunctionCalls()
        {
            var parser = new SolusParser();
            var func = new CountArgsFunction();
            parser.AddFunction(new SolusParser.ExFunction {
                Token = "count",
                Converter = SolusParser.BasicFunctionConverter(func),
//                NumArguments = -1,
                HasVariableNumArgs = true,
            });
            var vars = new VariableTable();


            var expr = parser.Compile("count(1, 2, 3)", cleanup:false);

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (expr as FunctionCall);
            Assert.AreEqual(3, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[2]);
            Assert.AreEqual(1f, (fcall.Arguments[0] as Literal).Value);
            Assert.AreEqual(2f, (fcall.Arguments[1] as Literal).Value);
            Assert.AreEqual(3f, (fcall.Arguments[2] as Literal).Value);
            Assert.AreEqual(3f, expr.Eval(new VariableTable()).Value);


            Assert.AreEqual(0, parser.Compile("count()").Eval(vars).Value);
            Assert.AreEqual(1, parser.Compile("count(1)").Eval(vars).Value);
            Assert.AreEqual(2, parser.Compile("count(1, 2)").Eval(vars).Value);
            Assert.AreEqual(4, parser.Compile("count(1, 2, 3, 4)").Eval(vars).Value);
        }

    }
}

