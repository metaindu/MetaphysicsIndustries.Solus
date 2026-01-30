
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
                    $"Call target is not a function: \"{literal.Value}\"");
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
            if (function is IOperation)
            {
                return InternalCleanUpOperation((IOperation)function, args);
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

        private Expression InternalCleanUpMultiplication(Function f, Expression[] args)
        {
            if (args.Length == 1)
                return args[0];
            var literals = new List<Literal>();
            var nonLiterals = new List<Expression>();
            foreach (var arg in args)
            {
                if (arg is Literal literal)
                {
                    if (literal.Value.ToFloat() == 0)
                    {
                        literals.Clear();
                        nonLiterals.Clear();
                        literals.Add(literal);
                        break;
                    }
                    if (literal.Value.ToFloat() == 1)
                        continue;
                    literals.Add(literal);
                }
                else
                    nonLiterals.Add(arg);
            }

            float value = 1;
            foreach (var literal in literals)
                value *= literal.Value.ToFloat();
            if (literals.Count > 0 && nonLiterals.Count > 0)
            {
                nonLiterals.Insert(0, new Literal(value));
                return new FunctionCall(f, nonLiterals);
            }
            if (literals.Count > 0)
                return new Literal(value);
            if (nonLiterals.Count > 0)
                return new FunctionCall(f, nonLiterals);
            throw new InvalidOperationException();
        }

        private Expression InternalCleanUpOperation(IOperation function, Expression[] args)
        {
            if (function is IAssociativeCommutativeOperation)
            {
                return InternalCleanUpAssociativeCommutativeOperation((IAssociativeCommutativeOperation)function, args);
            }
            if (function is IBinaryOperation)
            {
                return InternalCleanUpBinaryOperation((IBinaryOperation)function, args);
            }

            // throw new NotImplementedException();
            return null;
        }

        //public Expression CleanUpDelayAssignExpression(DelayAssignExpression expr)
        //{
        //    return new DelayAssignExpression(expr.Variable, CleanUp(expr.Expression));
        //} 


        public Expression[] CleanUpPartAssociativeOperationOperation(IOperation function, Expression[] args)
        {
            List<FunctionCall> assocOps = new List<FunctionCall>();
            (new FunctionCall((Function)function, args)).GatherMatchingFunctionCalls(assocOps);

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
                            var newCall = new FunctionCall((Function)function,
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

        public Expression[] InternalCleanUpPartAssociativeOperation(IOperation function, Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals)
        {
            if (function is IAssociativeCommutativeOperation)
            {
                return InternalCleanUpPartAssociativeOperationAssociativeCommutativeOperation((IAssociativeCommutativeOperation)function, args, combinedLiteral, nonLiterals);
            }
            if (function is IBinaryOperation)
            {
                return InternalCleanUpPartAssociativeOperationBinaryOperation((IBinaryOperation)function, args, combinedLiteral, nonLiterals);
            }

            throw new NotImplementedException();
        }

        public Expression[] InternalCleanUpPartAssociativeOperationBinaryOperation(IBinaryOperation function, Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals)
        {
            FunctionCall ret = new FunctionCall((Function)function, combinedLiteral);
            FunctionCall temp = ret;
            FunctionCall last = null;

            foreach (Expression expr in nonLiterals)
            {
                //Expression cleanExpr = CleanUp(expr);
                last = temp;
                temp = new FunctionCall((Function)function, expr);//cleanExpr);
                last.Arguments.Add(temp);
            }

            last.Arguments[1] = temp.Arguments[0];

            return ret.Arguments.ToArray();
        }


        public Expression InternalCleanUpAssociativeCommutativeOperation(IAssociativeCommutativeOperation function, Expression[] args)
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

            var newExpr = new FunctionCall((Function)function, args);

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

        public Expression[] InternalCleanUpPartAssociativeOperationAssociativeCommutativeOperation(IAssociativeCommutativeOperation function, Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals)
        {
            List<Expression> newArgs = new List<Expression>(nonLiterals.Count + 1);
            newArgs.Add(combinedLiteral);
            newArgs.AddRange(nonLiterals);
            return newArgs.ToArray();
        }

        public Expression InternalCleanUpBinaryOperation(IBinaryOperation function, Expression[] args)
        {
            if (args[0] is Literal &&
                args[1] is Literal)
            {
                var newExpr = new FunctionCall((Function)function, args);
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

            return new FunctionCall((Function)function, args);
        }
    }
}
