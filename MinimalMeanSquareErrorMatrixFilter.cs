using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class MinimalMeanSquareErrorMatrixFilter : WindowedMatrixFilter
    {
        public MinimalMeanSquareErrorMatrixFilter(int windowSize, double noiseVariance)
            : base(windowSize)
        {
            _noiseVariance = noiseVariance;
        }

        private double _noiseVariance;

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

        protected override double PerPixelOperation(Matrix input, int row, int column)
        {
            double signalMean;
            double signalVariance;
            double noiseVariance;

            signalMean = CalculateSignalMean(input, row, column);

            signalVariance = CalculateSignalVariance(input, row, column, signalMean);

            noiseVariance = CalculateNoiseVariance();

            double ratio = noiseVariance / signalVariance;

            return CalculateFinalValue(input, row, column, signalMean, ratio);
        }

        protected virtual double CalculateFinalValue(Matrix input, int row, int column, double signalMean, double ratio)
        {
            return CalculateFinalValue(input[row, column], signalMean, ratio);
        }

        protected virtual double CalculateFinalValue(double value, double signalMean, double ratio)
        {
            return (1 - ratio) * value + ratio * signalMean;
        }

        protected virtual double CalculateNoiseVariance()
        {
            return _noiseVariance;
        }

        protected virtual double CalculateSignalVariance(Matrix input, int row, int column, double signalMean)
        {
            double signalVariance;
            SignalVarianceInfo signalVarianceInfo = new SignalVarianceInfo();
            signalVarianceInfo.signalMean = signalMean;
            DoWindowPass(input, row, column, InternalCalcSignalVariance, signalVarianceInfo);

            signalVariance = signalVarianceInfo.sum / (signalVarianceInfo.count - 1);
            return signalVariance;
        }

        protected virtual double CalculateSignalMean(Matrix input, int row, int column)
        {
            double signalMean;
            SignalMeanInfo signalMeanInfo = new SignalMeanInfo();
            DoWindowPass(input, row, column, InternalCalcSignalMean, signalMeanInfo);

            //signalMean = sum / count;
            signalMean = signalMeanInfo.sum / signalMeanInfo.count;
            return signalMean;
        }

        protected virtual void InternalCalcSignalMean(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, SignalMeanInfo signalMeanInfo)
        {
            signalMeanInfo.sum += value;
            signalMeanInfo.count++;
        }
        protected virtual void InternalCalcSignalVariance(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, SignalVarianceInfo signalVarianceInfo)
        {
            value -= signalVarianceInfo.signalMean;
            signalVarianceInfo.sum += value * value;
            signalVarianceInfo.count++;
        }

        protected virtual void AddValueToMeasures(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, List<double> measures)
        {
            measures.Add(value);
        }

        public static int Compare(double x, double y)
        {
            return x.CompareTo(y);
        }
    }
}
