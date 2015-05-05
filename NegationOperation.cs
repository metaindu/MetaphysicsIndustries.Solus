using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection.Emit;

namespace MetaphysicsIndustries.Solus
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
