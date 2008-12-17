using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class LogarithmFunction : DualArgumentFunction
    {
        public LogarithmFunction()
            : base("Logarithm")
        {
        }

        public override string DisplayName
        {
            get
            {
                return "log";
            }
        }

        protected override float InternalCall(float arg0, float arg1)
        {
            return (float)Math.Log(arg0, arg1);
        }
    }
}
