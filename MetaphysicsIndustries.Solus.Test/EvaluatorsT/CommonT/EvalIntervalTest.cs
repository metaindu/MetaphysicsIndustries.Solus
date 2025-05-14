
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
            Assert.That(store.Values.Length, Is.EqualTo(5));
            Assert.That(store.Values[0].Value, Is.EqualTo(4));
            Assert.That(store.Values[1].Value, Is.EqualTo(9));
            Assert.That(store.Values[2].Value, Is.EqualTo(16));
            Assert.That(store.Values[3].Value, Is.EqualTo(25));
            Assert.That(store.Values[4].Value, Is.EqualTo(36));
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
            Assert.That(result.Length, Is.EqualTo(5));
            Assert.That(result[0].ToNumber().Value, Is.EqualTo(4));
            Assert.That(result[1].ToNumber().Value, Is.EqualTo(9));
            Assert.That(result[2].ToNumber().Value, Is.EqualTo(16));
            Assert.That(result[3].ToNumber().Value, Is.EqualTo(25));
            Assert.That(result[4].ToNumber().Value, Is.EqualTo(36));
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
            Assert.That(store.Values.GetLength(0), Is.EqualTo(5));
            Assert.That(store.Values.GetLength(1), Is.EqualTo(6));

            Assert.That(store.Values[0, 0].Value, Is.EqualTo(14));
            Assert.That(store.Values[0, 1].Value, Is.EqualTo(16));
            Assert.That(store.Values[0, 2].Value, Is.EqualTo(18));
            Assert.That(store.Values[0, 3].Value, Is.EqualTo(20));
            Assert.That(store.Values[0, 4].Value, Is.EqualTo(22));
            Assert.That(store.Values[0, 5].Value, Is.EqualTo(24));

            Assert.That(store.Values[1, 0].Value, Is.EqualTo(21));
            Assert.That(store.Values[1, 1].Value, Is.EqualTo(24));
            Assert.That(store.Values[1, 2].Value, Is.EqualTo(27));
            Assert.That(store.Values[1, 3].Value, Is.EqualTo(30));
            Assert.That(store.Values[1, 4].Value, Is.EqualTo(33));
            Assert.That(store.Values[1, 5].Value, Is.EqualTo(36));

            Assert.That(store.Values[2, 0].Value, Is.EqualTo(28));
            Assert.That(store.Values[2, 1].Value, Is.EqualTo(32));
            Assert.That(store.Values[2, 2].Value, Is.EqualTo(36));
            Assert.That(store.Values[2, 3].Value, Is.EqualTo(40));
            Assert.That(store.Values[2, 4].Value, Is.EqualTo(44));
            Assert.That(store.Values[2, 5].Value, Is.EqualTo(48));

            Assert.That(store.Values[3, 0].Value, Is.EqualTo(35));
            Assert.That(store.Values[3, 1].Value, Is.EqualTo(40));
            Assert.That(store.Values[3, 2].Value, Is.EqualTo(45));
            Assert.That(store.Values[3, 3].Value, Is.EqualTo(50));
            Assert.That(store.Values[3, 4].Value, Is.EqualTo(55));
            Assert.That(store.Values[3, 5].Value, Is.EqualTo(60));

            Assert.That(store.Values[4, 0].Value, Is.EqualTo(42));
            Assert.That(store.Values[4, 1].Value, Is.EqualTo(48));
            Assert.That(store.Values[4, 2].Value, Is.EqualTo(54));
            Assert.That(store.Values[4, 3].Value, Is.EqualTo(60));
            Assert.That(store.Values[4, 4].Value, Is.EqualTo(66));
            Assert.That(store.Values[4, 5].Value, Is.EqualTo(72));
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
            Assert.That(result.RowCount, Is.EqualTo(5));
            Assert.That(result.ColumnCount, Is.EqualTo(6));

            Assert.That(result[0, 0].ToNumber().Value, Is.EqualTo(14));
            Assert.That(result[0, 1].ToNumber().Value, Is.EqualTo(16));
            Assert.That(result[0, 2].ToNumber().Value, Is.EqualTo(18));
            Assert.That(result[0, 3].ToNumber().Value, Is.EqualTo(20));
            Assert.That(result[0, 4].ToNumber().Value, Is.EqualTo(22));
            Assert.That(result[0, 5].ToNumber().Value, Is.EqualTo(24));

            Assert.That(result[1, 0].ToNumber().Value, Is.EqualTo(21));
            Assert.That(result[1, 1].ToNumber().Value, Is.EqualTo(24));
            Assert.That(result[1, 2].ToNumber().Value, Is.EqualTo(27));
            Assert.That(result[1, 3].ToNumber().Value, Is.EqualTo(30));
            Assert.That(result[1, 4].ToNumber().Value, Is.EqualTo(33));
            Assert.That(result[1, 5].ToNumber().Value, Is.EqualTo(36));

            Assert.That(result[2, 0].ToNumber().Value, Is.EqualTo(28));
            Assert.That(result[2, 1].ToNumber().Value, Is.EqualTo(32));
            Assert.That(result[2, 2].ToNumber().Value, Is.EqualTo(36));
            Assert.That(result[2, 3].ToNumber().Value, Is.EqualTo(40));
            Assert.That(result[2, 4].ToNumber().Value, Is.EqualTo(44));
            Assert.That(result[2, 5].ToNumber().Value, Is.EqualTo(48));

            Assert.That(result[3, 0].ToNumber().Value, Is.EqualTo(35));
            Assert.That(result[3, 1].ToNumber().Value, Is.EqualTo(40));
            Assert.That(result[3, 2].ToNumber().Value, Is.EqualTo(45));
            Assert.That(result[3, 3].ToNumber().Value, Is.EqualTo(50));
            Assert.That(result[3, 4].ToNumber().Value, Is.EqualTo(55));
            Assert.That(result[3, 5].ToNumber().Value, Is.EqualTo(60));

            Assert.That(result[4, 0].ToNumber().Value, Is.EqualTo(42));
            Assert.That(result[4, 1].ToNumber().Value, Is.EqualTo(48));
            Assert.That(result[4, 2].ToNumber().Value, Is.EqualTo(54));
            Assert.That(result[4, 3].ToNumber().Value, Is.EqualTo(60));
            Assert.That(result[4, 4].ToNumber().Value, Is.EqualTo(66));
            Assert.That(result[4, 5].ToNumber().Value, Is.EqualTo(72));
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
            Assert.That(store.Values.GetLength(0), Is.EqualTo(2));
            Assert.That(store.Values.GetLength(1), Is.EqualTo(3));
            Assert.That(store.Values.GetLength(2), Is.EqualTo(5));

            Assert.That(store.Values[0, 0, 0].Value, Is.EqualTo(56));
            Assert.That(store.Values[0, 0, 1].Value, Is.EqualTo(64));
            Assert.That(store.Values[0, 0, 2].Value, Is.EqualTo(72));
            Assert.That(store.Values[0, 0, 3].Value, Is.EqualTo(80));
            Assert.That(store.Values[0, 0, 4].Value, Is.EqualTo(88));

            Assert.That(store.Values[0, 1, 0].Value, Is.EqualTo(70));
            Assert.That(store.Values[0, 1, 1].Value, Is.EqualTo(80));
            Assert.That(store.Values[0, 1, 2].Value, Is.EqualTo(90));
            Assert.That(store.Values[0, 1, 3].Value, Is.EqualTo(100));
            Assert.That(store.Values[0, 1, 4].Value, Is.EqualTo(110));

            Assert.That(store.Values[0, 2, 0].Value, Is.EqualTo(84));
            Assert.That(store.Values[0, 2, 1].Value, Is.EqualTo(96));
            Assert.That(store.Values[0, 2, 2].Value, Is.EqualTo(108));
            Assert.That(store.Values[0, 2, 3].Value, Is.EqualTo(120));
            Assert.That(store.Values[0, 2, 4].Value, Is.EqualTo(132));

            Assert.That(store.Values[1, 0, 0].Value, Is.EqualTo(84));
            Assert.That(store.Values[1, 0, 1].Value, Is.EqualTo(96));
            Assert.That(store.Values[1, 0, 2].Value, Is.EqualTo(108));
            Assert.That(store.Values[1, 0, 3].Value, Is.EqualTo(120));
            Assert.That(store.Values[1, 0, 4].Value, Is.EqualTo(132));

            Assert.That(store.Values[1, 1, 0].Value, Is.EqualTo(105));
            Assert.That(store.Values[1, 1, 1].Value, Is.EqualTo(120));
            Assert.That(store.Values[1, 1, 2].Value, Is.EqualTo(135));
            Assert.That(store.Values[1, 1, 3].Value, Is.EqualTo(150));
            Assert.That(store.Values[1, 1, 4].Value, Is.EqualTo(165));

            Assert.That(store.Values[1, 2, 0].Value, Is.EqualTo(126));
            Assert.That(store.Values[1, 2, 1].Value, Is.EqualTo(144));
            Assert.That(store.Values[1, 2, 2].Value, Is.EqualTo(162));
            Assert.That(store.Values[1, 2, 3].Value, Is.EqualTo(180));
            Assert.That(store.Values[1, 2, 4].Value, Is.EqualTo(198));
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
            Assert.That(aggr.State.Value, Is.EqualTo(25));
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
            Assert.That(aggr.State.Value, Is.EqualTo(18));
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
            Assert.That(aggr.State.Value, Is.EqualTo(162));
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
            Assert.That(aggrMax.State.Value, Is.EqualTo(7));
            Assert.That(aggrMin.State.Value, Is.EqualTo(3));
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
            Assert.That(aggr.State.ToNumber().Value, Is.EqualTo(25));
        }

    }
}
