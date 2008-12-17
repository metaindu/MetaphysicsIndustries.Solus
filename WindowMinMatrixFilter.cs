using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class WindowMinMatrixFilter : WeightedPMatrixFilter
    {
        public WindowMinMatrixFilter(int windowSize)
            : base(0, Matrix.FromUniform(1, windowSize, windowSize))
        {
        }
    }
}
