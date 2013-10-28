
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
        public static readonly ArccosecantFunction Value = new ArccosecantFunction();

		protected ArccosecantFunction()
		{
			this.Name = "Arccosecant";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
		{
            return new Literal((float)Math.Asin(1/args[0].Eval(env).Value));
        }

        public override string DisplayName
        {
            get
            {
                return "acsc";
            }
        }

        public override string DocString
        {
            get
            {
                return "The arccosecant function\n  acsc(x)\n\nReturns the arccosecant of x. That is, if csc(y) = x, then acsc(x) = y.";
            }
        }
    }
}
