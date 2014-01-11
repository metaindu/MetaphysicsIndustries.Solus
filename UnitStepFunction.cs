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

            instructions.Add(Instruction.LoadConstant(0));
            instructions.Add(Instruction.CompareLessThan());
            instructions.Add(Instruction.LoadConstant(1));
            instructions.Add(Instruction.CompareLessThan());

            return instructions;
        }
    }
}
