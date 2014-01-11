using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MetaphysicsIndustries.Solus
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
