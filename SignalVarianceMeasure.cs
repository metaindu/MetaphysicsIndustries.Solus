using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SignalVarianceMeasure
    {
        public double Measure(IEnumerable<double> input)
        {
            double sum = 0;
            double signalMean = (new SignalMeanMeasure()).Measure(input);
            int count = 0;

            foreach (double value in input)
            {
                double value2 = value - signalMean;

                sum += value2 * value2;
                count++;
            }

            return sum / (count - 1);
        }
    }
}
