using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class CoordinateTransformMatrixFilter : MatrixFilter
    {
        //public CoordinateTransformMatrixFilter(PairModulator mod)
        //{
        //    _mod = mod;
        //}

        //private PairModulator _mod;

        protected abstract Pair<double> Modulate(double x, double y);

        public override Matrix Apply(Matrix input)
        {
            int i;
            int j;
            Matrix ret = new Matrix(input.RowCount, input.ColumnCount);
            Pair<double> pair;

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    pair = new Pair<double>(i / (double)input.RowCount, j / (double)input.ColumnCount);
                    pair = Modulate(pair.First, pair.Second);
                    if (pair.First < 0 || pair.First > 1 ||
                        pair.Second < 0 || pair.Second > 1)
                    {
                        ret[i, j] = 0;
                    }
                    else
                    {
                        ret[i, j] =
                            input[
                                (int)(Math.Round(pair.First * (input.RowCount - 1))),
                                (int)(Math.Round(pair.Second * (input.ColumnCount - 1)))];
                    }
                }
            }

            return ret;
        }
    }
}
