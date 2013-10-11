using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class SqrtMacro : Macro
    {
        public static readonly SqrtMacro Value = new SqrtMacro();

        protected SqrtMacro()
        {
            Name = "sqrt";
            NumArguments = 1;
        }

        public override Expression InternalCall(IEnumerable<Expression> args, Dictionary<string, Expression> vars)
        {
            return SolusParser1.ConvertSqrtFunction(args, vars);
        }
    }
}
