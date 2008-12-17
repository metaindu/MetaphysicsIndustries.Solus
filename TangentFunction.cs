
/*****************************************************************************
 *                                                                           *
 *  TangentFunction.cs                                                       *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Tangent function.                             *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class TangentFunction : SingleArgumentFunction
	{
		public TangentFunction()
		{
			this.Name = "Tangent";
		}


        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
		{
			return new Literal(Math.Tan(args[0].Eval(varTable).Value));
		}

    }
}
