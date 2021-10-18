
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
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
        public static readonly HelpCommand Value = new HelpCommand(null,
            null);

        private static Dictionary<string, string> _helpLookups = 
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        static HelpCommand()
        {
            _helpLookups["solus"] = @"Solus - Mathematics tool";
            _helpLookups["t"] = "default time variable";
        }

        public HelpCommand(string topic, CommandSet commands)
        {
            _topic = topic;
            _commands = commands;
        }

        private readonly string _topic;
        private readonly CommandSet _commands;

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

            if (_commands.ContainsCommand(topic))
            {
                var command = _commands.GetCommand(topic);
                if (!string.IsNullOrEmpty(command.DocString))
                    return command.DocString;
                return "This command does not provide any information.";
            }

            if (env.ContainsFunction(topic))
            {
                var f = env.GetFunction(topic);
                if (!string.IsNullOrEmpty(f.DocString))
                    return f.DocString;
                return "This function does not provide any information.";
            }

            if (env.ContainsMacro(topic))
            {
                var m = env.GetMacro(topic);
                if (!string.IsNullOrEmpty(m.DocString))
                    return m.DocString;
                return "This macro does not provide any information.";
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

            if (commandSet.CountCommands() > 0)
            {
                sb.AppendLine("Commands:");
                line = "";
                var commands = commandSet.GetCommandNames().ToList();
                commands.Sort();
                foreach (var c in commands)
                    AddItem(c);
                if (line.Length > 0)
                    sb.AppendLine("  " + line);
                newline = true;
            }

            if (newline) sb.AppendLine();
            newline = false;

            if (env.CountFunctions() > 0)
            {
                sb.AppendLine("Functions:");
                line = "";
                var functions = env.GetFunctionNames().ToList();
                functions.Sort();
                foreach (var f in functions)
                    AddItem(f);
                if (line.Length > 0)
                    sb.AppendLine("  " + line);
                newline = true;
            }

            if (newline) sb.AppendLine();
            newline = false;

            if (env.CountMacros() > 0)
            {
                sb.AppendLine("Macros:");
                line = "";
                var macros = env.GetMacroNames().ToList();
                macros.Sort();
                foreach (var m in macros)
                    AddItem(m);
                if (line.Length > 0)
                    sb.AppendLine("  " + line);
                newline = true;
            }

            if (newline) sb.AppendLine();
            newline = false;

            if (env.CountVariables() > 0)
            {
                sb.AppendLine("Variables:");
                line = "";
                var variables = env.GetVariableNames().ToList();
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