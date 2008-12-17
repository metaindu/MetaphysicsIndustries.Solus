using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ZetaTrimmedMeanMatrixFilter : WindowedMatrixFilter
    {
        public ZetaTrimmedMeanMatrixFilter(int windowSize, double zeta)
            : base(windowSize)
        {
            _zeta = zeta;
        }

        private double _zeta;

        protected class SignalMeanInfo
        {
            public double sum = 0;
            public int count = 0;
        }

        protected class SignalVarianceInfo
        {
            public double sum = 0;
            public int count = 0;
            public double signalMean = 0;
        }

        protected class ZtmInfo
        {
            public ZtmInfo(int windowSize)
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
            double signalMean;
            double signalVariance;
            double trimmedMean;

            //calculate signal mean 1 and signal variance 1
            SignalMeanInfo signalMeanInfo = new SignalMeanInfo();
            DoWindowPass(input, row, column, CalculateSignalMean, signalMeanInfo);
            signalMean = signalMeanInfo.sum / signalMeanInfo.count;

            SignalVarianceInfo signalVarianceInfo = new SignalVarianceInfo();
            signalVarianceInfo.signalMean = signalMean;
            DoWindowPass(input, row, column, CalculateSignalVariance, signalVarianceInfo);
            signalVariance = signalVarianceInfo.sum / (signalVarianceInfo.count - 1);

            //calculate z and eta and trimmed mean
            ZtmInfo ztmInfo = new ZtmInfo(WindowSize);
            ztmInfo.mean = signalMean;
            ztmInfo.stdev = Math.Sqrt(signalVariance);
            DoWindowPass(input, row, column, CalcTrimmedMean, ztmInfo);
            trimmedMean = ztmInfo.sum / ztmInfo.eta;

            return trimmedMean;
        }

        protected virtual void CalcTrimmedMean(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, ZtmInfo info)
        {
            double z = Math.Abs((value - info.mean) / info.stdev);
            info.z[rowWithinWindow, columnWithinWindow] = z;

            if (z <= _zeta)
            {
                info.eta++;
                info.sum += value;
            }
        }

        //protected virtual void CalcSignalVariance2(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, ZtmmseInfo info)
        //{
        //    double z = info.z[rowWithinWindow, columnWithinWindow];
        //
        //    if (z <= Zeta)
        //    {
        //        value -= info.mean;
        //        value *= value;
        //        info.sum += value;
        //    }
        //}

        //protected virtual void AddValueToMeasures(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, List<double> measures)
        //{
        //    measures.Add(value);
        //}

        //protected int Compare(double x, double y)
        //{
        //    return x.CompareTo(y);
        //}

        protected virtual void CalculateSignalMean(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, SignalMeanInfo signalMeanInfo)
        {
            signalMeanInfo.sum += value;
            signalMeanInfo.count++;
        }
        protected virtual void CalculateSignalVariance(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, SignalVarianceInfo signalVarianceInfo)
        {
            value -= signalVarianceInfo.signalMean;
            signalVarianceInfo.sum += value * value;
            signalVarianceInfo.count++;
        }

    }
}
