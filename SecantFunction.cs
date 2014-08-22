
/*****************************************************************************
 *                                                                           *
 *  SecantFunction.cs                                                        *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Secant function.                              *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;


namespace MetaphysicsIndustries.Solus
{
    public class SecantFunction : SingleArgumentFunction
	{
        public static readonly SecantFunction Value = new SecantFunction();

        protected SecantFunction()
		{
			this.Name = "Secant";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
		{
            return new Literal((float)(1 / Math.Cos(args[0].Eval(env).Value)));
        }

        public override string DisplayName
        {
            get
            {
                return "sec";
            }
        }
    }
}
