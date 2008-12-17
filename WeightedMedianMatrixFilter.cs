using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class WeightedMedianMatrixFilter : WeightedPMatrixFilter
    {
        public WeightedMedianMatrixFilter(Matrix weights)
            : base(0.5, weights)
        {
        }
    }
}
