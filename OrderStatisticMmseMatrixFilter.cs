using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class OrderStatisticMmseMatrixFilter : MinimalMeanSquareErrorMatrixFilter
    {
        public OrderStatisticMmseMatrixFilter(int windowSize, double noiseVariance)
            : base(windowSize, noiseVariance)
        {
        }

        protected override double CalculateSignalMean(Matrix input, int row, int column)
        {
            List<double> measures = new List<double>(WindowSize * WindowSize);

            DoWindowPass(input, row, column, AddValueToMeasures, measures);

            return SelectValueFromMeasures(measures);
        }

        protected virtual double SelectValueFromMeasures(List<double> measures)
        {
            List<double> measures2 = new List<double>(measures);
            measures2.Sort(Compare);
            return SelectValueFromOrderedMeasures(measures);
        }

        protected abstract double SelectValueFromOrderedMeasures(List<double> measures);
    }
}
