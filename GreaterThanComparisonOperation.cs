using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class GreaterThanComparisonOperation : ComparisonOperation
    {
        public static readonly GreaterThanComparisonOperation Value = new GreaterThanComparisonOperation();

        protected GreaterThanComparisonOperation()
            : base(">")
        {
        }

        protected override bool Compare(float x, float y)
        {
            return x > y;
        }
    }
}
