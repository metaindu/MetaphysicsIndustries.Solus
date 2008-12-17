using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class WeightedMedianVectorFilter : MedianVectorFilter
    {
        public WeightedMedianVectorFilter(Vector weights)
            : base(weights.Length)
        {
        }
    }
}
