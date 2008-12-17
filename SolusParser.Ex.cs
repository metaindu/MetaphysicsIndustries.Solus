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
            GetRow,
            GetColumn,
            GetRow2,
            GetColumn2,

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
            //Mod,
            Add,
            Sub,
            Exponent,
            //BitShiftLeft,
            //BitShiftRight,
            //LessThan,
            //GreaterThan,
            //LessThanOrEqual,
            //GreaterThanOrEqual,
            //Equal,
            //NotEqual,
            //BitAnd,
            //BitXor,
            //BitOr,
            //LogAnd,
            //LogXor,
            //LogOr,
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
            //CompareCat = 100,
            //EqualityCat = 90,
            //BitwiseAnd = 80,
            //BitwiseXor = 70,
            //BitwiseOr = 60,
            //LogicalAnd = 50,
            //LogicalXor = 40,
            //LogicalOr = 30,
            //Conditional = 20,
            Comma = 10,
            Assign = 5,
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

        //Ex: The class that represents the nodes of a compiled expression tree. 
        [DebuggerDisplay("{Token}")]
        public class Ex
        {
            static Ex()
            {
                _functionTypes["sin"] = Func.Sine;
                _functionTypes["cos"] = Func.Cosine;
                _functionTypes["tan"] = Func.Tangent;
                _functionTypes["ln"] = Func.Ln;
                _functionTypes["log2"] = Func.Log2;
                _functionTypes["log10"] = Func.Log10;
                //_functionTypes["exp"] = Func.Exponent;
                _functionTypes["sqrt"] = Func.Sqrt;
                _functionTypes["rand"] = Func.Rand;
                //_functionTypes["int"] = Func.Int;
                //_functionTypes["pow"] = Func.Pow;
                _functionTypes["abs"] = Func.Abs;

                _functionTypes["sec"] = Func.Sec;
                _functionTypes["csc"] = Func.Csc;
                _functionTypes["cot"] = Func.Cot;
                _functionTypes["acos"] = Func.Acos;
                _functionTypes["asin"] = Func.Asin;
                _functionTypes["atan"] = Func.Atan;
                _functionTypes["asec"] = Func.Asec;
                _functionTypes["acsc"] = Func.Acsc;
                _functionTypes["acot"] = Func.Acot;
                _functionTypes["ceil"] = Func.Ceil;
                _functionTypes["floor"] = Func.Floor;
                _functionTypes["u"] = Func.UnitStep;

                _functionTypes["atan2"] = Func.Atan2;
                _functionTypes["log"] = Func.Log;

                _functionTypes["integrate"] = Func.Integrate;
                _functionTypes["derive"] = Func.Derive;
                _functionTypes["getrow"] = Func.GetRow;
                _functionTypes["getcolumn"] = Func.GetColumn;
                _functionTypes["getrow2"] = Func.GetRow2;
                _functionTypes["getcolumn2"] = Func.GetColumn2;

                _functionTypes["plot"] = Func.Plot;
                _functionTypes["plot3d"] = Func.Plot3d;
                _functionTypes["mathpaint"] = Func.MathPaint;
                _functionTypes["plotm"] = Func.PlotMatrix;
                _functionTypes["plotv"] = Func.PlotVector;

                _functionTypes["feedback"] = Func.Feedback;
                _functionTypes["subst"] = Func.Subst;




                _ranksFromNodeTypes[NodeType.Num] = Ranks.Values;
                _ranksFromNodeTypes[NodeType.Var] = Ranks.Values;
                _ranksFromNodeTypes[NodeType.Color] = Ranks.Values;
                _ranksFromNodeTypes[NodeType.String] = Ranks.Values;
                _ranksFromNodeTypes[NodeType.Paren] = Ranks.Containers;
                _ranksFromNodeTypes[NodeType.Index] = Ranks.Containers;
                _ranksFromNodeTypes[NodeType.Func] = Ranks.Functions;
                //_ranksFromNodeTypes[NodeType.BitNot] = Ranks.NotCat;
                //_ranksFromNodeTypes[NodeType.LogNot] = Ranks.NotCat;
                _ranksFromNodeTypes[NodeType.Exponent] = Ranks.Exponent;
                _ranksFromNodeTypes[NodeType.Mult] = Ranks.MultCat;
                _ranksFromNodeTypes[NodeType.Div] = Ranks.MultCat;
                //_ranksFromNodeTypes[NodeType.Mod] = Ranks.MultCat;
                _ranksFromNodeTypes[NodeType.Add] = Ranks.AddCat;
                _ranksFromNodeTypes[NodeType.Sub] = Ranks.AddCat;
                //_ranksFromNodeTypes[NodeType.BitShiftLeft] = Ranks.BitwiseShiftCat;
                //_ranksFromNodeTypes[NodeType.BitShiftRight] = Ranks.BitwiseShiftCat;
                //_ranksFromNodeTypes[NodeType.LessThan] = Ranks.CompareCat;
                //_ranksFromNodeTypes[NodeType.GreaterThan] = Ranks.CompareCat;
                //_ranksFromNodeTypes[NodeType.LessThanOrEqual] = Ranks.CompareCat;
                //_ranksFromNodeTypes[NodeType.GreaterThanOrEqual] = Ranks.CompareCat;
                //_ranksFromNodeTypes[NodeType.Equal] = Ranks.EqualityCat;
                //_ranksFromNodeTypes[NodeType.NotEqual] = Ranks.EqualityCat;
                //_ranksFromNodeTypes[NodeType.BitAnd] = Ranks.BitwiseAnd;
                //_ranksFromNodeTypes[NodeType.BitXor] = Ranks.BitwiseXor;
                //_ranksFromNodeTypes[NodeType.BitOr] = Ranks.BitwiseOr;
                //_ranksFromNodeTypes[NodeType.LogAnd] = Ranks.LogicalAnd;
                //_ranksFromNodeTypes[NodeType.LogXor] = Ranks.LogicalXor;
                //_ranksFromNodeTypes[NodeType.LogOr] = Ranks.LogicalOr;
                //_ranksFromNodeTypes[NodeType.Conditional] = Ranks.Conditional;
                _ranksFromNodeTypes[NodeType.Comma] = Ranks.Comma;
                _ranksFromNodeTypes[NodeType.Assign] = Ranks.Assign;
                _ranksFromNodeTypes[NodeType.DelayAssign] = Ranks.Assign;
                _ranksFromNodeTypes[NodeType.Whitespace] = Ranks.Whitespace;



                _argsFromFunctions[Func.Plot] = -1;
                _argsFromFunctions[Func.Plot3d] = -1;
                _argsFromFunctions[Func.Rand] = 0;
                _argsFromFunctions[Func.Atan2] = 2;
                _argsFromFunctions[Func.Log] = 2;
                _argsFromFunctions[Func.Integrate] = 2;
                _argsFromFunctions[Func.Derive] = 2;
                _argsFromFunctions[Func.GetRow] = 3;
                _argsFromFunctions[Func.GetColumn] = 3;
                _argsFromFunctions[Func.GetRow2] = 3;
                _argsFromFunctions[Func.GetColumn2] = 3;
                _argsFromFunctions[Func.MathPaint] = 5;
                _argsFromFunctions[Func.Feedback] = 2;
                _argsFromFunctions[Func.Subst] = 3;

                _argsFromFunctions[Func.Unknown] = -1;


                //_nodeTypes["~"] = NodeType.BitNot;
                _nodeTypes["^"] = NodeType.Exponent;
                _nodeTypes["*"] = NodeType.Mult;
                _nodeTypes["/"] = NodeType.Div;
                //_nodeTypes["%"] = NodeType.Mod;
                //_nodeTypes["&"] = NodeType.BitAnd;
                //_nodeTypes["^"] = NodeType.BitXor;
                //_nodeTypes["|"] = NodeType.BitOr;
                _nodeTypes["+"] = NodeType.Add;
                _nodeTypes["-"] = NodeType.Sub;
                //_nodeTypes["<<"] = NodeType.BitShiftLeft;
                //_nodeTypes[">>"] = NodeType.BitShiftRight;
                //_nodeTypes["!"] = NodeType.LogNot;
                //_nodeTypes["&&"] = NodeType.LogAnd;
                //_nodeTypes["^^"] = NodeType.LogXor;
                //_nodeTypes["||"] = NodeType.LogOr;
                //_nodeTypes["<"] = NodeType.LessThan;
                //_nodeTypes[">"] = NodeType.GreaterThan;
                //_nodeTypes["<="] = NodeType.LessThanOrEqual;
                //_nodeTypes[">="] = NodeType.GreaterThanOrEqual;
                //_nodeTypes["!="] = NodeType.NotEqual;
                //_nodeTypes["=="] = NodeType.Equal;
                //_nodeTypes["?"] = NodeType.Conditional;
                _nodeTypes[","] = NodeType.Comma;
                _nodeTypes["="] = NodeType.Assign;
                _nodeTypes[":="] = NodeType.DelayAssign;
                _nodeTypes["("] = NodeType.Paren;
                _nodeTypes[")"] = NodeType.Paren;
                _nodeTypes["["] = NodeType.Index;
                _nodeTypes["]"] = NodeType.Index;
                _nodeTypes["e"] = NodeType.Num;
                _nodeTypes["pi"] = NodeType.Num;


                _colors["black"] = Colors.Black;
                _colors["white"] = Colors.White;
                _colors["gray"] = Colors.Gray;
                _colors["red"] = Colors.Red;
                _colors["green"] = Colors.Green;
                _colors["blue"] = Colors.Blue;
                _colors["yellow"] = Colors.Yellow;
                _colors["cyan"] = Colors.Cyan;
                _colors["magenta"] = Colors.Magenta;

            }

            //public Ex()
            //{
            //}

            public Ex(string t, int location)
                //: this()
            {
                Token = t;
                _location = location;
            }

            //protected Ex(Decimal d, int location)
            //    : this("0", location)
            //{
            //    numValue = d;
            //}

            public void Dispose()
            {

            }

            public NodeType Type
            {
                get
                {
                    return _type;
                }
                protected set
                {
                    _type = value;
                }
            }

            public Ranks Rank
            {
                get
                {
                    return _rank;
                }
                protected set
                {
                    _rank = value;
                }
            }


            private static Dictionary<string, NodeType> _nodeTypes = new Dictionary<string, NodeType>(StringComparer.CurrentCultureIgnoreCase);
            public static NodeType GetNodeType(string token)
            {
                if (_nodeTypes.ContainsKey(token))
                {
                    return _nodeTypes[token];
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

            private static Dictionary<string, Colors> _colors = new Dictionary<string, Colors>(StringComparer.CurrentCultureIgnoreCase);
            public static Colors GetColor(string token)
            {
                if (_colors.ContainsKey(token))
                {
                    return _colors[token];
                }
                
                return Colors.Unknown;
            }

            private static Dictionary<string, Func> _functionTypes = new Dictionary<string, Func>(StringComparer.CurrentCultureIgnoreCase);
            public static Func GetFuncType(string token)
            {
                if (_functionTypes.ContainsKey(token))
                {
                    return _functionTypes[token];
                }

                return Func.Unknown;
            }

            private static Dictionary<NodeType, Ranks> _ranksFromNodeTypes = new Dictionary<NodeType, Ranks>();
            public static Ranks GetRank(NodeType n)
            {
                if (_ranksFromNodeTypes.ContainsKey(n))
                {
                    return _ranksFromNodeTypes[n];
                }

                
                return Ranks.Unknown;
            }

            private static Dictionary<Func, int> _argsFromFunctions = new Dictionary<Func, int>();
            public static int GetFuncArgs(Func f)
            {
                if (_argsFromFunctions.ContainsKey(f))
                {
                    return _argsFromFunctions[f];
                }

                return 1;
            }

            public string Token
            {
                get
                {
                    return _token;
                }
                protected set
                {
                    string _s = value;
                    _token = value;
                    _type = Ex.GetNodeType(value);
                    _rank = Ex.GetRank(_type);
                    if (_type == NodeType.Func)
                    {
                        func = Ex.GetFuncType(value);
                        //funcargs = GetFuncArgs(func);
                    }
                    else if (_type == NodeType.Num)
                    {
                        Match match;
                        if (_token.ToLower() == "e")
                        {
                            numValue = Math.E;
                        }
                        else if (_token.ToLower() == "pi")
                        {
                            numValue = Math.PI;
                        }
                        else if ((match = Regex.Match(_token, "^([+-]?)(?:0[dD])?([0-9]+)$")).Length > 0)
                        {
                            int i;
                            string s;
                            s = match.Groups[2].Value;
                            i = int.Parse(s);
                            if (match.Groups[1].Value == "-")
                            {
                                i = -i;
                            }
                            numValue = (double)(i);
                        }
                        else if ((match = Regex.Match(_token, "^[+-]?0[xX][0-9a-fA-F]+$")).Length > 0)
                        {
                            int i;
                            string tok = _token;
                            tok = tok.Replace("0x", string.Empty);
                            tok = tok.Replace("0X", string.Empty);
                            tok = tok.Replace("x", string.Empty);
                            tok = tok.Replace("X", string.Empty);
                            i = int.Parse(tok, System.Globalization.NumberStyles.HexNumber);
                            numValue = (double)(i);
                        }
                        else if ((match = Regex.Match(_token, "^([+-]?)0[oO]([0-7]+)$")).Length > 0)
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
                            numValue = (double)(num);
                        }
                        else if ((match = Regex.Match(_token, "^([+-]?)0[bB]([01]+)$")).Length > 0)
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
                            numValue = (double)(num);
                        }
                        else if ((match = Regex.Match(_token, "^[+-]?[0-9]*\\.[0-9]+(?:[eE][+-]?[0-9]+)?$")).Length > 0)
                        {
                            numValue = double.Parse(_token);
                        }
                    }
                    else if (_type == NodeType.Color)
                    {
                    }
                }
            }

            public int Location
            {
                get { return _location; }
                protected set { _location = value; }
            }

            public Ex left = null;
            //public int funcargs = 0;
            public Ex right = null;
            public double numValue = 0;
            public Func func = Func.Unknown;
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



