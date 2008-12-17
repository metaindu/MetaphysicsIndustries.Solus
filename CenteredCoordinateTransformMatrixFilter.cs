using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class CenteredCoordinateTransformMatrixFilter : CoordinateTransformMatrixFilter
    {
        protected override Pair<double> Modulate(double x, double y)
        {
            Pair<double> pair = SolusEngine.ConvertZeroOneToNegOneOne(x, y);

            pair = InternalModulate(pair);

            return SolusEngine.ConvertNegOneOneToZeroOne(pair.First, pair.Second);
        }

        protected abstract Pair<double> InternalModulate(Pair<double> pair);
    }
}
