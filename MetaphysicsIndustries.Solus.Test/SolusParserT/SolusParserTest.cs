
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
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
using MetaphysicsIndustries.Solus.Commands;
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
            //given
            SolusParser parser = new SolusParser();
            // when
            var expr = parser.GetExpression("2 + 2");
            // then
            Assert.IsInstanceOf<FunctionCall>(expr);
            var fc = (FunctionCall)expr;
            Assert.That(fc.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(fc.Arguments[0]);
            Assert.That(((Literal)fc.Arguments[0]).Value.ToNumber().Value,
                Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(fc.Arguments[1]);
            Assert.That(((Literal)fc.Arguments[1]).Value.ToNumber().Value,
                Is.EqualTo(2));
        }

        [Test]
        public void TestParensAndOperators()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression("a * (2+c)");
            // then
            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<Literal>(fcall.Function);
            var literal = (Literal)fcall.Function;
            Assert.That(literal.Value,
                Is.SameAs(MultiplicationOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[0]);
            Assert.That((fcall.Arguments[0] as VariableAccess).VariableName,
                Is.EqualTo("a"));
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            var fcall2 = (FunctionCall)fcall.Arguments[1];
            Assert.IsInstanceOf<Literal>(fcall2.Function);
            literal = (Literal)fcall2.Function;
            Assert.That(literal.Value, Is.SameAs(AdditionOperation.Value));
            Assert.That(fcall2.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.That(((Literal) fcall2.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(2.0f));
            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.That((fcall2.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("c"));
        }

        [Test]
        public void TestManyOperands()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 + a + 2 + b + 3 + c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<Literal>(fcall.Function);
            var literal = (Literal)fcall.Function;
            Assert.IsInstanceOf(typeof(AdditionOperation), literal.Value);
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

            Assert.That(sum, Is.EqualTo(6.0f));
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
            Assert.IsInstanceOf<Literal>(fcall.Function);
            var literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(AdditionOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(3));

            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.That((fcall.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("a"));

            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[2]);
            var fcall2 = (FunctionCall)fcall.Arguments[2];
            Assert.IsInstanceOf<Literal>(fcall2.Function);
            literal = (Literal)fcall2.Function;
            Assert.That(literal.Value,
                Is.SameAs(MultiplicationOperation.Value));
            Assert.That(fcall2.Arguments.Count, Is.EqualTo(4));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.That(((Literal) fcall2.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(2.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.That((fcall2.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("b"));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[2]);
            Assert.That(((Literal) fcall2.Arguments[2]).Value.ToFloat(),
                Is.EqualTo(3.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[3]);
            Assert.That((fcall2.Arguments[3] as VariableAccess).VariableName,
                Is.EqualTo("c"));
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators2()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 + a * 2 * b * 3 + c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<Literal>(fcall.Function);
            var literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(AdditionOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(3));

            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1.0f));


            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            var fcall2 = (FunctionCall)fcall.Arguments[1];
            Assert.IsInstanceOf<Literal>(fcall2.Function);
            literal = (Literal)fcall2.Function;
            Assert.That(literal.Value,
                Is.SameAs(MultiplicationOperation.Value));
            Assert.That(fcall2.Arguments.Count, Is.EqualTo(4));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[0]);
            Assert.That((fcall2.Arguments[0] as VariableAccess).VariableName,
                Is.EqualTo("a"));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[1]);
            Assert.That(((Literal) fcall2.Arguments[1]).Value.ToFloat(),
                Is.EqualTo(2.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[2]);
            Assert.That((fcall2.Arguments[2] as VariableAccess).VariableName,
                Is.EqualTo("b"));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[3]);
            Assert.That(((Literal) fcall2.Arguments[3]).Value.ToFloat(),
                Is.EqualTo(3.0f));


            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[2]);
            Assert.That((fcall.Arguments[2] as VariableAccess).VariableName,
                Is.EqualTo("c"));
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators3()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 * a * 2 * b + 3 + c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<Literal>(fcall.Function);
            var literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(AdditionOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(3));

            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            var fcall2 = (FunctionCall)fcall.Arguments[0];
            Assert.IsInstanceOf<Literal>(fcall2.Function);
            literal = (Literal)fcall2.Function;
            Assert.That(literal.Value,
                Is.SameAs(MultiplicationOperation.Value));
            Assert.That(fcall2.Arguments.Count, Is.EqualTo(4));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.That(((Literal) fcall2.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.That((fcall2.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("a"));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[2]);
            Assert.That(((Literal) fcall2.Arguments[2]).Value.ToFloat(),
                Is.EqualTo(2.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[3]);
            Assert.That((fcall2.Arguments[3] as VariableAccess).VariableName,
                Is.EqualTo("b"));

            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.That(((Literal) fcall.Arguments[1]).Value.ToFloat(),
                Is.EqualTo(3.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[2]);
            Assert.That((fcall.Arguments[2] as VariableAccess).VariableName,
                Is.EqualTo("c"));
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators4()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 * a * 2 + b + 3 * c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<Literal>(fcall.Function);
            var literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(AdditionOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(3));

            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            var fcall2 = (FunctionCall)fcall.Arguments[0];
            Assert.IsInstanceOf<Literal>(fcall2.Function);
            literal = (Literal)fcall2.Function;
            Assert.That(literal.Value,
                Is.SameAs(MultiplicationOperation.Value));
            Assert.That(fcall2.Arguments.Count, Is.EqualTo(3));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.That(((Literal) fcall2.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.That((fcall2.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("a"));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[2]);
            Assert.That(((Literal) fcall2.Arguments[2]).Value.ToFloat(),
                Is.EqualTo(2.0f));



            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.That((fcall.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("b"));


            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[2]);
            fcall2 = (FunctionCall)fcall.Arguments[2];
            Assert.IsInstanceOf<Literal>(fcall2.Function);
            literal = (Literal)fcall2.Function;
            Assert.That(literal.Value,
                Is.SameAs(MultiplicationOperation.Value));
            Assert.That(fcall2.Arguments.Count, Is.EqualTo(2));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.That(((Literal) fcall2.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(3.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.That((fcall2.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("c"));
        }

        [Test]
        public void TestMixedOperandsCommutativeAssociativeOperators5()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 * a + 2 + b * 3 * c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<Literal>(fcall.Function);
            var literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(AdditionOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(3));

            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            var fcall2 = (FunctionCall)fcall.Arguments[0];
            Assert.IsInstanceOf<Literal>(fcall2.Function);
            literal = (Literal)fcall2.Function;
            Assert.That(literal.Value,
                Is.SameAs(MultiplicationOperation.Value));
            Assert.That(fcall2.Arguments.Count, Is.EqualTo(2));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[0]);
            Assert.That(((Literal) fcall2.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[1]);
            Assert.That((fcall2.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("a"));


            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.That(((Literal) fcall.Arguments[1]).Value.ToFloat(),
                Is.EqualTo(2.0f));


            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[2]);
            fcall2 = (FunctionCall)fcall.Arguments[2];
            Assert.IsInstanceOf<Literal>(fcall2.Function);
            literal = (Literal)fcall2.Function;
            Assert.That(literal.Value,
                Is.SameAs(MultiplicationOperation.Value));
            Assert.That(fcall2.Arguments.Count, Is.EqualTo(3));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[0]);
            Assert.That((fcall2.Arguments[0] as VariableAccess).VariableName,
                Is.EqualTo("b"));

            Assert.IsInstanceOf(typeof(Literal), fcall2.Arguments[1]);
            Assert.That(((Literal) fcall2.Arguments[1]).Value.ToFloat(),
                Is.EqualTo(3.0f));

            Assert.IsInstanceOf(typeof(VariableAccess), fcall2.Arguments[2]);
            Assert.That((fcall2.Arguments[2] as VariableAccess).VariableName,
                Is.EqualTo("c"));
        }

        [Test]
        public void TestMixedOperandsAscendingPrecedence()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 | a & 2 + b * 3 ^ c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<Literal>(fcall.Function);
            var literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(BitwiseOrOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1.0f));
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.IsInstanceOf<Literal>(fcall.Function);
            literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(BitwiseAndOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[0]);
            Assert.That((fcall.Arguments[0] as VariableAccess).VariableName,
                Is.EqualTo("a"));
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.IsInstanceOf<Literal>(fcall.Function);
            literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(AdditionOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(2.0f));
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.IsInstanceOf<Literal>(fcall.Function);
            literal = (Literal)fcall.Function;
            Assert.That(literal.Value,
                Is.SameAs(MultiplicationOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[0]);
            Assert.That((fcall.Arguments[0] as VariableAccess).VariableName,
                Is.EqualTo("b"));
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[1]);
            fcall = (FunctionCall)(fcall.Arguments[1]);
            Assert.IsInstanceOf<Literal>(fcall.Function);
            literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(ExponentOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(3.0f));
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.That((fcall.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("c"));

        }

        [Test]
        public void TestMixedOperandsDescendingPrecedence()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("1 ^ a * 2 + b & 3 | c");

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (expr as FunctionCall);
            Assert.IsInstanceOf<Literal>(fcall.Function);
            var literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(BitwiseOrOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.That((fcall.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("c"));
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.IsInstanceOf<Literal>(fcall.Function);
            literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(BitwiseAndOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.That(((Literal) fcall.Arguments[1]).Value.ToFloat(),
                Is.EqualTo(3.0f));
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.IsInstanceOf<Literal>(fcall.Function);
            literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(AdditionOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.That((fcall.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("b"));
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.IsInstanceOf<Literal>(fcall.Function);
            literal = (Literal)fcall.Function;
            Assert.That(literal.Value,
                Is.SameAs(MultiplicationOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.That(((Literal) fcall.Arguments[1]).Value.ToFloat(),
                Is.EqualTo(2.0f));
            Assert.IsInstanceOf(typeof(FunctionCall), fcall.Arguments[0]);
            fcall = (fcall.Arguments[0] as FunctionCall);
            Assert.IsInstanceOf<Literal>(fcall.Function);
            literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(ExponentOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[1]);
            Assert.That((fcall.Arguments[1] as VariableAccess).VariableName,
                Is.EqualTo("a"));
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1.0f));
        }

        [Test]
        public void TestFunctionCall1()
        {
            var parser = new SolusParser();

            var expr = parser.GetExpression("sin(pi)", cleanup: false);

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<VariableAccess>(fcall.Function);
            var va = (VariableAccess)fcall.Function;
            Assert.That(va.VariableName, Is.EqualTo("sin"));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(1));
            Assert.IsInstanceOf<VariableAccess>(fcall.Arguments[0]);
            va = (VariableAccess)fcall.Arguments[0];
            Assert.That(va.VariableName, Is.EqualTo("pi"));
        }

        [Test]
        public void TestMacroCall2()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression("derive(x, x)", cleanup: false);
            // then
            Assert.IsInstanceOf<FunctionCall>(expr);
            var call = (FunctionCall)expr;
            Assert.IsInstanceOf<VariableAccess>(call.Function);
            var target = (VariableAccess)call.Function;
            Assert.That(target.VariableName, Is.EqualTo("derive"));
            Assert.That(call.Arguments.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestMacroCall3()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression("if(1, 2, 3)", cleanup: false);
            // then
            Assert.IsInstanceOf<FunctionCall>(expr);
            var call = (FunctionCall)expr;
            Assert.IsInstanceOf<VariableAccess>(call.Function);
            var target = (VariableAccess)call.Function;
            Assert.That(target.VariableName, Is.EqualTo("if"));
            Assert.That(call.Arguments.Count, Is.EqualTo(3));
        }

        [Test]
        public void TestFunctionAsVariable()
        {
            // given
            var parser = new SolusParser();
            var f = SineFunction.Value;
            // when
            var expr = parser.GetExpression("f(0)");
            // then
            Assert.IsInstanceOf<FunctionCall>(expr);
            var fc = (FunctionCall)expr;
            Assert.IsInstanceOf<VariableAccess>(fc.Function);
            var va = (VariableAccess)fc.Function;
            Assert.That(va.VariableName, Is.EqualTo("f"));
            Assert.IsInstanceOf<Literal>(fc.Arguments[0]);
            Assert.That(((Literal)fc.Arguments[0]).Value.ToNumber().Value,
                Is.EqualTo(0));
        }

        public class CustomAsdfFunction : Function
        {
            public CustomAsdfFunction()
                : base(new Types[] {Types.Scalar, Types.Scalar},
                    "asdf")
            {
            }

            public override IMathObject CustomCall(IMathObject[] args,
                SolusEnvironment env)
            {
                return new Number(3);
            }
            public override bool ProvidesCustomCall => true;

            public override IMathObject GetResultType(SolusEnvironment env,
                IEnumerable<IMathObject> argTypes)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void TestCustomFunctionCalls()
        {
            var parser = new SolusParser();
            var func = new CustomAsdfFunction();

            var expr = parser.GetExpression("asdf(1, 2)", cleanup: false);

            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (expr as FunctionCall);
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[0]);
            Assert.IsInstanceOf(typeof(Literal), fcall.Arguments[1]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1f));
            Assert.That(((Literal) fcall.Arguments[1]).Value.ToFloat(),
                Is.EqualTo(2f));
        }

        class CountArgsFunction : Function
        {
            public CountArgsFunction()
                : base(new Types[0], "count")
            {
            }

            public override IMathObject CustomCall(IMathObject[] args,
                SolusEnvironment env)
            {
                return new Number(args.Length);
            }
            public override bool ProvidesCustomCall => true;

            public override void CheckArguments(IMathObject[] args)
            {
            }

            public override IMathObject GetResultType(SolusEnvironment env,
                IEnumerable<IMathObject> argTypes)
            {
                throw new NotImplementedException();
            }
        }

        [Test]
        public void TestVarArgFunctionCalls0()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression("count()",
                cleanup: false);
            // then
            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.That(fcall.Arguments.Count, Is.EqualTo(0));
        }

        [Test]
        public void TestVarArgFunctionCalls1()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression("count(1)",
                cleanup: false);
            // then
            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.That(fcall.Arguments.Count, Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1f));
        }

        [Test]
        public void TestVarArgFunctionCalls2()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression("count(1, 2)",
                cleanup: false);
            // then
            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.That(fcall.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1f));
            Assert.That(((Literal) fcall.Arguments[1]).Value.ToFloat(),
                Is.EqualTo(2f));
        }

        [Test]
        public void TestVarArgFunctionCalls3()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression("count(1, 2, 3)",
                cleanup: false);
            // then
            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.That(fcall.Arguments.Count, Is.EqualTo(3));
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[2]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1f));
            Assert.That(((Literal) fcall.Arguments[1]).Value.ToFloat(),
                Is.EqualTo(2f));
            Assert.That(((Literal) fcall.Arguments[2]).Value.ToFloat(),
                Is.EqualTo(3f));
        }

        [Test]
        public void TestVarArgFunctionCalls4()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression("count(1, 2, 3, 4)",
                cleanup: false);
            // then
            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var fcall = (FunctionCall)expr;
            Assert.That(fcall.Arguments.Count, Is.EqualTo(4));
            Assert.IsInstanceOf<Literal>(fcall.Arguments[0]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[1]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[2]);
            Assert.IsInstanceOf<Literal>(fcall.Arguments[3]);
            Assert.That(((Literal) fcall.Arguments[0]).Value.ToFloat(),
                Is.EqualTo(1f));
            Assert.That(((Literal) fcall.Arguments[1]).Value.ToFloat(),
                Is.EqualTo(2f));
            Assert.That(((Literal) fcall.Arguments[2]).Value.ToFloat(),
                Is.EqualTo(3f));
            Assert.That(((Literal) fcall.Arguments[3]).Value.ToFloat(),
                Is.EqualTo(4f));
        }

        [Test]
        public void MultipleParensYieldsNestedFunctionCalls()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression(
                "f(1)(2,3)");
            // then
            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var call1 = (FunctionCall)expr;
            Assert.That(call1.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<FunctionCall>(call1.Function);
            var call2 = (FunctionCall)call1.Function;
            Assert.That(call2.Arguments.Count, Is.EqualTo(1));
            Assert.IsInstanceOf<VariableAccess>(call2.Function);
        }

        [Test]
        public void FunctionCallInParensIsValidCallTarget()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression(
                "(f(1))(2,3)");
            // then
            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var call1 = (FunctionCall)expr;
            Assert.That(call1.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<FunctionCall>(call1.Function);
            var call2 = (FunctionCall)call1.Function;
            Assert.That(call2.Arguments.Count, Is.EqualTo(1));
            Assert.IsInstanceOf<VariableAccess>(call2.Function);
        }

        [Test]
        public void ComponentAccessInParensIsValidCallTarget()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression(
                "(a[1])(2,3)");
            // then
            Assert.IsInstanceOf(typeof(FunctionCall), expr);
            var call1 = (FunctionCall)expr;
            Assert.That(call1.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<ComponentAccess>(call1.Function);
            var ca = (ComponentAccess)call1.Function;
            Assert.That(ca.Indexes.Count, Is.EqualTo(1));
            Assert.IsInstanceOf<VariableAccess>(ca.Expr);
        }

        [Test]
        public void InterleavedComponentAccessAndFunctionCallAreValid()
        {
            // given
            var parser = new SolusParser();
            // when
            var expr = parser.GetExpression(
                "a[1](2,3)[4,5,6](7,8,9,0)()");
            // then
            FunctionCall call = null;
            ComponentAccess ca = null;
            Assert.IsInstanceOf<FunctionCall>(expr);
            call = (FunctionCall)expr;
            Assert.That(call.Arguments.Count,
                Is.EqualTo(0));
            Assert.IsInstanceOf<FunctionCall>(call.Function);

            call = (FunctionCall)call.Function;
            Assert.That(call.Arguments.Count,
                Is.EqualTo(4));
            Assert.IsInstanceOf<ComponentAccess>(call.Function);

            ca =(ComponentAccess)call.Function;
            Assert.That(ca.Indexes.Count,
                Is.EqualTo(3));
            Assert.IsInstanceOf<FunctionCall>(ca.Expr);

            call = (FunctionCall)ca.Expr;
            Assert.That(call.Arguments.Count,
                Is.EqualTo(2));
            Assert.IsInstanceOf<ComponentAccess>(call.Function);

            ca = (ComponentAccess)call.Function;
            Assert.That(ca.Indexes.Count, Is.EqualTo(1));
            Assert.IsInstanceOf<VariableAccess>(ca.Expr);

            var va = (VariableAccess)ca.Expr;
            Assert.That(va.VariableName, Is.EqualTo("a"));
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
            var fcall = (FunctionCall)expr;
            Assert.IsInstanceOf<Literal>(fcall.Function);
            var literal = (Literal)fcall.Function;
            Assert.That(literal.Value, Is.SameAs(NegationOperation.Value));
            Assert.That(fcall.Arguments.Count, Is.EqualTo(1));
            Assert.IsInstanceOf(typeof(VariableAccess), fcall.Arguments[0]);
            Assert.That((fcall.Arguments[0] as VariableAccess).VariableName,
                Is.EqualTo("a"));
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
            Assert.IsTrue(literal.Value.IsString(null));
            Assert.That(literal.Value.ToStringValue().Value,
                Is.EqualTo("value"));
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
            Assert.IsTrue(literal.Value.IsString(null));
            Assert.That(literal.Value.ToStringValue().Value,
                Is.EqualTo("value"));
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
            Assert.That(ve.Length, Is.EqualTo(3));
            Assert.IsInstanceOf<Literal>(ve[0]);
            Assert.That(((Literal)ve[0]).Value.ToFloat(),
                Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(ve[1]);
            Assert.That(((Literal)ve[1]).Value.ToFloat(),
                Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(ve[2]);
            Assert.That(((Literal)ve[2]).Value.ToFloat(),
                Is.EqualTo(3));
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
            Assert.That(ve.Length, Is.EqualTo(3));
            Assert.IsInstanceOf<Literal>(ve[0]);
            Assert.That(((Literal)ve[0]).Value.ToFloat(),
                Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(ve[1]);
            Assert.That(((Literal)ve[1]).Value.ToFloat(),
                Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(ve[2]);
            Assert.That(((Literal)ve[2]).Value.ToFloat(),
                Is.EqualTo(3));
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
            Assert.That(me.RowCount, Is.EqualTo(2));
            Assert.That(me.ColumnCount, Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.That(((Literal) me[0, 0]).Value.ToFloat(), Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.That(((Literal) me[0, 1]).Value.ToFloat(), Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.That(((Literal) me[1, 0]).Value.ToFloat(), Is.EqualTo(3));
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.That(((Literal) me[1, 1]).Value.ToFloat(), Is.EqualTo(4));
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
            Assert.That(me.RowCount, Is.EqualTo(2));
            Assert.That(me.ColumnCount, Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.That(((Literal) me[0, 0]).Value.ToFloat(), Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.That(((Literal) me[0, 1]).Value.ToFloat(), Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.That(((Literal) me[1, 0]).Value.ToFloat(), Is.EqualTo(3));
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.That(((Literal) me[1, 1]).Value.ToFloat(), Is.EqualTo(4));
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
            Assert.That(me.RowCount, Is.EqualTo(2));
            Assert.That(me.ColumnCount, Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.That(((Literal) me[0, 0]).Value.ToFloat(), Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.That(((Literal) me[0, 1]).Value.ToFloat(), Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.That(((Literal) me[1, 0]).Value.ToFloat(), Is.EqualTo(3));
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.That(((Literal) me[1, 1]).Value.ToFloat(), Is.EqualTo(4));
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
            Assert.That(me.RowCount, Is.EqualTo(2));
            Assert.That(me.ColumnCount, Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.That(((Literal) me[0, 0]).Value.ToFloat(), Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.That(((Literal) me[0, 1]).Value.ToFloat(), Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.That(((Literal) me[1, 0]).Value.ToFloat(), Is.EqualTo(3));
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.That(((Literal) me[1, 1]).Value.ToFloat(), Is.EqualTo(4));
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
            Assert.That(me.RowCount, Is.EqualTo(2));
            Assert.That(me.ColumnCount, Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.That(((Literal) me[0, 0]).Value.ToFloat(), Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.That(((Literal) me[0, 1]).Value.ToFloat(), Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.That(((Literal) me[1, 0]).Value.ToFloat(), Is.EqualTo(3));
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.That(((Literal) me[1, 1]).Value.ToFloat(), Is.EqualTo(4));
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
            Assert.That(me.RowCount, Is.EqualTo(2));
            Assert.That(me.ColumnCount, Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.That(((Literal) me[0, 0]).Value.ToFloat(), Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.That(((Literal) me[0, 1]).Value.ToFloat(), Is.EqualTo(0));
            Assert.IsInstanceOf<Literal>(me[1, 0]);
            Assert.That(((Literal) me[1, 0]).Value.ToFloat(), Is.EqualTo(3));
            Assert.IsInstanceOf<Literal>(me[1, 1]);
            Assert.That(((Literal) me[1, 1]).Value.ToFloat(), Is.EqualTo(4));
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
            Assert.That(me.RowCount, Is.EqualTo(1));
            Assert.That(me.ColumnCount, Is.EqualTo(3));
            Assert.IsInstanceOf<Literal>(me[0, 0]);
            Assert.That(((Literal) me[0, 0]).Value.ToFloat(), Is.EqualTo(1));
            Assert.IsInstanceOf<Literal>(me[0, 1]);
            Assert.That(((Literal) me[0, 1]).Value.ToFloat(), Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(me[0, 2]);
            Assert.That(((Literal) me[0, 2]).Value.ToFloat(), Is.EqualTo(3));
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
            Assert.That(((VariableAccess)ca.Expr).VariableName,
                Is.EqualTo("a"));
            Assert.That(ca.Indexes.Count, Is.EqualTo(1));
            Assert.That(((Literal)ca.Indexes[0]).Value.ToFloat(),
                Is.EqualTo(1));
        }

        [Test]
        public void TestEqualComparison()
        {
            // given
            const string input = "a = b";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<FunctionCall>(result);
            var fc = (FunctionCall) result;
            Assert.IsInstanceOf<Literal>(fc.Function);
            var literal = (Literal)fc.Function;
            Assert.That(literal.Value,
                Is.SameAs(EqualComparisonOperation.Value));
            Assert.That(fc.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[0]);
            Assert.That(((VariableAccess)fc.Arguments[0]).VariableName,
                Is.EqualTo("a"));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[1]);
            Assert.That(((VariableAccess)fc.Arguments[1]).VariableName,
                Is.EqualTo("b"));
        }

        [Test]
        public void TestNotEqualComparison()
        {
            // given
            const string input = "a != b";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<FunctionCall>(result);
            var fc = (FunctionCall) result;
            Assert.IsInstanceOf<Literal>(fc.Function);
            var literal = (Literal)fc.Function;
            Assert.That(literal.Value,
                Is.SameAs(NotEqualComparisonOperation.Value));
            Assert.That(fc.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[0]);
            Assert.That(((VariableAccess)fc.Arguments[0]).VariableName,
                Is.EqualTo("a"));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[1]);
            Assert.That(((VariableAccess)fc.Arguments[1]).VariableName,
                Is.EqualTo("b"));
        }

        [Test]
        public void TestLessThanComparison()
        {
            // given
            const string input = "a < b";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<FunctionCall>(result);
            var fc = (FunctionCall) result;
            Assert.IsInstanceOf<Literal>(fc.Function);
            var literal = (Literal)fc.Function;
            Assert.That(literal.Value,
                Is.SameAs(LessThanComparisonOperation.Value));
            Assert.That(fc.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[0]);
            Assert.That(((VariableAccess)fc.Arguments[0]).VariableName,
                Is.EqualTo("a"));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[1]);
            Assert.That(((VariableAccess)fc.Arguments[1]).VariableName,
                Is.EqualTo("b"));
        }

        [Test]
        public void TestLessEqualComparison()
        {
            // given
            const string input = "a <= b";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<FunctionCall>(result);
            var fc = (FunctionCall) result;
            Assert.IsInstanceOf<Literal>(fc.Function);
            var literal = (Literal)fc.Function;
            Assert.That(literal.Value,
                Is.SameAs(LessThanOrEqualComparisonOperation.Value));
            Assert.That(fc.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[0]);
            Assert.That(((VariableAccess)fc.Arguments[0]).VariableName,
                Is.EqualTo("a"));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[1]);
            Assert.That(((VariableAccess)fc.Arguments[1]).VariableName,
                Is.EqualTo("b"));
        }

        [Test]
        public void TestGreaterThanComparison()
        {
            // given
            const string input = "a > b";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<FunctionCall>(result);
            var fc = (FunctionCall) result;
            Assert.IsInstanceOf<Literal>(fc.Function);
            var literal = (Literal)fc.Function;
            Assert.That(literal.Value,
                Is.SameAs(GreaterThanComparisonOperation.Value));
            Assert.That(fc.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[0]);
            Assert.That(((VariableAccess)fc.Arguments[0]).VariableName,
                Is.EqualTo("a"));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[1]);
            Assert.That(((VariableAccess)fc.Arguments[1]).VariableName,
                Is.EqualTo("b"));
        }

        [Test]
        public void TestGreaterEqualComparison()
        {
            // given
            const string input = "a >= b";
            var parser = new SolusParser();
            // when
            var result = parser.GetExpression(input);
            // then
            Assert.IsInstanceOf<FunctionCall>(result);
            var fc = (FunctionCall) result;
            Assert.IsInstanceOf<Literal>(fc.Function);
            var literal = (Literal)fc.Function;
            Assert.That(literal.Value,
                Is.SameAs(GreaterThanOrEqualComparisonOperation.Value));
            Assert.That(fc.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[0]);
            Assert.That(((VariableAccess)fc.Arguments[0]).VariableName,
                Is.EqualTo("a"));
            Assert.IsInstanceOf<VariableAccess>(fc.Arguments[1]);
            Assert.That(((VariableAccess)fc.Arguments[1]).VariableName,
                Is.EqualTo("b"));
        }

        [Test]
        public void ParseUserDefinedFunction()
        {
            // given
            const string input = "f(x) := x";
            var parser = new SolusParser();
            var cs = new CommandSet();
            cs.AddCommand(DeleteCommand.Value);
            cs.AddCommand(FuncAssignCommand.Value);
            cs.AddCommand(HelpCommand.Value);
            cs.AddCommand(VarAssignCommand.Value);
            cs.AddCommand(VarsCommand.Value);
            // when
            var result = parser.GetCommands(input, cs);
            // then
            Assert.IsNotNull(result);
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<FuncAssignCommandData>(result[0]);
            var udf = ((FuncAssignCommandData)result[0]).Func;
            Assert.That(udf.Name, Is.EqualTo("f"));
            Assert.That(udf.Argnames.Length, Is.EqualTo(1));
            Assert.That(udf.Argnames[0], Is.EqualTo("x"));
            Assert.IsInstanceOf<VariableAccess>(udf.Expression);
            var va = (VariableAccess)udf.Expression;
            Assert.That(va.VariableName, Is.EqualTo("x"));
        }

        [Test]
        public void ParseRecursiveUserDefinedFunction()
        {
            // given
            const string input = "f(x) := if(x=0,0,1+f(floor(x/10)))";
            var parser = new SolusParser();
            var cs = new CommandSet();
            cs.AddCommand(DeleteCommand.Value);
            cs.AddCommand(FuncAssignCommand.Value);
            cs.AddCommand(HelpCommand.Value);
            cs.AddCommand(VarAssignCommand.Value);
            cs.AddCommand(VarsCommand.Value);
            // when
            var result = parser.GetCommands(input, cs);
            // then
            Assert.IsNotNull(result);
            Assert.That(result.Length, Is.EqualTo(1));
            Assert.IsInstanceOf<FuncAssignCommandData>(result[0]);
            var udf = ((FuncAssignCommandData)result[0]).Func;
            Assert.That(udf.Name, Is.EqualTo("f"));
            Assert.That(udf.Argnames.Length, Is.EqualTo(1));
            Assert.That(udf.Argnames[0], Is.EqualTo("x"));
            Assert.IsInstanceOf<FunctionCall>(udf.Expression);
            var call = (FunctionCall)udf.Expression;
            Assert.IsInstanceOf<VariableAccess>(call.Function);
            Assert.That(((VariableAccess)call.Function).VariableName,
                Is.EqualTo("if"));
            Assert.That(call.Arguments.Count, Is.EqualTo(3));

            Assert.IsInstanceOf<FunctionCall>(call.Arguments[0]);
            var call2 = (FunctionCall)call.Arguments[0];
            Assert.IsInstanceOf<Literal>(call2.Function);
            Assert.That(((Literal)call2.Function).Value,
                Is.SameAs(EqualComparisonOperation.Value));
            Assert.IsInstanceOf<VariableAccess>(call2.Arguments[0]);
            Assert.That(((VariableAccess)call2.Arguments[0]).VariableName,
                Is.EqualTo("x"));
            Assert.IsInstanceOf<Literal>(call2.Arguments[1]);
            Assert.That(((Literal)call2.Arguments[1]).Value.ToNumber().Value,
                Is.EqualTo(0));

            Assert.IsInstanceOf<Literal>(call.Arguments[1]);
            Assert.That(((Literal)call.Arguments[1]).Value.ToNumber().Value,
                Is.EqualTo(0));

            Assert.IsInstanceOf<FunctionCall>(call.Arguments[2]);
            call2 = (FunctionCall)call.Arguments[2];
            Assert.IsInstanceOf<Literal>(call2.Function);
            Assert.That(((Literal)call2.Function).Value,
                Is.SameAs(AdditionOperation.Value));
            Assert.That(call2.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<Literal>(call2.Arguments[0]);
            Assert.That(((Literal)call2.Arguments[0]).Value.ToNumber().Value,
                Is.EqualTo(1));
            Assert.IsInstanceOf<FunctionCall>(call2.Arguments[1]);

            var call3 = (FunctionCall)call2.Arguments[1];
            Assert.IsInstanceOf<VariableAccess>(call3.Function);
            Assert.That(((VariableAccess)call3.Function).VariableName,
                Is.EqualTo("f"));
            Assert.That(call3.Arguments.Count, Is.EqualTo(1));
            Assert.IsInstanceOf<FunctionCall>(call3.Arguments[0]);

            var call4 = (FunctionCall)call3.Arguments[0];
            Assert.IsInstanceOf<VariableAccess>(call4.Function);
            Assert.That(((VariableAccess)call4.Function).VariableName,
                Is.EqualTo("floor"));
            Assert.That(call4.Arguments.Count, Is.EqualTo(1));
            Assert.IsInstanceOf<FunctionCall>(call4.Arguments[0]);

            var call5 = (FunctionCall)call4.Arguments[0];
            Assert.IsInstanceOf<Literal>(call5.Function);
            Assert.That(((Literal)call5.Function).Value,
                Is.SameAs(DivisionOperation.Value));
            Assert.That(call5.Arguments.Count, Is.EqualTo(2));
            Assert.IsInstanceOf<VariableAccess>(call5.Arguments[0]);
            Assert.That(((VariableAccess)call5.Arguments[0]).VariableName,
                Is.EqualTo("x"));
            Assert.IsInstanceOf<Literal>(call5.Arguments[1]);
            Assert.That(((Literal)call5.Arguments[1]).Value.ToNumber().Value,
                Is.EqualTo(10));
        }
    }
}
