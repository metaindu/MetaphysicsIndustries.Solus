using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class AlphaTrimmedMmsePlusAtmMatrixFilter : AlphaTrimmedMmseMatrixFilter
    {
        public AlphaTrimmedMmsePlusAtmMatrixFilter(int windowSize, double noiseVariance, double alpha)
            : base(windowSize, noiseVariance, alpha)
            //(int)Math.Round(alpha * windowSize * windowSize / 2.0), (int)Math.Round(alpha * windowSize * windowSize / 2.0))
        {
        }

        //protected AlphaTrimmedMmsePlusAtmMatrixFilter(int windowSize, double noiseVariance, int trimLeft, int trimRight)
        //    : base(windowSize, noiseVariance, trimLeft, trimRight)
        //{
        //}

        protected override double CalculateFinalValue(Matrix input, int row, int column, double signalMean, double ratio)
        {
            double value = input[row, column];

            List<double> measures = new List<double>(WindowSize * WindowSize);
            int trimLeft = 2;
            int trimRight = 2;

            DoWindowPass(input, row, column, 3, AddValueToMeasures, measures);
            measures.Sort(Compare);

            bool doAtm = false;

            if (value > measures[measures.Count / 2])
            {
                //white impulse?
                if (measures.GetRange(measures.Count - trimRight, trimRight).Contains(value))
                {
                    //yes, replace mmse calc with atm
                    doAtm = true;
                }
            }
            else
            {
                //black impulse?
                if (measures.GetRange(0, trimLeft).Contains(value))
                {
                    //yes, replace mmse calc with atm
                    doAtm = true;
                }
            }

            if (doAtm)
            {
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
                    foreach (double measure in measures)
                    {
                        sum += measure;
                    }
                    value = sum / measures.Count;
                }
                else
                {
                    value = 0;
                }

                return value;
            }

            return base.CalculateFinalValue(input, row, column, signalMean, ratio);
        }
    }
}
