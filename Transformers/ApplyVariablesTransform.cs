
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
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Transformers
{
    public class ApplyVariablesTransform
    {
        public Expression Transform(Expression expr,
            SolusEnvironment env)
        {
            switch (expr)
            {
                case Literal literal:
                    return Transform(literal, env);
                case FunctionCall fc:
                    return Transform(fc, env);
                case VariableAccess va:
                    return Transform(va, env);
                case MatrixExpression matrix:
                    return Transform(matrix, env);
                case VectorExpression vector:
                    return Transform(vector, env);
                case ComponentAccess ca:
                    return Transform(ca, env);
                case IntervalExpression interval:
                    return Transform(interval, env);
            }

            return expr;
        }

        private Expression Transform(Literal literal, SolusEnvironment env)
        {
            return literal;
        }

        private Expression Transform(FunctionCall func, SolusEnvironment env)
        {
            var f = Transform(func.Function, env);
            var transformedArgs = new List<Expression>();
            var allSame = f == func.Function;
            foreach (var argument in func.Arguments)
            {
                var expr = Transform(argument, env);
                allSame &= expr == argument;
                transformedArgs.Add(expr);
            }

            if (allSame) return func;
            return new FunctionCall(f, transformedArgs);
        }

        private Expression Transform(VariableAccess va, SolusEnvironment env)
        {
            if (env.ContainsVariable(va.VariableName))
            {
                // TODO: check for cycles
                var target = env.GetVariable(va.VariableName);
                if (target.IsIsExpression(env))
                    return Transform((Expression)target, env);
                return new Literal(target);
            }

            return va;
        }

        private Expression Transform(MatrixExpression matrix,
            SolusEnvironment env)
        {
            int r, c;
            var allSame = true;
            var transformedArgs =
                new Expression[matrix.RowCount, matrix.ColumnCount];
            for (r = 0; r < matrix.RowCount; r++)
            for (c = 0; c < matrix.ColumnCount; c++)
            {
                var arg = matrix[r, c];
                var transformedArg = Transform(arg, env);
                allSame &= transformedArg == arg;
                transformedArgs[r, c] = transformedArg;
            }

            if (allSame) return matrix;
            return new MatrixExpression(transformedArgs);
        }

        private Expression Transform(VectorExpression vector,
            SolusEnvironment env)
        {
            int i;
            var allSame = true;
            var transformedArgs = new Expression[vector.Length];
            for (i = 0; i < vector.Length; i++)
            {
                var arg = vector[i];
                var transformedArg = Transform(arg, env);
                allSame &= transformedArg == arg;
                transformedArgs[i] = transformedArg;
            }

            if (allSame) return vector;
            return new VectorExpression(vector.Length, transformedArgs);
        }

        private Expression Transform(ComponentAccess ca, SolusEnvironment env)
        {
            var expr = Transform(ca.Expr, env);
            int i;
            var allSame = expr == ca.Expr;
            var transformedIndexes = new Expression[ca.Indexes.Count];
            for (i = 0; i < ca.Indexes.Count; i++)
            {
                var index = ca.Indexes[i];
                var transformedIndex = Transform(index, env);
                allSame &= transformedIndex == index;
                transformedIndexes[i] = transformedIndex;
            }

            if (allSame) return ca;
            return new ComponentAccess(expr, transformedIndexes);
        }

        private Expression Transform(IntervalExpression interval,
            SolusEnvironment env)
        {
            var lower = Transform(interval.LowerBound, env);
            var upper = Transform(interval.UpperBound, env);
            if (lower == interval.LowerBound && upper == interval.UpperBound)
                return interval;
            return new IntervalExpression(
                lower, interval.OpenLowerBound,
                upper, interval.OpenUpperBound);
        }
    }
}
