using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ExponentOperation : BinaryOperation
    {
        public ExponentOperation()
        {
            Name = "^";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Exponent; }
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal(Math.Pow(args[0].Value, args[1].Value));
        }
    }
}
