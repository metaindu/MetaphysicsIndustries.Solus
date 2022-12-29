
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

/*****************************************************************************
 *                                                                           *
 *  Expression.cs                                                            *
 *                                                                           *
 *  The basic unit of calculation and parse trees.                           *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Expressions
{
	public abstract class Expression : IMathObject
    {
        public virtual IMathObject CustomEval(SolusEnvironment env) => null;
        public virtual bool ProvidesCustomEval => false;

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

        public static string[] GatherVariables(Expression expr)
        {
            var names = new HashSet<string>();

            expr.AcceptVisitor(varVisitor: (x) => names.Add(x.VariableName));

            return names.ToArray();
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

        /// <summary>
        /// Create a reduced, simpler form of the expression, if possible.
        /// For example, "1 + 1" can be reduced to "2". This method should
        /// not throw exceptions (TODO: clarify when that's the case).
        /// </summary>
        /// <param name="env"></param>
        /// <returns>A simplified expression, or the expression the method
        /// was called on, if it can't be simplified further.</returns>
        public virtual Expression Simplify(SolusEnvironment env)
        {
            return this;
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

        public abstract ISet GetResultType(SolusEnvironment env);

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => null;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => true;
        public bool? IsSet(SolusEnvironment env) => false;

        public bool IsConcrete => true;
        public string DocString => "";
    }
}
