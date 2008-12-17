
/*****************************************************************************
 *                                                                           *
 *  Operation.cs                                                             *
 *  17 November 2006                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  A specialized function which represents simple arithmetical operations   *
 *    such as addition of two numbers.                                       *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
	public abstract class Operation : Function
	{
        public abstract OperationPrecedence Precedence
        {
            get;
        }

        //public override Expression CleanUp(Expression[] args)
        //{
        //    return InternalCleanUp(args);
        //}

        public virtual bool IsCommutative   // a @ b == b @ a
        {
            get { return false; }
        }
        public virtual bool IsAssociative   // (a @ b) @ c == a @ (b @ c)
        {
            get { return false; }
        }

        public virtual double IdentityValue
        {
            get { return 1; }
        }

        //protected abstract Expression InternalCleanUp(Expression[] args);

        //protected Expression[] CleanUpPartAssociativeOperation(Expression[] args)
        //{
        //    List<FunctionCall> assocOps = new List<FunctionCall>();
        //    (new FunctionCall(this, args)).GatherMatchingFunctionCalls(assocOps);

        //    Set<FunctionCall> assocOpsSet = new Set<FunctionCall>(assocOps);
        //    Literal combinedLiteral = null;

        //    combinedLiteral = new Literal(IdentityValue);

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
        //                    combinedLiteral = Call(null, combinedLiteral, arg);
        //                }
        //                else
        //                {
        //                    nonLiterals.Add(arg);
        //                }
        //            }
        //        }
        //    }

        //    args = InternalCleanUpPartAssociativeOperation(args, combinedLiteral, nonLiterals);

        //    return args;
        //}

        //protected abstract Expression[] InternalCleanUpPartAssociativeOperation(Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals);
    }
}
