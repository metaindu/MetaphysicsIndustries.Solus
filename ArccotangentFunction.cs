
/*****************************************************************************
 *                                                                           *
 *  ArccotangentFunction.cs                                                  *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Arccotangent function.                        *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus
{
    public class ArccotangentFunction : SingleArgumentFunction
	{
        public static readonly ArccotangentFunction Value = new ArccotangentFunction();

		protected ArccotangentFunction()
		{
			this.Name = "Arccotangent";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
		{
            return new Literal((float)Math.Atan2(1, args[0].Eval(env).Value));
        }

        public override string DisplayName
        {
            get
            {
                return "acot";
            }
        }

        public override string DocString
        {
            get
            {
                return "The arccotangent function\n  acot(x)\n\nReturns the arccotangent of x. That is, if cot(y) = x, then acot(x) = y.";
            }
        }
    }
}
