
/*****************************************************************************
 *                                                                           *
 *  SineFunction.cs                                                          *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright (c) 2006-2021 Metaphysics Industries, Inc.                     *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  The class for the built-in Sine function.                                *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;

using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public class SineFunction : SingleArgumentFunction
	{
        public static readonly SineFunction Value = new SineFunction();

        protected SineFunction()
		{
			this.Name = "Sine";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            return new Literal((float)Math.Sin(args[0].Eval(env).Value));
		}

        public override string DisplayName
        {
            get
            {
                return "sin";
            }
        }

        public override string DocString
        {
            get
            {
                return "The sine function\n  sin(x)\n\nReturns the sine of x.";
            }
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            List<Instruction> instructions = new List<Instruction>();
            instructions.AddRange(arguments[0].ConvertToInstructions(varmap));
            instructions.Add(Instruction.Call(typeof(System.Math).GetMethod("Sin", new Type[] { typeof(float) })));
            return instructions;
        }
    }
}
