using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus.Commands
{
    public abstract class Command
    {
        public abstract void Execute(string input, SolusEnvironment env);

        public virtual string DocString => "";
    }
}
