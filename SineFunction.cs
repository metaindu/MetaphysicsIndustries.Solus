
/*****************************************************************************
 *                                                                           *
 *  SineFunction.cs                                                          *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Sine function.                                *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class SineFunction : SingleArgumentFunction
	{
		public SineFunction()
		{
			this.Name = "Sine";
		}


        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal(Math.Sin(args[0].Eval(varTable).Value));
		}

        public override string DisplayName
        {
            get
            {
                return "sin";
            }
        }
    }
}
