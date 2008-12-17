using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ZetaTrimmedMmsePlusAtmMatrixFilter : ZetaTrimmedMmseMatrixFilter
    {
        public ZetaTrimmedMmsePlusAtmMatrixFilter(int windowSize, double noiseVariance, double zeta, double alpha)
            : base(windowSize, noiseVariance, zeta)
        {
            _alpha = alpha;
        }

        private double _alpha;
        public double Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }


        protected override double CalculateFinalValue(Matrix input, int row, int column, double signalMean2, double signalVariance2, double noiseVariance)
        {

            // impulse rejection stage

            double ratio = noiseVariance / signalVariance2;
            double value = input[row, column];

            List<double> measures = new List<double>(WindowSize * WindowSize);
            int trim = (int)Math.Ceiling(Alpha * WindowSize * WindowSize / 2.0);

            DoWindowPass(input, row, column, 3, AddValueToMeasures, measures);
            measures.Sort(Compare);

            int i = measures.BinarySearch(value);
            if (i < 0) { i = ~i; }
            if (i < trim && i >= measures.Count - trim)
            {
                if (measures.Count > trim << 1)
                {
                    double sum = 0;
                    for (i = trim; i < measures.Count - trim; i++)
                    {
                        sum += measures[i];
                    }
                    value = sum / measures.Count;
                }
                else
                {
                    value = 0;
                }

                return value;
            }

            return CalculateFinalValue(value, signalMean2, ratio);
        }
    }
}
