
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Macros;

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class FunctionCall : Expression
    {
        public FunctionCall()
        {
            this.Init((Expression)null, null);
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

        public FunctionCall(Expression function, IEnumerable<Expression> args)
        {
            if (function == null)
                throw new ArgumentNullException(nameof(function));
            if (args == null) throw new ArgumentNullException(nameof(args));

            Init(function, args.ToArray());
        }

        public FunctionCall(Expression function, params Expression[] args)
        {
            Init(function, args);
        }

        public override Expression Clone()
        {
            FunctionCall ret = new FunctionCall(Function,
                                    Array.ConvertAll<Expression, Expression>(
                                        Arguments.ToArray(), CloneExpr));

            return ret;
        }

        private static Expression CloneExpr(Expression expr) => expr.Clone();

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

        public virtual List<Expression> Arguments
        {
            // TODO: make this immutable
            get
            {
                return _arguments;
            }
        }

        public Expression Function
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
                    this.OnFunctionChanged(EventArgs.Empty);
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

        private void Init(Function function, Expression[] args) =>
            Init(new Literal(function), args);

        private void Init(Expression function, Expression[] args)
        {
            _function = function;
            if (args != null)
                _arguments.AddRange(args);
        }

        private Expression _function;
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

        public override Expression Simplify(SolusEnvironment env)
        {
            var function = Function.Simplify(env);
            var args = Arguments.Select(
                a => a.Simplify(env)).ToArray();
            var allLiterals = args.All(a => a is Literal);
            if (allLiterals &&
                function is Literal literal &&
                literal.Value.IsIsFunction(env))
            {
                var f = (Function)literal.Value;
                var args2 = args.Select(
                    a => ((Literal)a).Value);
                var result = f.Call(env, args2.ToArray());
                return new Literal(result);
            }

            return new FunctionCall(function, args.ToArray());
        }

        public override string ToString()
        {
            if (Function == null) return "[call to null]";

            var expr = Function;
            if (expr is Literal literal &&
                literal.Value.IsIsFunction(null))
            {
                var f = (Function)literal.Value;
                return f.ToString(Arguments);
            }

            var name = "[unknown function]";
            if (expr is VariableAccess va)
                name = va.VariableName;

            var exprs = Arguments.ToArray();
            var strs = Array.ConvertAll(exprs, Expression.ToString);
            return $"{name}(" + string.Join(", ", strs) + ")";
        }

        private IMathObject[] _argumentResultCache;

        public override IMathObject Result
        {
            get
            {
                if (!(Function is Expression expr) ||
                    !(expr is Literal literal) ||
                    !(literal.Value is Function f))
                    return null;

                if (_argumentResultCache == null ||
                    _argumentResultCache.Length < Arguments.Count)
                    _argumentResultCache = new IMathObject[Arguments.Count];
                int i;
                for (i = 0; i < Arguments.Count; i++)
                    _argumentResultCache[i] = Arguments[i].Result;
                return f.GetResult(_argumentResultCache);
            }
        }
    }
}
