using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class RotateCoordinatesMatrixFilter : PolarCoordinateTransformMatrixFilter
    {
        public RotateCoordinatesMatrixFilter(double angle)
            //: base(this.Modulate)
        {
            _angle = angle;
        }

        double _angle;
        public virtual double Angle
        {
            get { return _angle; }
        }

        //protected override Pair<double> Modulate(double x, double y)
        //{
        //    x = SolusEngine.ConvertZeroOneToNegOneOne(x);
        //    y = SolusEngine.ConvertZeroOneToNegOneOne(y);
        //
        //    double r = Math.Sqrt(x * x + y * y);
        //    double theta = Math.Atan2(y, x);
        //
        //    theta += _angle;
        //
        //    x = r * Math.Cos(theta);
        //    y = r * Math.Sin(theta);
        //
        //    x = SolusEngine.ConvertNegOneOneToZeroOne(x);
        //    y = SolusEngine.ConvertNegOneOneToZeroOne(y);
        //
        //    return new Pair<double>(x, y);
        //}

        protected override Pair<double> InternalModulate2(Pair<double> pair)
        {
            pair.Second += Angle;
            return pair;
        }
    }
}
