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
    public partial class SolusParser
    {
        public SolusParser()
        {
            foreach (ExFunction func in _builtinFunctions)
            {
                AddFunction(func);
            }
        }

        private Dictionary<string, ExFunction> _functions = new Dictionary<string, ExFunction>(StringComparer.CurrentCultureIgnoreCase);

        public void AddFunction(ExFunction func)
        {
            if (_functions.ContainsKey(func.Token)) throw new ArgumentException("A function already uses that token.", "func");

            _functions.Add(func.Token, func);
        }

        public ExFunction? GetFunction(string token)
        {
            if (!_functions.ContainsKey(token)) return null;

            return _functions[token];
        }

        public Expression Compile(string expr)
        {
            return Compile(expr, new VariableTable());
        }

        public Expression Compile(string expr, VariableTable varTable)
        {
            if (varTable == null) throw new ArgumentNullException("varTable");

            Ex[] tokens;
            tokens = Tokenize(expr);

            return Compile(tokens, varTable);
        }

        public Expression Compile(Ex[] tokens, VariableTable varTable)
        {
            if (varTable == null) throw new ArgumentNullException("varTable");

            SyntaxCheck(tokens);

            tokens = Arrange(tokens);
            Ex ex = BuildTree(tokens);
            Expression expr = ConvertToSolusExpression(ex, varTable);
            CleanUpTransformer cleanup = new CleanUpTransformer();
            return cleanup.CleanUp(expr);
        }

        protected void SyntaxCheck(Ex[] tokens)
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
                    if (!GetFunction(ex.Token).HasValue)
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

        protected Expression Compile(Ex[] tokens)
        {
            return Compile(tokens, null);
        }

        protected Expression ConvertToSolusExpression(Ex ex, VariableTable varTable)
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
                    AssociativeCommutativeOperation.Addition, 
                    leftArg, 
                    rightArg);
            }
            else if (ex.Type == NodeType.BitAnd)
            {
                return new FunctionCall(
                    BinaryOperation.BitwiseAnd,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.BitNot)
            //{
            //}
            else if (ex.Type == NodeType.BitOr)
            {
                return new FunctionCall(
                    AssociativeCommutativeOperation.BitwiseOr,
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
                    BinaryOperation.Division,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.Equal)
            {
                return new FunctionCall(
                    ComparisonOperation.Equal,
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
                    ComparisonOperation.GreaterThan,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.GreaterThanOrEqual)
            {
                return new FunctionCall(
                    ComparisonOperation.GreaterThanOrEqual,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.Index)
            //{
            //}
            else if (ex.Type == NodeType.LessThan)
            {
                return new FunctionCall(
                    ComparisonOperation.LessThan,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.LessThanOrEqual)
            {
                return new FunctionCall(
                    ComparisonOperation.LessThanOrEqual,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.LogAnd)
            {
                return new FunctionCall(
                    BinaryOperation.LogicalAnd,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.LogNot)
            //{
            //}
            else if (ex.Type == NodeType.LogOr)
            {
                return new FunctionCall(
                    BinaryOperation.LogicalOr,
                    leftArg,
                    rightArg);
            }
            //else if (ex.Type == NodeType.LogXor)
            //{
            //}
            else if (ex.Type == NodeType.Mod)
            {
                return new FunctionCall(
                    BinaryOperation.ModularDivision,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.Mult)
            {
                return new FunctionCall(
                    AssociativeCommutativeOperation.Multiplication,
                    leftArg,
                    rightArg);
            }
            else if (ex.Type == NodeType.NotEqual)
            {
                return new FunctionCall(
                    ComparisonOperation.NotEqual,
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

        public delegate Expression FunctionConverter(IEnumerable<Expression> args, VariableTable vars);

        public static FunctionConverter BasicFunctionConverter(Function function)
        {
            return (args, vars) => { return new FunctionCall(function, args); };
        }

        private Expression ConvertFunctionExpression(Ex ex, VariableTable varTable, Expression leftArg, Expression rightArg)
        {
            Function function = null;
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

            if (_functions.ContainsKey(ex.Token))
            {
                return _functions[ex.Token].Converter(args,varTable);
            }
            else
            {
                throw new SolusParseException(ex, "Unknown function \"" + ex.Token + "\"");
            }
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

        public static Brush GetBrushFromExpression(Expression expression, VariableTable varTable)
        {
            if (expression is ColorExpression)
            {
                return ((ColorExpression)expression).Brush;
            }
            else //if (arg is Literal)
            {
                float value = expression.Eval(varTable).Value;// ((Literal)arg).Value;
                int iValue = (int)(value);
                Color color = Color.FromArgb(255, Color.FromArgb(iValue));
                return new SolidBrush(color);
            }
        }

        public static Pen GetPenFromExpression(Expression arg, VariableTable varTable)
        {
            if (arg is ColorExpression)
            {
                return ((ColorExpression)arg).Pen;
            }
            else //if (arg is Literal)
            {
                float value = arg.Eval(varTable).Value;// ((Literal)arg).Value;
                int iValue = (int)(value);
                Color color = Color.FromArgb(255, Color.FromArgb(iValue));
                return new Pen(color);
            }
        }

        private static Expression ConvertPlot3dExpression(IEnumerable<Expression> _args, VariableTable varTable)
        {
            List<Expression> args = _args.ToList();

            if (args.Count < 3 ||
                !(args[0] is VariableAccess) ||
                !(args[1] is VariableAccess))
            {
                throw new SolusParseException(null, "Plot command requires two variables and one expression to plot");
            }

            if ((args.Count > 5 && args.Count < 9) ||
                args.Count == 10 ||
                args.Count > 11)
            {
                throw new SolusParseException(null, "Incorrect number of arguments");
            }

            Brush fillBrush = Brushes.Green;
            Pen wirePen = Pens.Black;
            float xMin = -4;
            float xMax = 4;
            float yMin = -4;
            float yMax = 4;
            float zMin = -2;
            float zMax = 6;

            if (args.Count == 4 || args.Count == 5)
            {
                fillBrush = GetBrushFromExpression(args[3], varTable);

                if (args.Count == 5)
                {
                    wirePen = GetPenFromExpression(args[4], varTable);
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
                fillBrush = GetBrushFromExpression(args[9], varTable);
                wirePen = GetPenFromExpression(args[10], varTable);
            }
            else if (args.Count != 3)
            {
                throw new SolusParseException(null, "Incorrect number of arguments");
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

        private static Expression ConvertMathPaintExpression(IEnumerable<Expression> _args, VariableTable varTable)
        {
            List<Expression> args = _args.ToList();

            if (!(args[0] is VariableAccess))
            {
                throw new SolusParseException(null, "First argument to MathPaint command must be a variable reference.");
            }
            if (!(args[1] is VariableAccess))
            {
                throw new SolusParseException(null, "Second argument to MathPaint command must be a variable reference.");
            }

            return new MathPaintExpression(
                ((VariableAccess)args[0]).Variable,
                ((VariableAccess)args[1]).Variable,
                (int)(args[2].Eval(varTable).Value),
                (int)(args[3].Eval(varTable).Value),
                args[4]);
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


        protected void ConvertCommaToArgs(Ex ex, List<Expression> args, VariableTable varTable)
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

        public Ex[] Tokenize(string expr)
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

        protected Ex[] Arrange(Ex[] intokens)
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

        protected Ex BuildTree(Ex[] intokens)
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

        protected Ex BuildTree(Queue<Ex> b)
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

                int funcArgs = GetFunction(ex.Token).Value.NumArguments;
                if (funcArgs != 0)
                {
                    ex.Left = BuildTree(b);

                    if (funcArgs > -1)
                    {
                        //check comma operators
                        //ensure argument count is correct

                        int nargs = CountCommaArguments(ex.Left);
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
            //else if (IsUnaryOperation(r))
            //{
            //    ex.left = BuildTree(b);
            //}
            else if (IsBinaryOperation(r))
            {
                if (b.Count < 1) { throw new SolusParseException(ex, "Missing right operand"); }
                ex.Right = BuildTree(b);
                if (b.Count < 1) { throw new SolusParseException(ex, "Missing left operand"); }
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

        protected int CountCommaArguments(Ex ex)
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
