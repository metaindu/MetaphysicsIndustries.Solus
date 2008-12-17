using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ImpulseNoiseMatrixFilter : SaltAndPepperNoiseMatrixFilter
    {
        public ImpulseNoiseMatrixFilter(double probability)
            : base(probability, 0, 1)
        {
        }
    }
}
