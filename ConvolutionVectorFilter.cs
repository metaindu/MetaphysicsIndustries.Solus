using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ConvolutionVectorFilter : VectorFilter
    {
        public ConvolutionVectorFilter(Vector convolutionKernal)
        {
            if (convolutionKernal == null) { throw new ArgumentNullException("convolutionKernal"); }

            _convolutionKernal = convolutionKernal;
        }

        Vector _convolutionKernal;

        public override Vector Apply(Vector input)
        {
            return input.Convolution(_convolutionKernal);
        }
    }
}
