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

        protected override Literal InternalCall(Dictionary<string, Expression> varTable, Literal[] args)
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
