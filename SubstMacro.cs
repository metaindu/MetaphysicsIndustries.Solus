using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SubstMacro : Macro
    {
        public static readonly SubstMacro Value = new SubstMacro();

        protected SubstMacro()
        {
            Name = "subst";
            NumArguments = 3;
        }

        public override Expression InternalCall(IEnumerable<Expression> args, Dictionary<string, Expression> vars)
        {
            return SolusParser1.ConvertSubstExpression(args, vars);
        }
    }
}
