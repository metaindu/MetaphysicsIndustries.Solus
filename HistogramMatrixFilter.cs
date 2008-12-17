using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class HistogramMatrixFilter : MatrixFilter
    {
        public override Matrix Apply(Matrix input)
        {
            Matrix result = Matrix.FromUniform(1, 256, 256);

            int[] counts = new int[256];

            foreach (double value in input)
            {
                double value2 = 255 * Math.Max(0, Math.Min(1, value));

                counts[(int)Math.Round(value2)]++;
            }

            int max = counts[0];
            foreach (int value in counts)
            {
                max = (int)Math.Max(max, value);
            }

            int c;
            for (c = 0; c < 256; c++)
            {
                int height = 255 * counts[c] / max;
                int i;
                for (i = 0; i <= height; i++)
                {
                    result[c, 255 - i] = 0;
                }
            }

            return result;
        }
    }
}
