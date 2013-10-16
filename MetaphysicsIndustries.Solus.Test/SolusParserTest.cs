using System;
using NUnit.Framework;
using MetaphysicsIndustries.Collections;
using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus.Test
{
    [TestFixture]
    public class SolusParserTest
    {
        [Test]
        public void TestNormal()
        {
            SolusParser parser = new SolusParser();
            Environment env = new Environment();

            var expr = parser.GetExpression("2 + 2");
            var value = expr.Eval(env).Value;

            Assert.AreEqual(4.0f, value);
        }

        [Test]
        public void TestParensAndOperators()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("a * (2+c)");

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;
            Assert.AreSame(MultiplicationOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[0]);
            Assert.AreEqual("a", (fcall.Arguments[0] as VariableAccess).VariableName);
            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[1]);
            var fcall2 = (FunctionCall)fcall.Arguments[1];
            Assert.AreSame(AdditionOperation.Value, fcall2.Function);
            Assert.AreEqual(2, fcall2.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall2.Arguments[0]);
            Assert.AreEqual(2.0f, (fcall2.Arguments[0] as Literal).Value);
            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[1]);
            Assert.AreEqual("c", (fcall2.Arguments[1] as VariableAccess).VariableName);
        }

        [Test]
        public void TestManyOperands()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 + a + 2 + b + 3 + c");

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
                    varnames.Add((arg as VariableAccess).VariableName);
                }
            }

            Assert.AreEqual(6.0f, sum);
            Assert.That(varnames.Contains("a"));
            Assert.That(varnames.Contains("b"));
            Assert.That(varnames.Contains("c"));
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators1()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 + a + 2 * b * 3 * c");

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;

            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(3, fcall.Arguments.Count);

            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.AreEqual(1.0f, (fcall.Arguments[0] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[1]);
            Assert.AreEqual("a", (fcall.Arguments[1] as VariableAccess).VariableName);

            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[2]);
            var fcall2 = (FunctionCall)fcall.Arguments[2];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(4, fcall2.Arguments.Count);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[0]);
            Assert.AreEqual(2.0f, (fcall2.Arguments[0] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[1]);
            Assert.AreEqual("b", (fcall2.Arguments[1] as VariableAccess).VariableName);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[2]);
            Assert.AreEqual(3.0f, (fcall2.Arguments[2] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[3]);
            Assert.AreEqual("c", (fcall2.Arguments[3] as VariableAccess).VariableName);
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators2()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 + a * 2 * b * 3 + c");

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;

            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(3, fcall.Arguments.Count);

            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.AreEqual(1.0f, (fcall.Arguments[0] as Literal).Value);


            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[1]);
            var fcall2 = (FunctionCall)fcall.Arguments[1];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(4, fcall2.Arguments.Count);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[0]);
            Assert.AreEqual("a", (fcall2.Arguments[0] as VariableAccess).VariableName);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[1]);
            Assert.AreEqual(2.0f, (fcall2.Arguments[1] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[2]);
            Assert.AreEqual("b", (fcall2.Arguments[2] as VariableAccess).VariableName);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[3]);
            Assert.AreEqual(3.0f, (fcall2.Arguments[3] as Literal).Value);


            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[2]);
            Assert.AreEqual("c", (fcall.Arguments[2] as VariableAccess).VariableName);
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators3()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 * a * 2 * b + 3 + c");

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;
            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(3, fcall.Arguments.Count);

            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[0]);
            var fcall2 = (FunctionCall)fcall.Arguments[0];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(4, fcall2.Arguments.Count);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[0]);
            Assert.AreEqual(1.0f, (fcall2.Arguments[0] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[1]);
            Assert.AreEqual("a", (fcall2.Arguments[1] as VariableAccess).VariableName);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[2]);
            Assert.AreEqual(2.0f, (fcall2.Arguments[2] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[3]);
            Assert.AreEqual("b", (fcall2.Arguments[3] as VariableAccess).VariableName);

            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.AreEqual(3.0f, (fcall.Arguments[1] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[2]);
            Assert.AreEqual("c", (fcall.Arguments[2] as VariableAccess).VariableName);
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators4()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 * a * 2 + b + 3 * c");

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;
            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(3, fcall.Arguments.Count);

            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[0]);
            var fcall2 = (FunctionCall)fcall.Arguments[0];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(3, fcall2.Arguments.Count);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[0]);
            Assert.AreEqual(1.0f, (fcall2.Arguments[0] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[1]);
            Assert.AreEqual("a", (fcall2.Arguments[1] as VariableAccess).VariableName);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[2]);
            Assert.AreEqual(2.0f, (fcall2.Arguments[2] as Literal).Value);



            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[1]);
            Assert.AreEqual("b", (fcall.Arguments[1] as VariableAccess).VariableName);


            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[2]);
            fcall2 = (FunctionCall)fcall.Arguments[2];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(2, fcall2.Arguments.Count);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[0]);
            Assert.AreEqual(3.0f, (fcall2.Arguments[0] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[1]);
            Assert.AreEqual("c", (fcall2.Arguments[1] as VariableAccess).VariableName);
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators5()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 * a + 2 + b * 3 * c");

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;
            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(3, fcall.Arguments.Count);

            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[0]);
            var fcall2 = (FunctionCall)fcall.Arguments[0];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(2, fcall2.Arguments.Count);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[0]);
            Assert.AreEqual(1.0f, (fcall2.Arguments[0] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[1]);
            Assert.AreEqual("a", (fcall2.Arguments[1] as VariableAccess).VariableName);


            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.AreEqual(2.0f, (fcall.Arguments[1] as Literal).Value);


            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[2]);
            fcall2 = (FunctionCall)fcall.Arguments[2];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(3, fcall2.Arguments.Count);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[0]);
            Assert.AreEqual("b", (fcall2.Arguments[0] as VariableAccess).VariableName);

            Assert.IsInstanceOf<Literal>(fcall2.Arguments[1]);
            Assert.AreEqual(3.0f, (fcall2.Arguments[1] as Literal).Value);

            Assert.IsInstanceOf<VariableAccess>(fcall2.Arguments[2]);
            Assert.AreEqual("c", (fcall2.Arguments[2] as VariableAccess).VariableName);
        }

        [Test]
        public void TestMixedOperandsAscendingPrecedence()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 | a & 2 + b * 3 ^ c");

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;
            Assert.AreSame(BitwiseOrOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.AreEqual(1.0f, (fcall.Arguments[0] as Literal).Value);
            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[1]);

            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.AreSame(BitwiseAndOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[0]);
            Assert.AreEqual("a", (fcall.Arguments[0] as VariableAccess).VariableName);
            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[1]);

            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.AreEqual(2.0f, (fcall.Arguments[0] as Literal).Value);
            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[1]);

            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.AreSame(MultiplicationOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[0]);
            Assert.AreEqual("b", (fcall.Arguments[0] as VariableAccess).VariableName);
            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[1]);

            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.AreSame(ExponentOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.AreEqual(3.0f, (fcall.Arguments[0] as Literal).Value);
            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[1]);
            Assert.AreEqual("c", (fcall.Arguments[1] as VariableAccess).VariableName);

        }

        [Test]
        public void TestMixedOperandsDescendingPrecedence()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 ^ a * 2 + b & 3 | c");

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (expr as FunctionCall);
            Assert.AreSame(BitwiseOrOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[1]);
            Assert.AreEqual("c", (fcall.Arguments[1] as VariableAccess).VariableName);
            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[0]);

            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.AreSame(BitwiseAndOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.AreEqual(3.0f, (fcall.Arguments[1] as Literal).Value);
            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[0]);

            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[1]);
            Assert.AreEqual("b", (fcall.Arguments[1] as VariableAccess).VariableName);
            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[0]);

            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.AreSame(MultiplicationOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.AreEqual(2.0f, (fcall.Arguments[1] as Literal).Value);
            Assert.IsInstanceOf<FunctionCall>(fcall.Arguments[0]);

            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.AreSame(ExponentOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[1]);
            Assert.AreEqual("a", (fcall.Arguments[1] as VariableAccess).VariableName);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.AreEqual(1.0f, (fcall.Arguments[0] as Literal).Value);
        }

        [Test]
        public void TestFunctionCall1()
        {
            var parser = new SolusParser();
            Environment env = new Environment();
            env.Variables.Add("pi", new Literal((float)Math.PI));

            var expr = parser.GetExpression("sin(pi)", cleanup:false);

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<SineFunction>(fcall.Function);
            Assert.AreEqual(1, fcall.Arguments.Count);
//            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
//            Assert.AreEqual(Math.PI, (fcall.Arguments[0] as Literal).Value, 0.0001f);
            Assert.AreEqual(0f, fcall.Eval(env).Value, 0.0001f);
        }

        [Test]
        public void TestFunctionCall2()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("derive(x, x)", cleanup:false);

            Assert.IsInstanceOf<Literal>(expr);
            Assert.AreEqual(1f, (expr as Literal).Value);
        }

        [Test]
        public void TestFunctionCall3()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("if(1, 2, 3)", cleanup:false);

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<IfFunction>(fcall.Function);
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
                : base("asdf")
            {
                Types.Clear();
                Types.Add(typeof(Expression));
                Types.Add(typeof(Expression));
            }

            protected override Literal InternalCall(Environment env, Literal[] args)
            {
                return new Literal(3);
            }
        }

        [Test]
        public void TestCustomFunctionCalls()
        {
            var parser = new SolusParser();
            var func = new CustomAsdfFunction();
            Environment env = new Environment();
            env.AddFunction(func);


            var expr = parser.GetExpression("asdf(1, 2)", env: env, cleanup:false);

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (expr as FunctionCall);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.AreEqual(1f, (fcall.Arguments[0] as Literal).Value);
            Assert.AreEqual(2f, (fcall.Arguments[1] as Literal).Value);


            float value = expr.Eval(env).Value;

            Assert.AreEqual(3f, value);
        }

        class CountArgsFunction : Function
        {
            public CountArgsFunction()
                : base("count")
            {
            }

            protected override Literal InternalCall(Environment env, Literal[] args)
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
            var env = new Environment();
            env.AddFunction(func);


            var expr = parser.GetExpression("count(1, 2, 3)", env: env, cleanup:false);

            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (expr as FunctionCall);
            Assert.AreEqual(3, fcall.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[2]);
            Assert.AreEqual(1f, (fcall.Arguments[0] as Literal).Value);
            Assert.AreEqual(2f, (fcall.Arguments[1] as Literal).Value);
            Assert.AreEqual(3f, (fcall.Arguments[2] as Literal).Value);
            Assert.AreEqual(3f, expr.Eval(env).Value);


            Assert.AreEqual(0, parser.GetExpression("count()", env).Eval(env).Value);
            Assert.AreEqual(1, parser.GetExpression("count(1)", env).Eval(env).Value);
            Assert.AreEqual(2, parser.GetExpression("count(1, 2)", env).Eval(env).Value);
            Assert.AreEqual(4, parser.GetExpression("count(1, 2, 3, 4)", env).Eval(env).Value);
        }

        [Test]
        public void TestUnaryNegativeVarref()
        {
            // setup
            var parser = new SolusParser();

            // action
            var expr = parser.GetExpression("-a");

            // assertions
            Assert.IsInstanceOf<FunctionCall>(expr);
            var fcall = (expr as FunctionCall);
            Assert.AreSame(NegationOperation.Value, fcall.Function);
            Assert.AreEqual(1, fcall.Arguments.Count);
            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[0]);
            Assert.AreEqual("a", (fcall.Arguments[0] as VariableAccess).VariableName);
        }
    }
}

