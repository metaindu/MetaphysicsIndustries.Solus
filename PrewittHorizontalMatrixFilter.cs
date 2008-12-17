using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class PrewittHorizontalMatrixFilter : ConvolutionMatrixFilter
    {
        public PrewittHorizontalMatrixFilter()
            : base(GenerateMatrix())
        {
        }

        protected static Matrix GenerateMatrix()
        {
            Matrix y = new Matrix(3, 3);

            y[0, 0] = 1;
            y[1, 0] = 1;
            y[2, 0] = 1;
            y[0, 2] = -1;
            y[1, 2] = -1;
            y[2, 2] = -1;

            y.ApplyToAll((new SolusEngine.MultiplyModulator(1 / 3.0)).Modulate);

            return y;
        }

    }
}
