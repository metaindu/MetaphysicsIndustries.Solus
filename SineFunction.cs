
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
        public static readonly SineFunction Value = new SineFunction();

        protected SineFunction()
		{
			this.Name = "Sine";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            return new Literal((float)Math.Sin(args[0].Eval(env).Value));
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
