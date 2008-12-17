using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class Log2Function : SingleArgumentFunction
    {
        public Log2Function()
        {
            Name = "Logarithm of base two";
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal(Math.Log(args[0].Eval(varTable).Value, 2));
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
