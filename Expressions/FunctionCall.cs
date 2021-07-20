
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
 *  FunctionCall.cs                                                          *
 *                                                                           *
 *  A function call, providing arguments to the function.                    *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
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

        public override IMathObject Eval(SolusEnvironment env)
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

        public virtual IMathObject Call(SolusEnvironment env)
        {
            var evaledArgs =
                Arguments.Select(a => a.Eval(env)).ToArray();
            return Function.Call(env, evaledArgs);
        }

        public virtual List<Expression> Arguments
        {
            get
            {
                return _arguments;
            }
        }

        public Function Function
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
            var args = Arguments.Select(
                a => a.PreliminaryEval(env)).ToArray();
            var allLiterals = args.All(a => a is Literal);
            if (allLiterals)
            {
                var args2 = args.Select(
                    a => (IMathObject)((Literal) a).Value.ToNumber());
                var result = Function.Call(env, args2.ToArray());
                return new Literal(result.ToNumber().Value);
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