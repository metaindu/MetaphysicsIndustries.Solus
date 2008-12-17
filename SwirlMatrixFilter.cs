using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SwirlMatrixFilter : PolarCoordinateTransformMatrixFilter
    {
        public SwirlMatrixFilter(double factor)
        {
            _factor = factor;
        }

        double _factor;
        public virtual double Factor
        {
            get { return _factor; }
        }

        //protected override Pair<double> Modulate(double x, double y)
        //{
        //    return Modulate(x, y, _factor);
        //}

        //protected static Pair<double> Modulate(double x,double y,double factor)
        //{
        //    Pair<double> pair = SolusEngine.ConvertZeroOneToNegOneOne(x, y);
        //
        //    pair = SolusEngine.ConvertEuclideanToPolar(pair.First, pair.Second);
        //
        //    if (pair.First >= -1 && pair.First <= 1)
        //    {
        //        pair.Second += (1 - pair.First) * factor;
        //    }
        //
        //    pair = SolusEngine.ConvertPolarToEuclidean(pair.First, pair.Second);
        //
        //    pair = SolusEngine.ConvertNegOneOneToZeroOne(pair.First, pair.Second);
        //
        //    return pair;
        //}

        protected override bool CheckCoordinates(Pair<double> pair)
        {
            if (pair.First >= -1 && pair.First <= 1)
            {
                return true;
            }

            return false;
        }

        protected override Pair<double> InternalModulate2(Pair<double> pair)
        {
            pair.Second += (1 - pair.First) * Factor;

            return pair;
        }
    }
}
