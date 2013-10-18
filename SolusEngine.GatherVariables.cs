
/*****************************************************************************
 *                                                                           *
 *  SolusEngine.cs                                                           *
 *  17 November 2006                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright � 2006 Metaphysics Industries, Inc.                            *
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
using System.Drawing;

namespace MetaphysicsIndustries.Solus
{
    public partial class SolusEngine
    {
        public static Set<string> GatherVariables(Expression expr)
        {
            var names = new Set<string>();

            expr.AcceptVisitor(varVisitor: (x)=> names.Add(x.VariableName));

            return names;
        }
    }
}
