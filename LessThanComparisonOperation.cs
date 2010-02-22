using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class LessThanComparisonOperation : ComparisonOperation
    {
        public LessThanComparisonOperation()
            : base("<")
        {
        }

        protected override bool Compare(double x, double y)
        {
            return x < y;
        }
    }
}
