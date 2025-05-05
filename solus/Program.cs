
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
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
using MetaphysicsIndustries.Solus.Evaluators;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Transformers;
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
                    var eval = new BasicEvaluator();
                    foreach (var exprString in exprStrings)
                    {
                        var expr = parser.GetExpression(exprString);
                        var result = eval.Eval(expr, env);
                        Console.WriteLine(result);
                    }
                }
                else
                {
                    Repl();
                }
            }
            catch (SolusException se)
            {
                Console.Write("There was an error with the calculation");
                if (verbose)
                {
                    Console.WriteLine(":");
                    Console.Write(se.Message);
                }
                Console.WriteLine();
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

        private static void Repl()
        {
            var parser = new SolusParser();
            var env = new SolusEnvironment();
            var varApplier = new ApplyVariablesTransform();
            IEvaluator eval = new BasicEvaluator();

            var le = new LineEditor("solus");
            string line;
            var cs = new CommandSet();
            cs.AddCommand(DeleteCommand.Value);
            cs.AddCommand(FuncAssignCommand.Value);
            cs.AddCommand(HelpCommand.Value);
            cs.AddCommand(VarAssignCommand.Value);
            cs.AddCommand(VarsCommand.Value);
            HelpCommand.SetCommands(cs);

            while ((line = le.Edit(">>> ", "")) != null)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                ICommandData[] commandDatas = null;
                Expression expr = null;
                Exception ex = null;

                try
                {
                    commandDatas = parser.GetCommands(line, cs);
                }
                catch (Exception _ex)
                {
                    ex = _ex;
                }

                if (ex != null)
                {
                    try
                    {
                        expr = parser.GetExpression(line);
                        ex = null;
                    }
                    catch (Exception _ex)
                    {
                        ex = _ex;
                    }
                }

                if (ex != null)
                {
                    if (ex is SolusException se)
                    {
                        Console.Write(
                            $"There was an error with the input: " +
                            $"{se.Message}");
                        if (verbose)
                        {
                            Console.WriteLine();
                            Console.Write(se.ToString());
                        }
                    }
                    else
                    {
                        Console.Write("There was an error");
                        if (verbose)
                        {
                            Console.WriteLine(":");
                            Console.Write(ex.ToString());
                        }
                    }

                    Console.WriteLine();

                    continue;
                }

                try
                {
                    if (commandDatas != null)
                    {
                        foreach (var cdata in commandDatas)
                            cdata.Command.Execute(line, env, cdata);
                    }
                    else if (expr != null)
                    {
                        var varApplied = varApplier.Transform(expr, env);
                        var result = eval.Simplify(varApplied, env);
                        Console.WriteLine(result);
                    }
                }
                catch (SolusException se)
                {
                    Console.Write(
                        $"There was an error with the calculation: " +
                        $"{se.Message}");
                    if (verbose)
                    {
                        Console.WriteLine();
                        Console.Write(se.ToString());
                    }

                    Console.WriteLine();
                }
                catch (Exception _ex)
                {
                    Console.Write("There was an error");
                    if (verbose)
                    {
                        Console.WriteLine(":");
                        Console.WriteLine(_ex.ToString());
                    }
                    else
                    {
                        Console.WriteLine();
                    }
                }
            }
        }

        static void ShowVersion()
        {
            var assembly = Assembly.GetAssembly(typeof(BasicEvaluator));
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
