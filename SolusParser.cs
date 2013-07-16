/*****************************************************************************
 *                                                                           *
 *  SolusParser.cs                                                           *
 *  13 July 2006                                                             *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 January 2008                              *
 *                                                                           *
 *  A rudimentary parser kludge, taken from MathPaint.                       *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public partial class SolusParser : ExParser
    {
        public SolusParser()
        {
            foreach (ParseFunction func in _builtinFunctions)
            {
                AddFunction(func);
            }
        }

        public struct ParseFunction
        {
            public string Token;
            public FunctionConverter Converter;
            public int NumArguments;
            public bool HasVariableNumArgs;
        }

        protected static readonly List<ParseFunction> _builtinFunctions = new List<ParseFunction>()
        {
            new ParseFunction(){ Token="sin",       Converter=BasicFunctionConverter(Function.Sine),              NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="cos",       Converter=BasicFunctionConverter(Function.Cosine),            NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="tan",       Converter=BasicFunctionConverter(Function.Tangent),           NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="ln",        Converter=BasicFunctionConverter(Function.NaturalLogarithm),  NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="log2",      Converter=BasicFunctionConverter(Function.Log2),              NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="log10",     Converter=BasicFunctionConverter(Function.Log10),             NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="sqrt",      Converter=ConvertSqrtFunction,                                NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="rand",      Converter=(args, vars) => { return new RandomExpression(); }, NumArguments=0,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="abs",       Converter=BasicFunctionConverter(Function.AbsoluteValue),     NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="sec",       Converter=BasicFunctionConverter(Function.Secant),            NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="csc",       Converter=BasicFunctionConverter(Function.Cosecant),          NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="cot",       Converter=BasicFunctionConverter(Function.Cotangent),         NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="acos",      Converter=BasicFunctionConverter(Function.Arccosine),         NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="asin",      Converter=BasicFunctionConverter(Function.Arcsine),           NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="atan",      Converter=BasicFunctionConverter(Function.Arctangent),        NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="asec",      Converter=BasicFunctionConverter(Function.Arcsecant),         NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="acsc",      Converter=BasicFunctionConverter(Function.Arccosecant),       NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="acot",      Converter=BasicFunctionConverter(Function.Arccotangent),      NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="ceil",      Converter=BasicFunctionConverter(Function.Ceiling),           NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="floor",     Converter=BasicFunctionConverter(Function.Floor),             NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="unitstep",  Converter=BasicFunctionConverter(Function.UnitStep),          NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="atan2",     Converter=BasicFunctionConverter(Function.Arctangent2),       NumArguments=2,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="log",       Converter=BasicFunctionConverter(Function.Logarithm),         NumArguments=2,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="derive",    Converter=ConvertDeriveExpression,                            NumArguments=2,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="if",        Converter=BasicFunctionConverter(Function.If),                NumArguments=3,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="dist",      Converter=BasicFunctionConverter(Function.Dist),              NumArguments=2,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="distsq",    Converter=BasicFunctionConverter(Function.DistSq),            NumArguments=2,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="feedback",  Converter=ConvertFeedbackExpression,                          NumArguments=2,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="subst",     Converter=ConvertSubstExpression,                             NumArguments=3,  HasVariableNumArgs=false },
        };

        private Dictionary<string, ParseFunction> _functions = new Dictionary<string, ParseFunction>(StringComparer.CurrentCultureIgnoreCase);

        public void AddFunction(ParseFunction func)
        {
            if (_functions.ContainsKey(func.Token)) throw new ArgumentException("A function already uses that token.", "func");

            _functions.Add(func.Token, func);
        }

        public override ParseFunction? GetFunction(string token)
        {
            if (!_functions.ContainsKey(token)) return null;

            return _functions[token];
        }

        public delegate Expression FunctionConverter(IEnumerable<Expression> args, VariableTable vars);

        public static FunctionConverter BasicFunctionConverter(Function function)
        {
            return (args, vars) => { return new FunctionCall(function, args); };
        }

        private static Expression ConvertSubstExpression(IEnumerable<Expression> args, VariableTable varTable)
        {
            SubstTransformer subst = new SubstTransformer();
            CleanUpTransformer cleanup = new CleanUpTransformer();
            return cleanup.CleanUp(subst.Subst(args.First(), ((VariableAccess)args.ElementAt(1)).Variable, args.ElementAt(2)));
        }

        private static Expression ConvertFeedbackExpression(IEnumerable<Expression> args, VariableTable varTable)
        {
            Expression g = args.ElementAt(0);
            Expression h = args.ElementAt(1);

            return new FunctionCall(
                        BinaryOperation.Division,
                        g, 
                        new FunctionCall(
                            AssociativeCommutativeOperation.Addition, 
                            new Literal(1),
                            new FunctionCall(
                                AssociativeCommutativeOperation.Multiplication, 
                                g, 
                                h)));
        }

        static Expression ConvertDeriveExpression(IEnumerable<Expression> _args, VariableTable varTable)
        {
            DerivativeTransformer derive = new DerivativeTransformer();
            Expression expr = _args.First();
            Variable v = ((VariableAccess)_args.ElementAt(1)).Variable;

            return derive.Transform(expr, new VariableTransformArgs(v));
        }

        static Expression ConvertSqrtFunction(IEnumerable<Expression> _args, VariableTable varTable)
        {
            return new FunctionCall(BinaryOperation.Exponent, _args.First(), new Literal(0.5f));
        }


    }
}
