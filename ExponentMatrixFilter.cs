using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class ExponentMatrixFilter : ModulatorMatrixFilter
    {
        public ExponentMatrixFilter()
            : this(1)
        {
        }

        public ExponentMatrixFilter(double gamma)
        {
            _gamma = gamma;
        }

        private double _gamma;
        public double Gamma
        {
            get { return _gamma; }
            set { _gamma = value; }
        }

        public override double Modulate(double x)
        {
            return Math.Pow(x, _gamma);
        }
    }
}
