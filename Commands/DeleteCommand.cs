using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class DeleteCommand : Command
    {
        public DeleteCommand(IEnumerable<string> names)
        {
            _names = names.ToArray();
        }

        private readonly string[] _names;
        
        public override string DocString =>
            @"delete - Delete one or more object

  delete <var> [<var>...]
  del <var> [<var>...]

  var
    The name of a variable, function, or macro.
";

        public override void Execute(string input, SolusEnvironment env)
        {
            var unknown = _names.Where(name => 
                !env.Variables.ContainsKey(name) && 
                !env.Functions.ContainsKey(name) && 
                !env.Macros.ContainsKey(name)).ToList();

            if (unknown.Count > 0)
            {
                var sb = new StringBuilder();
                sb.AppendLine("The following variables do not exist:");
                foreach (var s in unknown)
                    sb.AppendLine(s);
                Console.WriteLine(sb.ToString());
                return;
            }

            foreach (var name in _names)
            {
                env.Variables.Remove(name);
                env.Functions.Remove(name);
                env.Macros.Remove(name);
            }
            
            Console.WriteLine("The variables were deleted successfully.");
        }
    }
}