
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
using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Transformers;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus
{
    public class Evaluator
    {
        public IMathObject Eval(Expression expr, SolusEnvironment env)
        {
            return expr.Eval(env);
        }

        public Expression Simplify(Expression expr, SolusEnvironment env)
        {
            CleanUpTransformer cleanup = new CleanUpTransformer();
            return cleanup.CleanUp(expr.Simplify(env));
        }

        public abstract class StoreOp1
        {
            public abstract void Store(int index, IMathObject value);
            public abstract void SetMinArraySize(int length);
        }

        public class StoreOp1<T> : StoreOp1
            where T : IMathObject
        {
            public T[] Values;

            public override void Store(int index, IMathObject value)
            {
                Values[index] = (T)value;
            }

            public override void SetMinArraySize(int length)
            {
                if (Values == null || Values.Length < length)
                    Values = new T[length];
            }
        }

        public class VectorStoreOp : StoreOp1
        {
            private IMathObject[] _values = null;
            private Vector? _result = null;

            public Vector GetResult()
            {
                if (!_result.HasValue)
                    _result = new Vector(_values);
                return _result.Value;
            }

            public override void Store(int index, IMathObject value)
            {
                _values[index] = value;
            }

            public override void SetMinArraySize(int length)
            {
                if (_values == null || _values.Length != length)
                    _values = new IMathObject[length];
            }
        }

        public abstract class AggregateOp
        {
            public abstract void Operate(IMathObject input,
                SolusEnvironment env);
        }

        public class AggregateOp<TIn, TOut> : AggregateOp
        {
            public TOut State;
            public Func<TIn, TOut, TOut> Function;

            public override void Operate(IMathObject input,
                SolusEnvironment env)
            {
                State = Function((TIn)input, State);
            }
        }

        public class FunctionAggregateOp : AggregateOp
        {
            public FunctionAggregateOp(Function function,
                IMathObject initialState)
            {
                Function = function;
                State = initialState;
            }

            public IMathObject State;
            public readonly Function Function;

            private readonly IMathObject[] _args = new IMathObject[2];
            public override void Operate(IMathObject input,
                SolusEnvironment env)
            {
                _args[0] = input;
                _args[1] = State;
                var result = Function.Call(env, _args);
                State = result;
            }
        }

        public void EvalInterval(Expression expr, SolusEnvironment env,
            VarInterval interval, int numSteps, StoreOp1 store,
            AggregateOp[] aggrs = null)
        {
            var delta = interval.Interval.CalcDelta(numSteps);

            var env2 = env.CreateChildEnvironment();
            env2.RemoveVariable(interval.Variable);
            var expr2 = Simplify(expr, env2);

            var literal = new Literal(0);
            env2.SetVariable(interval.Variable, literal);

            if (store != null)
                store.SetMinArraySize(numSteps);

            int i;
            for (i = 0; i < numSteps; i++)
            {
                var xx = delta * i + interval.Interval.LowerBound;
                literal.Value = xx.ToNumber();
                var v = Eval(expr2, env2);
                if (store != null)
                    store.Store(i, v);
                if (aggrs != null)
                    foreach (var aggr in aggrs)
                        aggr?.Operate(v, env2);
            }
        }

        public abstract class StoreOp2
        {
            public abstract void Store(int index0, int index1,
                IMathObject value);

            public abstract void SetMinArraySize(int length0, int length1);
        }

        public class StoreOp2<T> : StoreOp2
            where T : IMathObject
        {
            public T[,] Values;

            public override void Store(int index0, int index1,
                IMathObject value)
            {
                Values[index0, index1] = (T)value;
            }

            public override void SetMinArraySize(int length0, int length1)
            {
                if (Values == null ||
                    Values.GetLength(0) < length0 ||
                    Values.GetLength(1) < length1)
                {
                    Values = new T[length0, length1];
                }
            }
        }

        public class MatrixStoreOp : StoreOp2
        {
            private IMathObject[,] _values = null;
            private Matrix? _result = null;

            public Matrix GetResult()
            {
                if (!_result.HasValue)
                    _result = new Matrix(_values);
                return _result.Value;
            }

            public override void Store(int index0, int index1,
                IMathObject value)
            {
                _values[index0, index1] = value;
            }

            public override void SetMinArraySize(int length0, int length1)
            {
                if (_values == null ||
                    _values.GetLength(0) < length0 ||
                    _values.GetLength(1) < length1)
                    _values = new IMathObject[length0, length1];
            }
        }

        public void EvalInterval(
            Expression expr, SolusEnvironment env,
            VarInterval interval1, int numSteps1,
            VarInterval interval2, int numSteps2,
            StoreOp2 store, AggregateOp[] aggrs = null)
        {
            var delta1 = interval1.Interval.CalcDelta(numSteps1);
            var delta2 = interval2.Interval.CalcDelta(numSteps2);

            var env2 = env.CreateChildEnvironment();
            env2.RemoveVariable(interval1.Variable);
            env2.RemoveVariable(interval2.Variable);
            var expr2 = Simplify(expr, env2);

            var inputs1 = new IMathObject[numSteps1];
            var inputs2 = new IMathObject[numSteps2];

            int i;
            for (i = 0; i < numSteps1; i++)
                inputs1[i] =
                    (delta1 * i + interval1.Interval.LowerBound).ToNumber();
            for (i = 0; i < numSteps2; i++)
                inputs2[i] =
                    (delta2 * i + interval2.Interval.LowerBound).ToNumber();

            var literal1 = new Literal(0);
            var literal2 = new Literal(0);
            env2.SetVariable(interval1.Variable, literal1);
            env2.SetVariable(interval2.Variable, literal2);

            if (store != null)
                store.SetMinArraySize(numSteps1, numSteps2);

            for (i = 0; i < numSteps1; i++)
            {
                literal1.Value = inputs1[i];
                int j;
                for (j = 0; j < numSteps2; j++)
                {
                    literal2.Value = inputs2[j];
                    var v = Eval(expr2, env2);
                    if (store != null)
                        store.Store(i, j, v);
                    if (aggrs != null)
                        foreach (var aggr in aggrs)
                            aggr?.Operate(v, env2);
                }
            }
        }

        public abstract class StoreOp3
        {
            public abstract void Store(int index0, int index1, int index2,
                IMathObject value);

            public abstract void SetMinArraySize(int length0, int length1,
                int length2);
        }

        public class StoreOp3<T> : StoreOp3
            where T : IMathObject
        {
            public T[,,] Values;

            public override void Store(int index0, int index1, int index2,
                IMathObject value)
            {
                Values[index0, index1, index2] = (T)value;
            }

            public override void SetMinArraySize(int length0, int length1,
                int length2)
            {
                if (Values == null ||
                    Values.GetLength(0) < length0 ||
                    Values.GetLength(1) < length1 ||
                    Values.GetLength(2) < length2)
                {
                    Values = new T[length0, length1, length2];
                }
            }
        }

        public void EvalInterval(
            Expression expr, SolusEnvironment env,
            VarInterval interval1, int numSteps1,
            VarInterval interval2, int numSteps2,
            VarInterval interval3, int numSteps3,
            StoreOp3 store, AggregateOp[] aggrs = null)
        {
            var delta1 = interval1.Interval.CalcDelta(numSteps1);
            var delta2 = interval2.Interval.CalcDelta(numSteps2);
            var delta3 = interval3.Interval.CalcDelta(numSteps3);

            var env2 = env.CreateChildEnvironment();
            env2.RemoveVariable(interval1.Variable);
            env2.RemoveVariable(interval2.Variable);
            env2.RemoveVariable(interval3.Variable);
            var expr2 = Simplify(expr, env2);

            var inputs1 = new IMathObject[numSteps1];
            var inputs2 = new IMathObject[numSteps2];
            var inputs3 = new IMathObject[numSteps3];

            int i;
            for (i = 0; i < numSteps1; i++)
                inputs1[i] =
                    (delta1 * i + interval1.Interval.LowerBound).ToNumber();
            for (i = 0; i < numSteps2; i++)
                inputs2[i] =
                    (delta2 * i + interval2.Interval.LowerBound).ToNumber();
            for (i = 0; i < numSteps3; i++)
                inputs3[i] =
                    (delta3 * i + interval3.Interval.LowerBound).ToNumber();

            var literal1 = new Literal(0);
            var literal2 = new Literal(0);
            var literal3 = new Literal(0);
            env2.SetVariable(interval1.Variable, literal1);
            env2.SetVariable(interval2.Variable, literal2);
            env2.SetVariable(interval3.Variable, literal3);

            if (store != null)
                store.SetMinArraySize(numSteps1, numSteps2, numSteps3);

            for (i = 0; i < numSteps1; i++)
            {
                literal1.Value = inputs1[i];
                int j;
                for (j = 0; j < numSteps2; j++)
                {
                    literal2.Value = inputs2[j];
                    int k;
                    for (k = 0; k < numSteps3; k++)
                    {
                        literal3.Value = inputs3[k];
                        var v = Eval(expr2, env2);
                        if (store != null)
                            store.Store(i, j, k, v);
                        if (aggrs != null)
                            foreach (var aggr in aggrs)
                                aggr?.Operate(v, env2);
                    }
                }
            }
        }

        public void EvalMathPaint(Expression expr, SolusEnvironment env,
            int width, int height, StoreOp2<Number> store)
        {
            //previous values?
            SolusParser parser = new SolusParser();
            env.SetVariable("width", new Literal(width));
            env.SetVariable("width", new Literal(width));
            env.SetVariable("theta", parser.GetExpression("atan2(y,x)"));
            env.SetVariable("radius",
                parser.GetExpression("sqrt(x^2+y^2)"));
            env.SetVariable("i", new VariableAccess("x"));
            env.SetVariable("j", new VariableAccess("y"));

            var interval1 = new VarInterval("x",
                Interval.Integer(0, width - 1));
            var interval2 = new VarInterval("y",
                Interval.Integer(0, height - 1));
            EvalInterval(expr, env,
                interval1, width,
                interval2, height,
                store);
        }


        public void EvalMathPaint3D(Expression expr,
            SolusEnvironment env, int width, int height, int numFrames,
            StoreOp3<Number> store)
        {
            //previous values?
            SolusParser parser = new SolusParser();
            env.SetVariable("width", new Literal(width));
            env.SetVariable("height", new Literal(height));
            env.SetVariable("theta", parser.GetExpression("atan2(y,x)"));
            env.SetVariable("radius",
                parser.GetExpression("sqrt(x^2+y^2)"));
            env.SetVariable("i", new VariableAccess("x"));
            env.SetVariable("j", new VariableAccess("y"));
            env.SetVariable("numframes", new Literal(numFrames));
            env.SetVariable("k", new VariableAccess("z"));
            env.SetVariable("t", new VariableAccess("z"));

            var interval1 = new VarInterval("x",
                Interval.Integer(0, width - 1));
            var interval2 = new VarInterval("y",
                Interval.Integer(0, height - 1));
            var interval3 = new VarInterval("z",
                Interval.Integer(0, numFrames - 1));
            EvalInterval(expr, env,
                interval1, width,
                interval2, height,
                interval3, numFrames,
                store);
        }

        public static string[] GatherVariables(Expression expr)
        {
            var names = new HashSet<string>();

            expr.AcceptVisitor(varVisitor: (x) => names.Add(x.VariableName));

            return names.ToArray();
        }
    }
}
