using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class AbsoluteValueFunction : SingleArgumentFunction
    {
        public AbsoluteValueFunction()
        {
            Name = "Absolue Value";
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
        {
            return new Literal(Math.Abs(args[0].Eval(varTable).Value));
        }

        public override string DisplayName
        {
            get
            {
                return "abs";
            }
        }
    }
}
