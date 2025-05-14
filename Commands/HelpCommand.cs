
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MetaphysicsIndustries.Solus.Commands
{
    public class HelpCommand : Command
    {
        public static readonly HelpCommand Value = new HelpCommand();

        private static Dictionary<string, string> _helpLookups =
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        static HelpCommand()
        {
            _helpLookups["solus"] = @"Solus - Mathematics tool";
            _helpLookups["t"] = "default time variable";
        }

        public static void SetCommands(CommandSet commandSet)
        {
            _commands = commandSet;
        }

        private static CommandSet _commands = new CommandSet();

        public override string Name => "help";

        public override string DocString =>
            @"help - Get help about a topic

Get info about an object or topic:
  help <topic>

List the available topics:
  help list
";

        public override void Execute(string input, SolusEnvironment env,
            ICommandData data)
        {
            var topic = ((HelpCommandData) data).Topic;
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

            if (_commands.ContainsCommand(topic))
            {
                var command = _commands.GetCommand(topic);
                if (!string.IsNullOrEmpty(command.DocString))
                    return command.DocString;
                return "This command does not provide any information.";
            }

            if (env.ContainsVariable(topic))
            {
                var v = env.GetVariable(topic);
                if (!string.IsNullOrEmpty(v.DocString))
                    return v.DocString;
                if (v.IsIsFunction(env))
                    return "This function does not provide any information.";
                return "This object does not provide any information.";
            }

            if (_helpLookups.ContainsKey(topic))
                return _helpLookups[topic];

            if (topic == "list")
                return ConstructListText(env, _commands);

            return "Unknown topic \"" + topic + "\"";
        }

        public static string ConstructListText(SolusEnvironment env,
            CommandSet commandSet)
        {
            var sb = new StringBuilder();
            var line = "";
            var newline = false;

            var functions = new List<string>();
            var variables = new List<string>();
            var types = new List<string>();
            var values = new List<string>();

            foreach (var name in env.GetVariableNames())
            {
                var v = env.GetVariable(name);
                if (v.IsIsFunction(env))
                    functions.Add(name);
                else if (v is ISet)
                    types.Add(name);
                else if (v is Values.Boolean)
                    values.Add(name);
                else
                    variables.Add(name);
            }

            var sections = new List<Tuple<string, List<string>>>
            {
                new Tuple<string, List<string>>("Commands",
                    commandSet.GetCommandNames().ToList()),
                new Tuple<string, List<string>>("Functions", functions),
                new Tuple<string, List<string>>("Types", types),
                new Tuple<string, List<string>>("Values", values),
                new Tuple<string, List<string>>("Variables", variables),
                new Tuple<string, List<string>>("Additional topics",
                    _helpLookups.Keys.ToList()),
            };

            bool first = true;
            foreach (var section in sections)
            {
                if (!first && newline) sb.AppendLine();
                newline = false;
                first = false;

                var heading = section.Item1;
                var names = section.Item2;

                if (names.Count > 0)
                {
                    sb.AppendLine($"{heading}:");
                    line = "";
                    names.Sort();
                    foreach (var name in names)
                    {
                        var item = name + " ";
                        if ((line + item).Length > 76)
                        {
                            sb.AppendLine("  " + line);
                            line = "";
                        }

                        line += item;
                    }

                    if (line.Length > 0)
                        sb.AppendLine("  " + line);
                    newline = true;
                }
            }

            return sb.ToString();
        }
    }

    public class HelpCommandData : ICommandData
    {

        public HelpCommandData(string topic)
        {
            Topic = topic;
        }

        public Command Command => HelpCommand.Value;
        public string Topic { get; }
    }
}
