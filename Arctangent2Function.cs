using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class Arctangent2Function : DualArgumentFunction
    {
        public static readonly Arctangent2Function Value = new Arctangent2Function();

        protected Arctangent2Function()
            : base ("Arctangent 2")
        {
        }

        protected override float InternalCall(float arg0, float arg1)
        {
            return (float)Math.Atan2(arg0, arg1);
        }

        public override string DisplayName
        {
            get
            {
                return "atan2";
            }
        }

        public override string DocString
        {
            get
            {
                return "The atan2 function\n  atan(y, x)\n\nReturns the arctangent of y/x.";
            }
        }
    }
}
