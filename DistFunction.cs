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

        protected override float InternalCall(float arg0, float arg1)
        {
            return (float)Math.Sqrt(arg0 * arg0 + arg1 * arg1);
        }

        public override string DisplayName
        {
            get { return "dist"; }
        }
    }
}
