using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class AdvancedConvolutionMatrixFilter : MatrixFilter
    {
        public AdvancedConvolutionMatrixFilter(Matrix convolutionKernal, BiModulator firstOp, BiModulator secondOp)
        {
            if (convolutionKernal == null) { throw new ArgumentNullException("convolutionKernal"); }

            _convolutionKernal = convolutionKernal;
            _firstOp = firstOp;
            _secondOp = secondOp;
        }

        Matrix _convolutionKernal;
        BiModulator _firstOp;
        BiModulator _secondOp;

        public override Matrix Apply(Matrix input)
        {
            return input.AdvancedConvolution(_convolutionKernal, _firstOp, _secondOp);
        }
    }
}
