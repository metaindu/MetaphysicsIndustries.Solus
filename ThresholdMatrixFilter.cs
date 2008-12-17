using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ThresholdMatrixFilter : MatrixFilter
    {
        public ThresholdMatrixFilter(double threshold)
        {
            _threshold = threshold;
        }

        private double _threshold;
        public double Threshold
        {
            get { return _threshold; }
        }

        public override Matrix Apply(Matrix input)
        {
            Matrix m = input.Clone();

            m.ApplyToAll(ApplyThreshold);

            return m;
        }

        public double ApplyThreshold(double x)
        {
            return x >= _threshold ? 1 : 0;
        }
    }
}
