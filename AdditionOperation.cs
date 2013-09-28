using System;
using System.Collections.Generic;
using System.Text;

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

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
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
    }
}
