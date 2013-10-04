
/*****************************************************************************
 *                                                                           *
 *  FloorFunction.cs                                                         *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Floor function.                               *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public class FloorFunction : SingleArgumentFunction
	{
        public static readonly FloorFunction Value = new FloorFunction();

        protected FloorFunction()
		{
			this.Name = "Floor";
		}


        protected override Literal InternalCall(Dictionary<string, Expression> varTable, Literal[] args)
		{
            return new Literal((float)Math.Floor(args[0].Eval(varTable).Value));
		}

    }
}
