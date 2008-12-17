using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class DualBellEdgeDetectorMatrixFilter : OrderStatisticMatrixFilter
    {
        public DualBellEdgeDetectorMatrixFilter(int windowSize)
            : base(windowSize)
        {
        }

        //public DualBellEdgeDetectorMatrixFilter(int windowSize, double gamma)
        //    : base(windowSize)
        //{
        //    _gamma = gamma;
        //}

        //private double _gamma;
        //public double Gamma
        //{
        //    get { return _gamma; }
        //    set { _gamma = value; }
        //}


        protected override double SelectValueFromOrderedMeasures(List<double> measures)
        {
            if (measures.Count < 1)
            {
                return 0;
            }

            int splitIndex = 0;

            double signalMean = SolusEngine.CalculateMean(measures);
            double signalVariance = SolusEngine.CalculateVariance(measures, signalMean);
            double signalStdev = Math.Sqrt(signalVariance);

            int i;
            int start = -1;
            int count = 1;
            for (i = 0; i < measures.Count; i++)
            {
                if (measures[i] == signalMean)
                {
                    if (start < 0)
                    {
                        start = i;
                    }
                    else
                    {
                        count++;
                    }
                }
                else if (measures[i] > signalMean)
                {
                    if (start < 0)
                    {
                        start = i;
                    }

                    break;
                }
            }

            if (start < 0)
            {
                start = measures.Count - 1;
            }

            splitIndex = start + count / 2;

            double value;

            if (splitIndex < 1)
            {
                value = 0;
            }
            else
            {
                //List<double> lower = measures.GetRange(0, splitIndex);
                //List<double> higher = measures.GetRange(splitIndex, measures.Count - splitIndex);

                double lowerMean;
                //double lowerVariance;
                double higherMean;
                //double higherVariance;

                //if (lower.Count < 1 || higher.Count < 1)
                //{
                //    value = 0;
                //}
                //else 
                    if (signalVariance == 0)
                {
                    value = 0;
                }
                else
                {
                    lowerMean = SolusEngine.CalculateMean(measures, 0, splitIndex);
                    //lowerVariance = SolusEngine.CalculateVariance(measures, lowerMean, 0, splitIndex);
                    higherMean = SolusEngine.CalculateMean(measures, splitIndex, measures.Count - splitIndex);
                    //higherVariance = SolusEngine.CalculateVariance(measures, higherMean, splitIndex, measures.Count - splitIndex);

                    value = higherMean - lowerMean;
                    //value = Math.Abs((lowerMean - higherMean)) / Math.Sqrt(lowerVariance * higherVariance);
                    //value = 1 - higherVariance * lowerVariance / (signalVariance * signalVariance);
                    //value = (higherMean - lowerMean) / signalStdev;
                    //value = Math.Sqrt(higherMean - lowerMean);

                    //value = 1 - SolusEngine.CalculateNormalDistributionOverlap(lowerMean, lowerVariance, higherMean, higherVariance);
                    //value = 1 - Math.Sqrt(lowerVariance * higherVariance) / signalVariance;
                }

                if (double.IsNaN(value))
                {
                    value = 0;
                }
            }

            return value;// Math.Pow(value, Gamma);
        }
    }
}
