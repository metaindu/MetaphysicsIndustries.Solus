using System;
using MetaphysicsIndustries.Giza;
using System.Collections.Generic;
using System.Linq;

namespace MetaphysicsIndustries.Solus
{
    public class SolusParser2
    {
        SolusGrammar _grammar = new SolusGrammar();
        Parser _parser;
        Spanner _numberSpanner;

        public SolusParser2()
        {
            _parser = new Parser(_grammar.def_expr);
            _numberSpanner = new Spanner(_grammar.def_float_002D_number);

            foreach (ParseFunction func in _builtinFunctions)
            {
                AddFunction(func);
            }
            foreach (var kvp in _builtinMacros)
            {
                AddFunction(new ParseFunction { Token=kvp.Key, Converter=kvp.Value });
            }
        }

        protected static readonly List<ParseFunction> _builtinFunctions = new List<ParseFunction>()
        {
            new ParseFunction(){ Token="sin",       Converter=ParseFunction.BasicFunctionConverter(SineFunction.Value),             NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="cos",       Converter=ParseFunction.BasicFunctionConverter(CosineFunction.Value),           NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="tan",       Converter=ParseFunction.BasicFunctionConverter(TangentFunction.Value),          NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="ln",        Converter=ParseFunction.BasicFunctionConverter(NaturalLogarithmFunction.Value), NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="log2",      Converter=ParseFunction.BasicFunctionConverter(Log2Function.Value),             NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="log10",     Converter=ParseFunction.BasicFunctionConverter(Log10Function.Value),            NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="abs",       Converter=ParseFunction.BasicFunctionConverter(AbsoluteValueFunction.Value),    NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="sec",       Converter=ParseFunction.BasicFunctionConverter(SecantFunction.Value),           NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="csc",       Converter=ParseFunction.BasicFunctionConverter(CosecantFunction.Value),         NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="cot",       Converter=ParseFunction.BasicFunctionConverter(CotangentFunction.Value),        NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="acos",      Converter=ParseFunction.BasicFunctionConverter(ArccosineFunction.Value),        NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="asin",      Converter=ParseFunction.BasicFunctionConverter(ArcsineFunction.Value),          NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="atan",      Converter=ParseFunction.BasicFunctionConverter(ArctangentFunction.Value),       NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="asec",      Converter=ParseFunction.BasicFunctionConverter(ArcsecantFunction.Value),        NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="acsc",      Converter=ParseFunction.BasicFunctionConverter(ArccosecantFunction.Value),      NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="acot",      Converter=ParseFunction.BasicFunctionConverter(ArccotangentFunction.Value),     NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="ceil",      Converter=ParseFunction.BasicFunctionConverter(CeilingFunction.Value),          NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="floor",     Converter=ParseFunction.BasicFunctionConverter(FloorFunction.Value),            NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="unitstep",  Converter=ParseFunction.BasicFunctionConverter(UnitStepFunction.Value),         NumArguments=1,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="atan2",     Converter=ParseFunction.BasicFunctionConverter(Arctangent2Function.Value),      NumArguments=2,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="log",       Converter=ParseFunction.BasicFunctionConverter(LogarithmFunction.Value),        NumArguments=2,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="if",        Converter=ParseFunction.BasicFunctionConverter(IfFunction.Value),               NumArguments=3,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="dist",      Converter=ParseFunction.BasicFunctionConverter(DistFunction.Value),             NumArguments=2,  HasVariableNumArgs=false },
            new ParseFunction(){ Token="distsq",    Converter=ParseFunction.BasicFunctionConverter(DistSqFunction.Value),           NumArguments=2,  HasVariableNumArgs=false },
        };
        protected static readonly Dictionary<string, FunctionConverter> _builtinMacros = new Dictionary<string, FunctionConverter>
        {
            { "sqrt", SolusParser1.ConvertSqrtFunction },
            { "rand", (args, vars) => { return new RandomExpression(); } },
            { "derive", SolusParser1.ConvertDeriveExpression },
            { "feedback", SolusParser1.ConvertFeedbackExpression },
            { "subst", SolusParser1.ConvertSubstExpression },
        };

        public Expression Compile(string input, Dictionary<string, Expression> vars=null, bool cleanup=false)
        {
            if (vars == null)
            {
                vars = new Dictionary<string, Expression>();
            }

            return GetExpression(input, vars);
        }

        private Dictionary<string, ParseFunction> _functions = new Dictionary<string, ParseFunction>(StringComparer.CurrentCultureIgnoreCase);

        public void AddFunction(ParseFunction func)
        {
            if (_functions.ContainsKey(func.Token)) throw new ArgumentException("A function already uses that token.", "func");

            _functions.Add(func.Token, func);
        }

        protected ParseFunction? GetFunction(string token)
        {
            if (!_functions.ContainsKey(token)) return null;

            return _functions[token];
        }

