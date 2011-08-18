using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class NegationOperation : UnaryOperation
    {
        public NegationOperation()
        {
            Name = "-";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Negation; }
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal(-args[0].Value);
        }

        public override string ToString(ExpressionCollection arguments)
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
    }
}
