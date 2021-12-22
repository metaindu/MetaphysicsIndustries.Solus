
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Transformers;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Evaluators
{
    public class CompilingEvaluator : IEvaluator
    {
        private readonly ILCompiler _compiler = new ILCompiler();
        public IMathObject Eval(Expression expr, SolusEnvironment env)
        {
            var compiled = _compiler.Compile(expr);
            var cenv =
                _compiler.CompileEnvironment(compiled, env, this);
            return compiled.Evaluate(cenv).ToNumber();
        }

        public Expression Simplify(Expression expr, SolusEnvironment env)
        {
            var cleanup = new CleanUpTransformer();
            return cleanup.CleanUp(expr.Simplify(env));
        }

        public IMathObject Call(Function f, IMathObject[] args,
            SolusEnvironment env)
        {
            var literals = new Expression[args.Length];
            int i;
            for (i = 0; i < args.Length; i++)
                literals[i] = new Literal(args[i]);
            var expr = new FunctionCall(f, literals);
            return Eval(expr, env);
        }

        public void EvalInterval(
            Expression expr, SolusEnvironment env,
            VarInterval interval, int numSteps, StoreOp1 store,
            AggregateOp[] aggrs = null)
        {
            if (store != null)
                store.SetMinArraySize(numSteps);

            var delta = interval.Interval.CalcDelta(numSteps);

            var env2 = env.CreateChildEnvironment();
            env2.RemoveVariable(interval.Variable);
            var expr2 = Simplify(expr, env2);
            var compiled = _compiler.Compile(expr2);
            var cenv =
                _compiler.CompileEnvironment(compiled, env2, this);

            int i;
            for (i = 0; i < numSteps; i++)
            {
                var xx = delta * i + interval.Interval.LowerBound;
                // env2.SetVariable(interval.Variable, xx.ToNumber());
                cenv[interval.Variable] = xx;
                var v = compiled.Evaluate(cenv).ToNumber();
                if (store != null)
                    store.Store(i, v);
                if (aggrs != null)
                    foreach (var aggr in aggrs)
                        aggr?.Operate(v, env2, this);
            }
        }

        public void EvalInterval(
            Expression expr, SolusEnvironment env,
            VarInterval interval1, int numSteps1,
            VarInterval interval2, int numSteps2,
            StoreOp2 store, AggregateOp[] aggrs = null)
        {
            throw new System.NotImplementedException();
        }

        public void EvalInterval(
            Expression expr, SolusEnvironment env,
            VarInterval interval1, int numSteps1,
            VarInterval interval2, int numSteps2,
            VarInterval interval3, int numSteps3,
            StoreOp3 store, AggregateOp[] aggrs = null)
        {
            throw new System.NotImplementedException();
        }
    }
}