        public Expression GetExpression(string input, Dictionary<string, Expression> vars)
        {
            var errors = new List<Error>();

            var spans = _parser.Parse(input, errors);

            if (errors.ContainsNonWarnings())
            {
                throw new InvalidOperationException();
            }

            if (spans.Length < 1)
            {
                throw new InvalidOperationException();
            }

            if (spans.Length > 1)
            {
                throw new InvalidOperationException();
            }

            var span = spans[0];

            return GetExpressionFromExpr(span, vars);
        }

        Dictionary<Function, int> _operatorPrecedence = new Dictionary<Function, int>() {
            { AdditionOperation.Value, 120 },
            { MultiplicationOperation.Value, 130 },
            { DivisionOperation.Value, 130 },
            { ModularDivision.Value, 130 },
            { ExponentOperation.Value, 135 },
            { BitwiseAndOperation.Value, 100 },
            { BitwiseOrOperation.Value, 80 },
        };

        Expression GetExpressionFromExpr(Span span, Dictionary<string, Expression> vars)
        {
            var subexprs = new List<Expression>();
            var operators = new List<Operation>();
            var operset = new HashSet<Operation>();

            subexprs.Add(GetExpressionFromSubexpr(span.Subspans[0], vars));

            int i;
            for (i = 1; i < span.Subspans.Count; i += 2)
            {
                Operation op;
                Expression arg = GetExpressionFromSubexpr(span.Subspans[i + 1], vars);
                if (span.Subspans[i].Value == "-")
                {
                    op = AdditionOperation.Value;
                    arg = new FunctionCall(NegationOperation.Value, arg);
                }
                else
                {
                    op = GetOperationFromBinop(span.Subspans[i]);
                }
                operators.Add(op);
                subexprs.Add(arg);
                operset.Add(op);
            }

            var sortedOperset = new List<Operation>(operset);
            sortedOperset.Sort((x,y) => -(_operatorPrecedence[x].CompareTo(_operatorPrecedence[y])));

            foreach (var op in sortedOperset)
            {
                var indexes = Enumerable.Range(0, operators.Count).Where(ix => operators[ix] == op).ToList();
                var ranges = new List<Tuple<int, int>>();
                int start = indexes[0];
                for (i = 1; i < indexes.Count; i++)
                {
                    if (indexes[i] != indexes[i - 1] + 1)
                    {
                        ranges.Add(new Tuple<int, int>(start, indexes[i - 1]));
                        start = indexes[i];
                    }
                }
                ranges.Add(new Tuple<int, int>(start, indexes[indexes.Count-1]));

                Queue<Expression> newExpressions = new Queue<Expression>();
                foreach (var range in ranges)
                {
                    int first = range.Item1;
                    int last = range.Item2;

                    if (op.IsAssociative && 
                        op.IsCommutative)
                    {
                        int count = last - first + 1;
                        var fcargs = subexprs.GetRange(first, count + 1);
                        FunctionCall fcall = new FunctionCall(op, fcargs);
                        newExpressions.Enqueue(fcall);
                    }
                    else
                    {
                        var leftarg = subexprs[first];
                        for (i = first; i <= last; i++)
                        {
                            var rightarg = subexprs[i + 1];
                            var fcall = new FunctionCall(op, leftarg, rightarg);
                            leftarg = fcall;
                        }
                        newExpressions.Enqueue(leftarg);
                    }
                }

                int adjust = 0;
                foreach (var range in ranges)
                {
                    int first = range.Item1 - adjust;
                    int last = range.Item2 - adjust;

                    int count = last - first + 1;

                    operators.RemoveRange(first, count);
                    subexprs.RemoveRange(first, count + 1);
                    subexprs.Insert(first, newExpressions.Dequeue());

                    adjust += count;
                }
            }

            if (subexprs.Count != 1)
            {
                throw new InvalidOperationException();
            }

            return subexprs[0];
        }

        Expression GetExpressionFromSubexpr(Span span, Dictionary<string, Expression> vars)
        {
            var sub = span.Subspans[0];

            return GetExpressionFromSubexprPart(sub, vars);
        }

        Expression GetExpressionFromSubexprPart(Span span, Dictionary<string, Expression> vars)
        {
            var defref = span.DefRef;
            if (defref == _grammar.def_paren)
            {
                return GetExpressionFromExpr(span.Subspans[1], vars);
            }
            else if (defref == _grammar.def_function_002D_call)
            {
                return GetFunctionCallFromFunctioncall(span, vars);
            }
            else if (defref == _grammar.def_number)
            {
                return GetLiteralFromNumber(span);
            }
            else if (defref == _grammar.def_string)
            {
                return GetStringFromString(span);
            }
            else if (defref == _grammar.def_unary_002D_op)
            {
                return GetExpressionFromUnaryop(span, vars);
            }
            else if (defref == _grammar.def_varref)
            {
                return GetVariableAccessFromVarref(span, vars);
            }

            throw new NotImplementedException();
        }

