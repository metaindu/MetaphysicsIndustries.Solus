
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
using System.Drawing;

namespace MetaphysicsIndustries.Solus
{
    public partial class SolusEngine
    {
        protected class VariableGatherer
        {
            public void GatherVariable(Expression expr)
            {
                if (expr is VariableAccess)
                {
                    VariableAccess va = (VariableAccess)expr;

                    if (va.Variable != null)
                    {
                        _variables.Add(va.Variable);
                    }
                }
            }

            private Set<Variable> _variables = new Set<Variable>();
            public Set<Variable> Variables
            {
                get { return _variables; }
            }
        }

        public Set<Variable> GatherVariables(Expression expr)
        {
            VariableGatherer g = new VariableGatherer();
            expr.ApplyToExpressionTree(g.GatherVariable);
            return g.Variables;
        }
    }
}
