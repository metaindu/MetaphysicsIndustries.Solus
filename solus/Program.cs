
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
using NDesk.Options;
using System.Reflection;
using MetaphysicsIndustries.Solus;
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Commands;
using Mono.Terminal;

namespace solus
{
    class MainClass
    {
        static OptionSet _options;
        static bool showHelp = false;
        static bool showVersion = false;
        static bool verbose = false;

        public static void Main(string[] args)
        {
            var exprStrings = new List<string>();

            _options = new OptionSet() {
                {   "h|?|help",
                    "Print this help text and exit",
                    x => showHelp = true },
                {   "v|version",
                    "Print version and exit",
                    x => showVersion = true },
                {   "verbose",
                    "Print extra information with some subcommands",
                    x => verbose = true },
                {   "e=",
                    "Evaluate the expression and print the result",
                    x => exprStrings.Add(x) },
            };

            _options.Parse(args);

            try
            {
                if (showHelp)
                {
                    ShowUsage();
                    return;
                }
                else if (showVersion)
                {
                    ShowVersion();
                    return;
                }
                else if (exprStrings.Count > 0)
                {
                    var parser = new SolusParser();
                    var env = new SolusEnvironment();
                    foreach (var exprString in exprStrings)
                    {
                        var expr = parser.GetExpression(exprString, env);
                        var result = expr.Eval(env);
                        Console.WriteLine(result);
                    }
                }
                else
                {
                    //repl
                    var parser = new SolusParser();
                    var env = new SolusEnvironment();

                    var le = new LineEditor("solus");
                    string line;

                    while ((line = le.Edit(">>> ", "")) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        Command[] commands = null;
                        Expression expr = null;
                        Exception ex = null;

                        try
                        {
                            commands = parser.GetCommands(line, env);
                        }
                        catch (Exception _ex)
                        {
                            ex = _ex;
                        }

                        if (ex != null)
                        {
                            try
                            {
                                expr = parser.GetExpression(line, env);
                                ex = null;
                            }
                            catch (Exception _ex)
                            {
                                ex = _ex;
                            }
                        }

                        if (ex != null)
                        {
                            Console.Write("There was an error");
                            if (verbose)
                            {
                                Console.WriteLine(":");
                                Console.WriteLine(ex.ToString());
                            }
                            else
                                Console.WriteLine();
                            continue;
                        }

                        if (commands != null)
                        {
                            foreach (var command in commands)
                                command.Execute(line, env);
                        }
                        else if (expr != null)
                        {
                            var result = expr.PreliminaryEval(env);
                            Console.WriteLine(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Write("There was an internal error");
                if (verbose)
                {
                    Console.WriteLine(":");
                    Console.WriteLine(ex.ToString());
                }
                else
                {
                    Console.WriteLine();
                }
            }
        }

        static void ShowVersion()
        {
            var assembly = Assembly.GetAssembly(typeof(SolusEngine));
            var name = assembly.GetName();
            var version = name.Version;
            var versionString = version.ToString();
            Console.WriteLine("solus version " + versionString);
        }

        static void ShowUsage()
        {
            ShowVersion();
            Console.WriteLine();
            Console.WriteLine("Usage:");
            Console.WriteLine("    solus [options]");
            Console.WriteLine();

            _options.WriteOptionDescriptions(Console.Out);
        }
    }
}
