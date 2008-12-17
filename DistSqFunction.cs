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

        protected override float InternalCall(float arg0, float arg1)
        {
            return arg0 * arg0 + arg1 * arg1;
        }
    }
}
