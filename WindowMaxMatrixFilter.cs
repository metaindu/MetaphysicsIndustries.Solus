using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class WindowMaxMatrixFilter : WeightedPMatrixFilter
    {
        public WindowMaxMatrixFilter(int windowSize)
            : base(1, Matrix.FromUniform(1, windowSize, windowSize))
        {
        }
    }
}
