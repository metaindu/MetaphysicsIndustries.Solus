using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class OrderStatisticMatrixFilter : WindowedMatrixFilter
    {
        public OrderStatisticMatrixFilter(int windowSize)
            : base(windowSize)
        {
            //_windowSize = windowSize;
        }

        //public override Matrix Apply(Matrix input)
        //{
        //    Matrix result = input.CloneSize();

        //    int i;
        //    int j;

        //    for (i = 0; i < input.RowCount; i++)
        //    {
        //        for (j = 0; j < input.ColumnCount; j++)
        //        {
        //            result[i, j] = PerPixelOperation(input, i, j);
        //        }
        //    }

        //    return result;
        //}

        protected override double PerPixelOperation(Matrix input, int row, int column)
        {
            int i;
            int j;
            double value;
            int width = input.ColumnCount;
            int height = input.RowCount;

            int windowRadius = WindowSize / 2; //this will ignore any remainder

            List<double> measures = new List<double>(WindowSize * WindowSize);

            //measures.Clear();

            for (i = 0; i < 2 * windowRadius + 1; i++)
            {
                if (row - windowRadius + i < 0) { continue; }
                if (row - windowRadius + i >= width) { break; }

                for (j = 0; j < 2 * windowRadius + 1; j++)
                {
                    if (column - windowRadius + j < 0) { continue; }
                    if (column - windowRadius + j >= height) { break; }

                    value = input[row - windowRadius + i, column - windowRadius + j];

                    AddValueToMeasures(value, i, j, measures);
                }
            }

            //DoWindowPass(input, row, column, AddValueToMeasures, measures);

            return SelectValueFromMeasures(measures);
        }


        protected virtual double SelectValueFromMeasures(List<double> measures)
        {
            List<double> measures2 = new List<double>(measures);
            measures2.Sort(Compare);
            return SelectValueFromOrderedMeasures(measures2);
        }

        protected abstract double SelectValueFromOrderedMeasures(List<double> measures);

        //protected virtual void AddValueToMeasures(double value, int row, int column, object arg)
        //{
        //    List<double> measures = (List<double>)arg;
        //    AddValueToMeasures(value, row, column, measures);
        //}

        protected virtual void AddValueToMeasures(double value, int row, int column, List<double> measures)
        {
            measures.Add(value);
        }

        protected int Compare(double x, double y)
        {
            return x.CompareTo(y);
        }


    }
}
