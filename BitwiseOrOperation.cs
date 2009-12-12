using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class BitwiseOrOperation : AssociativeCommutativeOperation
    {
        public BitwiseOrOperation()
        {
            Name = "|";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Bitwise; }
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            long value = 0;

            foreach (Literal arg in args)
            {
                value |= (long)(arg.Value);
            }

            return new Literal(value);
        }

        public override double IdentityValue
        {
            get { return 0; }
        }

        public override bool Culls
        {
            get { return true; }
        }

        public override double CullValue
        {
            get { return 0; }
        }
    }
}
