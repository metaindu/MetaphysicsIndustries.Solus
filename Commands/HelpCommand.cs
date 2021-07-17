using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class HelpCommand : Command
    {
        public static readonly HelpCommand Value = new HelpCommand(null);

        private static Dictionary<string, string> _helpLookups = 
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        static HelpCommand()
        {
            _helpLookups["solus"] = @"Solus - Mathematics tool";
            _helpLookups["t"] = "default time variable";
        }

        public HelpCommand(string topic)
        {
            _topic = topic;
        }

        private readonly string _topic;

        public override string Name => "help";
        public override string DocString =>
@"help - Get help about a topic

Get info about an object or topic:
  help <topic>

List the available topics:
  help list
";

        public override void Execute(string input, SolusEnvironment env)
        {
            var topic = _topic;
            if (string.IsNullOrEmpty(topic))
                topic = "help";
            var text = ConstructText(env, topic);
            Console.Write(text);
            if (!text.EndsWith("\n"))
                Console.WriteLine();
        }
        
        public string ConstructText(SolusEnvironment env, string topic = "help")
        {
            if (topic == "help")
                return DocString;

            if (env.Commands.ContainsKey(topic))
            {
                if (!string.IsNullOrEmpty(env.Commands[topic].DocString))
                    return env.Commands[topic].DocString;
                return "This command does not provide any information.";
            }

            if (env.Functions.ContainsKey(topic))
            {
                if (!string.IsNullOrEmpty(env.Functions[topic].DocString))
                    return env.Functions[topic].DocString;
                return "This function does not provide any information.";
            }

            if (env.Macros.ContainsKey(topic))
            {
                if (!string.IsNullOrEmpty(env.Macros[topic].DocString))
                    return env.Macros[topic].DocString;
                return "This macro does not provide any information.";
            }

            if (_helpLookups.ContainsKey(topic))
                return _helpLookups[topic];

            if (topic == "list")
                return ConstructListText(env);

            return "Unknown topic \"" + topic + "\"";
        }

        public static string ConstructListText(SolusEnvironment env)
        {
            var sb = new StringBuilder();
            var line = "";
            var newline = false;

            void AddItem(string item)
            {
                item = item + " ";
                if ((line + item).Length > 76)
                {
                    sb.AppendLine("  " + line);
                    line = "";
                }

                line += item;
            }

            if (env.Commands.Count > 0)
            {
                sb.AppendLine("Commands:");
                line = "";
                var commands = env.Commands.Keys.ToList();
                commands.Sort();
                foreach (var c in commands)
                    AddItem(c);
                if (line.Length > 0)
                    sb.AppendLine("  " + line);
                newline = true;
            }

            if (newline) sb.AppendLine();
            newline = false;

            if (env.Functions.Count > 0)
            {
                sb.AppendLine("Functions:");
                line = "";
                var functions = env.Functions.Keys.ToList();
                functions.Sort();
                foreach (var f in functions)
                    AddItem(f);
                if (line.Length > 0)
                    sb.AppendLine("  " + line);
                newline = true;
            }

            if (newline) sb.AppendLine();
            newline = false;

            if (env.Macros.Count > 0)
            {
                sb.AppendLine("Macros:");
                line = "";
                var macros = env.Macros.Keys.ToList();
                macros.Sort();
                foreach (var m in macros)
                    AddItem(m);
                if (line.Length > 0)
                    sb.AppendLine("  " + line);
                newline = true;
            }

            if (newline) sb.AppendLine();
            newline = false;

            if (env.Variables.Count > 0)
            {
                sb.AppendLine("Variables:");
                line = "";
                var variables = env.Variables.Keys.ToList();
                variables.Sort();
                foreach (var v in variables)
                    AddItem(v);
                if (line.Length > 0)
                    sb.AppendLine("  " + line);
                newline = true;
            }

            if (newline) sb.AppendLine();
            newline = false;

            if (_helpLookups.Count > 0)
            {
                sb.AppendLine("Additional topics:");
                line = "";
                var topics = _helpLookups.Keys.ToList();
                topics.Sort();
                foreach (var t in topics)
                    AddItem(t);
                if (line.Length > 0)
                    sb.AppendLine("  " + line);
            }

            return sb.ToString();
        }
    }
}