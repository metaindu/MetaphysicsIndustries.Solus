using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SimpleScaleMatrixFilter : MatrixFilter
    {
        public SimpleScaleMatrixFilter(int scaleFactor)
        {
            if (scaleFactor < 2) { throw new ArgumentException("scaleFactor must be greater than 1"); }

            _scaleFactor = scaleFactor;
        }

        private int _scaleFactor;

        public override Matrix Apply(Matrix input)
        {
            int i;
            int j;

            int r = input.RowCount * _scaleFactor;
            int c = input.ColumnCount * _scaleFactor;

            Matrix result = new Matrix(input.RowCount * _scaleFactor, input.ColumnCount * _scaleFactor);

            for (i = 0; i < r; i++)
            {
                for (j = 0; j < c; j++)
                {
                    result[i, j] = input[i / _scaleFactor, j / _scaleFactor];
                }
            }

            return result;
        }
    }
}
