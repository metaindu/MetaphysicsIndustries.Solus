
using System;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class VarAssignCommand : Command
    {
        public static readonly VarAssignCommand Value =
            new VarAssignCommand(null, null);

        public VarAssignCommand(string name, Expression expr)
        {
            _name = name;
            _expr = expr;
        }

        private readonly string _name;
        private readonly Expression _expr;

        public override string Name => "var_assign";

        public override void Execute(string input, SolusEnvironment env)
        {
            env.Variables[_name] = _expr;
            Console.WriteLine($"{_name} := {_expr}");
        }
    }
}