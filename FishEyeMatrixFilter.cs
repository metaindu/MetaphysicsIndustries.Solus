using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class FishEyeMatrixFilter : PolarCoordinateTransformMatrixFilter
    {
        //public FishEyeMatrixFilter()
        //    : base(Modulate)
        //{
        //}

        //protected override Pair<double> Modulate(double x, double y)
        //{
        //    Pair<double> pair = SolusEngine.ConvertZeroOneToNegOneOne(x, y);
        //
        //    pair = SolusEngine.ConvertEuclideanToPolar(pair.First, pair.Second);
        //
        //    //r /= Math.Sqrt(2);
        //
        //    if (pair.First >= -1 && pair.First <= 1)
        //    {
        //        pair.First = (float)(2 * Math.Asin(pair.First) / Math.PI);
        //    }
        //
        //    //r *= Math.Sqrt(2);
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
            pair.First = (float)(2 * Math.Asin(pair.First) / Math.PI);

            return pair;
        }
    }
}
