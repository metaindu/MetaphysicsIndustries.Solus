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

        public override Expression InternalCall(IEnumerable<Expression> args, SolusEnvironment env)
        {
            return new FunctionCall(ExponentOperation.Value, args.First(), new Literal(0.5f));
        }

        public override string DocString
        {
            get
            {
                return "square root";
            }
        }
    }
}
