using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class IntervalFitMatrixFilter : IntervalFitBaseMatrixFilter
    {
        public IntervalFitMatrixFilter()
            : base(0, 1)
        {
        }

        public override Matrix Apply(Matrix input)
        {
            Pair<double> ret;

            ret = CalcInterval(input);

            Min = ret.First;
            Max = ret.Second;

            //accumulate & fire
            return base.Apply(input);
        }

        public static Pair<double> CalcInterval(Matrix input)
        {
            int i;
            int j;

            double min = input[0, 0];
            double max = min;

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    double value = input[i, j];
                    if (!double.IsNaN(value))
                    {
                        min = Math.Min(min, value);
                        max = Math.Max(max, value);
                    }
                    else
                    {
                        value = 0;
                    }
                }
            }

            return new Pair<double>(min, max);
        }

    }
}
