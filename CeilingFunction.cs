
/*****************************************************************************
 *                                                                           *
 *  CeilingFunction.cs                                                       *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright � 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Ceiling function.                             *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class CeilingFunction : SingleArgumentFunction
	{
		public CeilingFunction()
		{
			this.Name = "Ceiling";
		}


        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal((float)Math.Ceiling(args[0].Eval(varTable).Value));
        }
    }
}
