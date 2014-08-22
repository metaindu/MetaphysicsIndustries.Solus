using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class LogicalOrOperation : BinaryOperation
    {
        public static readonly LogicalOrOperation Value = new LogicalOrOperation();

        protected LogicalOrOperation()
        {
            Name = "||";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.LogicalOr; }
        }

        protected override float InternalBinaryCall(float x, float y)
        {
            return ((((long)x) != 0) || (((long)y) != 0)) ? 1 : 0;
        }
    }
}
