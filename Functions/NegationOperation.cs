
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
using System.Reflection.Emit;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class NegationOperation : UnaryOperation
    {
        public static readonly NegationOperation Value = new NegationOperation();

        protected NegationOperation()
        {
            Name = "-";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Negation; }
        }

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            return new Literal(-args[0].Value);
        }

        public override string ToString(List<Expression> arguments)
        {
            Expression arg = arguments[0];

            if (arg == null)
            {
                return DisplayName + "(" + Expression.ToString(arg) + ")";
            }
            
            FunctionCall call = arg.As<FunctionCall>();
            Function func = call != null ? call.Function : null;
            Operation oper = func != null ? func.As<Operation>() : null;

            if (oper != null && oper.Precedence < Precedence)
            {
                return DisplayName + "(" + arg.ToString() + ")";
            }
            else
            {
                return DisplayName + arg.ToString();
            }
        }

        public override float IdentityValue
        {
            get { return 0; }
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            List<Instruction> instructions = new List<Instruction>();
            instructions.AddRange(arguments[0].ConvertToInstructions(varmap));
            instructions.Add(new Instruction {
                ArgType=Instruction.ArgumentType.None,
                OpCode=OpCodes.Neg
            });
            return instructions;
        }
    }
}
