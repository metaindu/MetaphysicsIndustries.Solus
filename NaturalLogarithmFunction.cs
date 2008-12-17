using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class NaturalLogarithmFunction : SingleArgumentFunction
    {
        public NaturalLogarithmFunction()
        {
            Name = "Natural Logarithm";
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal((float)Math.Log(args[0].Eval(varTable).Value));
        }

        public override string DisplayName
        {
            get
            {
                return "ln";
            }
        }
    }
}
