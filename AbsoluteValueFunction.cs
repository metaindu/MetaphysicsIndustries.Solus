using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class AbsoluteValueFunction : SingleArgumentFunction
    {
        public static readonly AbsoluteValueFunction Value = new AbsoluteValueFunction();

        protected AbsoluteValueFunction()
        {
            Name = "Absolue Value";
        }

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            return new Literal(Math.Abs(args[0].Eval(env).Value));
        }

        public override string DisplayName
        {
            get
            {
                return "abs";
            }
        }

        public override string DocString
        {
            get
            {
                return "The absolute value function\n  abs(x)\n\nReturns the absolute value of x, x for (x >= 0) and -x for (x < 0).";
            }
        }
    }
}
