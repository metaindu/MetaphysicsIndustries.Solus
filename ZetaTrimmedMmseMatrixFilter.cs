using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ZetaTrimmedMmseMatrixFilter : MinimalMeanSquareErrorMatrixFilter
    {
        public ZetaTrimmedMmseMatrixFilter(int windowSize, double noiseVariance, double zeta)
            : base(windowSize, noiseVariance)
        {
            _zeta = zeta;
        }

        private double _zeta;
        public double Zeta
        {
            get { return _zeta; }
            set { _zeta = value; }
        }

        public static double CalculateOptimumZtmmseZeta(double impulseProbability)
        {
            double optimumZtmmseZeta;
            int kMax = 128;
            double[] c = new double[kMax];
            c[0] = 1;
            double sum = 0;
            double s = Math.Sqrt(Math.PI) / 2;
            for (int k = 0; k < kMax; k++)
            {
                int kk = 2 * k + 1;
                double cc = c[k];
                for (int m = 0; m <= k - 1; m++)
                {
                    cc += c[m] * c[k - 1 - m] / ((m + 1) * (2 * m + 1));
                }
                c[k] = cc;

                sum += cc * Math.Pow(s * (impulseProbability - 1), 2 * k + 1) / (2 * k + 1);
            }
            optimumZtmmseZeta = sum * -Math.Sqrt(2);
            return optimumZtmmseZeta;
        }

        protected class ZtmmseInfo
        {
            public ZtmmseInfo(int windowSize)
            {
                z = new double[windowSize, windowSize];
            }

            public double mean = 0;
            public double stdev = 0;
            public double[,] z;
            public double sum = 0;
            public int eta = 0;
        }

        protected override double PerPixelOperation(Matrix input, int row, int column)
        {
            double signalMean1;
            double signalVariance1;
            double signalMean2;
            double signalVariance2;
            double noiseVariance;

            //calculate signal mean 1 and signal variance 1

            SignalMeanInfo signalMeanInfo = new SignalMeanInfo();
            DoWindowPass(input, row, column, InternalCalcSignalMean, signalMeanInfo);
            signalMean1 = signalMeanInfo.sum / signalMeanInfo.count;


            SignalVarianceInfo signalVarianceInfo = new SignalVarianceInfo();
            signalVarianceInfo.signalMean = signalMean1;
            DoWindowPass(input, row, column, InternalCalcSignalVariance, signalVarianceInfo);

            signalVariance1 = signalVarianceInfo.sum / (signalVarianceInfo.count - 1);

            //calculate z and eta and signal mean 2

            ZtmmseInfo info1 = new ZtmmseInfo(WindowSize);
            info1.mean = signalMean1;
            info1.stdev = Math.Sqrt(signalVariance1);
            DoWindowPass(input, row, column, CalcEta, info1);

            signalMean2 = info1.sum / info1.eta;

            //calculate signal variance 2
            info1.sum = 0;
            info1.mean = signalMean2;
            DoWindowPass(input, row, column, CalcSignalVariance2, info1);

            signalVariance2 = info1.sum / (info1.eta - 1);

            //calculate noise variance

            noiseVariance = CalculateNoiseVariance();


            return CalculateFinalValue(input, row, column, signalMean2, signalVariance2, noiseVariance);
        }

        protected virtual double CalculateFinalValue(Matrix input, int row, int column, double signalMean2, double signalVariance2, double noiseVariance)
        {

            // impulse rejection stage goes here

            double ratio = noiseVariance / signalVariance2;
            double value = input[row, column];
            //double z = Math.Abs((value - signalMean2) / Math.Sqrt(signalVariance2));

            //if (z > Zeta)
            //{
            //    List<double> measures = new List<double>(WindowSize * WindowSize);
            //    int trimLeft = 2;
            //    int trimRight = 2;

            //    DoWindowPass(input, row, column, 3, AddValueToMeasures, measures);
            //    measures.Sort(Compare);
            //    if (measures.Count > trimLeft)
            //    {
            //        measures.RemoveRange(0, trimLeft);
            //    }
            //    if (measures.Count > trimRight)
            //    {
            //        measures.RemoveRange(measures.Count - trimRight, trimRight);
            //    }

            //    if (measures.Count > 0)
            //    {
            //        double sum = 0;
            //        foreach (double value2 in measures)
            //        {
            //            sum += value2;
            //        }
            //        value = sum / measures.Count;
            //    }
            //    else
            //    {
            //        return signalMean2;
            //    }
            //}

            return CalculateFinalValue(value, signalMean2, ratio);
        }

        protected virtual void CalcEta(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, ZtmmseInfo info)
        {
            double z = Math.Abs((value - info.mean) / info.stdev);
            info.z[rowWithinWindow, columnWithinWindow] = z;

            if (z <= Zeta)
            {
                info.eta++;
                info.sum += value;
            }
        }

        protected virtual void CalcSignalVariance2(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, ZtmmseInfo info)
        {
            double z = info.z[rowWithinWindow, columnWithinWindow];

            if (z <= Zeta)
            {
                value -= info.mean;
                value *= value;
                info.sum += value;
            }
        }

        //protected virtual void AddValueToMeasures(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, List<double> measures)
        //{
        //    measures.Add(value);
        //}

        //public static int Compare(double x, double y)
        //{
        //    return x.CompareTo(y);
        //}
    }
}
