using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class MedianMmseMatrixFilter : OrderStatisticMmseMatrixFilter
    {
        public MedianMmseMatrixFilter(int windowSize, double noiseVariance)
            : base(windowSize, noiseVariance)
        {
        }

        protected override double SelectValueFromOrderedMeasures(List<double> measures)
        {
            int index = measures.Count / 2;

            if (measures.Count % 2 == 0)
            {
                //even
                return (measures[index] + measures[index - 1]) / 2;
            }
            else
            {
                //odd
                return measures[index];
            }
        }
    }
}
