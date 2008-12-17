using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SobelMatrixFilter : MatrixFilter
    {
        public override Matrix Apply(Matrix input)
        {
            return GenerateMagnitudeMap(input);
        }

        public static Matrix GenerateMagnitudeMap(Matrix input)
        {
            return GenerateMaps(input, true, false).First;
        }

        public static Matrix GenerateDirectionMap(Matrix input)
        {
            return GenerateMaps(input, false, true).Second;
        }

        public static Pair<Matrix> GenerateMaps(Matrix input)
        {
            return GenerateMaps(input, true, true);
        }

        protected static Pair<Matrix> GenerateMaps(Matrix input, bool calcMagnitude, bool calcDirection)
        {
            Matrix x;
            Matrix y;
            GenerateGradients(input, out x, out y);

            return GenerateMaps(input, calcMagnitude, calcDirection, x, y);
        }

        public static Pair<Matrix> GenerateMaps(Matrix input, bool calcMagnitude, bool calcDirection, Matrix x, Matrix y)
        {

            Matrix magnitudeMap = input.CloneSize();
            Matrix directionMap = input.CloneSize();
            int i;
            int j;

            for (i = 0; i < input.RowCount; i++)
            {
                for (j = 0; j < input.ColumnCount; j++)
                {
                    double xx = x[i, j];
                    double yy = y[i, j];

                    if (calcMagnitude)
                    {
                        magnitudeMap[i, j] = Math.Sqrt(xx * xx + yy * yy);
                    }
                    if (calcDirection)
                    {
                        directionMap[i, j] = Math.Atan2(yy, xx);
                    }
                }
            }

            return new Pair<Matrix>(magnitudeMap, directionMap);
        }

        private static ExpandEdgeMatrixFilter _expandEdgeFilter = new ExpandEdgeMatrixFilter(1);

        public static void GenerateGradients(Matrix input, out Matrix x, out Matrix y)
        {
            Matrix gx = new Matrix(3, 3, 1, 0, -1, 2, 0, -2, 1, 0, -1);
            Matrix gy = new Matrix(3, 3, 1, 2, 1, 0, 0, 0, -1, -2, -1);


            x = _expandEdgeFilter.Apply(input);
            y = x;

            x = x.Convolution(gx);
            x = x.GetSlice(2, 2, input.RowCount, input.ColumnCount);

            y = y.Convolution(gy);
            y = y.GetSlice(2, 2, input.RowCount, input.ColumnCount);
        }
    }
}
