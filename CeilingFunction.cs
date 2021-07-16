
/*****************************************************************************
 *                                                                           *
 *  CeilingFunction.cs                                                       *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright (c) 2006-2021 Metaphysics Industries, Inc.                     *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Ceiling function.                             *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;


namespace MetaphysicsIndustries.Solus
{
    public class CeilingFunction : SingleArgumentFunction
	{
        public static readonly CeilingFunction Value = new CeilingFunction();

        protected CeilingFunction()
		{
			this.Name = "Ceiling";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            return new Literal((float)Math.Ceiling(args[0].Eval(env).Value));
        }

        public override string DisplayName
        {
            get
            {
                return "ceil";
            }
        }

        public override string DocString
        {
            get
            {
                return "The ceiling function\n  ceil(x)\n\nReturns the lowest integer that is greater than or equal to x.\n";
            }
        }
    }
}
