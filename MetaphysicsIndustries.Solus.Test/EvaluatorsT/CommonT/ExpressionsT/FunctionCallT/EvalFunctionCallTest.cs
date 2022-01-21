
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

using System.Linq;
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT.
    ExpressionsT.FunctionCallT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalFunctionCallTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        public void FunctionCallYieldsReturnValue()
        {
            // given
            var mf = new MockFunction(new[] { Types.Scalar }, "f")
            {
                CallF = args => args.First()
            };
            var expr = new FunctionCall(mf, new Literal(5));
            var eval = Util.CreateEvaluator<T>();
            // when
            var result0 = eval.Eval(expr, null);
            // then
            Assert.IsNotNull(result0);
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsScalar(null));
            Assert.IsInstanceOf<Number>(result0);
            var result = result0.ToNumber();
            Assert.AreEqual(5, result.Value);
        }

        [Test]
        public void FunctionCallWithVariableTargetYieldsValue()
        {
            // given
            var mf = new MockFunction(new[] { Types.Scalar }, "f")
            {
                CallF = args => args.First()
            };
            var expr = new FunctionCall(new VariableAccess("f"),
                new Expression[] { new Literal(5) });
            var eval = Util.CreateEvaluator<T>();
            var env = new SolusEnvironment();
            env.SetVariable("f", mf);
            // when
            var result0 = eval.Eval(expr, env);
            // then
            Assert.IsNotNull(result0);
            Assert.IsTrue(result0.IsConcrete);
            Assert.IsTrue(result0.IsIsScalar(null));
            Assert.IsInstanceOf<Number>(result0);
            var result = result0.ToNumber();
            Assert.AreEqual(5, result.Value);
        }

        [Test]
        [Ignore("IfMacro currently returns a Literal, which confuses " +
                "MultiplicationOperation")]
        public void RecursiveFunctionYieldsValue()
        {
            // given
            // factorial
            // f(x) := if( x<=1, 1, x * f(x-1) )
            var udf = new UserDefinedFunction("f", new[] { "x" },
                new FunctionCall(
                    new VariableAccess("if"),
                    new FunctionCall(
                        LessThanOrEqualComparisonOperation.Value,
                        new VariableAccess("x"),
                        new Literal(1)),
                    new Literal(1),
                    new FunctionCall(
                        MultiplicationOperation.Value,
                        new VariableAccess("x"),
                        new FunctionCall(
                            new VariableAccess("f"),
                            new FunctionCall(
                                AdditionOperation.Value,
                                new VariableAccess("x"),
                                new Literal(-1))))));
            var env = new SolusEnvironment();
            env.SetVariable("f", udf);
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(
                new VariableAccess("f"),
                new Literal(2));
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.IsInstanceOf<Number>(result);
            var n = (Number)result;
            Assert.AreEqual(2, n.Value);
        }
    }
}
