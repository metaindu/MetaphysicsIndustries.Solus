using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class GreaterThanComparisonOperation : ComparisonOperation
    {
        protected override bool Compare(double x, double y)
        {
            return x > y;
        }
    }
}
