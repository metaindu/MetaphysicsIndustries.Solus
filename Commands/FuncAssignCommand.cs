using System;
using System.Linq;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class FuncAssignCommand : Command
    {
        public static readonly FuncAssignCommand Value =
            new FuncAssignCommand(null);

        public FuncAssignCommand(UserDefinedFunction func)
        {
            _func = func;
        }

        private readonly UserDefinedFunction _func;

        public override string Name => "func_assign";

        public override void Execute(string input, SolusEnvironment env)
        {
            if (env.Functions.ContainsKey(_func.DisplayName))
                env.Functions.Remove(_func.DisplayName);
            env.AddFunction(_func);
            
            var varrefs = _func.Argnames.Select(x => new VariableAccess(x));
            var fcall = new FunctionCall(_func, varrefs);
            Console.WriteLine($"{fcall} := {_func.Expression}");
        }
    }
}