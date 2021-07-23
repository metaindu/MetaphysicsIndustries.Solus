
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
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.SolusParserT
{
    [TestFixture]
    public class SolusParserTest
    {
        [Test]
        public void TestNormal()
        {
            SolusParser parser = new SolusParser();
            SolusEnvironment env = new SolusEnvironment();

            var expr = parser.GetExpression("2 + 2");
            var value = expr.Eval(env).ToNumber().Value;

            Assert.AreEqual(4.0f, value);
        }

        [Test]
        public void TestParensAndOperators()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("a * (2+c)");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.AreSame(MultiplicationOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[0]);
            Assert.AreEqual("a", (fcall.Arguments[0] as VariableAccess).VariableName);
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            var fcall2 = (FunctionCall)fcall.Arguments[1];
            Assert.AreSame(AdditionOperation.Value, fcall2.Function);
            Assert.AreEqual(2, fcall2.Arguments.Count);
            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.AreEqual(2.0f,
                ((Literal) fcall2.Arguments[0]).Value.ToFloat());
            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.AreEqual("c", (fcall2.Arguments[1] as VariableAccess).VariableName);
        }

        [Test]
        public void TestManyOperands()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 + a + 2 + b + 3 + c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;

            Assert.IsInstanceOf(typeof(AdditionOperation), fcall.Function);
            Assert.GreaterOrEqual(fcall.Arguments.Count, 4);
            Assert.LessOrEqual(fcall.Arguments.Count, 6);

            HashSet<string> varnames = new HashSet<string>();

            float sum = 0;
            foreach (var arg in fcall.Arguments)
            {
                Assert.That(arg is Literal || arg is VariableAccess);
                if (arg is Literal)
                {
                    sum += (arg as Literal).Value.ToFloat();
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

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;

            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(3, fcall.Arguments.Count);

            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.AreEqual(1.0f,
                ((Literal) fcall.Arguments[0]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.AreEqual("a", (fcall.Arguments[1] as VariableAccess).VariableName);

            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[2]);
            var fcall2 = (FunctionCall)fcall.Arguments[2];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(4, fcall2.Arguments.Count);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.AreEqual(2.0f,
                ((Literal) fcall2.Arguments[0]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.AreEqual("b", (fcall2.Arguments[1] as VariableAccess).VariableName);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[2]);
            Assert.AreEqual(3.0f,
                ((Literal) fcall2.Arguments[2]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[3]);
            Assert.AreEqual("c", (fcall2.Arguments[3] as VariableAccess).VariableName);
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators2()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 + a * 2 * b * 3 + c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;

            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(3, fcall.Arguments.Count);

            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.AreEqual(1.0f,
                ((Literal) fcall.Arguments[0]).Value.ToFloat());


            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            var fcall2 = (FunctionCall)fcall.Arguments[1];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(4, fcall2.Arguments.Count);

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[0]);
            Assert.AreEqual("a", (fcall2.Arguments[0] as VariableAccess).VariableName);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[1]);
            Assert.AreEqual(2.0f,
                ((Literal) fcall2.Arguments[1]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[2]);
            Assert.AreEqual("b", (fcall2.Arguments[2] as VariableAccess).VariableName);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[3]);
            Assert.AreEqual(3.0f,
                ((Literal) fcall2.Arguments[3]).Value.ToFloat());


            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[2]);
            Assert.AreEqual("c", (fcall.Arguments[2] as VariableAccess).VariableName);
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators3()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 * a * 2 * b + 3 + c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(3, fcall.Arguments.Count);

            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            var fcall2 = (FunctionCall)fcall.Arguments[0];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(4, fcall2.Arguments.Count);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.AreEqual(1.0f,
                ((Literal) fcall2.Arguments[0]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.AreEqual("a", (fcall2.Arguments[1] as VariableAccess).VariableName);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[2]);
            Assert.AreEqual(2.0f,
                ((Literal) fcall2.Arguments[2]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[3]);
            Assert.AreEqual("b", (fcall2.Arguments[3] as VariableAccess).VariableName);

            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.AreEqual(3.0f,
                ((Literal) fcall.Arguments[1]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[2]);
            Assert.AreEqual("c", (fcall.Arguments[2] as VariableAccess).VariableName);
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators4()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 * a * 2 + b + 3 * c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(3, fcall.Arguments.Count);

            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            var fcall2 = (FunctionCall)fcall.Arguments[0];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(3, fcall2.Arguments.Count);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.AreEqual(1.0f,
                ((Literal) fcall2.Arguments[0]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.AreEqual("a", (fcall2.Arguments[1] as VariableAccess).VariableName);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[2]);
            Assert.AreEqual(2.0f,
                ((Literal) fcall2.Arguments[2]).Value.ToFloat());



            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.AreEqual("b", (fcall.Arguments[1] as VariableAccess).VariableName);


            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[2]);
            fcall2 = (FunctionCall)fcall.Arguments[2];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(2, fcall2.Arguments.Count);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.AreEqual(3.0f,
                ((Literal) fcall2.Arguments[0]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.AreEqual("c", (fcall2.Arguments[1] as VariableAccess).VariableName);
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators5()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 * a + 2 + b * 3 * c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(3, fcall.Arguments.Count);

            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            var fcall2 = (FunctionCall)fcall.Arguments[0];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(2, fcall2.Arguments.Count);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.AreEqual(1.0f,
                ((Literal) fcall2.Arguments[0]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.AreEqual("a", (fcall2.Arguments[1] as VariableAccess).VariableName);


            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.AreEqual(2.0f,
                ((Literal) fcall.Arguments[1]).Value.ToFloat());


            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[2]);
            fcall2 = (FunctionCall)fcall.Arguments[2];
            Assert.AreSame(MultiplicationOperation.Value, fcall2.Function);
            Assert.AreEqual(3, fcall2.Arguments.Count);

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[0]);
            Assert.AreEqual("b", (fcall2.Arguments[0] as VariableAccess).VariableName);

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[1]);
            Assert.AreEqual(3.0f,
                ((Literal) fcall2.Arguments[1]).Value.ToFloat());

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[2]);
            Assert.AreEqual("c", (fcall2.Arguments[2] as VariableAccess).VariableName);
        }

        [Test]
        public void TestMixedOperandsAscendingPrecedence()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 | a & 2 + b * 3 ^ c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.AreSame(BitwiseOrOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.AreEqual(1.0f,
                ((Literal) fcall.Arguments[0]).Value.ToFloat());
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.AreSame(BitwiseAndOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[0]);
            Assert.AreEqual("a", (fcall.Arguments[0] as VariableAccess).VariableName);
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.AreEqual(2.0f,
                ((Literal) fcall.Arguments[0]).Value.ToFloat());
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.AreSame(MultiplicationOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[0]);
            Assert.AreEqual("b", (fcall.Arguments[0] as VariableAccess).VariableName);
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.AreSame(ExponentOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.AreEqual(3.0f,
                ((Literal) fcall.Arguments[0]).Value.ToFloat());
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.AreEqual("c", (fcall.Arguments[1] as VariableAccess).VariableName);

        }

        [Test]
        public void TestMixedOperandsDescendingPrecedence()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 ^ a * 2 + b & 3 | c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (expr as FunctionCall);
            Assert.AreSame(BitwiseOrOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.AreEqual("c", (fcall.Arguments[1] as VariableAccess).VariableName);
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.AreSame(BitwiseAndOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.AreEqual(3.0f,
                ((Literal) fcall.Arguments[1]).Value.ToFloat());
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.AreSame(AdditionOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.AreEqual("b", (fcall.Arguments[1] as VariableAccess).VariableName);
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.AreSame(MultiplicationOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.AreEqual(2.0f,
                ((Literal) fcall.Arguments[1]).Value.ToFloat());
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.AreSame(ExponentOperation.Value, fcall.Function);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.AreEqual("a", (fcall.Arguments[1] as VariableAccess).VariableName);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.AreEqual(1.0f,
                ((Literal) fcall.Arguments[0]).Value.ToFloat());
        }

        [Test]
        public void TestFunctionCall1()
        {
            var parser = new SolusParser();
            SolusEnvironment env = new SolusEnvironment();
            env.Variables.Add("pi", new Literal((float)Math.PI));

            var expr = parser.GetExpression("sin(pi)", cleanup: false);

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf(typeof(SineFunction), fcall.Function);
            Assert.AreEqual(1, fcall.Arguments.Count);
            //            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            //            Assert.AreEqual(Math.PI, (fcall.Arguments[0] as Literal).Value, 0.0001f);
            Assert.AreEqual(0f, fcall.Eval(env).ToNumber().Value, 0.0001f);
        }

        [Test]
        public void TestFunctionCall2()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("derive(x, x)", cleanup: false);

            Assert.IsInstanceOf(typeof(Literal), expr);
            Assert.AreEqual(1f, ((Literal) expr).Value.ToFloat());
        }

        [Test]
        public void TestFunctionCall3()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("if(1, 2, 3)", cleanup: false);

            Assert.IsInstanceOf(typeof(Literal), expr);
            var literal = (Literal) expr;
            Assert.AreEqual(2, literal.Value.ToFloat());
        }

        public class CustomAsdfFunction : Function
        {
            public CustomAsdfFunction()
                : base(new Types[] {Types.Scalar, Types.Scalar},
                    "asdf")
            {
            }

            protected override IMathObject InternalCall(SolusEnvironment env,
                IMathObject[] args)
            {
                return new Number(3);
            }
        }

        [Test]
        public void TestCustomFunctionCalls()
        {
            var parser = new SolusParser();
            var func = new CustomAsdfFunction();
            SolusEnvironment env = new SolusEnvironment();
            env.AddFunction(func);


            var expr = parser.GetExpression("asdf(1, 2)", env: env, cleanup: false);

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (expr as FunctionCall);
            Assert.AreEqual(2, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.AreEqual(1f,
                ((Literal) fcall.Arguments[0]).Value.ToFloat());
            Assert.AreEqual(2f,
                ((Literal) fcall.Arguments[1]).Value.ToFloat());


            float value = expr.Eval(env).ToNumber().Value;

            Assert.AreEqual(3f, value);
        }

        class CountArgsFunction : Function
        {
            public CountArgsFunction()
                : base(new Types[0], "count")
            {
            }

            protected override IMathObject InternalCall(SolusEnvironment env,
                IMathObject[] args)
            {
                return new Number(args.Length);
            }

            public override void CheckArguments(IMathObject[] args)
            {
            }
        }

        [Test]
        public void TestVarArgFunctionCalls()
        {
            var parser = new SolusParser();
            var func = new CountArgsFunction();
            var env = new SolusEnvironment();
            env.AddFunction(func);


            var expr = parser.GetExpression("count(1, 2, 3)", env: env, cleanup: false);

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (expr as FunctionCall);
            Assert.AreEqual(3, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[2]);
            Assert.AreEqual(1f,
                ((Literal) fcall.Arguments[0]).Value.ToFloat());
            Assert.AreEqual(2f,
                ((Literal) fcall.Arguments[1]).Value.ToFloat());
            Assert.AreEqual(3f,
                ((Literal) fcall.Arguments[2]).Value.ToFloat());
            Assert.AreEqual(3f, expr.Eval(env).ToNumber().Value);


            Assert.AreEqual(0,
                parser.GetExpression("count()", env).
                    Eval(env).ToNumber().Value);
            Assert.AreEqual(1,
                parser.GetExpression("count(1)", env).
                    Eval(env).ToNumber().Value);
            Assert.AreEqual(2,
                parser.GetExpression("count(1, 2)", env).
                    Eval(env).ToNumber().Value);
            Assert.AreEqual(4,
                parser.GetExpression("count(1, 2, 3, 4)", env).
                    Eval(env).ToNumber().Value);
        }

        [Test]
        public void TestUnaryNegativeVarref()
        {
            // setup
            var parser = new SolusParser();

            // action
            var expr = parser.GetExpression("-a");

            // assertions
            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (expr as FunctionCall);
            Assert.AreSame(NegationOperation.Value, fcall.Function);
            Assert.AreEqual(1, fcall.Arguments.Count);
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[0]);
            Assert.AreEqual("a", (fcall.Arguments[0] as VariableAccess).VariableName);
        }

        [Test]
        public void TestStringParsedAsString()
        {
            // given
            const string input = "\"value\"";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.IsTrue(literal.Value.IsString);
            Assert.AreEqual("value",
                literal.Value.ToStringValue().Value);
        }

        [Test]
        public void TestStringParsedAsStringSingleQuote()
        {
            // given
            const string input = "'value'";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<Literal>(result);
            var literal = (Literal) result;
            Assert.IsTrue(literal.Value.IsString);
            Assert.AreEqual("value",
                literal.Value.ToStringValue().Value);
        }

        [Test]
        public void TestVectorLiteral()
        {
            // given
            const string input = "[1,2,3]";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<VectorExpression>(result);
            var ve = (VectorExpression) result;
            Assert.AreEqual(3, ve.Length);
            Assert.IsInstanceOf<Literal>(ve[0]);
            Assert.AreEqual(1,((Literal)ve[0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(ve[1]);
            Assert.AreEqual(2,((Literal)ve[1]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(ve[2]);
            Assert.AreEqual(3,((Literal)ve[2]).Value.ToFloat());
        }

        [Test]
        public void VectorLiteralWithTrailingCommaAddsNoComponents()
        {
            // given
            const string input = "[1,2,3,]";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<VectorExpression>(result);
            var ve = (VectorExpression) result;
            Assert.AreEqual(3, ve.Length);
            Assert.IsInstanceOf<Literal>(ve[0]);
            Assert.AreEqual(1,((Literal)ve[0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(ve[1]);
            Assert.AreEqual(2,((Literal)ve[1]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(ve[2]);
            Assert.AreEqual(3,((Literal)ve[2]).Value.ToFloat());
        }

        [Test]
        public void TestMatrixLiteral()
        {
            // given
            const string input = "[1,2;3,4]";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<MatrixExpression>(result);
            var me = (MatrixExpression) result;
            Assert.AreEqual(2, me.RowCount);
            Assert.AreEqual(2, me.ColumnCount);
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.AreEqual(1, ((Literal) me[0, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.AreEqual(2, ((Literal) me[0, 1]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.AreEqual(3, ((Literal) me[1, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.AreEqual(4, ((Literal) me[1, 1]).Value.ToFloat());
        }

        [Test]
        public void MatrixLiteralWithTrailingCommaAddsNoComponents1()
        {
            // given
            const string input = "[1,2;3,4,]";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<MatrixExpression>(result);
            var me = (MatrixExpression) result;
            Assert.AreEqual(2, me.RowCount);
            Assert.AreEqual(2, me.ColumnCount);
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.AreEqual(1, ((Literal) me[0, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.AreEqual(2, ((Literal) me[0, 1]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.AreEqual(3, ((Literal) me[1, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.AreEqual(4, ((Literal) me[1, 1]).Value.ToFloat());
        }

        [Test]
        public void MatrixLiteralWithTrailingCommaAddsNoComponents2()
        {
            // given
            const string input = "[1,2,;3,4]";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<MatrixExpression>(result);
            var me = (MatrixExpression) result;
            Assert.AreEqual(2, me.RowCount);
            Assert.AreEqual(2, me.ColumnCount);
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.AreEqual(1, ((Literal) me[0, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.AreEqual(2, ((Literal) me[0, 1]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.AreEqual(3, ((Literal) me[1, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.AreEqual(4, ((Literal) me[1, 1]).Value.ToFloat());
        }

        [Test]
        public void MatrixLiteralWithTrailingSemicolonAddsNoComponents()
        {
            // given
            const string input = "[1,2;3,4;]";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<MatrixExpression>(result);
            var me = (MatrixExpression) result;
            Assert.AreEqual(2, me.RowCount);
            Assert.AreEqual(2, me.ColumnCount);
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.AreEqual(1, ((Literal) me[0, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.AreEqual(2, ((Literal) me[0, 1]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.AreEqual(3, ((Literal) me[1, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.AreEqual(4, ((Literal) me[1, 1]).Value.ToFloat());
        }

        [Test]
        public void MatrixWithTrailingCommaAndSemicolonAddsNoComponents()
        {
            // given
            const string input = "[1,2;3,4,;]";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<MatrixExpression>(result);
            var me = (MatrixExpression) result;
            Assert.AreEqual(2, me.RowCount);
            Assert.AreEqual(2, me.ColumnCount);
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.AreEqual(1, ((Literal) me[0, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.AreEqual(2, ((Literal) me[0, 1]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.AreEqual(3, ((Literal) me[1, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.AreEqual(4, ((Literal) me[1, 1]).Value.ToFloat());
        }

        [Test]
        public void MatrixLiteralMissingComponentsTreatedAsZero()
        {
            // given
            const string input = "[1;3,4]";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<MatrixExpression>(result);
            var me = (MatrixExpression) result;
            Assert.AreEqual(2, me.RowCount);
            Assert.AreEqual(2, me.ColumnCount);
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.AreEqual(1, ((Literal) me[0, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.AreEqual(0, ((Literal) me[0, 1]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.AreEqual(3, ((Literal) me[1, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.AreEqual(4, ((Literal) me[1, 1]).Value.ToFloat());
        }

        [Test]
        public void SingleRowWithTrailingSemicolonYieldsMatrix()
        {
            // given
            const string input = "[1,2,3;]";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<MatrixExpression>(result);
            var me = (MatrixExpression) result;
            Assert.AreEqual(1, me.RowCount);
            Assert.AreEqual(3, me.ColumnCount);
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.AreEqual(1, ((Literal) me[0, 0]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.AreEqual(2, ((Literal) me[0, 1]).Value.ToFloat());
            Assert.IsInstanceOf<Literal>(me[0, 2]);
            Assert.AreEqual(3, ((Literal) me[0, 2]).Value.ToFloat());
        }

        [Test]
        public void TestComponentAccess()
        {
            // given
            const string input = "a[1]";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<ComponentAccess>(result);
            var ca = (ComponentAccess) result;
            Assert.IsInstanceOf<VariableAccess>(ca.Expr);
            Assert.AreEqual("a",
                ((VariableAccess)ca.Expr).VariableName);
            Assert.AreEqual(1, ca.Indexes.Count);
            Assert.AreEqual(1,
                ((Literal)ca.Indexes[0]).Value.ToFloat());
        }
    }
}