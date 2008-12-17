using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class AlphaTrimmedMeanMatrixFilter : OrderStatisticMatrixFilter
    {
        public AlphaTrimmedMeanMatrixFilter(int windowSize, double alpha)
            : base(windowSize)
        {
            if (alpha < 0 || alpha >= 0.5) { throw new ArgumentException("alpha must be between 0 and 0.5"); }
            _alpha = Math.Max(0, Math.Min(0.5, alpha));
        }

        private double _alpha;

        protected override double SelectValueFromOrderedMeasures(List<double> measures)
        {
            int i;
            int n;
            double sum = 0;
            int count = 0;

            n = measures.Count; // also, n should be equal to windowSize*windowSize, if no weights are used and we're not on the edge of the image

            for (i = (int)(Math.Round(_alpha * n + 1)); i < n - _alpha * n; i++)
            {
                sum += measures[i];
                count++;
            }

            return sum / count;
        }
    }
}
