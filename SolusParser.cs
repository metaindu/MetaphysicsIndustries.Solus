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

namespace MetaphysicsIndustries.Solus
{
    public partial class SolusParser
    {
        static SolusParser()
        {
            _functions[Func.Abs] = Function.AbsoluteValue;
            _functions[Func.Cosine] = Function.Cosine;
            _functions[Func.Ln] = Function.NaturalLogarithm;
            _functions[Func.Log10] = Function.Log10;
            _functions[Func.Log2] = Function.Log2;
            _functions[Func.Sine] = Function.Sine;
            _functions[Func.Tangent] = Function.Tangent;
            _functions[Func.Sec] = Function.Secant;
            _functions[Func.Csc] = Function.Cosecant;
            _functions[Func.Cot] = Function.Cotangent;
            _functions[Func.Acos] = Function.Arccosine;
            _functions[Func.Asin] = Function.Arcsine;
            _functions[Func.Atan] = Function.Arctangent;
            _functions[Func.Asec] = Function.Arcsecant;
            _functions[Func.Acsc] = Function.Arccosecant;
            _functions[Func.Acot] = Function.Arccotangent;
            _functions[Func.Ceil] = Function.Ceiling;
            _functions[Func.UnitStep] = Function.UnitStep;
            _functions[Func.Atan2] = Function.Arctangent2;
            _functions[Func.Log] = Function.Logarithm;
            _functions[Func.Floor] = Function.Floor;
        }

        private static Dictionary<Func, Function> _functions = new Dictionary<Func, Function>();


        private static SolusEngine _engine = new SolusEngine();

        public static Expression Compile(string expr)
        {
            return Compile(expr, null);
        }

        public static Expression Compile(string expr, VariableTable varTable)
        {
            if (varTable == null)
            {
                varTable = new VariableTable();
            }

            Ex[] tokens;
            tokens = Tokenize(expr);

            return Compile(tokens, varTable);
        }

        public static Expression Compile(Ex[] tokens, VariableTable varTable)
        {
            if (varTable == null)
            {
                varTable = new VariableTable();
            }

            SyntaxCheck(tokens);

            tokens = Arrange(tokens);
            Ex ex = BuildTree(tokens);
            Expression expr = ConvertToSolusExpression(ex, varTable);
            return _engine.CleanUp(expr);
        }

