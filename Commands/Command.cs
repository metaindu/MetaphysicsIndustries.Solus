
namespace MetaphysicsIndustries.Solus.Commands
{
    public abstract class Command
    {
        public abstract void Execute(string input, SolusEnvironment env);

        public abstract string Name { get; }
        public virtual string DocString => "";
    }
}
