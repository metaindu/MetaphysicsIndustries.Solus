using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class DistFunction : DualArgumentFunction
    {
        public DistFunction()
            : base("Distance")
        {
        }

        protected override double InternalCall(double arg0, double arg1)
        {
            return Math.Sqrt(arg0 * arg0 + arg1 * arg1);
        }

        public override string DisplayName
        {
            get { return "dist"; }
        }
    }
}
