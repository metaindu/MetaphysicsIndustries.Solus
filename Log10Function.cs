using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class Log10Function : SingleArgumentFunction
    {
        public static readonly Log10Function Value = new Log10Function();

        protected Log10Function()
        {
            Name = "Logarithm of base ten";
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal((float)Math.Log10(args[0].Eval(varTable).Value));
        }

        public override string DisplayName
        {
            get
            {
                return "log10";
            }
        }
    }
}