        Operation GetOperationFromBinop(Span span)
        {
            if (span.Value == "+")
            {
                return AdditionOperation.Value;
            }
            else if (span.Value == "-")
            {
                throw new NotImplementedException();
            }
            else if (span.Value == "*")
            {
                return MultiplicationOperation.Value;
            }
            else if (span.Value == "/")
            {
                return DivisionOperation.Value;
            }
            else if (span.Value == "%")
            {
                return ModularDivision.Value;
            }
            else if (span.Value == "^")
            {
                return ExponentOperation.Value;
            }
            else if (span.Value == "&")
            {
                return BitwiseAndOperation.Value;
            }
            else if (span.Value == "|")
            {
                return BitwiseOrOperation.Value;
            }

            throw new InvalidOperationException();
        }

        Expression GetFunctionCallFromFunctioncall(Span span, Dictionary<string, Expression> vars)
        {
            var name = span.Subspans[0].Value;
            ParseFunction? pfunc = GetFunction(name);
            if (pfunc == null)
            {
                throw new InvalidOperationException("Unknown function \"" + name + "\"");
            }

            var args = new List<Expression>();
            int i;
            for (i = 2; i < span.Subspans.Count - 1; i += 2)
            {
                args.Add(GetExpressionFromExpr(span.Subspans[i], vars));
            }

            return pfunc.Value.Converter(args, vars);
        }

        Literal GetLiteralFromNumber(Span span)
        {
            // thoroughly inexact, not conformant to standards, but will suffice for now.

            string value = span.Value;
            string lower = value.ToLower();

            if (value.StartsWith("0b"))
            {
                return new Literal(Convert.ToInt32(value.Substring(2), 2));
            }
            else if (value.StartsWith("0o"))
            {
                return new Literal(Convert.ToInt32(value.Substring(2), 8));
            }
            else if (value.StartsWith("0x"))
            {
                return new Literal(Convert.ToInt32(value.Substring(2), 16));
            }
            else
            {
                return GetLiteralFromFloatNumber(value);
            }
        }

        Literal GetLiteralFromFloatNumber(string value)
        {
            var errors = new List<Error>();
            var spans = _numberSpanner.Process(value, errors);

            if (errors.ContainsNonWarnings())
            {
                throw new NotImplementedException();
            }

            if (spans.Length < 1)
            {
                throw new NotImplementedException();
            }

            if (spans.Length > 1)
            {
                throw new NotImplementedException();
            }

            var s = spans[0];
            var subs = s.Subspans;
            int i = 0;
            int n = subs.Count;

            bool isNegative = false;
            int integerPart = 0;
            bool hasFractionalPart = false;
            int fractionalPart = 0;
            int fractionalDigits = 0;
            bool hasExponent = false;
            bool isExponentNegative = false;
            int exponentPart = 0;
            int exponentDigits = 0;

            if (subs[i].Node == _grammar.node_float_002D_number_0__002B__002D_)
            {
                if (subs[i].Value == "-")
                {
                    isNegative = true;
                }
                i++;
            }
            while (i < n && subs[i].Node == _grammar.node_float_002D_number_1__005C_d)
            {
                integerPart *= 10;
                integerPart += int.Parse(subs[i].Value);
                i++;
            }
            if (i < n && subs[i].Node == _grammar.node_float_002D_number_2__002E_)
            {
                i++;
                hasFractionalPart = true;
                while (i < n && subs[i].Node == _grammar.node_float_002D_number_3__005C_d)
                {
                    fractionalPart *= 10;
                    fractionalPart += int.Parse(subs[i].Value);
                    fractionalDigits++;
                    i++;
                }
            }
            if (i < n && subs[i].Node == _grammar.node_float_002D_number_4_Ee)
            {
                hasExponent = true;
                i++;
                if (subs[i].Node == _grammar.node_float_002D_number_5__002B__002D_)
                {
                    if (subs[i].Value == "-")
                    {
                        isExponentNegative = true;
                    }
                    i++;
                }
                while (i < n && subs[i].Node == _grammar.node_float_002D_number_6__005C_d)
                {
                    exponentPart *= 10;
                    exponentPart += int.Parse(subs[i].Value);
                    exponentDigits++;
                    i++;
                }
            }

            float fvalue = integerPart;

            if (hasFractionalPart)
            {
                float fractionalValue = (float)(fractionalPart * Math.Pow(10, -fractionalDigits));
                fvalue += fractionalValue;
            }

            if (hasExponent)
            {
                if (isExponentNegative)
                {
                    fvalue = (float)(fvalue * Math.Pow(10, -exponentPart));
                }
                else
                {
                    fvalue = (float)(fvalue * Math.Pow(10, exponentPart));
                }
            }

            return new Literal(fvalue);
        }

        Expression GetStringFromString(Span span)
        {
            throw new NotImplementedException();
        }

        Expression GetExpressionFromUnaryop(Span span, Dictionary<string, Expression> vars)
        {
            Expression arg = GetExpressionFromSubexprPart(span.Subspans[1], vars);

            if (span.Subspans[0].Value == "-")
            {
                arg = new FunctionCall(NegationOperation.Value, arg);
            }

            return arg;
        }

        VariableAccess GetVariableAccessFromVarref(Span span, Dictionary<string, Expression> vars)
        {
            string varname = span.Subspans[0].Value;

            return new VariableAccess(varname);
        }



    }
}

