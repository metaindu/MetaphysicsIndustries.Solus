
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

        public abstract Literal Eval(VariableTable varTable);

        public abstract Expression Clone();
        public static Expression Clone(Expression expr)
        {
            //used by Array.ConvertAll
            return expr.Clone();
        }

        //public delegate T Transformer<T>(Expression expr, VariableTable varTable);
        //public abstract T Transform<T>(VariableTable varTable, Transformer<T> transformer);
        //public static T Transform<T>(Expression expr, VariableTable varTable, Transformer<T> transformer)
        //{
        //    expr.Transform<T>(varTable, transformer);
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

        //public abstract Expression PreliminaryEval(VariableTable varTable);
        //public abstract GetDerivative(Variable

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

        public virtual Expression PreliminaryEval(VariableTable varTable)
        {
            return this;
        }

        //public Expression PreliminaryEval(VariableTable varTable)
        //{
        //    Expression evalExpr = InternalPreliminaryEval(varTable);
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
    }
}
