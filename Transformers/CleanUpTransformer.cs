
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
using System.Linq;
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Transformers
{
    public class CleanUpTransformer : ExpressionTransformer
    {
        private readonly IEvaluator _evaluator = new BasicEvaluator();

        public Expression CleanUp(Expression expr)
        {
            if (expr is FunctionCall)
            {
                return CleanUpFunctionCall((FunctionCall)expr);
            }

            return expr;
        }


        public override bool CanTransform(Expression expr)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override Expression Transform(Expression expr)
        {
            return CleanUp(expr);
        }

        public Expression CleanUpFunctionCall(FunctionCall fc)
        {
            var expr = fc.Function;
            if (!(expr is Literal literal))
                return fc;
            if (!literal.Value.IsIsFunction(null))
                throw new OperandException(
                    "Call target is not a function");
            var f = (Function)literal.Value;

            Expression[] args = fc.Arguments.ToArray();
            List<Expression> cleanArgs = new List<Expression>(args.Length);
            foreach (Expression arg in args)
            {
                Expression cleanArg = CleanUp(arg);
                cleanArgs.Add(cleanArg);
            }
            args = cleanArgs.ToArray();

            return CleanUpFunctionArgs(f, args) ?? fc;
        }


        public virtual Expression CleanUpFunctionArgs(Function function, Expression[] args)
        {
            if (function is Operation)
            {
                return InternalCleanUpOperation((Operation)function, args);
            }

            var newExpr = new FunctionCall(function, args);

            bool call = args.All(arg => arg is Literal);
            if (call)
            {
                var result = _evaluator.Eval(newExpr, null);
                return new Literal(result.ToNumber().Value);
            }

            return newExpr;
        }

        private Expression InternalCleanUpOperation(Operation function, Expression[] args)
        {
            if (function is AssociativeCommutativeOperation)
            {
                return InternalCleanUpAssociativeCommutativeOperation((AssociativeCommutativeOperation)function, args);
            }
            if (function is BinaryOperation)
            {
                return InternalCleanUpBinaryOperation((BinaryOperation)function, args);
            }

            // throw new NotImplementedException();
            return null;
        }

        //public Expression CleanUpDelayAssignExpression(DelayAssignExpression expr)
        //{
        //    return new DelayAssignExpression(expr.Variable, CleanUp(expr.Expression));
        //} 


        public Expression[] CleanUpPartAssociativeOperationOperation(Operation function, Expression[] args)
        {
            List<FunctionCall> assocOps = new List<FunctionCall>();
            (new FunctionCall(function, args)).GatherMatchingFunctionCalls(assocOps);

            HashSet<FunctionCall> assocOpsSet = new HashSet<FunctionCall>(assocOps);
            var combinedValue = function.IdentityValue;
            List<Expression> nonLiterals = new List<Expression>(assocOps.Count);

            foreach (FunctionCall opToCombine in assocOps)
            {
                foreach (Expression arg in opToCombine.Arguments)
                {
                    if (!(arg is FunctionCall) ||
                        !(assocOpsSet.Contains(arg as FunctionCall)))
                    {
                        if (arg is Literal)
                        {
                            var newCall = new FunctionCall(function,
                                new Literal(combinedValue), arg);
                            var result = _evaluator.Eval(newCall, null);
                            combinedValue = result.ToNumber().Value;
                        }
                        else
                        {
                            nonLiterals.Add(arg);
                        }
                    }
                }
            }

            args = InternalCleanUpPartAssociativeOperation(function, args,
                new Literal(combinedValue), nonLiterals);

            return args;
        }

        public Expression[] InternalCleanUpPartAssociativeOperation(Operation function, Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals)
        {
            if (function is AssociativeCommutativeOperation)
            {
                return InternalCleanUpPartAssociativeOperationAssociativeCommutativeOperation((AssociativeCommutativeOperation)function, args, combinedLiteral, nonLiterals);
            }
            if (function is BinaryOperation)
            {
                return InternalCleanUpPartAssociativeOperationBinaryOperation((BinaryOperation)function, args, combinedLiteral, nonLiterals);
            }

            throw new NotImplementedException();
        }

        public Expression[] InternalCleanUpPartAssociativeOperationBinaryOperation(BinaryOperation function, Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals)
        {
            FunctionCall ret = new FunctionCall(function, combinedLiteral);
            FunctionCall temp = ret;
            FunctionCall last = null;

            foreach (Expression expr in nonLiterals)
            {
                //Expression cleanExpr = CleanUp(expr);
                last = temp;
                temp = new FunctionCall(function, expr);//cleanExpr);
                last.Arguments.Add(temp);
            }

            last.Arguments[1] = temp.Arguments[0];

            return ret.Arguments.ToArray();
        }


        public Expression InternalCleanUpAssociativeCommutativeOperation(AssociativeCommutativeOperation function, Expression[] args)
        {
            if (args.Length == 1)
            {
                return args[0];
            }

            args = CleanUpPartAssociativeOperationOperation(function, args);

            if (args.Length == 1)
            {
                return args[0];
            }

            if (function.Collapses)
            {
                foreach (Expression arg in args)
                {
                    if (arg is Literal literal &&
                        literal.Value.ToFloat() == function.CollapseValue)
                    {
                        return new Literal(function.CollapseValue);
                    }
                }
            }

            if (function.Culls)
            {
                List<Expression> args2 = new List<Expression>(args.Length);
                foreach (Expression arg in args)
                {
                    if (!(arg is Literal literal) ||
                        literal.Value.ToFloat() != function.CullValue)
                    {
                        args2.Add(arg);
                    }
                }

                if (args2.Count < args.Length)
                {
                    args = args2.ToArray();
                }
            }

            if (args.Length == 1)
            {
                return args[0];
            }

            var newExpr = new FunctionCall(function, args);

            bool call = true;
            foreach (Expression arg in args)
            {
                if (!(arg is Literal))
                {
                    call = false;
                    break;
                }
            }
            if (call)
            {
                var result = _evaluator.Eval(newExpr, null);
                return new Literal(result.ToNumber().Value);
            }

            return newExpr;
        }

        public Expression[] InternalCleanUpPartAssociativeOperationAssociativeCommutativeOperation(AssociativeCommutativeOperation function, Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals)
        {
            List<Expression> newArgs = new List<Expression>(nonLiterals.Count + 1);
            newArgs.Add(combinedLiteral);
            newArgs.AddRange(nonLiterals);
            return newArgs.ToArray();
        }

        public Expression InternalCleanUpBinaryOperation(BinaryOperation function, Expression[] args)
        {
            if (args[0] is Literal &&
                args[1] is Literal)
            {
                var newExpr = new FunctionCall(function, args);
                var result = _evaluator.Eval(newExpr, null);
                return new Literal(result.ToNumber().Value);
            }

            if (function.IsAssociative)
            {
                args = CleanUpPartAssociativeOperationOperation(function, args);
            }

            if (function.HasIdentityValue)
            {
                if (function.IsCommutative &&
                    args[0] is Literal literal0 &&
                    literal0.Value.ToFloat() == function.IdentityValue)
                {
                    return args[1];
                }

                if (args[1] is Literal literal1 &&
                    literal1.Value.ToFloat() == function.IdentityValue)
                {
                    return args[0];
                }
            }

            return new FunctionCall(function, args);
        }
    }
}
