using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class Macro
    {
        public string Name;
        public int NumArguments;
        public bool HasVariableNumArgs;

        public abstract Expression InternalCall(IEnumerable<Expression> args, Dictionary<string, Expression> vars);

        public virtual Expression Call(IEnumerable<Expression> args, Dictionary<string, Expression> vars)
        {
            List<Expression> arglist = new List<Expression>(args);
            if (!HasVariableNumArgs &&
                arglist.Count != NumArguments)
            {
                throw new ArgumentException("Incorrect number of arguments.", "arg");
            }

            return InternalCall(args, vars);
        }
    }
}
