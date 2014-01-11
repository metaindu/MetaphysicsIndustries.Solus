using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public class ExponentOperation : BinaryOperation
    {
        public static readonly ExponentOperation Value = new ExponentOperation();

        protected ExponentOperation()
        {
            Name = "^";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Exponent; }
        }

        //protected override Literal InternalCall(VariableTable env, Literal[] args)
        //{
        //    return new Literal(Math.Pow(args[0].Value, args[1].Value));
        //}

        protected override float InternalBinaryCall(float x, float y)
        {
            return (float)Math.Pow(x, y);
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            List<Instruction> instructions = new List<Instruction>();

            instructions.AddRange(arguments[0].ConvertToInstructions(varmap));

            if (arguments[1] is Literal)
            {
                var value = (arguments[1] as Literal).Value;

                if (value == 1)
                {
                    return instructions;
                }
                if (value == value.Round() &&
                    value > 1 &&
                    value < 16)
                {
                    int i;
                    for (i = 1; i < value; i++)
                    {
                        instructions.Add(Instruction.Dup());
                    }
                    for (i = 1; i < value; i++)
                    {
                        instructions.Add(Instruction.Mul());
                    }
                    return instructions;
                }
                if (value == 1 / 2.0f)
                {
                    instructions.Add(
                        Instruction.Call(
                            typeof(System.Math).GetMethod("Sqrt", new Type[] { typeof(float) })));

                    return instructions;
                }
            }

            instructions.AddRange(arguments[1].ConvertToInstructions(varmap));

            instructions.Add(
                Instruction.Call(
                    typeof(System.Math).GetMethod("Pow")));

            return instructions;

        }
    }
}
