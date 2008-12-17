using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class MaxMinMatrixFilter : AdvancedConvolutionMatrixFilter
    {
        public MaxMinMatrixFilter(int windowSize)
            : this(Matrix.FromUniform(1, windowSize, windowSize))
        {
        }

        public MaxMinMatrixFilter(Matrix convolutionKernel)
            : base(convolutionKernel, Math.Max, Math.Min)
        {
        }
    }
}
