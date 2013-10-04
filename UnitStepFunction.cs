using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class UnitStepFunction : SingleArgumentFunction
    {
        public static readonly UnitStepFunction Value = new UnitStepFunction();

        protected UnitStepFunction()
        {
            Name = "UnitStep";
        }

        protected override Literal InternalCall(Dictionary<string, Expression> varTable, Literal[] args)
        {
            if (args[0].Value >= 0)
            {
                return new Literal(1);
            }
            else
            {
                return new Literal(0);
            }
        }

        public override string DisplayName
        {
            get
            {
                return "u";
            }
        }
    }
}
