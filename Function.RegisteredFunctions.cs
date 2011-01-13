
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
            RegisterFunctionType(new ArccosecantFunction());
            RegisterFunctionType(new ArccosineFunction());
            RegisterFunctionType(new ArccotangentFunction());
            RegisterFunctionType(new ArcsecantFunction());
            RegisterFunctionType(new ArcsineFunction());
            RegisterFunctionType(new ArctangentFunction());
            RegisterFunctionType(new CeilingFunction());
            RegisterFunctionType(new CosecantFunction());
            RegisterFunctionType(new CosineFunction());
            RegisterFunctionType(new CotangentFunction());
            RegisterFunctionType(new FloorFunction());
            RegisterFunctionType(new SecantFunction());
            RegisterFunctionType(new SineFunction());
            RegisterFunctionType(new TangentFunction());
            RegisterFunctionType(new NaturalLogarithmFunction());
            RegisterFunctionType(new UnitStepFunction());
            RegisterFunctionType(new Log10Function());
            RegisterFunctionType(new Log2Function());
            RegisterFunctionType(new LogarithmFunction());
            RegisterFunctionType(new AbsoluteValueFunction());
            RegisterFunctionType(new Arctangent2Function());
            RegisterFunctionType(new IfFunction());
            RegisterFunctionType(new DistFunction());
            RegisterFunctionType(new DistSqFunction());
        }

    }
}