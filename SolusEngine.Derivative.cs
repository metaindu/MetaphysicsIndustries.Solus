
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
        DerivativeTransformer _derivativeTransformer = new DerivativeTransformer();

        public Expression GetDerivative(Expression expr, Variable var)
        {
            Expression derivative;

            derivative = _derivativeTransformer.Transform(expr, new VariableTransformArgs(var));
                //InternalGetDerivative(expr, var);

            CleanUpTransformer cleanup = new CleanUpTransformer();
            derivative = cleanup.CleanUp(derivative);

            return derivative;
        }
    }
}
