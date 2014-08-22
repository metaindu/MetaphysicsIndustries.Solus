using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class LessThanComparisonOperation : ComparisonOperation
    {
        public static readonly LessThanComparisonOperation Value = new LessThanComparisonOperation();

        protected LessThanComparisonOperation()
            : base("<")
        {
        }

        protected override bool Compare(float x, float y)
        {
            return x < y;
        }
    }
}
