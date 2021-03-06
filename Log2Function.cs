using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class Log2Function : SingleArgumentFunction
    {
        public static readonly Log2Function Value = new Log2Function();

        protected Log2Function()
        {
            Name = "Logarithm of base two";
        }

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            return new Literal((float)Math.Log(args[0].Eval(env).Value, 2));
        }

        public override string DisplayName
        {
            get
            {
                return "log2";
            }
        }
    }
}
