using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class Macro
    {
        public string Name = string.Empty;
        public int NumArguments = 0;
        public bool HasVariableNumArgs = false;

        public abstract Expression InternalCall(IEnumerable<Expression> args, SolusEnvironment env);

        public virtual Expression Call(IEnumerable<Expression> args, SolusEnvironment env)
        {
            List<Expression> arglist = new List<Expression>(args);
            if (!HasVariableNumArgs &&
                arglist.Count != NumArguments)
            {
                throw new ArgumentException("Incorrect number of arguments.", "arg");
            }

            return InternalCall(args, env);
        }

        public virtual string DocString
        {
            get { return string.Empty; }
        }
    }
}
