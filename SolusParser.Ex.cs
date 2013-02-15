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

        public enum Func
        {
            Sine,
            Cosine,
            Tangent,
            Ln,
            Log2,
            Log10,
            //Exponent,
            Sqrt,
            Rand,
            //Int,
            //Pow,
            Abs,

            Sec,
            Csc,
            Cot,
            Acos,
            Asin,
            Atan,
            Asec,
            Acsc,
            Acot,
            Ceil,
            Floor,

            UnitStep,

            Atan2,
            Log,

            Integrate,
            Derive,
            //GetRow,
            //GetColumn,
            //GetRow2,
            //GetColumn2,

            If,
            Dist,
            DistSq,

            Plot,
            Plot3d,
            MathPaint,
            PlotVector,
            PlotMatrix,

            Feedback,

            Subst,

            Unknown,
        }

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

        private static Dictionary<string, NodeType> _ex_nodeTypes = new Dictionary<string, NodeType>(StringComparer.CurrentCultureIgnoreCase);
        private static Dictionary<string, Colors> _ex_colors = new Dictionary<string, Colors>(StringComparer.CurrentCultureIgnoreCase);
        private static Dictionary<string, Func> _ex_functionTypes = new Dictionary<string, Func>(StringComparer.CurrentCultureIgnoreCase);
        private static Dictionary<NodeType, Ranks> _ex_ranksFromNodeTypes = new Dictionary<NodeType, Ranks>();
        private static Dictionary<Func, int> _ex_argsFromFunctions = new Dictionary<Func, int>();

        public static void InitEx()
        {
            _ex_functionTypes["sin"] = Func.Sine;
            _ex_functionTypes["cos"] = Func.Cosine;
            _ex_functionTypes["tan"] = Func.Tangent;
            _ex_functionTypes["ln"] = Func.Ln;
            _ex_functionTypes["log2"] = Func.Log2;
            _ex_functionTypes["log10"] = Func.Log10;
            //_functionTypes["exp"] = Func.Exponent;
            _ex_functionTypes["sqrt"] = Func.Sqrt;
            _ex_functionTypes["rand"] = Func.Rand;
            //_functionTypes["int"] = Func.Int;
            //_functionTypes["pow"] = Func.Pow;
            _ex_functionTypes["abs"] = Func.Abs;

            _ex_functionTypes["sec"] = Func.Sec;
            _ex_functionTypes["csc"] = Func.Csc;
            _ex_functionTypes["cot"] = Func.Cot;
            _ex_functionTypes["acos"] = Func.Acos;
            _ex_functionTypes["asin"] = Func.Asin;
            _ex_functionTypes["atan"] = Func.Atan;
            _ex_functionTypes["asec"] = Func.Asec;
            _ex_functionTypes["acsc"] = Func.Acsc;
            _ex_functionTypes["acot"] = Func.Acot;
            _ex_functionTypes["ceil"] = Func.Ceil;
            _ex_functionTypes["floor"] = Func.Floor;
            _ex_functionTypes["ustep"] = Func.UnitStep;
            _ex_functionTypes["unitstep"] = Func.UnitStep;

            _ex_functionTypes["atan2"] = Func.Atan2;
            _ex_functionTypes["log"] = Func.Log;

            _ex_functionTypes["integrate"] = Func.Integrate;
            _ex_functionTypes["derive"] = Func.Derive;
            //_functionTypes["getrow"] = Func.GetRow;
            //_functionTypes["getcolumn"] = Func.GetColumn;
            //_functionTypes["getrow2"] = Func.GetRow2;
            //_functionTypes["getcolumn2"] = Func.GetColumn2;

            _ex_functionTypes["if"] = Func.If;
            _ex_functionTypes["dist"] = Func.Dist;
            _ex_functionTypes["distsq"] = Func.DistSq;

            _ex_functionTypes["plot"] = Func.Plot;
            _ex_functionTypes["plot3d"] = Func.Plot3d;
            _ex_functionTypes["mathpaint"] = Func.MathPaint;
            _ex_functionTypes["plotm"] = Func.PlotMatrix;
            _ex_functionTypes["plotv"] = Func.PlotVector;

            _ex_functionTypes["feedback"] = Func.Feedback;
            _ex_functionTypes["subst"] = Func.Subst;




            _ex_ranksFromNodeTypes[NodeType.Num] = Ranks.Values;
            _ex_ranksFromNodeTypes[NodeType.Var] = Ranks.Values;
            _ex_ranksFromNodeTypes[NodeType.Color] = Ranks.Values;
            _ex_ranksFromNodeTypes[NodeType.String] = Ranks.Values;
            _ex_ranksFromNodeTypes[NodeType.Paren] = Ranks.Containers;
            _ex_ranksFromNodeTypes[NodeType.Index] = Ranks.Containers;
            _ex_ranksFromNodeTypes[NodeType.Func] = Ranks.Functions;
            //_ranksFromNodeTypes[NodeType.BitNot] = Ranks.NotCat;
            //_ranksFromNodeTypes[NodeType.LogNot] = Ranks.NotCat;
            _ex_ranksFromNodeTypes[NodeType.Exponent] = Ranks.Exponent;
            _ex_ranksFromNodeTypes[NodeType.Mult] = Ranks.MultCat;
            _ex_ranksFromNodeTypes[NodeType.Div] = Ranks.MultCat;
            _ex_ranksFromNodeTypes[NodeType.Mod] = Ranks.MultCat;
            _ex_ranksFromNodeTypes[NodeType.Add] = Ranks.AddCat;
            _ex_ranksFromNodeTypes[NodeType.Sub] = Ranks.AddCat;
            //_ranksFromNodeTypes[NodeType.BitShiftLeft] = Ranks.BitwiseShiftCat;
            //_ranksFromNodeTypes[NodeType.BitShiftRight] = Ranks.BitwiseShiftCat;
            _ex_ranksFromNodeTypes[NodeType.LessThan] = Ranks.CompareCat;
            _ex_ranksFromNodeTypes[NodeType.GreaterThan] = Ranks.CompareCat;
            _ex_ranksFromNodeTypes[NodeType.LessThanOrEqual] = Ranks.CompareCat;
            _ex_ranksFromNodeTypes[NodeType.GreaterThanOrEqual] = Ranks.CompareCat;
            _ex_ranksFromNodeTypes[NodeType.Equal] = Ranks.EqualityCat;
            _ex_ranksFromNodeTypes[NodeType.NotEqual] = Ranks.EqualityCat;
            _ex_ranksFromNodeTypes[NodeType.BitAnd] = Ranks.BitwiseAnd;
            //_ranksFromNodeTypes[NodeType.BitXor] = Ranks.BitwiseXor;
            _ex_ranksFromNodeTypes[NodeType.BitOr] = Ranks.BitwiseOr;
            _ex_ranksFromNodeTypes[NodeType.LogAnd] = Ranks.LogicalAnd;
            //_ranksFromNodeTypes[NodeType.LogXor] = Ranks.LogicalXor;
            _ex_ranksFromNodeTypes[NodeType.LogOr] = Ranks.LogicalOr;
            //_ranksFromNodeTypes[NodeType.Conditional] = Ranks.Conditional;
            _ex_ranksFromNodeTypes[NodeType.Comma] = Ranks.Comma;
            _ex_ranksFromNodeTypes[NodeType.Assign] = Ranks.Assign;
            _ex_ranksFromNodeTypes[NodeType.DelayAssign] = Ranks.Assign;
            _ex_ranksFromNodeTypes[NodeType.Whitespace] = Ranks.Whitespace;



            _ex_argsFromFunctions[Func.Plot] = -1;
            _ex_argsFromFunctions[Func.Plot3d] = -1;
            _ex_argsFromFunctions[Func.Rand] = 0;
            _ex_argsFromFunctions[Func.Atan2] = 2;
            _ex_argsFromFunctions[Func.Log] = 2;
            _ex_argsFromFunctions[Func.Integrate] = 2;
            _ex_argsFromFunctions[Func.Derive] = 2;
            //_argsFromFunctions[Func.GetRow] = 3;
            //_argsFromFunctions[Func.GetColumn] = 3;
            //_argsFromFunctions[Func.GetRow2] = 3;
            //_argsFromFunctions[Func.GetColumn2] = 3;
            _ex_argsFromFunctions[Func.MathPaint] = 5;
            _ex_argsFromFunctions[Func.Feedback] = 2;
            _ex_argsFromFunctions[Func.Subst] = 3;
            _ex_argsFromFunctions[Func.If] = 3;
            _ex_argsFromFunctions[Func.Dist] = 2;
            _ex_argsFromFunctions[Func.DistSq] = 2;

            _ex_argsFromFunctions[Func.Unknown] = -1;


            //_nodeTypes["~"] = NodeType.BitNot;
            _ex_nodeTypes["^"] = NodeType.Exponent;
            _ex_nodeTypes["*"] = NodeType.Mult;
            _ex_nodeTypes["/"] = NodeType.Div;
            _ex_nodeTypes["%"] = NodeType.Mod;
            _ex_nodeTypes["&"] = NodeType.BitAnd;
            //_nodeTypes["^"] = NodeType.BitXor;
            _ex_nodeTypes["|"] = NodeType.BitOr;
            _ex_nodeTypes["+"] = NodeType.Add;
            _ex_nodeTypes["-"] = NodeType.Sub;
            //_nodeTypes["<<"] = NodeType.BitShiftLeft;
            //_nodeTypes[">>"] = NodeType.BitShiftRight;
            //_nodeTypes["!"] = NodeType.LogNot;
            _ex_nodeTypes["&&"] = NodeType.LogAnd;
            //_nodeTypes["^^"] = NodeType.LogXor;
            _ex_nodeTypes["||"] = NodeType.LogOr;
            _ex_nodeTypes["<"] = NodeType.LessThan;
            _ex_nodeTypes[">"] = NodeType.GreaterThan;
            _ex_nodeTypes["<="] = NodeType.LessThanOrEqual;
            _ex_nodeTypes[">="] = NodeType.GreaterThanOrEqual;
            _ex_nodeTypes["!="] = NodeType.NotEqual;
            _ex_nodeTypes["=="] = NodeType.Equal;
            //_nodeTypes["?"] = NodeType.Conditional;
            _ex_nodeTypes[","] = NodeType.Comma;
            _ex_nodeTypes["="] = NodeType.Assign;
            _ex_nodeTypes[":="] = NodeType.DelayAssign;
            _ex_nodeTypes["("] = NodeType.Paren;
            _ex_nodeTypes[")"] = NodeType.Paren;
            _ex_nodeTypes["["] = NodeType.Index;
            _ex_nodeTypes["]"] = NodeType.Index;
            _ex_nodeTypes["e"] = NodeType.Num;
            _ex_nodeTypes["pi"] = NodeType.Num;


            _ex_colors["black"] = Colors.Black;
            _ex_colors["white"] = Colors.White;
            _ex_colors["gray"] = Colors.Gray;
            _ex_colors["red"] = Colors.Red;
            _ex_colors["green"] = Colors.Green;
            _ex_colors["blue"] = Colors.Blue;
            _ex_colors["yellow"] = Colors.Yellow;
            _ex_colors["cyan"] = Colors.Cyan;
            _ex_colors["magenta"] = Colors.Magenta;

        }

        public static NodeType GetNodeType(string token)
        {
            if (_ex_nodeTypes.ContainsKey(token))
            {
                return _ex_nodeTypes[token];
            }
            else if (GetFuncType(token) != Func.Unknown) return NodeType.Func;
            else if (GetColor(token) != Colors.Unknown) return NodeType.Color;
            else if ((Regex.Match(token, "^\\\"[^\\\"]*\\\"$")).Length > 0)
            {
                return NodeType.String;
            }
            else if ((Regex.Match(token, "^[a-zA-Z_]\\w*$")).Length > 0)
            {
                return NodeType.Var;
            }
            else if ((Regex.Match(token, "^[+-]?[0-9]*\\.[0-9]+(?:[eE][+-]?[0-9]+)?$")).Length > 0 ||
                     (Regex.Match(token, "^[+-]?(?:0[dD])?[0-9]+$")).Length > 0 ||
                     (Regex.Match(token, "^[+-]?0[xX][0-9a-fA-F]+$")).Length > 0 ||
                     (Regex.Match(token, "^[+-]?0[oO][0-7]+$")).Length > 0 ||
                     (Regex.Match(token, "^[+-]?0[bB][01]+$")).Length > 0)
            {
                return NodeType.Num;
            }
            else if ((Regex.Match(token, "^\\s+$")).Length > 0)
            {
                return NodeType.Whitespace;
            }

            return NodeType.Unknown;
        }

        public static Colors GetColor(string token)
        {
            if (_ex_colors.ContainsKey(token))
            {
                return _ex_colors[token];
            }

            return Colors.Unknown;
        }

        public static Func GetFuncType(string token)
        {
            if (_ex_functionTypes.ContainsKey(token))
            {
                return _ex_functionTypes[token];
            }

            return Func.Unknown;
        }

        public static Ranks GetRank(NodeType n)
        {
            if (_ex_ranksFromNodeTypes.ContainsKey(n))
            {
                return _ex_ranksFromNodeTypes[n];
            }


            return Ranks.Unknown;
        }

        public static int GetFuncArgs(Func f)
        {
            if (_ex_argsFromFunctions.ContainsKey(f))
            {
                return _ex_argsFromFunctions[f];
            }

            return 1;
        }

        public static float ParseNumber(string token)
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

            return 0;
        }


        //Ex: The class that represents the nodes of a compiled expression tree.
        [DebuggerDisplay("{Token}, {Type}, {Rank}, {Location}")]
        public class Ex
        {
            static Ex()
            {
                InitEx();
            }

            public static Ex FromToken(string token, int location)
            {
                NodeType type = GetNodeType(token);

                if (type == NodeType.Func)
                {
                    Func func = GetFuncType(token);
                    return new Ex(token, location, func);
                }
                else if (type == NodeType.Num)
                {
                    float numValue = ParseNumber(token);
                    return new Ex(token, location, numValue);
                }
                else
                {
                    return new Ex(token, type, GetRank(type), location);
                }
            }

            Ex(string token, int location)
            {
                _token = token;
                _location = location;
            }
            Ex(string token, NodeType type, Ranks rank, int location)
                : this(token, location)
            {
                _type = type;
                _rank = rank;
            }
            Ex(string token, int location, Func func)
                : this(token, NodeType.Func, GetRank(NodeType.Func), location)
            {
                Func = func;
            }
            Ex(string token, int location, float numValue)
                : this(token, NodeType.Num, GetRank(NodeType.Num), location)
            {
                NumValue = numValue;
            }

            public NodeType Type
            {
                get { return _type; }
                protected set { _type = value; }
            }

            public Ranks Rank
            {
                get { return _rank; }
                protected set { _rank = value; }
            }

            public string Token
            {
                get { return _token; }
            }

            public int Location
            {
                get { return _location; }
                protected set { _location = value; }
            }

            public Ex Left = null;
            public Ex Right = null;
            public float NumValue = 0;
            public Func Func = Func.Unknown;
            private NodeType _type = NodeType.Unknown;
            private Ranks _rank = Ranks.Unknown;
            private string _token = string.Empty;
            private int _location = 0;

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



