using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class Arctangent2Function : DualArgumentFunction
    {
        public Arctangent2Function()
            : base ("Arctangent 2")
        {
        }

        protected override double InternalCall(double arg0, double arg1)
        {
            return Math.Atan2(arg0, arg1);
        }

        public override string DisplayName
        {
            get
            {
                return "atan2";
            }
        }
    }
}
