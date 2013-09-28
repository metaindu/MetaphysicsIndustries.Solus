
/*****************************************************************************
 *                                                                           *
 *  Function.Registered.cs                                                   *
 *  9 December 2010                                                          *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2010 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  A mathematical function that can be evaluated with a set of              *
 *    parameters. This serves as a base class that is inherited by other,    *
 *    specialized classes, each representing a different mathematical        *
 *    function (e.g. "SineFunction : Function"). This base class performs    *
 *    all necessary type checking on given arguments based on information    *
 *    specified by the derived class.                                        *
 *                                                                           *
 *  This file registers functions.                                           *
 *                                                                           *
 *****************************************************************************/


using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public abstract partial class Function
    {
        private static void RegisterFunctions()
        {
            RegisterFunctionType(ArccosecantFunction.Value);
            RegisterFunctionType(ArccosineFunction.Value);
            RegisterFunctionType(ArccotangentFunction.Value);
            RegisterFunctionType(ArcsecantFunction.Value);
            RegisterFunctionType(ArcsineFunction.Value);
            RegisterFunctionType(ArctangentFunction.Value);
            RegisterFunctionType(CeilingFunction.Value);
            RegisterFunctionType(CosecantFunction.Value);
            RegisterFunctionType(CosineFunction.Value);
            RegisterFunctionType(CotangentFunction.Value);
            RegisterFunctionType(FloorFunction.Value);
            RegisterFunctionType(SecantFunction.Value);
            RegisterFunctionType(SineFunction.Value);
            RegisterFunctionType(TangentFunction.Value);
            RegisterFunctionType(NaturalLogarithmFunction.Value);
            RegisterFunctionType(UnitStepFunction.Value);
            RegisterFunctionType(Log10Function.Value);
            RegisterFunctionType(Log2Function.Value);
            RegisterFunctionType(LogarithmFunction.Value);
            RegisterFunctionType(AbsoluteValueFunction.Value);
            RegisterFunctionType(Arctangent2Function.Value);
            RegisterFunctionType(IfFunction.Value);
            RegisterFunctionType(DistFunction.Value);
            RegisterFunctionType(DistSqFunction.Value);
        }

    }
}