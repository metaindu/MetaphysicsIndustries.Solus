
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
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Transformers;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Evaluators
{
    public class CompilingEvaluator : IEvaluator
    {
        private readonly ILCompiler _compiler = new ILCompiler();

        public IMathObject Eval(Expression expr, SolusEnvironment env)
        {
            var ec = new ExpressionChecker();
            ec.IsWellFormed(expr);

            // We can't rely on callers to have applied all variables. We
            // have to do it here, even if it turns out to be a no-op in some
            // cases.
            var avt = new ApplyVariablesTransform();
            expr = avt.Transform(expr, env);
            ec.IsWellDefined(expr, env);

            var varNames = Expression.GatherVariables(expr);
            var variables =
                new VariableIdentityMap();
            foreach (var varName in varNames)
            {
                if (!env.ContainsVariable(varName))
                    throw new NameException(
                        $"Variable \"{varName}\" is not bound");
                var value = env.GetVariable(varName);
                if (value.IsIsExpression(env))
                    value = ((Expression)value).GetResultType(env);
                variables[varName] = new VariableIdentity
                {
                    Name = varName,
                    // IlType =
                    Value = value,
                    Source = VariableSource.Param
                };
            }

            var compiled = _compiler.Compile(expr, env, variables);
            return compiled.Evaluate(env);
        }

        public Expression Simplify(Expression expr, SolusEnvironment env)
        {
            var cleanup = new CleanUpTransformer();
            return cleanup.CleanUp(expr.Simplify(env));
        }

        public void EvalInterval(
            Expression expr, SolusEnvironment env,
            VarInterval interval, int numSteps, StoreOp1 store,
            AggregateOp[] aggrs = null)
        {
            if (store != null)
                store.SetMinArraySize(numSteps);

            var varNames = Expression.GatherVariables(expr);
            var variables =
                new VariableIdentityMap();
            foreach (var varName in varNames)
            {
                if (!env.ContainsVariable(varName) &&
                    varName != interval.Variable)
                    throw new NameException(
                        $"Variable \"{varName}\" is not bound");
                if (varName == interval.Variable)
                    variables[varName] = new VariableIdentity
                    {
                        Name = varName,
                        Source = VariableSource.Param
                    };
                else
                {
                    var value = env.GetVariable(varName);
                    if (value.IsIsExpression(env))
                        value = ((Expression)value).GetResultType(env);
                    var mathType = value.GetMathType();
                    variables[varName] = new VariableIdentity
                    {
                        Name = varName,
                        // IlType =
                        Value = value,
                        Source = VariableSource.Param
                    };
                }
            }

            var delta = interval.Interval.CalcDelta(numSteps);

            var env2 = env.CreateChildEnvironment();
            env2.RemoveVariable(interval.Variable);
            env2.SetVariableType(interval.Variable, Reals.Value);
            var expr2 = Simplify(expr, env2);
            var compiled = _compiler.Compile(expr2, env2, variables);
            object[] varValuesInOrder = null;
            compiled.CompileEnvironment(env2, ref varValuesInOrder);

            var intervalVarIndex =
                Array.IndexOf(compiled.VariableNames, interval.Variable);
            int i;
            for (i = 0; i < numSteps; i++)
            {
                var xx = delta * i + interval.Interval.LowerBound;
                varValuesInOrder[intervalVarIndex] = xx;
                var v = compiled.Evaluate(varValuesInOrder).ToNumber();
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
            if (store != null)
                store.SetMinArraySize(numSteps1, numSteps2);

            var varNames = Expression.GatherVariables(expr);
            var variables = new VariableIdentityMap();
            foreach (var varName in varNames)
            {
                if (!env.ContainsVariable(varName) &&
                    varName != interval1.Variable &&
                    varName != interval2.Variable)
                    throw new NameException(
                        $"Variable \"{varName}\" is not bound");
                if (varName == interval1.Variable ||
                    varName == interval2.Variable)
                    variables[varName] = new VariableIdentity
                    {
                        Name = varName,
                        Source = VariableSource.Param
                    };
                else
                {
                    var value = env.GetVariable(varName);
                    if (value.IsIsExpression(env))
                        value = ((Expression)value).GetResultType(env);
                    variables[varName] = new VariableIdentity
                    {
                        Name = varName,
                        // IlType =
                        Value = value,
                        Source = VariableSource.Param
                    };
                }
            }

            var delta1 = interval1.Interval.CalcDelta(numSteps1);
            var delta2 = interval2.Interval.CalcDelta(numSteps2);

            var env2 = env.CreateChildEnvironment();
            env2.RemoveVariable(interval1.Variable);
            env2.SetVariableType(interval1.Variable, Reals.Value);
            env2.RemoveVariable(interval2.Variable);
            env2.SetVariableType(interval2.Variable, Reals.Value);
            var expr2 = Simplify(expr, env2);
            var compiled = _compiler.Compile(expr2, env2, variables);
            object[] varValuesInOrder = null;
            compiled.CompileEnvironment(env2, ref varValuesInOrder);

            var intervalVarIndex1 =
                Array.IndexOf(compiled.VariableNames, interval1.Variable);
            var intervalVarIndex2 =
                Array.IndexOf(compiled.VariableNames, interval2.Variable);

            // TODO: don't always do nested loops. there might be faster ways.
            int i, j;
            for (i = 0; i < numSteps1; i++)
            {
                var xx = delta1 * i + interval1.Interval.LowerBound;
                varValuesInOrder[intervalVarIndex1] = xx;
                for (j = 0; j < numSteps2; j++)
                {
                    var yy = delta2 * j + interval2.Interval.LowerBound;
                    varValuesInOrder[intervalVarIndex2] = yy;
                    var v = compiled.Evaluate(varValuesInOrder).ToNumber();
                    if (store != null)
                        store.Store(i, j, v);
                    if (aggrs != null)
                        foreach (var aggr in aggrs)
                            aggr?.Operate(v, env2, this);
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
            if (store != null)
                store.SetMinArraySize(numSteps1, numSteps2, numSteps3);

            var varNames = Expression.GatherVariables(expr);
            var variables = new VariableIdentityMap();
            foreach (var varName in varNames)
            {
                if (!env.ContainsVariable(varName) &&
                    varName != interval1.Variable &&
                    varName != interval2.Variable &&
                    varName != interval3.Variable)
                    throw new NameException(
                        $"Variable \"{varName}\" is not bound");
                if (varName == interval1.Variable ||
                    varName == interval2.Variable ||
                    varName == interval3.Variable)
                    variables[varName] = new VariableIdentity
                    {
                        Name = varName,
                        Source = VariableSource.Param
                    };
                else
                {
                    var value = env.GetVariable(varName);
                    if (value.IsIsExpression(env))
                        value = ((Expression)value).GetResultType(env);
                    variables[varName] = new VariableIdentity
                    {
                        Name = varName,
                        //MathType =
                        // IlType =
                        Value = value,
                        Source = VariableSource.Param
                    };
                }
            }

            var delta1 = interval1.Interval.CalcDelta(numSteps1);
            var delta2 = interval2.Interval.CalcDelta(numSteps2);
            var delta3 = interval3.Interval.CalcDelta(numSteps3);

            var env2 = env.CreateChildEnvironment();
            env2.RemoveVariable(interval1.Variable);
            env2.SetVariableType(interval1.Variable, Reals.Value);
            env2.RemoveVariable(interval2.Variable);
            env2.SetVariableType(interval2.Variable, Reals.Value);
            env2.RemoveVariable(interval3.Variable);
            env2.SetVariableType(interval3.Variable, Reals.Value);
            var expr2 = Simplify(expr, env2);
            var compiled = _compiler.Compile(expr2, env2, variables);
            object[] varValuesInOrder = null;
            compiled.CompileEnvironment(env2, ref varValuesInOrder);

            var intervalVarIndex1 =
                Array.IndexOf(compiled.VariableNames, interval1.Variable);
            var intervalVarIndex2 =
                Array.IndexOf(compiled.VariableNames, interval2.Variable);
            var intervalVarIndex3 =
                Array.IndexOf(compiled.VariableNames, interval3.Variable);
            // TODO: don't always do nested loops. there might be faster ways.
            int i, j, k;
            for (i = 0; i < numSteps1; i++)
            {
                var xx = delta1 * i + interval1.Interval.LowerBound;
                varValuesInOrder[intervalVarIndex1] = xx;
                for (j = 0; j < numSteps2; j++)
                {
                    var yy = delta2 * j + interval2.Interval.LowerBound;
                    varValuesInOrder[intervalVarIndex2] = yy;
                    for (k = 0; k < numSteps3; k++)
                    {
                        var zz = delta3 * k + interval3.Interval.LowerBound;
                        varValuesInOrder[intervalVarIndex3] = zz;
                        var v = compiled.Evaluate(varValuesInOrder).ToNumber();
                        if (store != null)
                            store.Store(i, j, k, v);
                        if (aggrs != null)
                            foreach (var aggr in aggrs)
                                aggr?.Operate(v, env2, this);
                    }
                }
            }
        }
    }
}
