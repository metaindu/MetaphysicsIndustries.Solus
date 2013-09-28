using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class SingleArgumentFunction : Function
    {
        protected SingleArgumentFunction()
        {
            Types.Add(typeof(Expression));
        }
    }
}
