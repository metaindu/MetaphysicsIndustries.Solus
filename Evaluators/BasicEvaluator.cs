
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Macros;
using MetaphysicsIndustries.Solus.Transformers;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Evaluators
{
    public partial class BasicEvaluator : IEvaluator
    {
        public IMathObject Eval(Expression expr, SolusEnvironment env)
        {
            var ec = new ExpressionChecker();
            ec.Check(expr, env);

            // We can't rely on callers to have applied all variables. We
            // have to do it here, even if it turns out to be a no-op in some
            // cases.
            var avt = new ApplyVariablesTransform();
            expr = avt.Transform(expr, env);
            ec.Check(expr, env);

            switch (expr)
            {
                case ColorExpression ce:
                    return Eval(ce, env);
                case ComponentAccess ca:
                    return Eval(ca, env);
                case DerivativeOfVariable dov:
                    return Eval(dov, env);
                case FunctionCall fc:
                    return Eval(fc, env);
                case IntervalExpression interval:
                    return Eval(interval, env);
                case Literal literal:
                    return Eval(literal, env);
                case MatrixExpression me:
                    return Eval(me, env);
                case RandomExpression re:
                    return Eval(re, env);
                case VariableAccess va:
                    return Eval(va, env);
                case VectorExpression ve:
                    return Eval(ve, env);
                default:
                    if (expr.ProvidesCustomEval)
                        return expr.CustomEval(env);
                    throw new ArgumentException(
                        $"Unknown expression type: {expr.GetType().Name}",
                        nameof(expr));
            }
        }

        public IMathObject Call(Function f, IMathObject[] args,
            SolusEnvironment env)
        {
            switch (f)
            {
                case AbsoluteValueFunction ff:
                    return CallFunction(ff, args, env);
                case AdditionOperation ff:
                    return CallFunction(ff, args, env);
                case ArccosecantFunction ff:
                    return CallFunction(ff, args, env);
                case ArccosineFunction ff:
                    return CallFunction(ff, args, env);
                case ArccotangentFunction ff:
                    return CallFunction(ff, args, env);
                case ArcsecantFunction ff:
                    return CallFunction(ff, args, env);
                case ArcsineFunction ff:
                    return CallFunction(ff, args, env);
                case Arctangent2Function ff:
                    return CallFunction(ff, args, env);
                case ArctangentFunction ff:
                    return CallFunction(ff, args, env);
                case BitwiseAndOperation ff:
                    return CallFunction(ff, args, env);
                case BitwiseOrOperation ff:
                    return CallFunction(ff, args, env);
                case CeilingFunction ff:
                    return CallFunction(ff, args, env);
                case CosecantFunction ff:
                    return CallFunction(ff, args, env);
                case CosineFunction ff:
                    return CallFunction(ff, args, env);
                case CotangentFunction ff:
                    return CallFunction(ff, args, env);
                case DeriveOperator ff:
                    return CallFunction(ff, args, env);
                case DistFunction ff:
                    return CallFunction(ff, args, env);
                case DistSqFunction ff:
                    return CallFunction(ff, args, env);
                case DivisionOperation ff:
                    return CallFunction(ff, args, env);
                case EqualComparisonOperation ff:
                    return CallFunction(ff, args, env);
                case ExponentOperation ff:
                    return CallFunction(ff, args, env);
                case FactorialFunction ff:
                    return CallFunction(ff, args, env);
                case FloorFunction ff:
                    return CallFunction(ff, args, env);
                case GreaterThanComparisonOperation ff:
                    return CallFunction(ff, args, env);
                case GreaterThanOrEqualComparisonOperation ff:
                    return CallFunction(ff, args, env);
                case IfOperator ff:
                    return CallFunction(ff, args, env);
                case LessThanComparisonOperation ff:
                    return CallFunction(ff, args, env);
                case LessThanOrEqualComparisonOperation ff:
                    return CallFunction(ff, args, env);
                case LoadImageFunction ff:
                    return CallFunction(ff, args, env);
                case Log10Function ff:
                    return CallFunction(ff, args, env);
                case Log2Function ff:
                    return CallFunction(ff, args, env);
                case LogarithmFunction ff:
                    return CallFunction(ff, args, env);
                case LogicalAndOperation ff:
                    return CallFunction(ff, args, env);
                case LogicalOrOperation ff:
                    return CallFunction(ff, args, env);
                case MaximumFiniteFunction ff:
                    return CallFunction(ff, args, env);
                case MaximumFunction ff:
                    return CallFunction(ff, args, env);
                case MinimumFiniteFunction ff:
                    return CallFunction(ff, args, env);
                case MinimumFunction ff:
                    return CallFunction(ff, args, env);
                case ModularDivision ff:
                    return CallFunction(ff, args, env);
                case MultiplicationOperation ff:
                    return CallFunction(ff, args, env);
                case NaturalLogarithmFunction ff:
                    return CallFunction(ff, args, env);
                case NegationOperation ff:
                    return CallFunction(ff, args, env);
                case NotEqualComparisonOperation ff:
                    return CallFunction(ff, args, env);
                case SecantFunction ff:
                    return CallFunction(ff, args, env);
                case SineFunction ff:
                    return CallFunction(ff, args, env);
                case SizeFunction ff:
                    return CallFunction(ff, args, env);
                case TangentFunction ff:
                    return CallFunction(ff, args, env);
                case UnitStepFunction ff:
                    return CallFunction(ff, args, env);
                case UserDefinedFunction ff:
                    return CallFunction(ff, args, env);
                default:
                    if (f.ProvidesCustomCall)
                        return f.CustomCall(args, env);
                    throw new ArgumentException(
                        $"Unknown function: {f.GetType().Name}",
                        nameof(f));
            }
        }

        public Expression Call(Macro m, Expression[] args,
            SolusEnvironment env)
        {
            if (args.Length != m.NumArguments)
                throw new OperandException(
                    "Incorrect number of arguments.");
            switch (m)
            {
                case RandMacro mm:
                    return CallMacro(mm, args, env);
                case SqrtMacro mm:
                    return CallMacro(mm, args, env);
                case SubstMacro mm:
                    return CallMacro(mm, args, env);
                default:
                    throw new ArgumentException(
                        $"Unknown macro: {m.GetType().Name}",
                        nameof(m));
            }
        }

        public Expression Simplify(Expression expr, SolusEnvironment env)
        {
            CleanUpTransformer cleanup = new CleanUpTransformer();
            var simplified = expr.Simplify(env);
            return cleanup.CleanUp(simplified);
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
                        aggr?.Operate(v, env2, this);
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
                                aggr?.Operate(v, env2, this);
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
    }
}
