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

        public static readonly GreaterThanComparisonOperation GreaterThan = new GreaterThanComparisonOperation();
        public static readonly GreaterThanOrEqualComparisonOperation GreaterThanOrEqual = new GreaterThanOrEqualComparisonOperation();
        public static readonly LessThanComparisonOperation LessThan = new LessThanComparisonOperation();
        public static readonly LessThanOrEqualComparisonOperation LessThanOrEqual = new LessThanOrEqualComparisonOperation();
        public static readonly EqualComparisonOperation Equal = new EqualComparisonOperation();
        public static readonly NotEqualComparisonOperation NotEqual = new NotEqualComparisonOperation();

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
