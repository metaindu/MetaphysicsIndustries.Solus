using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class MmseEdgeDetectionMatrixfilter : MinimalMeanSquareErrorMatrixFilter
    {
        public MmseEdgeDetectionMatrixfilter(int windowSize, double noiseVariance)
            : this(windowSize, noiseVariance, 1)
        {
        }

        public MmseEdgeDetectionMatrixfilter(int windowSize, double noiseVariance, double gamma)
            : base(windowSize, noiseVariance)
        {
            _gamma = gamma;
        }

        private double  _gamma;
        public double  Gamma
        {
            get { return _gamma; }
        }


        //private double _noiseVariance;

        //protected class SignalMeanInfo
        //{
        //    public double sum = 0;
        //    public int count = 0;
        //}

        //protected class SignalVarianceInfo
        //{
        //    public double sum = 0;
        //    public int count = 0;
        //    public double signalMean = 0;
        //}

        //protected override double PerPixelOperation(Matrix input, int row, int column)
        //{
        //    double signalMean;
        //    double signalVariance;
        //    double noiseVariance;

        //    signalMean = CalculateSignalMean(input, row, column);

        //    signalVariance = CalculateSignalVariance(input, row, column, signalMean);

        //    noiseVariance = CalculateNoiseVariance();

        //    double ratio = noiseVariance / signalVariance;

        //    return CalculateFinalValue(input, row, column, signalMean, ratio);
        //}

        protected override double CalculateFinalValue(Matrix input, int row, int column, double signalMean, double ratio)
        {
            return 1 - Math.Pow(ratio, Gamma);// *input[row, column] + ratio * signalMean;
        }

        //protected virtual double CalculateNoiseVariance()
        //{
        //    return _noiseVariance;
        //}

        //protected virtual double CalculateSignalVariance(Matrix input, int row, int column, double signalMean)
        //{
        //    double signalVariance;
        //    SignalVarianceInfo signalVarianceInfo = new SignalVarianceInfo();
        //    signalVarianceInfo.signalMean = signalMean;
        //    DoWindowPass(input, row, column, InternalCalcSignalVariance, signalVarianceInfo);

        //    signalVariance = signalVarianceInfo.sum / (signalVarianceInfo.count - 1);
        //    return signalVariance;
        //}

        //protected virtual double CalculateSignalMean(Matrix input, int row, int column)
        //{
        //    double signalMean;
        //    SignalMeanInfo signalMeanInfo = new SignalMeanInfo();
        //    DoWindowPass(input, row, column, InternalCalcSignalMean, signalMeanInfo);

        //    //signalMean = sum / count;
        //    signalMean = signalMeanInfo.sum / signalMeanInfo.count;
        //    return signalMean;
        //}

        //protected virtual void InternalCalcSignalMean(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, SignalMeanInfo signalMeanInfo)
        //{
        //    signalMeanInfo.sum += value;
        //    signalMeanInfo.count++;
        //}
        //protected virtual void InternalCalcSignalVariance(double value, int row, int column, int rowWithinWindow, int columnWithinWindow, SignalVarianceInfo signalVarianceInfo)
        //{
        //    value -= signalVarianceInfo.signalMean;
        //    signalVarianceInfo.sum += value * value;
        //    signalVarianceInfo.count++;
        //}
    }
}
