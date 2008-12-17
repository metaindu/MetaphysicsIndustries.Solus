using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class GaussianNoiseVectorFilter : VectorFilter
    {
        private static Random _rand = new Random();

        public GaussianNoiseVectorFilter(double mean, double variance)
        {
            _mean = mean;
            _variance = variance;
        }

        private double _mean;
        private double _variance;

        public override Vector Apply(Vector input)
        {
            Vector ret = new Vector(input.Length);
            int i;

            for (i = 0; i < input.Length; i++)
            {

                int j;
                int n = 20;
                double x = 0;

                for (j = 0; j < n; j++)
                {
                    x += _rand.NextDouble();
                }
                x -= n / 2.0f;
                x *= Math.Sqrt(12.0 / n);

                x = _mean + Math.Sqrt(_variance) * x;

                ret[i] = input[i]+ x;
            }
            return ret;
        }
    }
}
