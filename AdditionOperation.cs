using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class AdditionOperation : AssociativeCommutativeOperation
    {
        public AdditionOperation()
        {
            Name = "+";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Addition; }
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            double sum = 0;
            foreach (Literal arg in args)
            {
                sum += arg.Value;
            }
            return new Literal(sum);
        }

        public override double IdentityValue
        {
            get
            {
                return 0;
            }
        }
    }
}
