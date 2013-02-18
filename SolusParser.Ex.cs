/*****************************************************************************
 *                                                                           *
 *  SolusParser.Ex.cs                                                        *
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

namespace MetaphysicsIndustries.Solus
{
    public partial class SolusParser
    {
        public struct ExFunction
        {
            public string Token;
            public FunctionConverter Converter;
            public int NumArguments;
            public bool HasVariableNumArgs;
        }

        protected static readonly List<ExFunction> _builtinFunctions = new List<ExFunction>()
        {
            new ExFunction(){ Token="sin",       Converter=BasicFunctionConverter(Function.Sine),              NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="cos",       Converter=BasicFunctionConverter(Function.Cosine),            NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="tan",       Converter=BasicFunctionConverter(Function.Tangent),           NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="ln",        Converter=BasicFunctionConverter(Function.NaturalLogarithm),  NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="log2",      Converter=BasicFunctionConverter(Function.Log2),              NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="log10",     Converter=BasicFunctionConverter(Function.Log10),             NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="sqrt",      Converter=ConvertSqrtFunction,                                NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="rand",      Converter=(args, vars) => { return new RandomExpression(); }, NumArguments=0,  HasVariableNumArgs=false },
            new ExFunction(){ Token="abs",       Converter=BasicFunctionConverter(Function.AbsoluteValue),     NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="sec",       Converter=BasicFunctionConverter(Function.Secant),            NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="csc",       Converter=BasicFunctionConverter(Function.Cosecant),          NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="cot",       Converter=BasicFunctionConverter(Function.Cotangent),         NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="acos",      Converter=BasicFunctionConverter(Function.Arccosine),         NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="asin",      Converter=BasicFunctionConverter(Function.Arcsine),           NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="atan",      Converter=BasicFunctionConverter(Function.Arctangent),        NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="asec",      Converter=BasicFunctionConverter(Function.Arcsecant),         NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="acsc",      Converter=BasicFunctionConverter(Function.Arccosecant),       NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="acot",      Converter=BasicFunctionConverter(Function.Arccotangent),      NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="ceil",      Converter=BasicFunctionConverter(Function.Ceiling),           NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="floor",     Converter=BasicFunctionConverter(Function.Floor),             NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="unitstep",  Converter=BasicFunctionConverter(Function.UnitStep),          NumArguments=1,  HasVariableNumArgs=false },
            new ExFunction(){ Token="atan2",     Converter=BasicFunctionConverter(Function.Arctangent2),       NumArguments=2,  HasVariableNumArgs=false },
            new ExFunction(){ Token="log",       Converter=BasicFunctionConverter(Function.Logarithm),         NumArguments=2,  HasVariableNumArgs=false },
            new ExFunction(){ Token="derive",    Converter=ConvertDeriveExpression,                            NumArguments=2,  HasVariableNumArgs=false },
            new ExFunction(){ Token="if",        Converter=BasicFunctionConverter(Function.If),                NumArguments=3,  HasVariableNumArgs=false },
            new ExFunction(){ Token="dist",      Converter=BasicFunctionConverter(Function.Dist),              NumArguments=2,  HasVariableNumArgs=false },
            new ExFunction(){ Token="distsq",    Converter=BasicFunctionConverter(Function.DistSq),            NumArguments=2,  HasVariableNumArgs=false },
            new ExFunction(){ Token="feedback",  Converter=ConvertFeedbackExpression,                          NumArguments=2,  HasVariableNumArgs=false },
            new ExFunction(){ Token="subst",     Converter=ConvertSubstExpression,                             NumArguments=3,  HasVariableNumArgs=false },
        };

        public enum NodeType
        {
            Num,
            Var,
            Color,
            Paren,
            Index,
            Func,
            //BitNot,
            //LogNot,
            Mult,
            Div,
            Mod,
            Add,
            Sub,
            Exponent,
            //BitShiftLeft,
            //BitShiftRight,
            LessThan,
            GreaterThan,
            LessThanOrEqual,
            GreaterThanOrEqual,
            Equal,
            NotEqual,
            BitAnd,
            //BitXor,
            BitOr,
            LogAnd,
            //LogXor,
            LogOr,
            //Conditional,
            Comma,
            Whitespace,

            Assign,
            DelayAssign,

            String,

            Unknown,
        }

        public enum Ranks
        {
            //Numbers = 170,
            //Variables = 170,
            //Colors = 170,
            //Strings = 170,
            Values = 170,
            Containers = 160,
            Functions = 150,
            //NotCat = 140,
            Exponent = 135,
            MultCat = 130,
            AddCat = 120,
            //BitwiseShiftCat = 110,
            BitwiseAnd = 100,
            //BitwiseXor = 90,
            BitwiseOr = 80,
            CompareCat = 70,
            EqualityCat = 60,
            LogicalAnd = 50,
            //LogicalXor = 40,
            LogicalOr = 30,
            //Conditional = 20,
            Assign = 10,
            Comma = 5,
            Whitespace = 0,

            Unknown = -1,
        }

        public enum Colors
        {
            Black,
            White,
            Gray,
            Red,
            Green,
            Blue,
            Yellow,
            Cyan,
            Magenta,

            Unknown,
        }

        private Dictionary<string, NodeType> _ex_nodeTypes = new Dictionary<string, NodeType>(StringComparer.CurrentCultureIgnoreCase)
        {
            { "^", NodeType.Exponent },
            { "*", NodeType.Mult },
            { "/", NodeType.Div },
            { "%", NodeType.Mod },
            { "&", NodeType.BitAnd },
            { "|", NodeType.BitOr },
            { "+", NodeType.Add },
            { "-", NodeType.Sub },
            { "&&", NodeType.LogAnd },
            { "||", NodeType.LogOr },
            { "<", NodeType.LessThan },
            { ">", NodeType.GreaterThan },
            { "<=", NodeType.LessThanOrEqual },
            { ">=", NodeType.GreaterThanOrEqual },
            { "!=", NodeType.NotEqual },
            { "==", NodeType.Equal },
            { ",", NodeType.Comma },
            { "=", NodeType.Assign },
            { ":=", NodeType.DelayAssign },
            { "(", NodeType.Paren },
            { ")", NodeType.Paren },
            { "[", NodeType.Index },
            { "]", NodeType.Index },
        };

        private Dictionary<string, Colors> _ex_colors = new Dictionary<string, Colors>(StringComparer.CurrentCultureIgnoreCase)
        {
            { "black", Colors.Black },
            { "white", Colors.White },
            { "gray", Colors.Gray },
            { "red", Colors.Red },
            { "green", Colors.Green },
            { "blue", Colors.Blue },
            { "yellow", Colors.Yellow },
            { "cyan", Colors.Cyan },
            { "magenta", Colors.Magenta },
        };

        private Dictionary<NodeType, Ranks> _ex_ranksFromNodeTypes = new Dictionary<NodeType, Ranks>()
        {
            { NodeType.Num, Ranks.Values },
            { NodeType.Var, Ranks.Values },
            { NodeType.Color, Ranks.Values },
            { NodeType.String, Ranks.Values },
            { NodeType.Paren, Ranks.Containers },
            { NodeType.Index, Ranks.Containers },
            { NodeType.Func, Ranks.Functions },
            { NodeType.Exponent, Ranks.Exponent },
            { NodeType.Mult, Ranks.MultCat },
            { NodeType.Div, Ranks.MultCat },
            { NodeType.Mod, Ranks.MultCat },
            { NodeType.Add, Ranks.AddCat },
            { NodeType.Sub, Ranks.AddCat },
            { NodeType.LessThan, Ranks.CompareCat },
            { NodeType.GreaterThan, Ranks.CompareCat },
            { NodeType.LessThanOrEqual, Ranks.CompareCat },
            { NodeType.GreaterThanOrEqual, Ranks.CompareCat },
            { NodeType.Equal, Ranks.EqualityCat },
            { NodeType.NotEqual, Ranks.EqualityCat },
            { NodeType.BitAnd, Ranks.BitwiseAnd },
            { NodeType.BitOr, Ranks.BitwiseOr },
            { NodeType.LogAnd, Ranks.LogicalAnd },
            { NodeType.LogOr, Ranks.LogicalOr },
            { NodeType.Comma, Ranks.Comma },
            { NodeType.Assign, Ranks.Assign },
            { NodeType.DelayAssign, Ranks.Assign },
            { NodeType.Whitespace, Ranks.Whitespace },
        };

        public Colors GetColor(string token)
        {
            if (_ex_colors.ContainsKey(token))
            {
                return _ex_colors[token];
            }

            return Colors.Unknown;
        }

        public Ranks GetRank(NodeType n)
        {
            if (_ex_ranksFromNodeTypes.ContainsKey(n))
            {
                return _ex_ranksFromNodeTypes[n];
            }

            return Ranks.Unknown;
        }

        public static float? ParseNumber(string token)
        {
            Match match;
            if (token.ToLower() == "e")
            {
                return (float)Math.E;
            }
            else if (token.ToLower() == "pi")
            {
                return (float)Math.PI;
            }
            else if ((match = Regex.Match(token, "^([+-]?)(?:0[dD])?([0-9]+)$")).Length > 0)
            {
                int i;
                string s;
                s = match.Groups[2].Value;
                i = int.Parse(s);
                if (match.Groups[1].Value == "-")
                {
                    i = -i;
                }
                return (float)(i);
            }
            else if ((match = Regex.Match(token, "^[+-]?0[xX][0-9a-fA-F]+$")).Length > 0)
            {
                int i;
                string tok = token;
                tok = tok.Replace("0x", string.Empty);
                tok = tok.Replace("0X", string.Empty);
                tok = tok.Replace("x", string.Empty);
                tok = tok.Replace("X", string.Empty);
                i = int.Parse(tok, System.Globalization.NumberStyles.HexNumber);
                return (float)(i);
            }
            else if ((match = Regex.Match(token, "^([+-]?)0[oO]([0-7]+)$")).Length > 0)
            {
                int i;
                int j;
                int num;
                string s;
                num = 0;
                s = match.Groups[2].Value;
                j = s.Length;
                for (i = 0; i < j; i++)
                {
                    num *= 8;
                    num += ((int)(s[i]) - (int)('0'));
                }
                if (match.Groups[1].Value == "-")
                {
                    num = -num;
                }
                return (float)(num);
            }
            else if ((match = Regex.Match(token, "^([+-]?)0[bB]([01]+)$")).Length > 0)
            {
                int i;
                int j;
                int num;
                string s;
                num = 0;
                s = match.Groups[2].Value;
                j = s.Length;
                for (i = 0; i < j; i++)
                {
                    num *= 2;
                    num += ((int)(s[i]) - (int)('0'));
                }
                if (match.Groups[1].Value == "-")
                {
                    num = -num;
                }
                return (float)(num);
            }
            else if ((match = Regex.Match(token, "^[+-]?[0-9]*\\.[0-9]+(?:[eE][+-]?[0-9]+)?$")).Length > 0)
            {
                return float.Parse(token);
            }

            return null;
        }

        public Ex ExFromToken(string token, int location)
        {
            NodeType type = NodeType.Unknown;

            ExFunction? function = GetFunction(token);
            float? numValue = ParseNumber(token);

            if (_ex_nodeTypes.ContainsKey(token))
            {
                type = _ex_nodeTypes[token];
            }
            else if (numValue.HasValue)
            {
                return new Ex(token, NodeType.Num, GetRank(NodeType.Num), location, numValue.Value);
            }
            else if (function.HasValue)
            {
                return new Ex(token, NodeType.Func, GetRank(NodeType.Func), location, function.Value);
            }
            else if (GetColor(token) != Colors.Unknown)
            {
                type = NodeType.Color;
            }
            else if ((Regex.Match(token, "^\\\"[^\\\"]*\\\"$")).Length > 0)
            {
                type = NodeType.String;
            }
            else if ((Regex.Match(token, "^[a-zA-Z_]\\w*$")).Length > 0)
            {
                type = NodeType.Var;
            }
            else if ((Regex.Match(token, "^\\s+$")).Length > 0)
            {
                type = NodeType.Whitespace;
            }

            return new Ex(token, type, GetRank(type), location);
        }

        //Ex: The class that represents the nodes of a compiled expression tree.
        [DebuggerDisplay("{Token}, {Type}, {Rank}, {Location}")]
        public class Ex
        {
            internal Ex(string token, NodeType type, Ranks rank, int location)
                : this(token, type, rank, location, new ExFunction(), 0)
            {
            }
            internal Ex(string token, NodeType type, Ranks rank, int location, float numValue)
                : this(token, type, rank, location, new ExFunction(), numValue)
            {
            }
            internal Ex(string token, NodeType type, Ranks rank, int location, ExFunction func)
                : this(token, type, rank, location, func, 0)
            {
            }
            internal Ex(string token, NodeType type, Ranks rank, int location, ExFunction func, float numValue)
            {
                Token = token;
                Location = location;
                Type = type;
                Rank = rank;
                Func = func;
                NumValue = numValue;
            }

            public Ex Left = null;
            public Ex Right = null;
            public float NumValue { get; protected set; }
            public ExFunction Func { get; protected set; }
            public NodeType Type { get; protected set; }
            public Ranks Rank { get; protected set; }
            public string Token { get; protected set; }
            public int Location { get; protected set; }

            public string GetStringContents()
            {
                string token = Token;

                token = token.TrimStart('"');
                token = token.TrimEnd('"');

                return token;
            }
        }
    }
}



