
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
using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public class CosineFunction : SingleArgumentFunction
	{
        public static readonly CosineFunction Value = new CosineFunction();

        protected CosineFunction()
		{
			this.Name = "Cosine";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
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

        public override string DocString
        {
            get
            {
                return "The cosine function\n  cos(x)\n\nReturns the cosine of x.";
            }
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            List<Instruction> instructions = new List<Instruction>();
            instructions.AddRange(arguments[0].ConvertToInstructions(varmap));
            instructions.Add(Instruction.Call(typeof(System.Math).GetMethod("Cosine", new Type[] { typeof(float) })));
            return instructions;
        }
    }
}
