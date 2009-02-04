using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class DistSqFunction : DualArgumentFunction
    {
        public DistSqFunction()
            : base("DistanceSquared")
        {
        }

        public override string DisplayName
        {
            get { return "distsq"; }
        }

        protected override double InternalCall(double arg0, double arg1)
        {
            return arg0 * arg0 + arg1 * arg1;
        }
    }
}
