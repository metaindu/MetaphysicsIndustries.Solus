using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ExpandEdgeMatrixFilter : MatrixFilter
    {
        public ExpandEdgeMatrixFilter(int borderWidth)
        {
            if (borderWidth < 1) { throw new ArgumentException("borderWidth"); }

            _borderWidth = borderWidth;
        }

        private int _borderWidth;
        public int BorderWidth
        {
            get { return _borderWidth; }
            set { _borderWidth = value; }
        }

        public override Matrix Apply(Matrix input)
        {
            int i = 2*BorderWidth+1;

            Matrix result = new Matrix(input.RowCount + 2 * BorderWidth, input.ColumnCount + 2 * BorderWidth);

            int j;
            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    result[i + BorderWidth, j + BorderWidth] = input[i, j];
                }
            }
            for (i = 0; i < BorderWidth; i++)
            {
                for (j = 0; j < BorderWidth; j++)
                {
                    result[i, j] = input[0, 0];
                    result[result.RowCount - i - 1, j] = input[input.RowCount - 1, 0];
                    result[i, result.ColumnCount - j - 1] = input[0, input.ColumnCount - 1];
                    result[result.RowCount - i - 1, result.ColumnCount - j - 1] = input[input.RowCount - 1, input.ColumnCount - 1];
                }
                for (j = 0; j < input.ColumnCount; j++)
                {
                    result[i, j + BorderWidth] = input[0, j];
                    result[result.RowCount - i - 1, BorderWidth + j] = input[input.RowCount - 1, j];
                }
            }
            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < BorderWidth; j++)
                {
                    result[BorderWidth + i, j] = input[i, 0];
                    result[BorderWidth + i, result.ColumnCount - j - 1] = input[i, input.ColumnCount - 1];
                }
            }

            return result;
        }
    }
}
