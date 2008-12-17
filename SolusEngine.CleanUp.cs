
/*****************************************************************************
 *                                                                           *
 *  SolusEngine.cs                                                           *
 *  17 November 2006                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The central core of processing in Solus. Does some rudimentary parsing   *
 *    and evaluation and stuff.                                              *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;
using System.Diagnostics;

namespace MetaphysicsIndustries.Solus
{
    public partial class SolusEngine
    {
        public Expression CleanUp(Expression expr)
        {
            return CleanUpTransformer.DefaultInstance.CleanUp(expr);
        }

            //if (expr == null)
            //{
            //    throw new ArgumentNullException("expr");
            //}

            //if (expr is Literal)
            //{
            //    return CleanUpLiteral(expr as Literal);
            //}
            //else if (expr is VariableAccess)
            //{
            //    return CleanUpVariableAccess(expr as VariableAccess);
            //}
            //else if (expr is FunctionCall)
            //{
            //    return CleanUpFunctionCall((expr as FunctionCall).Function, (expr as FunctionCall).Arguments.ToArray());
            //}
            //else if (expr is PlotExpression || expr is Plot3dExpression)
            //{
            //    return expr.Clone();
            //}
            //else if (expr is ColorExpression)
            //{
            //    return expr.Clone();
            //}
            //else if (expr is RandomExpression)
            //{
            //    return expr.Clone();
            //}
            //else if (expr is AssignExpression)
            //{
            //    return expr.Clone();
            //}
            //else if (expr is DelayAssignExpression)
            //{
            //    return expr.Clone();
            //}
            //else if (expr is Matrix)    //!@#$
            //{
            //    return expr;
            //}
            //else if (expr is Vector)    //!@#$
            //{
            //    return expr;
            //}
            //else if (expr is MathPaintExpression)
            //{
            //    return expr.Clone();
            //}
            //else
            //{
            //    throw new InvalidOperationException("Unknown expression: " + expr.ToString());
            //}
        //}

        //private Expression CleanUpLiteral(Literal literal)
        //{
        //    return literal;
        //}

        //private Expression CleanUpFunctionCall(Function function, Expression[] args)
        //{
        //    List<Expression> cleanArgs = new List<Expression>(args.Length);
        //    foreach (Expression arg in args)
        //    {
        //        cleanArgs.Add(CleanUp(arg));
        //    }
        //    args = cleanArgs.ToArray();

        //    if (function is Operation)
        //    {
        //        return CleanUpOperation(function as Operation, args);
        //    }
        //    else
        //    {
        //        bool call = true;
        //        foreach (Expression arg in args)
        //        {
        //            if (!(arg is Literal))
        //            {
        //                call = false;
        //                break;
        //            }
        //        }

        //        if (call)
        //        {
        //            return function.Call(null, args);
        //        }

        //        return new FunctionCall(function, args);
        //    }
        //}

        //private Expression CleanUpOperation(Operation operation, Expression[] args)
        //{
        //    if (operation is BinaryOperation)
        //    {
        //        return CleanUpBinaryOperation(operation as BinaryOperation, args);
        //    }
        //    else if (operation is AssociativeCommutativeOperation)
        //    {
        //        return CleanUpAssociativeCommutativeOperation(operation as AssociativeCommutativeOperation, args);
        //    }
        //    //else if (operation == AssociativeCommutativeOperation.Addition)
        //    //{
        //    //    return CleanUpAdditionOperation(operation as AdditionOperation, args);
        //    //}
        //    else
        //    {
        //        throw new InvalidOperationException("Unknown operation: " + operation.ToString());
        //    }
        //}

        //private Expression CleanUpAssociativeCommutativeOperation(AssociativeCommutativeOperation operation, Expression[] args)
        //{
        //    if (args.Length == 1)
        //    {
        //        return args[0];
        //    }

        //    args = CleanUpPartAssociativeOperation(operation, args);

        //    if (args.Length == 1)
        //    {
        //        return args[0];
        //    }

        //    if (operation.Collapses)
        //    {
        //        foreach (Expression arg in args)
        //        {
        //            if (arg is Literal && (arg as Literal).Value == operation.CollapseValue)
        //            {
        //                return new Literal(operation.CollapseValue);
        //            }
        //        }
        //    }

        //    if (operation.Culls)
        //    {
        //        List<Expression> args2 = new List<Expression>(args.Length);
        //        foreach (Expression arg in args)
        //        {
        //            if (!(arg is Literal) || (arg as Literal).Value != operation.CullValue)
        //            {
        //                args2.Add(arg);
        //            }
        //        }

        //        if (args2.Count < args.Length)
        //        {
        //            args = args2.ToArray();
        //        }
        //    }

        //    if (args.Length == 1)
        //    {
        //        return args[0];
        //    }

        //    bool call = true;
        //    foreach (Expression arg in args)
        //    {
        //        if (!(arg is Literal))
        //        {
        //            call = false;
        //            break;
        //        }
        //    }

        //    if (call)
        //    {
        //        return operation.Call(null, args);
        //    }

        //    return new FunctionCall(operation, args);
        //}

        //private Expression CleanUpBinaryOperation(BinaryOperation binaryOperation, Expression[] args)
        //{
        //    if (args[0] is Literal &&
        //        args[1] is Literal)
        //    {
        //        return binaryOperation.Call(null, args);
        //    }

        //    if (binaryOperation.IsAssociative)
        //    {
        //        args = CleanUpPartAssociativeOperation(binaryOperation, args);
        //    }

        //    if (binaryOperation.IsCommutative &&
        //        args[0] is Literal &&
        //        (args[0] as Literal).Value == binaryOperation.IdentityValue)
        //    {
        //        return args[1];
        //    }

        //    if (args[1] is Literal &&
        //        (args[1] as Literal).Value == binaryOperation.IdentityValue)
        //    {
        //        return args[0];
        //    }


        //    return new FunctionCall(binaryOperation, args);
        //}

        //private Expression[] CleanUpPartAssociativeOperation(Operation operation, Expression[] args)
        //{
        //    List<FunctionCall> assocOps = new List<FunctionCall>();
        //    GatherMatchingFunctionCalls(new FunctionCall(operation, args), assocOps);

        //    Set<FunctionCall> assocOpsSet = new Set<FunctionCall>(assocOps);
        //    Literal combinedLiteral = null;

        //    combinedLiteral = new Literal(operation.IdentityValue);

        //    List<Expression> nonLiterals = new List<Expression>(assocOps.Count);

        //    foreach (FunctionCall opToCombine in assocOps)
        //    {
        //        foreach (Expression arg in opToCombine.Arguments)
        //        {
        //            if (!(arg is FunctionCall) ||
        //                !(assocOpsSet.Contains(arg as FunctionCall)))
        //            {
        //                if (arg is Literal)
        //                {
        //                    combinedLiteral = operation.Call(null, combinedLiteral, arg);
        //                }
        //                else
        //                {
        //                    nonLiterals.Add(arg);
        //                }
        //            }
        //        }
        //    }

        //    if (operation is AssociativeCommutativeOperation)
        //    {
        //        List<Expression> newArgs = new List<Expression>(nonLiterals.Count + 1);
        //        newArgs.Add(combinedLiteral);
        //        newArgs.AddRange(nonLiterals);
        //        args = newArgs.ToArray();
        //    }
        //    else if (operation is BinaryOperation)
        //    {
        //        FunctionCall ret = new FunctionCall(operation, combinedLiteral);
        //        FunctionCall temp = ret;
        //        FunctionCall last = null;

        //        foreach (Expression expr in nonLiterals)
        //        {
        //            //Expression cleanExpr = CleanUp(expr);
        //            last = temp;
        //            temp = new FunctionCall(operation, expr);//cleanExpr);
        //            last.Arguments.Add(temp);
        //        }

        //        last.Arguments[1] = temp.Arguments[0];

        //        args = ret.Arguments.ToArray();
        //    }
        //    else
        //    {
        //        throw new InvalidOperationException("Unknown operation: " + operation.ToString());
        //    }

        //    return args;
        //}

        //private void GatherMatchingFunctionCalls(FunctionCall functionCall, ICollection<FunctionCall> matchingFunctionCalls)
        //{
        //    bool first = true;

        //    foreach (Expression arg in functionCall.Arguments)
        //    {
        //        if (arg is FunctionCall &&
        //            (arg as FunctionCall).Function == functionCall.Function)
        //        {
        //            FunctionCall argCall = arg as FunctionCall;
        //            GatherMatchingFunctionCalls(argCall, matchingFunctionCalls);
        //        }

        //        if (first)
        //        {
        //            matchingFunctionCalls.Add(functionCall);
        //            first = false;
        //        }
        //    }
        //}

        //private Expression CleanUpAdditionOperation(AdditionOperation additionOperation, Expression[] args)
        //{
        //    return new FunctionCall(additionOperation, args);
        //}

        //private Expression CleanUpMultiplicationOperation(MultiplicationOperation multiplicationOperation, Expression[] args)
        //{
        //    return new FunctionCall(multiplicationOperation, args);
        //}

        //private Expression CleanUpDivisionOperation(DivisionOperation divisionOperation, Expression[] args)
        //{
        //    return new FunctionCall(divisionOperation, args);
        //}

        //private Expression CleanUpExponentOperation(ExponentOperation exponentOperation, Expression[] args)
        //{
        //    return new FunctionCall(exponentOperation, args);
        //}

        //private Expression CleanUpVariableAccess(VariableAccess variableAccess)
        //{
        //    return variableAccess;
        //}
    }
}
