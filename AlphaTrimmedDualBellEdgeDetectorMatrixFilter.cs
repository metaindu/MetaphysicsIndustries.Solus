using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class AlphaTrimmedDualBellEdgeDetectorMatrixFilter : DualBellEdgeDetectorMatrixFilter
    {
        public AlphaTrimmedDualBellEdgeDetectorMatrixFilter(double alpha, int windowSize)
            : base(windowSize)
        {
            _alpha = alpha;
        }

        private double _alpha;
        public double Alpha
        {
            get { return _alpha; }
            set { _alpha = value; }
        }

        protected override double SelectValueFromOrderedMeasures(List<double> measures)
        {
            List<double> measures2 = new List<double>(measures);

            int alphaCount = (int)Math.Ceiling(WindowSize * WindowSize * Alpha / 2);

            if (measures2.Count > alphaCount)
            {
                measures2.RemoveRange(0, alphaCount);
            }
            if (measures2.Count > alphaCount)
            {
                measures2.RemoveRange(measures2.Count - alphaCount, alphaCount);
            }

            return base.SelectValueFromOrderedMeasures(measures2);
        }
    }
}
