using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class DivisionOperation : BinaryOperation
    {
        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Division; }
        }

        public override bool IsCommutative
        {
            get
            {
                return false;
            }
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal(args[0].Value / args[1].Value);
        }
    }
}
