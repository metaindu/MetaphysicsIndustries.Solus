using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class NotEqualComparisonOperation : ComparisonOperation
    {
        public NotEqualComparisonOperation()
            :base("!=")
        {
        }
        protected override bool Compare(double x, double y)
        {
            return x != y;
        }

        public override OperationPrecedence Precedence
        {
            get
            {
                return OperationPrecedence.Equality;
            }
        }

        public override bool IsCommutative
        {
            get
            {
                return true;
            }
        }
    }
}
