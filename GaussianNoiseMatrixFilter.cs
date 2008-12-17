using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class GaussianNoiseMatrixFilter : MatrixFilter
    {
        public GaussianNoiseMatrixFilter(
            //double mean,
            double variance)
        {
            //_mean = mean;
            _variance = variance;
        }

        //private double _mean;
        private double _variance;

        public override Matrix Apply(Matrix input)
        {
            Matrix ret = new Matrix(input.RowCount, input.ColumnCount);
            int i;
            int j;

            double stdev = Math.Sqrt(_variance);

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    int k;
                    int n = 20;
                    double x = 0;

                    for (k = 0; k < n; k++)
                    {
                        x += _rand.NextDouble();
                    }
                    x -= n / 2.0f;
                    x *= Math.Sqrt(12.0 / n);

                    x = 
                        //_mean + 
                        stdev * x;

                    ret[i,j] = input[i,j] + x;
                }
            }
            return ret;
        }
    }
}
