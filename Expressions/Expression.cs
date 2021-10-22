
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

/*****************************************************************************
 *                                                                           *
 *  Expression.cs                                                            *
 *                                                                           *
 *  The basic unit of calculation and parse trees.                           *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
{
	public abstract class Expression
	{
        public abstract IMathObject Eval(SolusEnvironment env);

        public class CompiledExpression
        {
            public Func<Dictionary<string, float>, float> Method;
            public string[] CompiledVars;
        }

        public abstract Expression Clone();

        public abstract void AcceptVisitor(IExpressionVisitor visitor);

        public void AcceptVisitor(
            Action<Literal> literalVisitor = null,
            Action<FunctionCall> funcVisitor = null,
            Action<VariableAccess> varVisitor = null,
            Action<DerivativeOfVariable> dvarVisitor = null)
        {
            if (literalVisitor == null) literalVisitor = DelegateExpressionVisitor.DoNothing<Literal>;
            if (funcVisitor == null) funcVisitor = DelegateExpressionVisitor.DoNothing<FunctionCall>;
            if (varVisitor == null) varVisitor = DelegateExpressionVisitor.DoNothing<VariableAccess>;
            if (dvarVisitor == null) dvarVisitor = DelegateExpressionVisitor.DoNothing<DerivativeOfVariable>;

            var visitor = new DelegateExpressionVisitor {
                LiteralVisitor = literalVisitor,
                FuncVisitor = funcVisitor,
                VarVisitor = varVisitor,
                DvarVisitor = dvarVisitor,
            };

            AcceptVisitor(visitor);
        }



        protected virtual void InternalApplyToExpressionTree(SolusAction action, bool applyToChildrenBeforeParent)
        {
        }
        public void ApplyToExpressionTree(SolusAction action)
        {
            ApplyToExpressionTree(action, true);
        }
        public void ApplyToExpressionTree(SolusAction action, bool applyToChildrenBeforeParent)
        {
            if (!applyToChildrenBeforeParent)
            {
                action(this);
            }

            InternalApplyToExpressionTree(action, applyToChildrenBeforeParent);

            if (applyToChildrenBeforeParent)
            {
                action(this);
            }
        }

        public virtual Expression PreliminaryEval(SolusEnvironment env)
        {
            return this;
        }

        public bool IsFunction(Function function)
        {
            return (this is FunctionCall && ((FunctionCall)this).Function == function);
        }

        public T As<T>()
            where T : Expression
        {
            return this as T;
        }

        public static string ToString(Expression expr)
        {
            if (expr == null)
            {
                return "[null]";
            }

            return expr.ToString();
        }

        public abstract IEnvMathObject Result { get; }
    }
}