        public static void SyntaxCheck(Ex[] tokens)
        {
            int i;
            for (i = 0; i < tokens.Length - 1; i++)
            {
                if (tokens[i].Type == NodeType.Func &&
                    tokens[i + 1].Token != "(")
                {
                    throw new SolusParseException(tokens[i], "Function call \"" + tokens[i].Token + "\" must include open parenthesis");
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
                throw new SolusParseException(tokens[i], "Function call \"" + tokens[i].Token + "\" must include open parenthesis");
            }

            foreach (Ex ex in tokens)
            {
                if (ex.Type == NodeType.Unknown)
                {
                    throw new SolusParseException(ex, "Unknown token type");
                }
                else if (ex.Type == NodeType.Func)
                {
                    if (Ex.GetFuncType(ex.Token) == Func.Unknown)
                    {
                        throw new SolusParseException(ex, "Unknown function \"" + ex.Token + "\"");
                    }
                }

                if (ex.Rank == Ranks.Unknown)
                {
                    throw new SolusParseException(ex, "Unknown token type");
                }
            }
        }

        public static Expression Compile(Ex[] tokens)
        {
            return Compile(tokens, null);
        }

        protected static Expression ConvertToSolusExpression(Ex ex, VariableTable varTable)
        {
            Expression leftArg = null;
            Expression rightArg = null;
            if (ex.left != null)
            {
                leftArg = ConvertToSolusExpression(ex.left, varTable);
            }
            if (ex.right != null)
            {
                rightArg = ConvertToSolusExpression(ex.right, varTable);
            }

            if (ex.Type == NodeType.Add)
            {
                return new FunctionCall(
                    AssociativeCommutativeOperation.Addition, 
                    leftArg, 
                    rightArg);
            }
            //else if (ex.Type == NodeType.BitAnd)
            //{
            //    return new FunctionCall(

            //}
            //else if (ex.Type == NodeType.BitNot)
            //{
            //}
            //else if (ex.Type == NodeType.BitOr)
            //{
            //}
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
                    BinaryOperation.Division,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.Equal)
            //{
            //}
            else if (ex.Type == NodeType.Func)
            {
                return ConvertFunctionExpression(ex, varTable, leftArg, rightArg);
            }
            //else if (ex.Type == NodeType.GreaterThan)
            //{
            //}
            //else if (ex.Type == NodeType.GreaterThanOrEqual)
            //{
            //}
            //else if (ex.Type == NodeType.Index)
            //{
            //}
            //else if (ex.Type == NodeType.LessThan)
            //{
            //}
            //else if (ex.Type == NodeType.LessThanOrEqual)
            //{
            //}
            //else if (ex.Type == NodeType.LogAnd)
            //{
            //}
            //else if (ex.Type == NodeType.LogNot)
            //{
            //}
            //else if (ex.Type == NodeType.LogOr)
            //{
            //}
            //else if (ex.Type == NodeType.LogXor)
            //{
            //}
            //else if (ex.Type == NodeType.Mod)
            //{
            //}
            else if (ex.Type == NodeType.Mult)
            {
                return new FunctionCall(
                    AssociativeCommutativeOperation.Multiplication,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.NotEqual)
            //{
            //}
            else if (ex.Type == NodeType.Num)
            {
                return new Literal(ex.numValue);
            }
            else if (ex.Type == NodeType.Sub)
            {
                return new FunctionCall(
                    AssociativeCommutativeOperation.Addition,
                    leftArg,
                    new FunctionCall(
                        AssociativeCommutativeOperation.Multiplication,
                        new Literal(-1),
                        rightArg));
            }
            else if (ex.Type == NodeType.Var)
            {
                if (!varTable.ContainsKey(ex.Token))
                {
                    varTable.Add(new Variable(ex.Token));
                }

                return new VariableAccess(varTable[ex.Token]);
            }
            else if (ex.Type == NodeType.Color)
            {
                switch (Ex.GetColor(ex.Token))
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
                    BinaryOperation.Exponent,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.Assign)
            {
                if (!(leftArg is VariableAccess))
                {
                    throw new SolusParseException(ex, "The left operand of an assignment must be a variable.");
                }

                return new AssignExpression(((VariableAccess)leftArg).Variable, rightArg.Eval(varTable));
            }
            else if (ex.Type == NodeType.DelayAssign)
            {
                if (!(leftArg is VariableAccess))
                {
                    throw new SolusParseException(ex, "The left operand of an assignment must be a variable.");
                }

                return new DelayAssignExpression(((VariableAccess)leftArg).Variable, rightArg);
            }

            string error = "Unknown node type: ";
            if (ex.Type != NodeType.Unknown)
            {
                error += ex.Type;
            }
            error += " " + ex.Token;
            throw new SolusParseException(ex, error);
        }

        private static Expression ConvertFunctionExpression(Ex ex, VariableTable varTable, Expression leftArg, Expression rightArg)
        {
            Function function = null;
            List<Expression> args = new List<Expression>();

            if (ex.left != null)
            {
                if (ex.left.Type == NodeType.Comma)
                {
                    ConvertCommaToArgs(ex.left, args, varTable);
                }
                else
                {
                    args.Add(leftArg);
                }
            }

            if (ex.right != null)
            {
                if (ex.right.Type == NodeType.Comma)
                {
                    ConvertCommaToArgs(ex.right, args, varTable);
                }
                else
                {
                    args.Add(rightArg);
                }
            }

            if (_functions.ContainsKey(ex.func))
            {
                function = _functions[ex.func];
            }
            //else if (ex.func == Func.Exponent)
            //{
            //    function = BinaryOperation.Exponent;
            //    args.Insert(0, new Literal(Math.E));
            //    //return new FunctionCall(
            //    //    BinaryOperation.Exponent,
            //    //    new Literal(Math.E),
            //    //    leftArg);
            //}
            //else if (ex.func == Func.Int)
            //{
            //    function = Function.Floor;
            //}
            //else if (ex.func == Func.Pow)
            //{
            //    function = BinaryOperation.Exponent;
            //    //return new FunctionCall(
            //    //    BinaryOperation.Exponent,
            //    //    leftArg,
            //    //    rightArg);
            //}
            else if (ex.func == Func.Rand)
            {
                return new RandomExpression();
            }
            else if (ex.func == Func.Sqrt)
            {
                function = BinaryOperation.Exponent;
                args.Add(new Literal(0.5f));
                //return new FunctionCall(
                //    BinaryOperation.Exponent,
                //    leftArg,
                //    new Literal(0.5f));
            }
            else if (ex.func == Func.Integrate)
            {
                throw new SolusParseException(ex, "That function is not yet implemented");
                //throw new NotImplementedException();
            }
            else if (ex.func == Func.Derive)
            {
                if (!(args[1] is VariableAccess))
                {
                    throw new SolusParseException(ex, "Argument must be a variable");
                }

                return _engine.GetDerivative(args[0], ((VariableAccess)args[1]).Variable);
            }
            else if (ex.func == Func.GetRow)
            {
                return ConvertGetRowExpression(ex, varTable, args);
            }
            else if (ex.func == Func.GetColumn)
            {
                return ConvertGetColumnExpression(ex, varTable, args);
            }
            else if (ex.func == Func.GetRow2)
            {
                return ConvertGetRow2Expression(ex, varTable, args);
            }
            else if (ex.func == Func.GetColumn2)
            {
                return ConvertGetColumn2Expression(ex, varTable, args);
            }
            else if (ex.func == Func.Plot)
            {
                return ConvertPlotExpression(ex, args);
            }
            else if (ex.func == Func.Plot3d)
            {
                return ConvertPlot3dExpression(ex, varTable, args);
            }
            else if (ex.func == Func.MathPaint)
            {
                return ConvertMathPaintExpression(ex, varTable, args);
            }
            else if (ex.func == Func.PlotMatrix)
            {
                return ConvertPlotMatrixExpression(ex, varTable, args);
            }
            else if (ex.func == Func.PlotVector)
            {
                Expression arg = args[0];

                while (arg is VariableAccess)
                {
                    Expression arg2 = varTable[((VariableAccess)arg).Variable];
                    if (arg == arg2) { break; }
                    arg = arg2;
                }

                if (!(arg is SolusVector))
                {
                    throw new SolusParseException(ex, "The argument to plotv must be a vector.");
                }

                return new PlotVectorExpression((SolusVector)arg);
            }
            else if (ex.func == Func.Feedback)
            {
                return ConvertFeedbackExpression(ex, varTable, args);
            }
            else if (ex.func == Func.Subst)
            {
                return ConvertSubstExpression(ex, varTable, args);
            }
            else
            {
                throw new SolusParseException(ex, "Unknown function \"" + ex.Token + "\"");
            }

            return new FunctionCall(
                function,
                args);
        }

        private static Expression ConvertSubstExpression(Ex ex, VariableTable varTable, List<Expression> args)
        {
            if (!(args[1] is VariableAccess))
            {
                throw new SolusParseException(ex, "The second argument must be a variable");
            }

            return _engine.CleanUp(_engine.Subst(args[0], ((VariableAccess)args[1]).Variable, args[2]));
        }

        private static Expression ConvertFeedbackExpression(Ex ex, VariableTable varTable, List<Expression> args)
        {
            Expression g = args[0];
            Expression h = args[1];

            return new FunctionCall(BinaryOperation.Division,
                g, new FunctionCall(AssociativeCommutativeOperation.Addition, new Literal(1),
                    new FunctionCall(AssociativeCommutativeOperation.Multiplication, g, h)));
        }

        private static Expression ConvertGetRowExpression(Ex ex, VariableTable varTable, List<Expression> args)
        {
            Variable var;
            SolusMatrix mat;
            int row;
            Expression arg;

            if (!(args[0] is VariableAccess))
            {
                throw new SolusParseException(ex, "The first argument must be a variable.");
            }

            arg = args[1];
            while (arg is VariableAccess)
            {
                var = ((VariableAccess)arg).Variable;
                if (!varTable.Contains(var)) { break; }

                if (varTable[var] == arg) { break; }

                arg = varTable[var];
            }

            if (!(arg is SolusMatrix))
            {
                throw new SolusParseException(ex, "The second argument must be a matrix.");
            }

            var = ((VariableAccess)args[0]).Variable;
            mat = (SolusMatrix)arg;
            row = (int)(args[2].Eval(varTable).Value);

            varTable[var] = mat.GetRow(row);
            return varTable[var];
        }

        private static Expression ConvertGetColumnExpression(Ex ex, VariableTable varTable, List<Expression> args)
        {
            Variable var;
            SolusMatrix mat;
            int rolumn;
            Expression arg;

            if (!(args[0] is VariableAccess))
            {
                throw new SolusParseException(ex, "The first argument must be a variable.");
            }

            arg = args[1];
            while (arg is VariableAccess)
            {
                var = ((VariableAccess)arg).Variable;
                if (!varTable.Contains(var)) { break; }

                if (varTable[var] == arg) { break; }

                arg = varTable[var];
            }

            if (!(arg is SolusMatrix))
            {
                throw new SolusParseException(ex, "The second argument must be a matrix.");
            }

            var = ((VariableAccess)args[0]).Variable;
            mat = (SolusMatrix)arg;
            rolumn = (int)(args[2].Eval(varTable).Value);

            varTable[var] = mat.GetColumn(rolumn);
            return varTable[var];
        }

        private static Expression ConvertGetRow2Expression(Ex ex, VariableTable varTable, List<Expression> args)
        {
            Variable var;
            SolusMatrix mat;
            int row;
            Expression arg;

            if (!(args[0] is VariableAccess))
            {
                throw new SolusParseException(ex, "The first argument must be a variable.");
            }

            arg = args[1];
            while (arg is VariableAccess)
            {
                var = ((VariableAccess)arg).Variable;
                if (!varTable.Contains(var)) { break; }

                if (varTable[var] == arg) { break; }

                arg = varTable[var];
            }

            if (!(arg is SolusMatrix))
            {
                throw new SolusParseException(ex, "The second argument must be a matrix.");
            }

            var = ((VariableAccess)args[0]).Variable;
            mat = (SolusMatrix)arg;
            row = (int)(args[2].Eval(varTable).Value);

            SolusVector ret = mat.GetRow(row);
            int i;
            for (i = 0; i < ret.Length; i++)
            {
                if (ret[i] is Literal)
                {
                    ret[i] = new Literal((int)(((Literal)ret[i]).Value) & 0x000000FF);
                }
            }
            varTable[var] = ret;
            return varTable[var];
        }

        private static Expression ConvertGetColumn2Expression(Ex ex, VariableTable varTable, List<Expression> args)
        {
            Variable var;
            SolusMatrix mat;
            int column;
            Expression arg;

            if (!(args[0] is VariableAccess))
            {
                throw new SolusParseException(ex, "The first argument must be a variable.");
            }

            arg = args[1];
            while (arg is VariableAccess)
            {
                var = ((VariableAccess)arg).Variable;
                if (!varTable.Contains(var)) { break; }

                if (varTable[var] == arg) { break; }

                arg = varTable[var];
            }

            if (!(arg is SolusMatrix))
            {
                throw new SolusParseException(ex, "The second argument must be a matrix.");
            }

            var = ((VariableAccess)args[0]).Variable;
            mat = (SolusMatrix)arg;
            column = (int)(args[2].Eval(varTable).Value);

            SolusVector ret = mat.GetColumn(column);
            int i;
            for (i = 0; i < ret.Length; i++)
            {
                if (ret[i] is Literal)
                {
                    ret[i] = new Literal((int)(((Literal)ret[i]).Value) & 0x000000FF);
                }
            }
            varTable[var] = ret;
            return ret;
        }

        private static Expression ConvertPlotMatrixExpression(Ex ex, VariableTable varTable, List<Expression> args)
        {
            Expression arg = args[0];

            while (arg is VariableAccess)
            {
                Expression arg2 = varTable[((VariableAccess)arg).Variable];
                if (arg == arg2) { break; }
                arg = arg2;
            }

            if (!(arg is SolusMatrix))
            {
                throw new SolusParseException(ex, "The argument to plotm must be a matrix.");
            }

            return new PlotMatrixExpression((SolusMatrix)arg);
        }

        private static Expression ConvertPlot3dExpression(Ex ex, VariableTable varTable, List<Expression> args)
        {
            if (args.Count < 3 ||
                !(args[0] is VariableAccess) ||
                !(args[1] is VariableAccess))
            {
                throw new SolusParseException(ex, "Plot command requires two variables and one expression to plot");
            }

            if ((args.Count > 5 && args.Count < 9) ||
                args.Count == 10 ||
                args.Count > 11)
            {
                throw new SolusParseException(ex, "Incorrect number of arguments");
            }

            Brush fillBrush = Brushes.Green;
            Pen wirePen = Pens.Black;
            double xMin = -4;
            double xMax = 4;
            double yMin = -4;
            double yMax = 4;
            double zMin = -2;
            double zMax = 6;

            if (args.Count == 4 || args.Count == 5)
            {
                fillBrush = _engine.GetBrushFromExpression(args[3], varTable);

                if (args.Count == 5)
                {
                    wirePen = _engine.GetPenFromExpression(args[4], varTable);
                }
            }
            else if (args.Count == 9)
            {
                //3 --> xMin

                xMin = args[3].Eval(varTable).Value;
                xMax = args[4].Eval(varTable).Value;
                yMin = args[5].Eval(varTable).Value;
                yMax = args[6].Eval(varTable).Value;
                zMin = args[7].Eval(varTable).Value;
                zMax = args[8].Eval(varTable).Value;
            }
            else if (args.Count == 11)
            {
                xMin = args[3].Eval(varTable).Value;
                xMax = args[4].Eval(varTable).Value;
                yMin = args[5].Eval(varTable).Value;
                yMax = args[6].Eval(varTable).Value;
                zMin = args[7].Eval(varTable).Value;
                zMax = args[8].Eval(varTable).Value;
                fillBrush = _engine.GetBrushFromExpression(args[9], varTable);
                wirePen = _engine.GetPenFromExpression(args[10], varTable);
            }
            else if (args.Count != 3)
            {
                throw new SolusParseException(ex, "Incorrect number of arguments");
            }

            return new Plot3dExpression(
                ((VariableAccess)args[0]).Variable,
                ((VariableAccess)args[1]).Variable,
                args[2],
                xMin, xMax,
                yMin, yMax,
                zMin, zMax,
                wirePen, fillBrush);
        }

        private static Expression ConvertPlotExpression(Ex ex, List<Expression> args)
        {
            if (args.Count < 2 || !(args[0] is VariableAccess))
            {
                throw new SolusParseException(ex, "Plot command requires one variable and at least one expression to plot");
            }

            List<Expression> exprs = new List<Expression>(args);
            exprs.RemoveAt(0);

            return new PlotExpression(((VariableAccess)args[0]).Variable, exprs.ToArray());
        }

        private static Expression ConvertMathPaintExpression(Ex ex, VariableTable varTable, List<Expression> args)
        {
            if (!(args[0] is VariableAccess))
            {
                throw new SolusParseException(ex, "First argument to MathPaint command must be a variable reference.");
            }
            if (!(args[1] is VariableAccess))
            {
                throw new SolusParseException(ex, "Second argument to MathPaint command must be a variable reference.");
            }

            return new MathPaintExpression(
                ((VariableAccess)args[0]).Variable,
                ((VariableAccess)args[1]).Variable,
                (int)(args[2].Eval(varTable).Value),
                (int)(args[3].Eval(varTable).Value),
                args[4]);
        }




        protected static void ConvertCommaToArgs(Ex ex, List<Expression> args, VariableTable varTable)
        {
            if (ex.left != null)
            {
                if (ex.left.Type == NodeType.Comma)
                {
                    ConvertCommaToArgs(ex.left, args, varTable);
                }
                else
                {
                    args.Add(ConvertToSolusExpression(ex.left, varTable));
                }
            }
            else
            {
                Debug.WriteLine("ex.left == null in SolusParser.ConvertCommaToArgs");
            }

            if (ex.right != null)
            {
                if (ex.right.Type == NodeType.Comma)
                {
                    ConvertCommaToArgs(ex.right, args, varTable);
                }
                else
                {
                    args.Add(ConvertToSolusExpression(ex.right, varTable));
                }
            }
            else
            {
                Debug.WriteLine("ex.right == null in SolusParser.ConvertCommaToArgs");
            }
        }

        public static Ex[] Tokenize(string expr)
        {
            string _expr = expr;

            string[] chunks;
            List<string> chunks2;
            Ex[] tokens;
            string pattern;

            pattern = "\\s+";
            pattern = "( " +
                        "(?: \\\"[^\\\"]*\\\") | " +
                        //"(?: (?:(?<=^|[\\(\\[+\\-*/\\^,<>&|%~?:])[+-])?[0-9]*\\.[0-9]+(?:[eE][+-]?[0-9]+)?) | " +
                        "(?: (?:(?<=^|[\\(\\[+\\-*/\\^,]\\s*)[+-])?[0-9]*\\.[0-9]+(?:[eE][+-]?[0-9]+)?) | " +
                        "(?: (?:(?<=^|[\\(\\[+\\-*/\\^,]\\s*)[+-])?0[xX][0-9a-fA-F]+) | " +
                        "(?: (?:(?<=^|[\\(\\[+\\-*/\\^,]\\s*)[+-])?0[oO][0-7]+) | " +
                        "(?: (?:(?<=^|[\\(\\[+\\-*/\\^,]\\s*)[+-])?0[bB][01]+) | " +
                        "(?: (?:(?<=^|[\\(\\[+\\-*/\\^,]\\s*)[+-])?(?:0[dD])?[0-9]+) | " +
                        "(?: [*/+\\-,]) | " +
                        "(?: [\\^]) | " +
                        "(?: [=]) | " +
                        "(?: \\:\\=) | " +
                        //"(?: [~%&|!<>?,]) | " +
                        "(?: [a-zA-Z_]\\w*) | " +
                        //"(?: \\<\\<|\\>\\>|\\&\\&|\\\\|\\|\\|) | " +
                        //"(?: \\<\\=|\\>\\=|\\!\\=|\\=\\=) | " +
                        "(?: [\\[\\]\\(\\)]) | " +
                      "\\s+ )";

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

                Ex ex = new Ex(token, locations[i]);
                tokens[i] = ex;

                //if (Ex.GetNodeType(token) == NodeType.Func &&
                //    Ex.GetFuncType(token) == Func.Unknown)
                //{
                //    throw new SolusParseException(ex, "Unknown function: " + ex.Token);
                //}
            }

            return tokens;
        }

