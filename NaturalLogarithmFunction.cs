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

        protected override Literal InternalCall(Dictionary<string, Expression> varTable, Literal[] args)
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
