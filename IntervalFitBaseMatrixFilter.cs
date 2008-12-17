using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class IntervalFitBaseMatrixFilter : MatrixFilter
    {
        public IntervalFitBaseMatrixFilter(double min, double max)
        {
            _min = min;
            _max = max;
        }

        double _min;
        public double Min
        {
            get { return _min; }
            protected set { _min = value; }
        }
	
        double _max;
        public double Max
        {
            get { return _max; }
            protected set { _max = value; }
        }

        public override Matrix Apply(Matrix input)
        {
            return IntervalFit(input, Min, Max);
        }

        public static Matrix IntervalFit(Matrix input, double min, double max)
        {
            int i;
            int j;

            Matrix output = input.CloneSize();

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    double value = input[i, j];
                    if (double.IsNaN(value))
                    {
                        value = min;
                    }
                    output[i, j] = SolusEngine.IntervalFit(value, min, max);
                }
            }

            return output;
        }
    }
}
