using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class LessThanOrEqualComparisonOperation : ComparisonOperation
    {
        public LessThanOrEqualComparisonOperation()
            : base("<=")
        {
        }

        protected override bool Compare(float x, float y)
        {
            return x <= y;
        }
    }
}
