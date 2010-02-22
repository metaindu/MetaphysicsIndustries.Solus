using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class LogicalOrOperation : BinaryOperation
    {
        public LogicalOrOperation()
        {
            Name = "||";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.LogicalOr; }
        }

        protected override double InternalBinaryCall(double x, double y)
        {
            return ((((long)x) != 0) || (((long)y) != 0)) ? 1 : 0;
        }
    }
}
