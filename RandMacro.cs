using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class RandMacro : Macro
    {
        public static readonly RandMacro Value = new RandMacro();

        protected RandMacro()
        {
            Name = "rand";
            NumArguments = 0;
        }

        public override Expression InternalCall(IEnumerable<Expression> args, Dictionary<string, Expression> vars)
        {
            return new RandomExpression();
        }
    }
}
