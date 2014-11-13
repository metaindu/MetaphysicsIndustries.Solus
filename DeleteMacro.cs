using System;
using System.Collections.Generic;
using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public class DeleteMacro : Macro
    {
        public static readonly DeleteMacro Value = new DeleteMacro();
        protected DeleteMacro()
        {
            Name = "delete";
            NumArguments = 1;
        }


        public override Expression InternalCall(IEnumerable<Expression> args, SolusEnvironment env)
        {
            var v = ((VariableAccess)args.First()).VariableName;

            Expression retval = new Literal(0);
            if (env.Variables.ContainsKey(v))
            {
                retval = env.Variables[v];
                env.Variables.Remove(v);
            }
            else if (env.Functions.ContainsKey(v))
            {
                env.Functions.Remove(v);
            }

            return retval;
        }

        public override string DocString
        {
            get
            {
                return "The derive operator\n  derive(f(x), x)\n\nReturns the derivative of f(x) with respect to x.";
            }
        }
    }
}

