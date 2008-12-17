using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class LowPassVectorFilter : MovingAverageVectorFilter
    {
        public LowPassVectorFilter()
            : base(2)
        {
        }
    }
}
