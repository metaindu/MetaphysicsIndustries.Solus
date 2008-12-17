using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class UnitStepFunction : SingleArgumentFunction
    {
        public UnitStepFunction()
        {
            Name = "UnitStep";
        }

        protected override Literal InternalCall(VariableTable varTable, Literal[] args)
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
