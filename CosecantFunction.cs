
/*****************************************************************************
 *                                                                           *
 *  CosecantFunction.cs                                                      *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Cosecant function.                            *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;


namespace MetaphysicsIndustries.Solus
{
    public class CosecantFunction : SingleArgumentFunction
	{
        public static readonly CosecantFunction Value = new CosecantFunction();

        protected CosecantFunction()
		{
			this.Name = "Cosecant";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
		{
            return new Literal((float)(1 / Math.Sin(args[0].Eval(env).Value)));
        }

        public override string DisplayName
        {
            get
            {
                return "csc";
            }
        }

        public override string DocString
        {
            get
            {
                return "The cosecant function\n  csc(x)\n\nReturns the cosecant of x, which is equal to 1 / sin(x).";
            }
        }
    }
}
