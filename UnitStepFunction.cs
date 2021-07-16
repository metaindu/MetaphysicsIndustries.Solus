
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

using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class UnitStepFunction : SingleArgumentFunction
    {
        public static readonly UnitStepFunction Value = new UnitStepFunction();

        protected UnitStepFunction()
        {
            Name = "UnitStep";
        }

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            if (args[0].Value >= 0)
            {
                return new Literal(1);
            }
            else
            {
                return new Literal(0);
            }
        }

        public override string DisplayName
        {
            get
            {
                return "unitstep";
            }
        }

        public override string DocString
        {
            get
            {
                return "unit step function";
            }
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            var instructions = new List<Instruction>();

            instructions.AddRange(arguments[0].ConvertToInstructions(varmap));

            instructions.Add(Instruction.LoadConstant(0.0f));
            instructions.Add(Instruction.CompareLessThan());
            instructions.Add(Instruction.LoadConstant(1));
            instructions.Add(Instruction.CompareLessThan());
            instructions.Add(Instruction.ConvertR4());

            return instructions;
        }
    }
}
