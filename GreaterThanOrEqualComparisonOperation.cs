using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class GreaterThanOrEqualComparisonOperation : ComparisonOperation
    {
        public GreaterThanOrEqualComparisonOperation()
            : base(">=")
        {
        }

        protected override bool Compare(float x, float y)
        {
            return x >= y;
        }
    }
}
