
/*****************************************************************************
 *                                                                           *
 *  ArccosecantFunction.cs                                                   *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Arccosecant function.                         *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class ArccosecantFunction : SingleArgumentFunction
	{
		public ArccosecantFunction()
		{
			this.Name = "Arccosecant";
		}


        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
		{
            return new Literal(Math.Asin(1/args[0].Eval(varTable).Value));
        }

    }
}
