
/*****************************************************************************
 *                                                                           *
 *  ArctangentFunction.cs                                                    *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Arctangent function.                          *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class ArctangentFunction : SingleArgumentFunction
	{
        public static readonly ArctangentFunction Value = new ArctangentFunction();

		protected ArctangentFunction()
		{
			this.Name = "Arctangent";
		}


        protected override Literal InternalCall(Environment env, Literal[] args)
		{
            return new Literal((float)Math.Atan(args[0].Eval(env).Value));
		}

        public override string DisplayName
        {
            get
            {
                return "atan";
            }
        }
    }
}
