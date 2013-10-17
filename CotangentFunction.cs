
/*****************************************************************************
 *                                                                           *
 *  CotangentFunction.cs                                                     *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Cotangent function.                           *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class CotangentFunction : SingleArgumentFunction
	{
        public static readonly CotangentFunction Value = new CotangentFunction();

        protected CotangentFunction()
		{
			this.Name = "Cotangent";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
		{
            return new Literal((float)(1 / Math.Tan(args[0].Eval(env).Value)));
        }

    }
}
