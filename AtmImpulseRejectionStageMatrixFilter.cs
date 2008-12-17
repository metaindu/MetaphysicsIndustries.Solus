using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class AtmImpulseRejectionStageMatrixFilter : OrderStatisticMatrixFilter
    {
        public AtmImpulseRejectionStageMatrixFilter(double alpha)
            : base(3)
        {
        }


        protected override double SelectValueFromOrderedMeasures(List<double> measures)
        {
            throw new NotImplementedException();
        }
    }
}
