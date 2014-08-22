using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class NotEqualComparisonOperation : ComparisonOperation
    {
        public static readonly NotEqualComparisonOperation Value = new NotEqualComparisonOperation();

        protected NotEqualComparisonOperation()
            :base("!=")
        {
        }
        protected override bool Compare(float x, float y)
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
