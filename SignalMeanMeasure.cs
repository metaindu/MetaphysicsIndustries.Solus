using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SignalMeanMeasure
    {
        public double Measure(IEnumerable<double> input)
        {
            double sum = 0;
            int count = 0;

            foreach (double value in input)
            {
                sum += value;
                count++;
            }

            return sum / count;
        }
    }
}
