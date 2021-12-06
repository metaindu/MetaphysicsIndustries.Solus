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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Macros;
using MetaphysicsIndustries.Solus.Transformers;

namespace MetaphysicsIndustries.Solus
{
    public partial class Evaluator
    {
        public Expression CallMacro(DeriveMacro mm, Expression[] args,
            SolusEnvironment env)
        {
            var derive = new DerivativeTransformer();
            var expr = args[0];
            var v = ((VariableAccess)args[1]).VariableName;
            return derive.Transform(expr, new VariableTransformArgs(v));
        }

        public Expression CallMacro(IfMacro mm, Expression[] args,
            SolusEnvironment env)
        {
            var eval = new Evaluator();
            var value = eval.Eval(args[0], env).ToNumber().Value;
            if (value == 0 ||
                float.IsNaN(value) ||
                float.IsInfinity(value))
                return new Literal(eval.Eval(args[2], env));
            return new Literal(eval.Eval(args[1], env));
        }

        public Expression CallMacro(RandMacro mm, Expression[] args,
            SolusEnvironment env)
        {
            return new RandomExpression();
        }

        public Expression CallMacro(SqrtMacro mm, Expression[] args,
            SolusEnvironment env)
        {
            return new FunctionCall(ExponentOperation.Value,
                args[0], new Literal(0.5f));
        }

        public Expression CallMacro(SubstMacro mm, Expression[] args,
            SolusEnvironment env)
        {
            var subst = new SubstTransformer();
            var cleanup = new CleanUpTransformer();
            var var = ((VariableAccess)args[1]).VariableName;
            return cleanup.CleanUp(
                subst.Subst(args[0], var, args[2]));
        }
    }
}
