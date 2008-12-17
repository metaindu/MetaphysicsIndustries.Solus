using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public abstract class SingleArgumentFunction : Function
    {
        public SingleArgumentFunction()
        {
            Types.Add(typeof(Expression));
        }
    }
}
