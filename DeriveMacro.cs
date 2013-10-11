using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class DeriveMacro : Macro
    {
        public static readonly DeriveMacro Value = new DeriveMacro();

        protected DeriveMacro()
        {
            Name = "derive";
            NumArguments = 2;
        }

        public override Expression InternalCall(IEnumerable<Expression> args, Dictionary<string, Expression> vars)
        {
            return SolusParser1.ConvertDeriveExpression(args, vars);
        }
    }
}
