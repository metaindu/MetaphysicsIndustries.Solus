using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class DualArgumentFunction : Function
    {
        protected DualArgumentFunction(string name)
            : base(name)
        {
            Types.Clear();
            Types.Add(typeof(Expression));
            Types.Add(typeof(Expression));
        }

        protected override sealed Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            return new Literal(InternalCall(args[0].Value, args[1].Value));
        }

        protected abstract float InternalCall(float arg0, float arg1);
    }
}
