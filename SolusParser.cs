
using System;
using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Giza;

namespace MetaphysicsIndustries.Solus
{
    public class SolusParser
    {
        SolusGrammar _grammar = new SolusGrammar();
        Parser _parser;
        Spanner _numberSpanner;

        public SolusParser()
        {
            _parser = new Parser(_grammar.def_expr);
            _numberSpanner = new Spanner(_grammar.def_float_002D_number);
        }

        public Expression GetExpression(string input, SolusEnvironment env=null, bool cleanup=false)
        {
            if (env == null)
            {
                env = new SolusEnvironment();
            }

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

            var expr = GetExpressionFromExpr(span, env);

            if (cleanup)
            {
                var ct = new CleanUpTransformer();
                expr = ct.CleanUp(expr);
            }

            return expr;
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

        Expression GetExpressionFromExpr(Span span, SolusEnvironment env)
        {
            var subexprs = new List<Expression>();
            var operators = new List<Operation>();
            var operset = new HashSet<Operation>();

            subexprs.Add(GetExpressionFromSubexpr(span.Subspans[0], env));

            int i;
            for (i = 1; i < span.Subspans.Count; i += 2)
            {
                Operation op;
                Expression arg = GetExpressionFromSubexpr(span.Subspans[i + 1], env);
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

        Expression GetExpressionFromSubexpr(Span span, SolusEnvironment env)
        {
            var sub = span.Subspans[0];

            return GetExpressionFromSubexprPart(sub, env);
        }

        Expression GetExpressionFromSubexprPart(Span span, SolusEnvironment env)
        {
            var defref = span.DefRef;
            if (defref == _grammar.def_paren)
            {
                return GetExpressionFromExpr(span.Subspans[1], env);
            }
            else if (defref == _grammar.def_function_002D_call)
            {
                return GetFunctionCallFromFunctioncall(span, env);
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
                return GetExpressionFromUnaryop(span, env);
            }
            else if (defref == _grammar.def_varref)
            {
                return GetVariableAccessFromVarref(span, env);
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

        Expression GetFunctionCallFromFunctioncall(Span span, SolusEnvironment env)
        {
            var name = span.Subspans[0].Value;

            var args = new List<Expression>();
            int i;
            for (i = 2; i < span.Subspans.Count - 1; i += 2)
            {
                args.Add(GetExpressionFromExpr(span.Subspans[i], env));
            }

            if (env.Functions.ContainsKey(name))
            {
                return new FunctionCall(env.Functions[name], args); 
            }
            else if (env.Macros.ContainsKey(name))
            {
                return env.Macros[name].Call(args, env);
            }
            else
            {
                throw new InvalidOperationException("Unknown function \"" + name + "\"");
            }
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

        Expression GetExpressionFromUnaryop(Span span, SolusEnvironment env)
        {
            Expression arg = GetExpressionFromSubexprPart(span.Subspans[1], env);

            if (span.Subspans[0].Value == "-")
            {
                arg = new FunctionCall(NegationOperation.Value, arg);
            }

            return arg;
        }

        VariableAccess GetVariableAccessFromVarref(Span span, SolusEnvironment env)
        {
            string varname = span.Subspans[0].Value;

            return new VariableAccess(varname);
        }
    }
}
