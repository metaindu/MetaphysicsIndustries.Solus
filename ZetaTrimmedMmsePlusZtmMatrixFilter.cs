using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ZetaTrimmedMmsePlusZtmMatrixFilter : ZetaTrimmedMmseMatrixFilter
    {
        public ZetaTrimmedMmsePlusZtmMatrixFilter(int windowSize, double noiseVariance, double zeta)//, double secondZeta)
            : base(windowSize, noiseVariance, zeta)
        {
        }

        protected override double CalculateFinalValue(Matrix input, int row, int column, double signalMean2, double signalVariance2, double noiseVariance)
        {

            // impulse rejection stage

            double ratio = noiseVariance / signalVariance2;
            double value = input[row, column];
            double z = Math.Abs((value - signalMean2) / Math.Sqrt(signalVariance2));

            if (z > Zeta)
            {
                List<double> measures = new List<double>(WindowSize * WindowSize);
                int trimLeft = 2;
                int trimRight = 2;

                DoWindowPass(input, row, column, 3, AddValueToMeasures, measures);
                measures.Sort(Compare);
                if (measures.Count > trimLeft)
                {
                    measures.RemoveRange(0, trimLeft);
                }
                if (measures.Count > trimRight)
                {
                    measures.RemoveRange(measures.Count - trimRight, trimRight);
                }

                if (measures.Count > 0)
                {
                    double sum = 0;
                    foreach (double value2 in measures)
                    {
                        sum += value2;
                    }
                    value = sum / measures.Count;
                }
                else
                {
                    return signalMean2;
                }
            }

            return CalculateFinalValue(value, signalMean2, ratio);
        }
    }
}
