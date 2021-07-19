
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class AdditionOperation : AssociativeCommutativeOperation
    {
        public static readonly AdditionOperation Value = new AdditionOperation();

        protected AdditionOperation()
        {
            Name = "+";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Addition; }
        }

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            float sum = 0;
            foreach (Literal arg in args)
            {
                sum += arg.Value;
            }
            return new Literal(sum);
        }

        public override float IdentityValue
        {
            get
            {
                return 0;
            }
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            var instructions = new List<Instruction>();

            bool first = true;
            foreach (var arg in arguments)
            {
                if (!first &&
                    arg is FunctionCall &&
                    (arg as FunctionCall).Function == NegationOperation.Value)
                {
                    instructions.AddRange((arg as FunctionCall).Arguments[0].ConvertToInstructions(varmap));
                    instructions.Add(Instruction.Sub());
                }
                else
                {
                    instructions.AddRange(arg.ConvertToInstructions(varmap));
                    if (!first)
                    {
                        instructions.Add(Instruction.Add());
                    }
                    first = false;
                }
            }

            return instructions;
        }
    }
}
