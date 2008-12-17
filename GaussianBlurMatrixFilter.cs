using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class GaussianBlurMatrixFilter : ConvolutionMatrixFilter
    {
        public GaussianBlurMatrixFilter(int width)
            : base(GenerateMatrix(width))
        {
        }

        protected static Matrix GenerateMatrix(int width)
        {
            Matrix mat = new Matrix(width, width);
            return mat;
        }
    }
}
