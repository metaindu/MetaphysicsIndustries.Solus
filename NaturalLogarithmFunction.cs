using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class NaturalLogarithmFunction : SingleArgumentFunction
    {
        public static readonly NaturalLogarithmFunction Value = new NaturalLogarithmFunction();

        protected NaturalLogarithmFunction()
        {
            Name = "Natural Logarithm";
        }

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            return new Literal((float)Math.Log(args[0].Eval(env).Value));
        }

        public override string DisplayName
        {
            get
            {
                return "ln";
            }
        }

        public override string DocString
        {
            get
            {
                return "The natural logarithm function";
            }
        }
    }
}
