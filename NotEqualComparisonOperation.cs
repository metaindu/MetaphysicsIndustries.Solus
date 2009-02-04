using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class NotEqualComparisonOperation : ComparisonOperation
    {
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
    }
}
