using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ConvolutionMatrixFilter : MatrixFilter
    {
        public ConvolutionMatrixFilter(Matrix convolutionKernal)
        {
            if (convolutionKernal == null) { throw new ArgumentNullException("convolutionKernal"); }

            _convolutionKernal = convolutionKernal;
        }

        Matrix _convolutionKernal;

        public override Matrix Apply(Matrix input)
        {
            return input.Convolution(_convolutionKernal);
        }
    }
}
