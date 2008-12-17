using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class Log10Function : SingleArgumentFunction
    {
        public Log10Function()
        {
            Name = "Logarithm of base ten";
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal(Math.Log10(args[0].Eval(varTable).Value));
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
