
/*****************************************************************************
 *                                                                           *
 *  ArcsineFunction.cs                                                       *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Arcsine function.                             *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class ArcsineFunction : SingleArgumentFunction
	{
        public static readonly ArcsineFunction Value = new ArcsineFunction();

		protected ArcsineFunction()
		{
			this.Name = "Arcsine";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
		{
            return new Literal((float)Math.Asin(args[0].Eval(env).Value));
		}

        public override string DisplayName
        {
            get
            {
                return "asin";
            }
        }

        public override string DocString
        {
            get
            {
                return "The arcsine function\n  asin(x)\n\nReturns the arcsine of x. That is, if sin(y) = x, then asin(x) = y.";
            }
        }
    }
}
