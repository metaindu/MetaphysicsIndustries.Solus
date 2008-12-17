using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SsimErrorMeasure
    {

        public SsimErrorMeasure()
        {
            int windowSize = 7;

            _windowSize = windowSize;
        }

        private int _windowSize;

        public double Measure(Matrix x, Matrix y)
        {
            if (x == null) { throw new ArgumentNullException("a"); }
            if (y == null) { throw new ArgumentNullException("b"); }
            if (x.RowCount != y.RowCount ||
                x.ColumnCount != y.ColumnCount)
            {
                throw new ArgumentException("Matrix sizes do not match");
            }

            Matrix map = GenerateMap(x, y);

            double mssim = CalculateMeasureFromMap(map);

            return mssim;
        }

        public static double CalculateMeasureFromMap(Matrix map)
        {
            double mssim = 0;
            foreach (double value in map)
            {
                mssim += value;
            }

            mssim /= map.Count;
            return mssim;
        }

        public Matrix GenerateMap(Matrix x, Matrix y)
        {
            return GenerateMap(x, y, _windowSize);
        }

        public static Matrix GenerateMap(Matrix x, Matrix y, int windowSize)
        {
            Matrix map = x.CloneSize();

            int r;
            int c;

            double L = 1;

            double k1 = 0.01;
            double k2 = 0.03;

            double c1 = k1 * k1 * L * L;
            double c2 = k2 * k2 * L * L;
            double c3 = c2 / 2;

            double alpha = 1;
            double beta = 1;
            double gamma = 1;




            for (r = 0; r < x.RowCount; r++)
            {
                for (c = 0; c < x.ColumnCount; c++)
                {
                    double xMean = 0;
                    double yMean = 0;
                    double xSigma = 0;
                    double ySigma = 0;
                    double sigma2 = 0;

                    int i;
                    int j;
                    int w2 = windowSize / 2;

                    int count;

                    //Pair<double> pair = new Pair<double>(0,0);

                    count = 0;
                    for (i = Math.Max(r - w2, 0); i <= r + w2; i++)
                    {
                        if (i >= x.RowCount) { break; }

                        for (j = Math.Max(c - w2, 0); j <= c + w2; j++)
                        {
                            if (j >= x.ColumnCount) { break; }

                            xMean += x[i, j];
                            yMean += y[i, j];

                            count++;
                        }
                    }

                    xMean /= count;
                    yMean /= count;

                    count = 0;
                    for (i = Math.Max(r - w2, 0); i <= r + w2; i++)
                    {
                        if (i >= x.RowCount) { break; }

                        for (j = Math.Max(c - w2, 0); j <= c + w2; j++)
                        {
                            if (j >= x.ColumnCount) { break; }

                            double xValue = x[i, j] - xMean;
                            xSigma += xValue * xValue;

                            double yValue = y[i, j] - yMean;
                            ySigma += yValue * yValue;

                            sigma2 += xValue * yValue;

                            count++;
                        }
                    }

                    xSigma = Math.Sqrt(xSigma / (count - 1));
                    ySigma = Math.Sqrt(ySigma / (count - 1));
                    sigma2 /= (count - 1);


                    double lum = (2 * xMean * yMean + c1) / (xMean * xMean + yMean * yMean + c1);
                    double con = (2 * xSigma * ySigma + c2) / (xSigma * xSigma + ySigma * ySigma + c2);
                    double str = (sigma2 + c3) / (xSigma * ySigma + c3);

                    double ssim = Math.Pow(lum, alpha) *
                                    Math.Pow(con, beta) *
                                    Math.Pow(str, gamma);

                    map[r, c] = ssim;
                }
            }
            return map;
        }

        public static double Measure(Matrix x, Matrix y, int windowSize)
        {
            return CalculateMeasureFromMap(GenerateMap(x, y, windowSize));
        }
    }
}
