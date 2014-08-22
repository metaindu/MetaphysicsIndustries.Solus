using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class ComparisonOperation : BinaryOperation
    {
        protected ComparisonOperation(string name)
        {
            Name = name;
        }

        protected override sealed float InternalBinaryCall(float x, float y)
        {
            return Compare(x, y) ? 1 : 0;
        }

        protected abstract bool Compare(float x, float y);

        public override OperationPrecedence Precedence
        {
            get { return OperationPrecedence.Comparison; }
        }

        public override bool HasIdentityValue
        {
            get { return false; }
        }
    }
}
