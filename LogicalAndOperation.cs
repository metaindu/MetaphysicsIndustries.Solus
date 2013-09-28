using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class LogicalAndOperation : BinaryOperation
    {
        public static readonly LogicalAndOperation Value = new LogicalAndOperation();

        protected LogicalAndOperation()
        {
            Name = "&&";
        }

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.LogicalAnd; }
        }

        protected override float InternalBinaryCall(float x, float y)
        {
            return ((((long)x) != 0) && (((long)y) != 0)) ? 1 : 0;
        }
    }
}
