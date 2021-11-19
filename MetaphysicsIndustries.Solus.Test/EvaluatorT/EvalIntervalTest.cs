
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
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorT
{
    [TestFixture]
    public class EvalIntervalTest
    {
        [Test]
        public void SingleIntervalEval()
        {
            // given
            var eval = new Evaluator();
            var expr = new FunctionCall(MultiplicationOperation.Value,
                new VariableAccess("x"),
                new VariableAccess("x"));
            var env = new SolusEnvironment();
            var interval = new VarInterval("x",
                new Interval(2, false, 6, false, false));
            var numSteps = 5;
            float[] values = null;
            // when
            eval.EvalInterval(expr, env, interval, numSteps, ref values);
            // then
            Assert.IsNotNull(values);
            Assert.AreEqual(5, values.Length);
            Assert.AreEqual(4, values[0]);
            Assert.AreEqual(9, values[1]);
            Assert.AreEqual(16, values[2]);
            Assert.AreEqual(25, values[3]);
            Assert.AreEqual(36, values[4]);
        }

        [Test]
        public void TwoIntervalEval()
        {
            // given
            var eval = new Evaluator();
            var expr = new FunctionCall(MultiplicationOperation.Value,
                new VariableAccess("x"),
                new VariableAccess("y"));
            var env = new SolusEnvironment();
            var interval1 = new VarInterval("x",
                new Interval(2, false, 6, false, false));
            var numSteps1 = 5;
            var interval2 = new VarInterval("y",
                new Interval(7, false, 12, false, false));
            var numSteps2 = 6;
            float[,] values = null;
            // when
            eval.EvalInterval(expr, env,
                interval1, numSteps1,
                interval2, numSteps2,
                ref values);
            // then
            Assert.IsNotNull(values);
            Assert.AreEqual(5, values.GetLength(0));
            Assert.AreEqual(6, values.GetLength(1));

            Assert.AreEqual(14, values[0, 0]);
            Assert.AreEqual(16, values[0, 1]);
            Assert.AreEqual(18, values[0, 2]);
            Assert.AreEqual(20, values[0, 3]);
            Assert.AreEqual(22, values[0, 4]);
            Assert.AreEqual(24, values[0, 5]);

            Assert.AreEqual(21, values[1, 0]);
            Assert.AreEqual(24, values[1, 1]);
            Assert.AreEqual(27, values[1, 2]);
            Assert.AreEqual(30, values[1, 3]);
            Assert.AreEqual(33, values[1, 4]);
            Assert.AreEqual(36, values[1, 5]);

            Assert.AreEqual(28, values[2, 0]);
            Assert.AreEqual(32, values[2, 1]);
            Assert.AreEqual(36, values[2, 2]);
            Assert.AreEqual(40, values[2, 3]);
            Assert.AreEqual(44, values[2, 4]);
            Assert.AreEqual(48, values[2, 5]);

            Assert.AreEqual(35, values[3, 0]);
            Assert.AreEqual(40, values[3, 1]);
            Assert.AreEqual(45, values[3, 2]);
            Assert.AreEqual(50, values[3, 3]);
            Assert.AreEqual(55, values[3, 4]);
            Assert.AreEqual(60, values[3, 5]);

            Assert.AreEqual(42, values[4, 0]);
            Assert.AreEqual(48, values[4, 1]);
            Assert.AreEqual(54, values[4, 2]);
            Assert.AreEqual(60, values[4, 3]);
            Assert.AreEqual(66, values[4, 4]);
            Assert.AreEqual(72, values[4, 5]);
        }

        [Test]
        public void ThreeIntervalEval()
        {
            // given
            var eval = new Evaluator();
            var expr = new FunctionCall(MultiplicationOperation.Value,
                new VariableAccess("x"),
                new VariableAccess("y"),
                new VariableAccess("z"));
            var env = new SolusEnvironment();
            var interval1 = new VarInterval("x",
                new Interval(2, false, 3, false, false));
            var numSteps1 = 2;
            var interval2 = new VarInterval("y",
                new Interval(4, false, 6, false, false));
            var numSteps2 = 3;
            var interval3 = new VarInterval("z",
                new Interval(7, false, 11, false, false));
            var numSteps3 = 5;
            float[,,] values = null;
            // when
            eval.EvalInterval(expr, env,
                interval1, numSteps1,
                interval2, numSteps2,
                interval3, numSteps3,
                ref values);
            // then
            Assert.IsNotNull(values);
            Assert.AreEqual(2, values.GetLength(0));
            Assert.AreEqual(3, values.GetLength(1));
            Assert.AreEqual(5, values.GetLength(2));

            Assert.AreEqual(56, values[0, 0, 0]);
            Assert.AreEqual(64, values[0, 0, 1]);
            Assert.AreEqual(72, values[0, 0, 2]);
            Assert.AreEqual(80, values[0, 0, 3]);
            Assert.AreEqual(88, values[0, 0, 4]);

            Assert.AreEqual(70, values[0, 1, 0]);
            Assert.AreEqual(80, values[0, 1, 1]);
            Assert.AreEqual(90, values[0, 1, 2]);
            Assert.AreEqual(100, values[0, 1, 3]);
            Assert.AreEqual(110, values[0, 1, 4]);

            Assert.AreEqual(84, values[0, 2, 0]);
            Assert.AreEqual(96, values[0, 2, 1]);
            Assert.AreEqual(108, values[0, 2, 2]);
            Assert.AreEqual(120, values[0, 2, 3]);
            Assert.AreEqual(132, values[0, 2, 4]);

            Assert.AreEqual(84, values[1, 0, 0]);
            Assert.AreEqual(96, values[1, 0, 1]);
            Assert.AreEqual(108, values[1, 0, 2]);
            Assert.AreEqual(120, values[1, 0, 3]);
            Assert.AreEqual(132, values[1, 0, 4]);

            Assert.AreEqual(105, values[1, 1, 0]);
            Assert.AreEqual(120, values[1, 1, 1]);
            Assert.AreEqual(135, values[1, 1, 2]);
            Assert.AreEqual(150, values[1, 1, 3]);
            Assert.AreEqual(165, values[1, 1, 4]);

            Assert.AreEqual(126, values[1, 2, 0]);
            Assert.AreEqual(144, values[1, 2, 1]);
            Assert.AreEqual(162, values[1, 2, 2]);
            Assert.AreEqual(180, values[1, 2, 3]);
            Assert.AreEqual(198, values[1, 2, 4]);
        }

        [Test]
        [Ignore("too slow")]
        public void PlotSomething()
        {
            // given
            var parser = new SolusParser();
            var env = new SolusEnvironment();
            var expr = parser.GetExpression("x*e^(-x*x-y*y)", env);
            var interval1 = new VarInterval("x", -2, 2);
            var interval2 = new VarInterval("y", -2, 2);
            float[,] values = null;
            var eval = new Evaluator();
            env.SetVariable("e", new Literal(2.718281828f));
            // when
            int startTime = System.Environment.TickCount;
            eval.EvalInterval(expr, env,
                interval1, 4000,
                interval2, 4000,
                ref values);
            int endTime = System.Environment.TickCount;
            // then
            Assert.LessOrEqual(endTime - startTime, 1);
        }
    }
}
