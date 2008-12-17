using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class PolarCoordinateTransformMatrixFilter : CenteredCoordinateTransformMatrixFilter
    {
        protected override Pair<double> InternalModulate(Pair<double> pair)
        {
            Pair<double> pair2 = SolusEngine.ConvertEuclideanToPolar(pair.First, pair.Second);

            if (!CheckCoordinates(pair2))
            {
                return pair;
            }

            pair2 = InternalModulate2(pair2);

            return SolusEngine.ConvertPolarToEuclidean(pair2.First, pair2.Second);
        }

        protected virtual bool CheckCoordinates(Pair<double> pair)
        {
            return true;
        }

        protected abstract Pair<double> InternalModulate2(Pair<double> pair);
    }
}
