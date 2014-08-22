using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public class MultiplicationOperation : AssociativeCommutativeOperation
    {
        public static readonly MultiplicationOperation Value = new MultiplicationOperation();

        protected MultiplicationOperation()
        {
            Name = "*";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Multiplication; }
        }

        //public override bool IsCommutative
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        //public override bool IsAssociative
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            float value = 1;
            int i;
            for (i = 0; i < args.Length; i++)
            {
                value *= args[i].Value;
            }
            return new Literal(value);
        }

        public override bool Collapses
        {
            get
            {
                return true;
            }
        }

        public override float CollapseValue
        {
            get
            {
                return 0;
            }
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            var instructions = new List<Instruction>();

            foreach (var arg in arguments)
            {
                instructions.AddRange(arg.ConvertToInstructions(varmap));
            }

            int i;
            for (i = 1; i < arguments.Count; i++)
            {
                instructions.Add(Instruction.Mul());
            }

            return instructions;
        }
    }
}
