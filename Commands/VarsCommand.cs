
using System;
using System.Text;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class VarsCommand : Command
    {
        public override string DocString =>
            @"vars - Print a list of all defined variables";

        public override void Execute(string input, SolusEnvironment env)
        {
            var sb = new StringBuilder();
            foreach (var name in env.Variables.Keys)
            {
                var value = env.Variables[name];
                var valueString = value.ToString();

                if (value is SolusVector vector)
                {
                    valueString = "Vector (" + vector.Length.ToString() + ")";
                }
                else if (value is SolusMatrix mat)
                {
                    valueString = "Matrix (" + mat.RowCount + ", " + mat.ColumnCount + ")";
                }

                sb.AppendLine($"{name} = {valueString}");
            }
            
            Console.WriteLine(sb.ToString());
        }
    }
}