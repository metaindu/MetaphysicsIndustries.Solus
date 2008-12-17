using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class InverseFourierTransformVectorFilter : FourierTransformVectorFilter
    {
        protected override double Sign
        {
            get
            {
                return 1;
            }
        }

        protected override double ScaleForInverse(double x, int length)
        {
            return x / length;
        }
    }
}