        public static Ex[] Arrange(Ex[] intokens)
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

                if (ex.Token == ")" || ex.Token == "]" || ex.Token == "}")
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
                            throw new SolusParseException(ex, "No matching left parenthesis");
                        }
                        else if (cc == "[")
                        {
                            throw new SolusParseException(ex, "No matching left bracket");
                        }
                        else
                        {
                            throw new SolusParseException(ex, "No matching left brace");
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

        public static Ex BuildTree(Ex[] intokens)
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
                throw new SolusParseException(b.Peek(), "Excess tokens");
            }
            return ret;
        }

        protected static Ex BuildTree(Queue<Ex> b)
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

                int funcArgs = Ex.GetFuncArgs(ex.func);
                if (funcArgs != 0)
                {
                    ex.left = BuildTree(b);

                    if (funcArgs > -1)
                    {
                        //check comma operators
                        //ensure argument count is correct

                        int nargs = CountCommaArguments(ex.left);
                        if (nargs > funcArgs)
                        {
                            //throw exception
                            throw new SolusParseException(ex, "Too many arguments");
                        }
                        else if (nargs < funcArgs)
                        {
                            throw new SolusParseException(ex, "Too few arguments");
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
            //else if (r == Ranks.NotCat) //single-argument operators
            //{
            //    ex.left = BuildTree(b);
            //}
            else if (r == Ranks.MultCat || r == Ranks.AddCat ||
                     r == Ranks.Exponent ||
                    //r == Ranks.BitwiseShiftCat ||
                    //r == Ranks.CompareCat || r == Ranks.EqualityCat || r == Ranks.BitwiseAnd ||
                    //r == Ranks.BitwiseXor || r == Ranks.BitwiseOr || r == Ranks.LogicalAnd ||
                    //r == Ranks.LogicalXor || r == Ranks.LogicalOr || r == Ranks.Conditional ||
                     r == Ranks.Comma || r == Ranks.Assign) //binary operations
            {
                if (b.Count < 1) { throw new SolusParseException(ex, "Missing right operand"); }
                ex.right = BuildTree(b);
                if (b.Count < 1) { throw new SolusParseException(ex, "Missing left operand"); }
                ex.left = BuildTree(b);
            }


            return ex;
        }

        protected static int CountCommaArguments(Ex ex)
        {
            if (ex == null)
            {
                return 0;
            }

            if (ex.Type == NodeType.Comma)
            {
                return CountCommaArguments(ex.left) + CountCommaArguments(ex.right);
            }

            return 1;
        }
    }
}
