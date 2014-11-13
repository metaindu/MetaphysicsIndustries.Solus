using System;
using System.Collections.Generic;
using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public class AssignMacro : Macro
    {
        public static readonly AssignMacro Value = new AssignMacro();
        protected AssignMacro()
        {
            Name = "assign";
            HasVariableNumArgs = true;
        }


        public override Expression InternalCall(IEnumerable<Expression> args, SolusEnvironment env)
        {
            var args2 = args.ToList();
            var v = ((VariableAccess)args2[0]).VariableName;
            var value = args2[args2.Count-1].PreliminaryEval(env);
            if (args2.Count > 2)
            {
                var funcargs = args2.Skip(1).Take(args2.Count - 2).Select(e => ((VariableAccess)e).VariableName);
                env.Functions[v] = new UserDefinedFunction(v, funcargs.ToArray(), value);
            }
            else
            {
                env.Variables[v] = value;
            }

            return value;
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

