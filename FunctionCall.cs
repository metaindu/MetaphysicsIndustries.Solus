
/*****************************************************************************
 *                                                                           *
 *  FunctionCall.cs                                                          *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  A function call, providing arguments to the function.                    *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class FunctionCall : Expression
    {
        public FunctionCall()
        {
            this.Init(null, null);
        }

        public FunctionCall(Function f, ICollection<Expression> a)
        {
            if (f == null) { throw new ArgumentNullException("f"); }
            if (a == null) { throw new ArgumentNullException("a"); }

            Expression[] aa = new Expression[a.Count];
            a.CopyTo(aa, 0);
            Init(f, aa);
        }

        public FunctionCall(Function f, params Expression[] a)
        {
            if (f == null) { throw new ArgumentNullException("f"); }

            Init(f, a);
        }

        public override void Dispose()
        {
            _arguments.Clear();
            _arguments.Dispose();
            _arguments = null;
            _function = null;
        }

        public override Expression Clone()
        {
            FunctionCall ret = new FunctionCall(Function,
                                    Array.ConvertAll<Expression, Expression>(
                                        Arguments.ToArray(), Expression.Clone));

            return ret;
        }

        public override Literal Eval(VariableTable varTable)
        {
            return Call(varTable);
        }

        //public override Expression CleanUp()
        //{
        //    Expression[] args = Arguments.ToArray();
        //    List<Expression> cleanArgs = new List<Expression>(args.Length);
        //    foreach (Expression arg in args)
        //    {
        //        cleanArgs.Add(arg.CleanUp());
        //    }
        //    args = cleanArgs.ToArray();

        //    return Function.CleanUp(args);
        //}

        public void GatherMatchingFunctionCalls(ICollection<FunctionCall> matchingFunctionCalls)
        {
            bool first = true;

            foreach (Expression arg in Arguments)
            {
                if (arg is FunctionCall &&
                    (arg as FunctionCall).Function == Function)
                {
                    FunctionCall argCall = arg as FunctionCall;
                    argCall.GatherMatchingFunctionCalls(matchingFunctionCalls);
                }

                if (first)
                {
                    matchingFunctionCalls.Add(this);
                    first = false;
                }
            }
        }

        public virtual Literal Call(VariableTable varTable)
        {
            return Function.Call(varTable, Arguments.ToArray());
        }

        public virtual ExpressionCollection Arguments
        {
            get
            {
                return _arguments;
            }
        }

        public Solus.Function Function
        {
            get
            {
                return _function;
            }
            set
            {
                if (_function != value)
                {
                    _function = value;
                    this.OnFunctionChanged(new EventArgs());
                }
            }
        }

        public event EventHandler FunctionChanged;

        protected virtual void OnFunctionChanged(EventArgs e)
        {
            if (FunctionChanged != null)
            {
                this.FunctionChanged(this, e);
            }
        }

        protected void Init(Function f, Expression[] a)
        {
            _function = f;

            if (a != null)
            {
                _arguments.AddRange(a);
            }
        }

        private Function _function;
        private ExpressionCollection _arguments = new ExpressionCollection();

        protected override void InternalApplyToExpressionTree(SolusAction action, bool applyToChildrenBeforeParent)
        {
            foreach (Expression expr in Arguments)
            {
                expr.ApplyToExpressionTree(action, applyToChildrenBeforeParent);
            }
        }

        public override Expression PreliminaryEval(VariableTable varTable)
        {
            List<Expression> args = new List<Expression>(Arguments.Count);

            bool allLiterals = true;
            foreach (Expression arg in Arguments)
            {
                Expression arg2 = arg.PreliminaryEval(varTable);
                if (!(arg2 is Literal))
                {
                    allLiterals = false;
                }
                args.Add(arg2);
            }

            if (allLiterals)
            {
                return Function.Call(varTable, args.ToArray());
            }
            else
            {
                return new FunctionCall(Function, args.ToArray());
            }
        }
    }
}
