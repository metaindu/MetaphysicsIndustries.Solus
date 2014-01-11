
/*****************************************************************************
 *                                                                           *
 *  Expression.cs                                                            *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The basic unit of calculation and parse trees.                           *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
	public abstract class Expression : IDisposable, ICloneable
	{
        public virtual void Dispose()
        {
        }

        public abstract Literal Eval(SolusEnvironment env);

        public abstract Expression Clone();
        public static Expression Clone(Expression expr)
        {
            //used by Array.ConvertAll
            return expr.Clone();
        }

        //public delegate T Transformer<T>(Expression expr, VariableTable env);
        //public abstract T Transform<T>(VariableTable env, Transformer<T> transformer);
        //public static T Transform<T>(Expression expr, VariableTable env, Transformer<T> transformer)
        //{
        //    expr.Transform<T>(env, transformer);
        //}

        #region ICloneable Members

        object ICloneable.Clone()
        {
            return Clone();
        }

        #endregion

        //public virtual Expression CleanUp()
        //{
        //    return this;
        //}

        //public abstract Expression PreliminaryEval(VariableTable env);
        //public abstract GetDerivative(Variable

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

        //public Expression PreliminaryEval(VariableTable env)
        //{
        //    Expression evalExpr = InternalPreliminaryEval(env);
        //    Expression cleanExpr = evalExpr.CleanUp();
        //    return cleanExpr;
        //}

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

        public virtual IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap)
        {
            throw new NotImplementedException();
        }
    }
}
