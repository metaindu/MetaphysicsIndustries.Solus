using System;
using NDesk.Options;
using System.Reflection;
using MetaphysicsIndustries.Solus;
using System.Collections.Generic;
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
                        try
                        {
                            var expr = parser.GetExpression(line, env);
                            var result = expr.Eval(env);
                            Console.WriteLine(result);
                        }
                        catch (Exception ex)
                        {
                            Console.Write("There was an error");
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
