using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class WeightedPMatrixFilter : OrderStatisticMatrixFilter
    {
        public WeightedPMatrixFilter(double pFactor, Matrix weights)
            : base(Math.Min(weights.RowCount, weights.ColumnCount))
        {
            //_operator = new PFactorOperator(pFactor, weights);
            _pFactor = Math.Max(0, Math.Min(1, pFactor));
            _weights = weights.Clone();
        }

        //protected class PFactorOperator : IPerPixelOperator
        //{
        //    public PFactorOperator(double pFactor, Matrix weights)
        //    {
        //        int i;
        //        int j;
        //        double w;

        //        _weights = new double[weights.RowCount, weights.ColumnCount];
        //        _weightTotal = 0;
        //        for (i = 0; i < weights.RowCount; i++)
        //        {
        //            for (j = 0; j < weights.ColumnCount; j++)
        //            {
        //                w = weights[i, j];
        //                _weightTotal += w;
        //                _weights[i, j] = w;
        //            }
        //        }

        //        _radius = (int)(Math.Min(weights.RowCount, weights.ColumnCount) / 2.0);

        //        _pFactor = Math.Max(0, Math.Min(1, pFactor));

        //        _measures = new List<double>(Math.Max((int)Math.Ceiling(_weightTotal), weights.Count));
        //    }

        //    List<double> _measures;
        //    private int _radius;
        //    private double[,] _weights;
        //    private double _weightTotal;
        //    private double[,] _values;
        //    private double _pFactor;
        //    public int GetExtraWidth(Matrix input) { return 0; }
        //    public int GetExtraHeight(Matrix input) { return 0; }
        //    public void SetValues(double[,] values)
        //    {
        //        _values = values;
        //    }

        //    public double Operate(int row, int column)
        //    {
        //        int i;
        //        int j;
        //        int x;
        //        double value;
        //        double weight;
        //        int width = _values.GetLength(0);
        //        int height = _values.GetLength(1);

        //        _measures.Clear();

        //        for (i = 0; i < 2 * _radius + 1; i++)
        //        {
        //            if (row - _radius + i < 0) { continue; }
        //            if (row - _radius + i >= width) { break; }

        //            for (j = 0; j < 2 * _radius + 1; j++)
        //            {
        //                if (column - _radius + j < 0) { continue; }
        //                if (column - _radius + j >= height) { break; }

        //                value = _values[row - _radius + i, column - _radius + j];
        //                weight = _weights[i, j];

        //                if (weight < 0)
        //                {
        //                    weight = -weight;
        //                    value = -value;
        //                }

        //                for (x = 0; x < weight; x++)
        //                {
        //                    _measures.Add(value);
        //                }
        //            }
        //        }

        //        _measures.Sort(Compare);

        //        int index = (int)Math.Ceiling((_measures.Count - 1) * _pFactor);
        //        return _measures[index];
        //    }

        //    protected int Compare(double x, double y)
        //    {
        //        return x.CompareTo(y);
        //    }

        //}

        //private PFactorOperator _operator;

        //public override Matrix Apply(Matrix input)
        //{
        //    return input.PerPixelOperation(_operator);
        //}

        private double _pFactor;
        private Matrix _weights;

        protected override void AddValueToMeasures(double value, int row, int column, List<double> measures)
        {
            int x;
            double weight;

            weight = _weights[row, column];

            if (weight < 0)
            {
                weight = -weight;
                value = -value;
            }

            for (x = 0; x < weight; x++)
            {
                measures.Add(value);
            }
        }

        protected override double SelectValueFromOrderedMeasures(List<double> measures)
        {
            int index = (int)Math.Ceiling((measures.Count - 1) * _pFactor);
            return measures[index];
        }

    }
}
