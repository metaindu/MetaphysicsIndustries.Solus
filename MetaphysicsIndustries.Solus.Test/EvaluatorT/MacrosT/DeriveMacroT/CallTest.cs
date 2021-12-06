
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

using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorT.MacrosT.DeriveMacroT
{
    [TestFixture]
    public class CallTest
    {
        [Test]
        public void DeriveYieldsDerivative()
        {
            // given
            var parser = new SolusParser();
            var expr = new FunctionCall(
                new Literal(DeriveMacro.Value),
                parser.GetExpression("3*x^2+5*x+7"),
                new VariableAccess("x"));
            var env = new SolusEnvironment();
            var eval = new Evaluator();
            // when
            var result = eval.Eval(expr, env);
            // then
            Assert.IsInstanceOf<FunctionCall>(result);
            var fc = (FunctionCall)result;
            Assert.IsInstanceOf<Literal>(fc.Function);
            var f = (Literal)fc.Function;
            Assert.AreSame(AdditionOperation.Value, f.Value);
            Assert.AreEqual(2, fc.Arguments.Count);
            Assert.IsTrue(fc.Arguments[0] is Literal ||
                          fc.Arguments[0] is FunctionCall);
            Assert.IsTrue(fc.Arguments[1] is Literal ||
                          fc.Arguments[1] is FunctionCall);
            Literal lit;
            FunctionCall fc2;
            if (fc.Arguments[0] is Literal)
            {
                lit = (Literal)fc.Arguments[0];
                fc2 = (FunctionCall)fc.Arguments[1];
            }
            else
            {
                fc2 = (FunctionCall)fc.Arguments[0];
                lit = (Literal)fc.Arguments[1];
            }

            Assert.AreEqual(5,lit.Value.ToNumber().Value);

            Assert.IsInstanceOf<Literal>(fc2.Function);
            Assert.AreSame(MultiplicationOperation.Value,
                ((Literal)fc2.Function).Value);
            Assert.AreEqual(2, fc2.Arguments.Count);
            Assert.IsInstanceOf<Literal>(fc2.Arguments[0]);

            var arg11 = (Literal)fc2.Arguments[0];
            Assert.AreEqual(3,arg11.Value.ToNumber().Value);

            Assert.IsInstanceOf<FunctionCall>(fc2.Arguments[1]);
            var arg12 = (FunctionCall)fc2.Arguments[1];
            Assert.IsInstanceOf<Literal>(arg12.Function);
            Assert.AreSame(MultiplicationOperation.Value,
                ((Literal)arg12.Function).Value);
            Assert.AreEqual(2, arg12.Arguments.Count);
            Assert.IsInstanceOf<Literal>(arg12.Arguments[0]);
            Assert.AreEqual(2,
                ((Literal)arg12.Arguments[0]).Value.ToNumber().Value);

            Assert.IsInstanceOf<VariableAccess>(arg12.Arguments[1]);
            Assert.AreEqual("x",
                ((VariableAccess)arg12.Arguments[1]).VariableName);
        }
    }
}
