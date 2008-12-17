using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class MinMaxMatrixFilter : AdvancedConvolutionMatrixFilter
    {
        public MinMaxMatrixFilter(int windowSize)
            : this(Matrix.FromUniform(1, windowSize, windowSize))
        {
        }

        public MinMaxMatrixFilter(Matrix convolutionKernel)
            : base(convolutionKernel, Math.Min, Math.Max)
        {
        }
    }
}
