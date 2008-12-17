using System;
using System.Collections.Generic;
using System.Text;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class CleanUpTransformer : ExpressionTransformer
    {
        public static readonly CleanUpTransformer DefaultInstance = new CleanUpTransformer();

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
            Expression[] args = fc.Arguments.ToArray();
            List<Expression> cleanArgs = new List<Expression>(args.Length);
            foreach (Expression arg in args)
            {
                cleanArgs.Add(CleanUp(arg));
            }
            args = cleanArgs.ToArray();

            return CleanUpFunctionArgs(fc.Function, args);
        }


        public virtual Expression CleanUpFunctionArgs(Function function, Expression[] args)
        {
            if (function is Operation)
            {
                return InternalCleanUpOperation((Operation)function, args);
            }

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
                return function.Call(null, args);
            }

            return new FunctionCall(function, args);
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

            throw new NotImplementedException();
        }

        //public Expression CleanUpDelayAssignExpression(DelayAssignExpression expr)
        //{
        //    return new DelayAssignExpression(expr.Variable, CleanUp(expr.Expression));
        //} 


        public Expression[] CleanUpPartAssociativeOperationOperation(Operation function, Expression[] args)
        {
            List<FunctionCall> assocOps = new List<FunctionCall>();
            (new FunctionCall(function, args)).GatherMatchingFunctionCalls(assocOps);

            Set<FunctionCall> assocOpsSet = new Set<FunctionCall>(assocOps);
            Literal combinedLiteral = null;

            combinedLiteral = new Literal(function.IdentityValue);

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
                            combinedLiteral = function.Call(null, combinedLiteral, arg);
                        }
                        else
                        {
                            nonLiterals.Add(arg);
                        }
                    }
                }
            }

            args = InternalCleanUpPartAssociativeOperation(function, args, combinedLiteral, nonLiterals);

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
                    if (arg is Literal && (arg as Literal).Value == function.CollapseValue)
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
                    if (!(arg is Literal) || (arg as Literal).Value != function.CullValue)
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
                return function.Call(null, args);
            }

            return new FunctionCall(function, args);
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
                return function.Call(null, args);
            }

            if (function.IsAssociative)
            {
                args = CleanUpPartAssociativeOperationOperation(function, args);
            }

            if (function.IsCommutative &&
                args[0] is Literal &&
                (args[0] as Literal).Value == function.IdentityValue)
            {
                return args[1];
            }

            if (args[1] is Literal &&
                (args[1] as Literal).Value == function.IdentityValue)
            {
                return args[0];
            }


            return new FunctionCall(function, args);
        }
    }
}
