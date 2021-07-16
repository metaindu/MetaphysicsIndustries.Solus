
/*****************************************************************************
 *                                                                           *
 *  CotangentFunction.cs                                                     *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright (c) 2006-2021 Metaphysics Industries, Inc.                     *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Cotangent function.                           *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;


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

        public override string DisplayName
        {
            get
            {
                return "cot";
            }
        }

        public override string DocString
        {
            get
            {
                return "The cotangent function\n  cot(x)\n\nReturns the cotangent of x, which is equal to 1 / tan(x).";
            }
        }
    }
}
