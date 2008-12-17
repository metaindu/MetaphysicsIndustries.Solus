using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ArithmeticMeanFilter : ConvolutionMatrixFilter
    {
        public ArithmeticMeanFilter(int width)
            : base(Matrix.FromUniform(1 / (double)(width * width), width, width))
        {
        }
    }
}
