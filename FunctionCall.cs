
/*****************************************************************************
 *                                                                           *
 *  FunctionCall.cs                                                          *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright � 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  A function call, providing arguments to the function.                    *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;

using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public class FunctionCall : Expression
    {
        public FunctionCall()
        {
            this.Init(null, null);
        }

        public FunctionCall(Function function, IEnumerable<Expression> args)
        {
            if (function == null) { throw new ArgumentNullException("function"); }
            if (args == null) { throw new ArgumentNullException("args"); }

            Init(function, args.ToArray());
        }

        public FunctionCall(Function function, params Expression[] args)
        {
            if (function == null) { throw new ArgumentNullException("function"); }

            Init(function, args);
        }

        public override void Dispose()
        {
            _arguments.Clear();
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

        public override Literal Eval(SolusEnvironment env)
        {
            return Call(env);
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

        public virtual Literal Call(SolusEnvironment env)
        {
            return Function.Call(env, Arguments.ToArray());
        }

        public virtual List<Expression> Arguments
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

        protected void Init(Function function, Expression[] args)
        {
            _function = function;

            if (args != null)
            {
                _arguments.AddRange(args);
            }
        }

        private Function _function;
        private List<Expression> _arguments = new List<Expression>();

        protected override void InternalApplyToExpressionTree(SolusAction action, bool applyToChildrenBeforeParent)
        {
            foreach (Expression expr in Arguments)
            {
                expr.ApplyToExpressionTree(action, applyToChildrenBeforeParent);
            }
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);

            foreach (Expression expr in Arguments)
            {
                expr.AcceptVisitor(visitor);
            }
        }

        public override Expression PreliminaryEval(SolusEnvironment env)
        {
            List<Expression> args = new List<Expression>(Arguments.Count);

            bool allLiterals = true;
            foreach (Expression arg in Arguments)
            {
                Expression arg2 = arg.PreliminaryEval(env);
                if (!(arg2 is Literal))
                {
                    allLiterals = false;
                }
                args.Add(arg2);
            }

            if (allLiterals)
            {
                return Function.Call(env, args.ToArray());
            }
            else
            {
                return new FunctionCall(Function, args.ToArray());
            }
        }

        public override string ToString()
        {
            if (Function != null)
            {
                return Function.ToString(Arguments);
            }
            else
            {
                Expression[] exprs = Arguments.ToArray();
                string[] strs = Array.ConvertAll<Expression, string>(exprs, Expression.ToString);
                return "[unknown function](" + string.Join(", ", strs) + ")";
            }
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap)
        {
            return Function.ConvertToInstructions(varmap, Arguments);

        }
    }
}
