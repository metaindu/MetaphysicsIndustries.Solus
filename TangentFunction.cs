
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
        public static readonly TangentFunction Value = new TangentFunction();

        protected TangentFunction()
		{
			this.Name = "Tangent";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
		{
            return new Literal((float)Math.Tan(args[0].Eval(env).Value));
		}

        public override string DisplayName
        {
            get
            {
                return "tan";
            }
        }
    }
}
