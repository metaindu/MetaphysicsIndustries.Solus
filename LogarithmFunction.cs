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

        protected override double InternalCall(double arg0, double arg1)
        {
            return Math.Log(arg0, arg1);
        }
    }
}
