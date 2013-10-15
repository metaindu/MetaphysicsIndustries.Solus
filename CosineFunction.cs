
/*****************************************************************************
 *                                                                           *
 *  CosineFunction.cs                                                        *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Cosine function.                              *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class CosineFunction : SingleArgumentFunction
	{
        public static readonly CosineFunction Value = new CosineFunction();

        protected CosineFunction()
		{
			this.Name = "Cosine";
		}


        protected override Literal InternalCall(Environment env, Literal[] args)
		{
            return new Literal((float)Math.Cos(args[0].Eval(env).Value));
		}

        public override string DisplayName
        {
            get
            {
                return "cos";
            }
        }
    }
}
