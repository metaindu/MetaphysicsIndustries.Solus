
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
        public Expression Subst(Expression exprToTransform, Variable variableToReplace, Expression exprToInsert)
        {
            if (exprToTransform is FunctionCall)
            {
                List<Expression> args = new List<Expression>(((FunctionCall)exprToTransform).Arguments);
                int i;
                for (i = 0; i < args.Count; i++)
                {
                    args[i] = Subst(args[i], variableToReplace, exprToInsert);
                }

                return new FunctionCall(((FunctionCall)exprToTransform).Function, args);
            }
            else if (exprToTransform is VariableAccess)
            {
                if (((VariableAccess)exprToTransform).Variable == variableToReplace)
                {
                    return exprToInsert;
                }
                else
                {
                    return exprToTransform;
                }
            }
            else if (exprToTransform is Literal)
            {
                return exprToTransform;
            }
            else
            {
                throw new InvalidOperationException("Unknown expression type or invalid target of substitution");
            }
        }
    }
}
