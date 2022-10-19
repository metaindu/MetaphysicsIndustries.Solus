
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

using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.EvaluatorsT.CommonT
{
    [TestFixture(typeof(BasicEvaluator))]
    [TestFixture(typeof(CompilingEvaluator))]
    public class EvalIntervalTest<T>
        where T : IEvaluator, new()
    {
        [Test]
        public void SingleIntervalEval()
        {
            // given
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(MultiplicationOperation.Value,
                new VariableAccess("x"),
                new VariableAccess("x"));
            var env = new SolusEnvironment();
            var interval = new VarInterval("x",
                new Interval(2, false, 6, false, false));
            var numSteps = 5;
            var store = new StoreOp1<Number>();
            // when
            eval.EvalInterval(expr, env, interval, numSteps, store);
            // then
            Assert.IsNotNull(store.Values);
            Assert.AreEqual(5, store.Values.Length);
            Assert.AreEqual(4, store.Values[0].Value);
            Assert.AreEqual(9, store.Values[1].Value);
            Assert.AreEqual(16, store.Values[2].Value);
            Assert.AreEqual(25, store.Values[3].Value);
            Assert.AreEqual(36, store.Values[4].Value);
        }

        [Test]
        public void StoreToVector()
        {
            // given
            var eval = Util.CreateEvaluator<T>();
            var expr = new FunctionCall(MultiplicationOperation.Value,
                new VariableAccess("x"),
                new VariableAccess("x"));
            var env = new SolusEnvironment();
            var interval = new VarInterval("x",
                new Interval(2, false, 6, false, false));
            var numSteps = 5;
            var store = new VectorStoreOp();
            // when
            eval.EvalInterval(expr, env, interval, numSteps, store);
            // then
            var result = store.GetResult();
            Assert.AreEqual(5, result.Length);
            Assert.AreEqual(4, result[0].ToNumber().Value);
            Assert.AreEqual(9, result[1].ToNumber().Value);
            Assert.AreEqual(16, result[2].ToNumber().Value);
            Assert.AreEqual(25, result[3].ToNumber().Value);
            Assert.AreEqual(36, result[4].ToNumber().Value);
        }

        [Test]
        public void TwoIntervalEval()
        {
            // given
            var eval = Util.CreateEvaluator<T>();
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
            var store = new StoreOp2<Number>();
            // when
            eval.EvalInterval(expr, env,
                interval1, numSteps1,
                interval2, numSteps2,
                store);
            // then
            Assert.IsNotNull(store.Values);
            Assert.AreEqual(5, store.Values.GetLength(0));
            Assert.AreEqual(6, store.Values.GetLength(1));

            Assert.AreEqual(14, store.Values[0, 0].Value);
            Assert.AreEqual(16, store.Values[0, 1].Value);
            Assert.AreEqual(18, store.Values[0, 2].Value);
            Assert.AreEqual(20, store.Values[0, 3].Value);
            Assert.AreEqual(22, store.Values[0, 4].Value);
            Assert.AreEqual(24, store.Values[0, 5].Value);

            Assert.AreEqual(21, store.Values[1, 0].Value);
            Assert.AreEqual(24, store.Values[1, 1].Value);
            Assert.AreEqual(27, store.Values[1, 2].Value);
            Assert.AreEqual(30, store.Values[1, 3].Value);
            Assert.AreEqual(33, store.Values[1, 4].Value);
            Assert.AreEqual(36, store.Values[1, 5].Value);

            Assert.AreEqual(28, store.Values[2, 0].Value);
            Assert.AreEqual(32, store.Values[2, 1].Value);
            Assert.AreEqual(36, store.Values[2, 2].Value);
            Assert.AreEqual(40, store.Values[2, 3].Value);
            Assert.AreEqual(44, store.Values[2, 4].Value);
            Assert.AreEqual(48, store.Values[2, 5].Value);

            Assert.AreEqual(35, store.Values[3, 0].Value);
            Assert.AreEqual(40, store.Values[3, 1].Value);
            Assert.AreEqual(45, store.Values[3, 2].Value);
            Assert.AreEqual(50, store.Values[3, 3].Value);
            Assert.AreEqual(55, store.Values[3, 4].Value);
            Assert.AreEqual(60, store.Values[3, 5].Value);

            Assert.AreEqual(42, store.Values[4, 0].Value);
            Assert.AreEqual(48, store.Values[4, 1].Value);
            Assert.AreEqual(54, store.Values[4, 2].Value);
            Assert.AreEqual(60, store.Values[4, 3].Value);
            Assert.AreEqual(66, store.Values[4, 4].Value);
            Assert.AreEqual(72, store.Values[4, 5].Value);
        }

        [Test]
        public void StoreToMatrix()
        {
            // given
            var eval = Util.CreateEvaluator<T>();
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
            var store = new MatrixStoreOp();
            // when
            eval.EvalInterval(expr, env,
                interval1, numSteps1,
                interval2, numSteps2,
                store);
            // then
            var result = store.GetResult();
            Assert.AreEqual(5, result.RowCount);
            Assert.AreEqual(6, result.ColumnCount);

            Assert.AreEqual(14, result[0, 0].ToNumber().Value);
            Assert.AreEqual(16, result[0, 1].ToNumber().Value);
            Assert.AreEqual(18, result[0, 2].ToNumber().Value);
            Assert.AreEqual(20, result[0, 3].ToNumber().Value);
            Assert.AreEqual(22, result[0, 4].ToNumber().Value);
            Assert.AreEqual(24, result[0, 5].ToNumber().Value);

            Assert.AreEqual(21, result[1, 0].ToNumber().Value);
            Assert.AreEqual(24, result[1, 1].ToNumber().Value);
            Assert.AreEqual(27, result[1, 2].ToNumber().Value);
            Assert.AreEqual(30, result[1, 3].ToNumber().Value);
            Assert.AreEqual(33, result[1, 4].ToNumber().Value);
            Assert.AreEqual(36, result[1, 5].ToNumber().Value);

            Assert.AreEqual(28, result[2, 0].ToNumber().Value);
            Assert.AreEqual(32, result[2, 1].ToNumber().Value);
            Assert.AreEqual(36, result[2, 2].ToNumber().Value);
            Assert.AreEqual(40, result[2, 3].ToNumber().Value);
            Assert.AreEqual(44, result[2, 4].ToNumber().Value);
            Assert.AreEqual(48, result[2, 5].ToNumber().Value);

            Assert.AreEqual(35, result[3, 0].ToNumber().Value);
            Assert.AreEqual(40, result[3, 1].ToNumber().Value);
            Assert.AreEqual(45, result[3, 2].ToNumber().Value);
            Assert.AreEqual(50, result[3, 3].ToNumber().Value);
            Assert.AreEqual(55, result[3, 4].ToNumber().Value);
            Assert.AreEqual(60, result[3, 5].ToNumber().Value);

            Assert.AreEqual(42, result[4, 0].ToNumber().Value);
            Assert.AreEqual(48, result[4, 1].ToNumber().Value);
            Assert.AreEqual(54, result[4, 2].ToNumber().Value);
            Assert.AreEqual(60, result[4, 3].ToNumber().Value);
            Assert.AreEqual(66, result[4, 4].ToNumber().Value);
            Assert.AreEqual(72, result[4, 5].ToNumber().Value);
        }

        [Test]
        public void ThreeIntervalEval()
        {
            // given
            var eval = Util.CreateEvaluator<T>();
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
            var store = new StoreOp3<Number>();
            // when
            eval.EvalInterval(expr, env,
                interval1, numSteps1,
                interval2, numSteps2,
                interval3, numSteps3,
                store);
            // then
            Assert.IsNotNull(store.Values);
            Assert.AreEqual(2, store.Values.GetLength(0));
            Assert.AreEqual(3, store.Values.GetLength(1));
            Assert.AreEqual(5, store.Values.GetLength(2));

            Assert.AreEqual(56, store.Values[0, 0, 0].Value);
            Assert.AreEqual(64, store.Values[0, 0, 1].Value);
            Assert.AreEqual(72, store.Values[0, 0, 2].Value);
            Assert.AreEqual(80, store.Values[0, 0, 3].Value);
            Assert.AreEqual(88, store.Values[0, 0, 4].Value);

            Assert.AreEqual(70, store.Values[0, 1, 0].Value);
            Assert.AreEqual(80, store.Values[0, 1, 1].Value);
            Assert.AreEqual(90, store.Values[0, 1, 2].Value);
            Assert.AreEqual(100, store.Values[0, 1, 3].Value);
            Assert.AreEqual(110, store.Values[0, 1, 4].Value);

            Assert.AreEqual(84, store.Values[0, 2, 0].Value);
            Assert.AreEqual(96, store.Values[0, 2, 1].Value);
            Assert.AreEqual(108, store.Values[0, 2, 2].Value);
            Assert.AreEqual(120, store.Values[0, 2, 3].Value);
            Assert.AreEqual(132, store.Values[0, 2, 4].Value);

            Assert.AreEqual(84, store.Values[1, 0, 0].Value);
            Assert.AreEqual(96, store.Values[1, 0, 1].Value);
            Assert.AreEqual(108, store.Values[1, 0, 2].Value);
            Assert.AreEqual(120, store.Values[1, 0, 3].Value);
            Assert.AreEqual(132, store.Values[1, 0, 4].Value);

            Assert.AreEqual(105, store.Values[1, 1, 0].Value);
            Assert.AreEqual(120, store.Values[1, 1, 1].Value);
            Assert.AreEqual(135, store.Values[1, 1, 2].Value);
            Assert.AreEqual(150, store.Values[1, 1, 3].Value);
            Assert.AreEqual(165, store.Values[1, 1, 4].Value);

            Assert.AreEqual(126, store.Values[1, 2, 0].Value);
            Assert.AreEqual(144, store.Values[1, 2, 1].Value);
            Assert.AreEqual(162, store.Values[1, 2, 2].Value);
            Assert.AreEqual(180, store.Values[1, 2, 3].Value);
            Assert.AreEqual(198, store.Values[1, 2, 4].Value);
        }

        [Test]
        [Ignore("too slow")]
        public void PlotSomething()
        {
            // given
            var parser = new SolusParser();
            var env = new SolusEnvironment();
            var expr = parser.GetExpression("x*e^(-x*x-y*y)");
            var interval1 = new VarInterval("x", -2, 2);
            var interval2 = new VarInterval("y", -2, 2);
            var store = new StoreOp2<Number>();
            var eval = Util.CreateEvaluator<T>();
            env.SetVariable("e", new Literal(2.718281828f));
            // when
            int startTime = System.Environment.TickCount;
            eval.EvalInterval(expr, env,
                interval1, 4000,
                interval2, 4000,
                store);
            int endTime = System.Environment.TickCount;
            // then
            Assert.LessOrEqual(endTime - startTime, 1);
        }

        [Test]
        public void AggregateOpRunsOnEachValue1()
        {
            // given
            var expr = new FunctionCall(
                MultiplicationOperation.Value,
                new VariableAccess("x"),
                new VariableAccess("x"));
            var eval = Util.CreateEvaluator<T>();

            Number Collect(Number n, Number state)
            {
                // find the max
                if (n.Value > state.Value)
                    return n;
                return state;
            }

            var interval = new VarInterval("x", 1, 5);
            var env = new SolusEnvironment();
            var aggr = new AggregateOp<Number, Number>
            {
                Function = Collect,
                State = 0.ToNumber()
            };
            // when
            eval.EvalInterval(expr, env, interval, 5, null,
                new AggregateOp[] { aggr });
            // then
            Assert.AreEqual(25, aggr.State.Value);
        }

        [Test]
        public void AggregateOpRunsOnEachValue2()
        {
            // given
            var expr = new FunctionCall(
                MultiplicationOperation.Value,
                new VariableAccess("x"),
                new VariableAccess("y"));
            var eval = Util.CreateEvaluator<T>();

            Number Max(Number n, Number state)
            {
                // find the max
                if (n.Value > state.Value)
                    return n;
                return state;
            }

            var interval1 = new VarInterval("x", 1, 3);
            var interval2 = new VarInterval("y", 4, 6);
            var env = new SolusEnvironment();
            var aggr = new AggregateOp<Number, Number>
            {
                Function = Max,
                State = 0.ToNumber()
            };
            // when
            eval.EvalInterval(expr, env,
                interval1, 3,
                interval2, 3,
                null,
                new AggregateOp[] { aggr });
            // then
            Assert.AreEqual(18, aggr.State.Value);
        }

        [Test]
        public void AggregateOpRunsOnEachValue3()
        {
            // given
            var expr = new FunctionCall(
                MultiplicationOperation.Value,
                new VariableAccess("x"),
                new VariableAccess("y"),
                new VariableAccess("z"));
            var eval = Util.CreateEvaluator<T>();

            Number Max(Number n, Number state)
            {
                // find the max
                if (n.Value > state.Value)
                    return n;
                return state;
            }

            var interval1 = new VarInterval("x", 1, 3);
            var interval2 = new VarInterval("y", 4, 6);
            var interval3 = new VarInterval("z", 7, 9);
            var env = new SolusEnvironment();
            var aggr = new AggregateOp<Number, Number>
            {
                Function = Max,
                State = 0.ToNumber()
            };
            // when
            eval.EvalInterval(expr, env,
                interval1, 3,
                interval2, 3,
                interval3, 3,
                null,
                new AggregateOp[] { aggr });
            // then
            Assert.AreEqual(162, aggr.State.Value);
        }

        [Test]
        public void MultipleAggregateOpsYieldMultipleResults()
        {
            // given
            var expr = new FunctionCall(
                AdditionOperation.Value,
                new VariableAccess("x"),
                new Literal(2));
            var eval = Util.CreateEvaluator<T>();
            var interval = new VarInterval("x", 1, 5);
            var env = new SolusEnvironment();
            var aggrMax = new AggregateOp<Number, Number>
            {
                Function = (Number n, Number state) =>
                    // find the max
                    n.Value > state.Value ? n : state,
                State = 0.ToNumber()
            };
            var aggrMin = new AggregateOp<Number, Number>
            {
                Function = (Number n, Number state) =>
                    // find the max
                    n.Value < state.Value ? n : state,
                State = 1000.ToNumber()
            };
            var aggrs = new AggregateOp[] { aggrMin, aggrMax };
            // when
            eval.EvalInterval(expr, env, interval, 5, null, aggrs);
            // then
            Assert.AreEqual(7, aggrMax.State.Value);
            Assert.AreEqual(3, aggrMin.State.Value);
        }

        [Test]
        public void FunctionAggregateOpRunsOnEachValue()
        {
            // given
            var expr = new FunctionCall(
                MultiplicationOperation.Value,
                new VariableAccess("x"),
                new VariableAccess("x"));
            var eval = Util.CreateEvaluator<T>();
            var interval = new VarInterval("x", 1, 5);
            var env = new SolusEnvironment();
            var aggr = new FunctionAggregateOp(
                MaximumFiniteFunction.Value, float.NaN.ToNumber());
            // when
            eval.EvalInterval(expr, env, interval, 5, null,
                new AggregateOp[] { aggr });
            // then
            Assert.AreEqual(25, aggr.State.ToNumber().Value);
        }

    }
}
