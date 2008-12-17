using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class MedianMatrixFilter : WeightedMedianMatrixFilter
    {
        public MedianMatrixFilter(int width)
            : base(Matrix.FromUniform(1, width, width))
        {
        }
    }
}
