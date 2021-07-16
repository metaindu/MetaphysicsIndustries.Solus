
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

/*****************************************************************************
 *                                                                           *
 *  CosineFunction.cs                                                        *
 *                                                                           *
 *  The class for the built-in Cosine function.                              *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;

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
            instructions.Add(Instruction.Call(typeof(System.Math).GetMethod("Cos", new Type[] { typeof(float) })));
            return instructions;
        }
    }
}
