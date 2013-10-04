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
    public abstract class ExParser
    {
        protected abstract ParseFunction? GetFunction(string token);

        public Expression Compile(string expr, bool cleanup=true)
        {
            return Compile(expr, new VariableTable(), cleanup);
        }

        public Expression Compile(string expr, VariableTable varTable, bool cleanup=true)
        {
            if (varTable == null) throw new ArgumentNullException("varTable");

            Ex[] tokens;
            tokens = Tokenize(expr);

            return Compile(tokens, varTable, cleanup);
        }

        enum NodeType
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

        enum Ranks
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

        enum Colors
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

        private Colors GetColor(string token)
        {
            if (_ex_colors.ContainsKey(token))
            {
                return _ex_colors[token];
            }

            return Colors.Unknown;
        }

        private Ranks GetRank(NodeType n)
        {
            if (_ex_ranksFromNodeTypes.ContainsKey(n))
            {
                return _ex_ranksFromNodeTypes[n];
            }

            return Ranks.Unknown;
        }

        private static float? ParseNumber(string token)
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

        private Ex ExFromToken(string token, int location)
        {
            NodeType type = NodeType.Unknown;

            ParseFunction? function = GetFunction(token);
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
        private class Ex
        {
            public Ex(string token, NodeType type, Ranks rank, int location)
                : this(token, type, rank, location, new ParseFunction(), 0)
            {
            }
            public Ex(string token, NodeType type, Ranks rank, int location, float numValue)
                : this(token, type, rank, location, new ParseFunction(), numValue)
            {
            }
            public Ex(string token, NodeType type, Ranks rank, int location, ParseFunction func)
                : this(token, type, rank, location, func, 0)
            {
            }
            public Ex(string token, NodeType type, Ranks rank, int location, ParseFunction func, float numValue)
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
            public ParseFunction Func { get; protected set; }
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

        private Expression Compile(Ex[] tokens, VariableTable varTable, bool cleanup=true)
        {
            if (varTable == null) throw new ArgumentNullException("varTable");

            SyntaxCheck(tokens);

            tokens = Arrange(tokens);
            Ex ex = BuildTree(tokens);
            Expression expr = ConvertToSolusExpression(ex, varTable);
            if (cleanup)
            {
                CleanUpTransformer cut = new CleanUpTransformer();
                expr = cut.CleanUp(expr);
            }
            return expr;
        }

        private void SyntaxCheck(Ex[] tokens)
        {
            int i;
            for (i = 0; i < tokens.Length - 1; i++)
            {
                if (tokens[i].Type == NodeType.Func &&
                    tokens[i + 1].Token != "(")
                {
                    throw new SolusParseException(tokens[i].Location, "Function call \"" + tokens[i].Token + "\" must include open parenthesis");
                }
                //if (tokens[i + 1].Token == "(" &&
                //    tokens[i].Type != NodeType.Func &&
                //    tokens[i].Rank != Ranks.Containers)
                //{
                //    if (tokens[i].Rank == Ranks.Numbers && tokens[i].Type != NodeType.Num)
                //    {
                //        throw new SolusParseException(tokens[i], "Unknown function \"" + tokens[i].Token + "\"");
                //    }
                //    else
                //    {
                //        throw new SolusParseException(tokens[i], "Syntax error");
                //    }
                //}
            }
            if (tokens[i].Type == NodeType.Func)
            {
                throw new SolusParseException(tokens[i].Location, "Function call \"" + tokens[i].Token + "\" must include open parenthesis");
            }

            foreach (Ex ex in tokens)
            {
                if (ex.Type == NodeType.Unknown)
                {
                    throw new SolusParseException(ex.Location, "Unknown token type");
                }
                else if (ex.Type == NodeType.Func)
                {
                    if (!GetFunction(ex.Token).HasValue)
                    {
                        throw new SolusParseException(ex.Location, "Unknown function \"" + ex.Token + "\"");
                    }
                }

                if (ex.Rank == Ranks.Unknown)
                {
                    throw new SolusParseException(ex.Location, "Unknown token type");
                }
            }
        }

        private Expression Compile(Ex[] tokens)
        {
            return Compile(tokens, null);
        }

        private Expression ConvertToSolusExpression(Ex ex, VariableTable varTable)
        {
            Expression leftArg = null;
            Expression rightArg = null;
            if (ex.Left != null)
            {
                leftArg = ConvertToSolusExpression(ex.Left, varTable);
            }
            if (ex.Right != null)
            {
                rightArg = ConvertToSolusExpression(ex.Right, varTable);
            }

            if (ex.Type == NodeType.Add)
            {
                return new FunctionCall(
                    AdditionOperation.Value, 
                    leftArg, 
                    rightArg);
            }
            else if (ex.Type == NodeType.BitAnd)
            {
                return new FunctionCall(
                    BitwiseAndOperation.Value,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.BitNot)
            //{
            //}
            else if (ex.Type == NodeType.BitOr)
            {
                return new FunctionCall(
                    BitwiseOrOperation.Value,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.BitShiftLeft)
            //{
            //}
            //else if (ex.Type == NodeType.BitShiftRight)
            //{
            //}
            //else if (ex.Type == NodeType.BitXor)
            //{
            //}
            else if (ex.Type == NodeType.Comma)
            {
                return null;
            }
            //else if (ex.Type == NodeType.Conditional)
            //{
            //}
            else if (ex.Type == NodeType.Div)
            {
                return new FunctionCall(
                    DivisionOperation.Value,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.Equal)
            {
                return new FunctionCall(
                    EqualComparisonOperation.Value,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.Func)
            {
                return ConvertFunctionExpression(ex, varTable, leftArg, rightArg);
            }
            else if (ex.Type == NodeType.GreaterThan)
            {
                return new FunctionCall(
                    GreaterThanComparisonOperation.Value,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.GreaterThanOrEqual)
            {
                return new FunctionCall(
                    GreaterThanOrEqualComparisonOperation.Value,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.Index)
            //{
            //}
            else if (ex.Type == NodeType.LessThan)
            {
                return new FunctionCall(
                    LessThanComparisonOperation.Value,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.LessThanOrEqual)
            {
                return new FunctionCall(
                    LessThanOrEqualComparisonOperation.Value,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.LogAnd)
            {
                return new FunctionCall(
                    LogicalAndOperation.Value,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.LogNot)
            //{
            //}
            else if (ex.Type == NodeType.LogOr)
            {
                return new FunctionCall(
                    LogicalOrOperation.Value,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.LogXor)
            //{
            //}
            else if (ex.Type == NodeType.Mod)
            {
                return new FunctionCall(
                    ModularDivision.Value,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.Mult)
            {
                return new FunctionCall(
                    MultiplicationOperation.Value,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.NotEqual)
            {
                return new FunctionCall(
                    NotEqualComparisonOperation.Value,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.Num)
            {
                return new Literal(ex.NumValue);
            }
            else if (ex.Type == NodeType.Sub)
            {
                return new FunctionCall(
                    AdditionOperation.Value,
                    leftArg,
                    new FunctionCall(
                    MultiplicationOperation.Value,
                    new Literal(-1),
                    rightArg));
            }
            else if (ex.Type == NodeType.Var)
            {
                if (!varTable.ContainsKey(ex.Token))
                {
                    varTable.Add(new Variable(ex.Token));
                }

                return new VariableAccess(ex.Token);
            }
            else if (ex.Type == NodeType.Color)
            {
                switch (GetColor(ex.Token))
                {
                    case Colors.Black: return ColorExpression.Black;
                    case Colors.White: return ColorExpression.White;
                    case Colors.Gray: return ColorExpression.Gray;
                    case Colors.Red: return ColorExpression.Red;
                    case Colors.Green: return ColorExpression.Green;
                    case Colors.Blue: return ColorExpression.Blue;
                    case Colors.Yellow: return ColorExpression.Yellow;
                    case Colors.Cyan: return ColorExpression.Cyan;
                    case Colors.Magenta: return ColorExpression.Magenta;
                }
            }
            else if (ex.Type == NodeType.Exponent)
            {
                return new FunctionCall(
                    ExponentOperation.Value,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.Assign)
            {
                if (!(leftArg is VariableAccess))
                {
                    throw new SolusParseException(ex.Location, "The left operand of an assignment must be a variable.");
                }

                return new AssignExpression(((VariableAccess)leftArg).VariableName, rightArg.Eval(varTable));
            }
            else if (ex.Type == NodeType.DelayAssign)
            {
                if (!(leftArg is VariableAccess))
                {
                    throw new SolusParseException(ex.Location, "The left operand of an assignment must be a variable.");
                }

                return new DelayAssignExpression(varTable[((VariableAccess)leftArg).VariableName], rightArg);
            }

            string error = "Unknown node type: ";
            if (ex.Type != NodeType.Unknown)
            {
                error += ex.Type;
            }
            error += " " + ex.Token;
            throw new SolusParseException(ex.Location, error);
        }

        private Expression ConvertFunctionExpression(Ex ex, VariableTable varTable, Expression leftArg, Expression rightArg)
        {
            List<Expression> args = new List<Expression>();

            if (ex.Left != null)
            {
                if (ex.Left.Type == NodeType.Comma)
                {
                    ConvertCommaToArgs(ex.Left, args, varTable);
                }
                else
                {
                    args.Add(leftArg);
                }
            }

            if (ex.Right != null)
            {
                if (ex.Right.Type == NodeType.Comma)
                {
                    ConvertCommaToArgs(ex.Right, args, varTable);
                }
                else
                {
                    args.Add(rightArg);
                }
            }

            if (GetFunction(ex.Token).HasValue)
            {
                return GetFunction(ex.Token).Value.Converter(args,varTable);
            }
            else
            {
                throw new SolusParseException(ex.Location, "Unknown function \"" + ex.Token + "\"");
            }
        }

        private void ConvertCommaToArgs(Ex ex, List<Expression> args, VariableTable varTable)
        {
            if (ex.Left != null)
            {
                if (ex.Left.Type == NodeType.Comma)
                {
                    ConvertCommaToArgs(ex.Left, args, varTable);
                }
                else
                {
                    args.Add(ConvertToSolusExpression(ex.Left, varTable));
                }
            }
            else
            {
                Debug.WriteLine("ex.left == null in SolusParser.ConvertCommaToArgs");
            }

            if (ex.Right != null)
            {
                if (ex.Right.Type == NodeType.Comma)
                {
                    ConvertCommaToArgs(ex.Right, args, varTable);
                }
                else
                {
                    args.Add(ConvertToSolusExpression(ex.Right, varTable));
                }
            }
            else
            {
                Debug.WriteLine("ex.right == null in SolusParser.ConvertCommaToArgs");
            }
        }

        private Ex[] Tokenize(string expr)
        {
            string _expr = expr;

            string[] chunks;
            List<string> chunks2;
            Ex[] tokens;
            string pattern;

            pattern = "\\s+";
            pattern = 
                "( " +
                    "(?: \\\"[^\\\"]*\\\") | " +
                    //@"(?: (?:(?<=^|[\(\[+\-*/\^,<>&|%~?:])[+-])?[0-9]*\.[0-9]+(?:[eE][+-]?[0-9]+)?) | " +
                    @"(?: (?:(?<=^|[\(\[+\-*/\^,]\\s*)[+-])?[0-9]*\.[0-9]+(?:[eE][+-]?[0-9]+)?) | " +
                    @"(?: (?:(?<=^|[\(\[+\-*/\^,]\\s*)[+-])?0[xX][0-9a-fA-F]+) | " +
                    @"(?: (?:(?<=^|[\(\[+\-*/\^,]\\s*)[+-])?0[oO][0-7]+) | " +
                    @"(?: (?:(?<=^|[\(\[+\-*/\^,]\\s*)[+-])?0[bB][01]+) | " +
                    @"(?: (?:(?<=^|[\(\[+\-*/\^,]\\s*)[+-])?(?:0[dD])?[0-9]+) | " +
                    @"(?: [*/%]) | " +
                    @"(?: [+\-]) | " +
                    @"(?: [,]) | " +
                    @"(?: \&\&|\|\|) | " +
                    @"(?: [\&\|]) | " +
                    @"(?: [\^]) | " +
                    @"(?: \=\=) | " +
                    @"(?: [=]) | " +
                    @"(?: \:\=) | " +
                    //@"(?: [~%&|!<>?,]) | " +
                    @"(?: [a-zA-Z_]\w*) | " +
                    //@"(?: \<\<|\>\>|\&\&|\|\|) | " +
                    @"(?: \<\=|\>\=|\!\=|\=\=) | " +
                    @"(?: [<>]) | " +
                    @"(?: [\[\]\(\)]) | " +
                    @"\s+ )";

            chunks = Regex.Split(expr, pattern, RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace);
            chunks2 = new List<string>();

            int location = 0;
            List<int> locations = new List<int>();

            foreach (string s in chunks)
            {
                if (s.Length > 0 && (Regex.Match(s, "\\s+$")).Length <= 0)
                {
                    chunks2.Add(s);
                    locations.Add(location);
                }

                location += s.Length;
            }

            int i;
            int j;
            j = chunks2.Count;
            tokens = new Ex[j];
            for (i = 0; i < j; i++)
            {
                string token = chunks2[i];

                Ex ex = ExFromToken(token, locations[i]);
                tokens[i] = ex;

                //if (Ex.GetNodeType(token) == NodeType.Func &&
                //    Ex.GetFuncType(token) == Func.Unknown)
                //{
                //    throw new SolusParseException(ex, "Unknown function: " + ex.Token);
                //}
            }

            return tokens;
        }

        private Ex[] Arrange(Ex[] intokens)
        {


            Queue<Ex> a;
            Stack<Ex> b;
            Stack<Ex> c;

            a = new Queue<Ex>(intokens.Length);
            foreach (Ex ex in intokens)
            {
                a.Enqueue(ex);
            }

            b = new Stack<Ex>(intokens.Length);
            c = new Stack<Ex>(intokens.Length);

            while (a.Count > 0)
            {
                Ex ex;
                string cc;

                ex = a.Dequeue();

                if (ex.Token == ")" || ex.Token == "]" || ex.Token == "}")// || ex.Token == ":")
                {
                    if (ex.Token == ")")
                    {
                        cc = "(";
                    }
                    else if (ex.Token == "]")
                    {
                        cc = "[";
                    }
                    else
                    {
                        cc = "}";
                    }

                    while (c.Count > 0 && c.Peek().Token != cc)
                    {
                        b.Push(c.Pop());
                    }
                    if (c.Count < 1)
                    {
                        if (cc == "(")
                        {
                            throw new SolusParseException(ex.Location, "No matching left parenthesis");
                        }
                        else if (cc == "[")
                        {
                            throw new SolusParseException(ex.Location, "No matching left bracket");
                        }
                        else
                        {
                            throw new SolusParseException(ex.Location, "No matching left brace");
                        }
                    }
                    Ex cp;
                    cp = c.Pop();
                    if (cc == "[" || cc == "{")
                    {
                        b.Push(cp);
                    }

                    if (c.Count > 0 && c.Peek().Type == NodeType.Func)
                    {
                        b.Push(c.Pop());
                    }
                }
                else if (ex.Rank == Ranks.Values)// == NodeType.Var || ex.Type == NodeType.Num)
                {
                    b.Push(ex);
                }
                else
                {
                    Ex cc2;
                    Ranks cr;

                    if (c.Count > 0)
                    {
                        cc2 = c.Peek();
                        cr = cc2.Rank;

                        while (c.Count > 0 && ex.Rank <= cr && cr < Ranks.Functions)
                        {
                            b.Push(c.Pop());
                            if (c.Count > 0)
                            {
                                cc2 = c.Peek();
                                cr = cc2.Rank;
                            }
                        }
                    }
                    c.Push(ex);
                }
            }
            while (c.Count > 0)
            {
                b.Push(c.Pop());
            }

            int i;
            int j;
            Ex[] tokens;

            j = b.Count;
            tokens = new Ex[j];
            for (i = 0; i < j; i++)
            {
                tokens[i] = b.Pop();
            }

            return tokens;
        }

        private Ex BuildTree(Ex[] intokens)
        {
            Ex[] _intokens = intokens;

            Queue<Ex> b;
            int i;
            int j;

            j = intokens.Length;
            b = new Queue<Ex>(j);
            for (i = 0; i < j; i++)
            {
                b.Enqueue(intokens[i]);
            }

            Ex ret = BuildTree(b);
            if (b.Count > 0)
            {
                throw new SolusParseException(b.Peek().Location, "Excess tokens");
            }
            return ret;
        }

        private Ex BuildTree(Queue<Ex> b)
        {
            Ex ex;
            Ranks r;
            NodeType t;

            ex = b.Dequeue();
            r = ex.Rank;
            t = ex.Type;

            if (t == NodeType.Func) //functions
            {
                //this could be combined with the next section,
                //but it's a good idea to separate it visually.
                //we could conceiveably add a for loop here at some 
                //point in the future, for functions with more than two 
                //arguments.
                //
                //currently, functions with more than one argument in
                //the expression simply hold a pointer to a comma
                //operator.
                //
                //we'll want to put in some logic to check the number of
                //cascaded comma operators against the functions'
                //desired argument counts.

                ParseFunction exf = GetFunction(ex.Token).Value;
                int funcArgs = exf.NumArguments;
                if ((funcArgs != 0 || exf.HasVariableNumArgs) && b.Count > 0)
                {
                    ex.Left = BuildTree(b);

                    if (!exf.HasVariableNumArgs)
                    {
                        //check comma operators
                        //ensure argument count is correct

                        int nargs = CountCommaArguments(ex.Left);
                        if (nargs > funcArgs)
                        {
                            //throw exception
                            throw new SolusParseException(ex.Location, "Too many arguments");
                        }
                        else if (nargs < funcArgs)
                        {
                            throw new SolusParseException(ex.Location, "Too few arguments");
                        }
                    }
                }
            }
            //else if (t == NodeType.Index) //indexer (we're not sure how many arguments)
            //{
            //    //the indexer is kinda like functions in that it can 
            //    //have a variable number of arguments. However, its 
            //    //central, important difference is that while all 
            //    //functions' arguments counts are established at 
            //    //compile-time, the indexer's argument count is 
            //    //determined at run-time. The correct count is dependent
            //    //on the dimensions of the underlying ValueTable from
            //    //which values are taken.
            //    //
            //    //typically, the indexer has two arguments separated by
            //    //a comma.
            //    //
            //    //the count should probably be confirmed during 
            //    //evaluation of the tree. we don't care about the 
            //    //number of arguments or commas at this point int the 
            //    //code.

            //    ex.left = BuildTree(b);
            //}
            //else if (IsUnaryOperation(r))
            //{
            //    ex.left = BuildTree(b);
            //}
            else if (IsBinaryOperation(r))
            {
                if (b.Count < 1) { throw new SolusParseException(ex.Location, "Missing right operand"); }
                ex.Right = BuildTree(b);
                if (b.Count < 1) { throw new SolusParseException(ex.Location, "Missing left operand"); }
                ex.Left = BuildTree(b);
            }


            return ex;
        }

        private static bool IsUnaryOperation(Ranks r)
        {
            return false;// r == Ranks.NotCat;
        }
        private static bool IsBinaryOperation(Ranks r)
        {
            return 
                r == Ranks.MultCat || 
                    r == Ranks.AddCat ||
                    r == Ranks.Exponent ||
                    //r == Ranks.BitwiseShiftCat ||
                    r == Ranks.CompareCat || 
                    r == Ranks.EqualityCat ||
                    r == Ranks.BitwiseAnd ||
                    //r == Ranks.BitwiseXor || 
                    r == Ranks.BitwiseOr ||
                    r == Ranks.LogicalAnd ||
                    //r == Ranks.LogicalXor || 
                    r == Ranks.LogicalOr || 
                    //r == Ranks.Conditional ||
                    r == Ranks.Comma || 
                    r == Ranks.Assign;
        }

        private int CountCommaArguments(Ex ex)
        {
            if (ex == null)
            {
                return 0;
            }

            if (ex.Type == NodeType.Comma)
            {
                return CountCommaArguments(ex.Left) + CountCommaArguments(ex.Right);
            }

            return 1;
        }
    }
}



