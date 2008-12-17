using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class MmsePlusAtmMatrixFilter : MinimalMeanSquareErrorMatrixFilter
    {
        public MmsePlusAtmMatrixFilter(double alphaForRejection, int windowSize, double noiseVariance)
            :base(windowSize, noiseVariance)
        {
            _alphaForRejection = alphaForRejection;
        }

        private double _alphaForRejection;
        public double AlphaForRejection
        {
            get { return _alphaForRejection; }
            set { _alphaForRejection = value; }
        }

        protected override double CalculateFinalValue(Matrix input, int row, int column, double signalMean, double ratio)
        {
            double value = input[row, column];
            int rejectionWindowSize = 3;

            List<double> measures = new List<double>(rejectionWindowSize * rejectionWindowSize);
            int alphaCount = (int)Math.Ceiling(rejectionWindowSize * rejectionWindowSize * AlphaForRejection / 2);

            DoWindowPass(input, row, column, rejectionWindowSize, AddValueToMeasures, measures);
            measures.Sort(Compare);

            bool doAtm = false;

            if (value > measures[measures.Count / 2])
            {
                //white impulse?
                if (measures.GetRange(measures.Count - alphaCount, alphaCount).Contains(value))
                {
                    //yes, replace mmse calc with atm
                    doAtm = true;
                }
            }
            else
            {
                //black impulse?
                if (measures.GetRange(0, alphaCount).Contains(value))
                {
                    //yes, replace mmse calc with atm
                    doAtm = true;
                }
            }

            if (doAtm)
            {
                if (measures.Count > alphaCount)
                {
                    measures.RemoveRange(0, alphaCount);
                }
                if (measures.Count > alphaCount)
                {
                    measures.RemoveRange(measures.Count - alphaCount, alphaCount);
                }

                if (measures.Count > 0)
                {
                    //double sum = 0;
                    //foreach (double measure in measures)
                    //{
                    //    sum += measure;
                    //}
                    //value = sum / measures.Count;
                    value = SolusEngine.CalculateMean(measures);
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
