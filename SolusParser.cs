
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Giza;
using MetaphysicsIndustries.Solus.Commands;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Transformers;
using Expression = MetaphysicsIndustries.Solus.Expressions.Expression;

namespace MetaphysicsIndustries.Solus
{
    public class SolusParser
    {
        protected SolusGrammar _grammar;
        protected Parser _parser;
        protected Parser _parserC;
        protected Spanner _numberSpanner;

        public SolusParser()
            : this(new SolusGrammar())
        {
        }

        protected SolusParser(SolusGrammar grammar)
        {
            _grammar = grammar;
            _parser = new Parser(_grammar.def_expr);
            _parserC = new Parser(_grammar.def_commands);
            _numberSpanner = new Spanner(_grammar.def_float_002D_number);
        }

        public Expression GetExpression(string input, bool cleanup = false)
        {
            var errors = new List<Error>();

            var spans = _parser.Parse(input.ToCharacterSource(), errors);

            if (errors.ContainsNonWarnings())
            {
                var error = errors.GetFirstNonWarning();
                throw new ParseException(-1, error.Description);
            }

            if (spans.Length < 1)
            {
                throw new ParseException(-1,
                    "There were more no valid parses of the input.");
            }

            if (spans.Length > 1)
            {
                throw new ParseException(-1,
                    "There were more than one valid parses of the input.");
            }

            var span = spans[0];

            var expr = GetExpressionFromExpr(span);

            if (cleanup)
            {
                var ct = new CleanUpTransformer();
                expr = ct.CleanUp(expr);
            }

            return expr;
        }

        public ICommandData[] GetCommands(string input,
            CommandSet commandSet = null)
        {
            var errors = new List<Error>();
            var spans = _parserC.Parse(input.ToCharacterSource(), errors);
            if (errors.ContainsNonWarnings())
            {
                var error = errors.GetFirstNonWarning();
                throw new ParseException(-1, error.Description);
            }

            if (spans.Length < 1)
                throw new ParseException(-1,
                    "There were more no valid parses of the input.");
            if (spans.Length > 1)
                throw new ParseException(-1,
                    "There were more than one valid parses of the input.");
            var span = spans[0];

            var commands = new List<ICommandData>();
            foreach (var sub in span.Subspans)
            {
                if (sub.DefRef != _grammar.def_command) continue;
                commands.Add(GetCommandFromCommand(sub, commandSet));
            }

            return commands.ToArray();
        }

        Dictionary<Function, int> _operatorPrecedence = new Dictionary<Function, int>()
        {
            {AdditionOperation.Value, 120},
            {MultiplicationOperation.Value, 130},
            {DivisionOperation.Value, 130},
            {ModularDivision.Value, 130},
            {ExponentOperation.Value, 135},
            {BitwiseAndOperation.Value, 100},
            {BitwiseOrOperation.Value, 80},
            {EqualComparisonOperation.Value, 70},
            {NotEqualComparisonOperation.Value, 70},
            {LessThanComparisonOperation.Value, 75},
            {LessThanOrEqualComparisonOperation.Value, 75},
            {GreaterThanComparisonOperation.Value, 75},
            {GreaterThanOrEqualComparisonOperation.Value, 75},
            {LogicalAndOperation.Value, 62},
            {LogicalOrOperation.Value, 60},
        };

        ICommandData GetCommandFromCommand(Span span, CommandSet commandSet)
        {
            var sub = span.Subspans[0];
            var def = sub.DefRef;

            if (def == _grammar.def_delete_002D_command)
                return GetDeleteCommandFromSpan(sub);
            if (def == _grammar.def_func_002D_assign_002D_command)
                return GetFuncAssignCommandFromSpan(sub);
            if (def == _grammar.def_help_002D_command)
                return GetHelpCommandFromSpan(sub, commandSet);
            if (def == _grammar.def_var_002D_assign_002D_command)
                return GetVarAssignCommandFromSpan(sub);
            if (def == _grammar.def_vars_002D_command)
                return GetVarsCommandFromSpan(sub);

            throw new ParseException(-1,
                $"Unknown command, \"{def}\"");
        }

        ICommandData GetDeleteCommandFromSpan(Span span)
        {
            var names = span.Subspans.Skip(1).
                Select(sub => sub.Subspans[0].Value);
            return new DeleteCommandData(names);
        }

        ICommandData GetFuncAssignCommandFromSpan(Span span)
        {
            var funcname = span.Subspans[0].Value;

            var args = new List<STuple<string, VariableAccess>>();
            int i;
            for (i = 1; i < span.Subspans.Count; i++)
            {
                if (span.Subspans[i].Node ==
                    _grammar.node_func_002D_assign_002D_command_9__0029_)
                    break;
                if (span.Subspans[i].DefRef != _grammar.def_identifier)
                    continue;
                var varName = span.Subspans[i].Value;
                VariableAccess varType = null;
                if (i < span.Subspans.Count - 1 &&
                    span.Subspans[i + 1].Node == _grammar
                        .node_func_002D_assign_002D_command_3__003A_ &&
                    i < span.Subspans.Count - 2 &&
                    span.Subspans[i + 2].Node == _grammar
                        .node_func_002D_assign_002D_command_4_varref)
                {
                    varType =
                        GetVariableAccessFromVarref(span.Subspans[i + 2]);
                    i += 2;
                }
                args.Add(new STuple<string, VariableAccess>(
                    varName, varType));
            }

            var expr = GetExpressionFromExpr(span.Subspans.Last());

            return new FuncAssignCommandData(funcname, args, expr);
        }

        ICommandData GetHelpCommandFromSpan(Span span, CommandSet commandSet)
        {
            var topic = "help";
            if (span.Subspans.Count >= 2)
                topic = span.Subspans[1].Value;
            return new HelpCommandData(topic);
        }

        ICommandData GetVarAssignCommandFromSpan(Span span)
        {
            var varname = span.Subspans[0].Subspans[0].Value;
            var expr = GetExpressionFromExpr(span.Subspans[2]);
            return new VarAssignCommandData(varname, expr);
        }

        ICommandData GetVarsCommandFromSpan(Span span)
        {
            return new VarsCommandData();
        }

        public Expression GetExpressionFromExpr(Span span)
        {
            var subexprs = new List<Expression>();
            var operators = new List<Function>();
            var operset = new HashSet<Function>();

            subexprs.Add(GetExpressionFromSubexpr(span.Subspans[0]));

            int i;
            for (i = 1; i < span.Subspans.Count; i += 2)
            {
                Function op;
                Expression arg = GetExpressionFromSubexpr(
                    span.Subspans[i + 1]);
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

            var sortedOperset = new List<Function>(operset);
            sortedOperset.Sort((x, y) =>
                -_operatorPrecedence[x].CompareTo(_operatorPrecedence[y]));

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

                ranges.Add(new Tuple<int, int>(start, indexes[indexes.Count - 1]));

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
                throw new InvalidOperationException(
                    $"The expressions should have been combined " +
                    $"into a single tree, but for some reason, there are " +
                    $"{subexprs.Count} separate expressions.");
            }

            return subexprs[0];
        }

        public Expression GetExpressionFromSubexpr(Span span)
        {
            var sub = span.Subspans[0];

            return GetExpressionFromSubexprPart(sub);
        }

        public Expression GetExpressionFromSubexprPart(Span span)
        {
            var defref = span.DefRef;
            if (defref == _grammar.def_paren)
                return GetExpressionFromExpr(span.Subspans[1]);
            if (defref == _grammar.def_number)
                return GetLiteralFromNumber(span);
            if (defref == _grammar.def_string)
                return GetLiteralFromString(span);
            if (defref == _grammar.def_unary_002D_op)
                return GetExpressionFromUnaryop(span);
            if (defref == _grammar.def_varref)
                return GetVariableAccessFromVarref(span);
            if (defref == _grammar.def_array_002D_literal)
                return GetTensorExpressionFromArrayLiteral(span);
            if (defref == _grammar.
                def_function_002D_call_002D_or_002D_component_002D_access)
                return GetExpressionFromFunctionCallOrComponentAccess(
                    span);
            throw new ParseException(-1,
                $"Unknown subexpression, \"{defref}\"");
        }

        public Function GetOperationFromBinop(Span span)
        {
            if (span.Value == "+")
            {
                return AdditionOperation.Value;
            }
            else if (span.Value == "-")
            {
                // NOTE: the "-" case is already covered by
                // GetExpressionFromExpr
                return null;
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
            if (span.Value == "=")
                return EqualComparisonOperation.Value;
            if (span.Value == "!=")
                return NotEqualComparisonOperation.Value;
            if (span.Value == "<")
                return LessThanComparisonOperation.Value;
            if (span.Value == "<=")
                return LessThanOrEqualComparisonOperation.Value;
            if (span.Value == ">")
                return GreaterThanComparisonOperation.Value;
            if (span.Value == ">=")
                return GreaterThanOrEqualComparisonOperation.Value;
            if (span.Value == "and")
                return LogicalAndOperation.Value;
            if (span.Value == "or")
                return LogicalOrOperation.Value;

            throw new ParseException(-1,
                $"Unknown binary operator, \"{span.Value}\"");
        }

        public Literal GetLiteralFromNumber(Span span)
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

        public Literal GetLiteralFromFloatNumber(string value)
        {
            var errors = new List<Error>();
            var spans = _numberSpanner.Process(value.ToCharacterSource(), errors);

            if (errors.ContainsNonWarnings())
                throw new ParseException(-1,
                    errors.GetFirstNonWarning().Description);
            if (spans.Length < 1)
                throw new ParseException(-1,
                    "There were more no valid parses of the input.");
            if (spans.Length > 1)
                throw new ParseException(-1,
                    "There were more than one valid parses of the input.");

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
                float fractionalValue =
                    (float) (fractionalPart * Math.Pow(10, -fractionalDigits));
                fvalue += fractionalValue;
            }

            if (hasExponent)
            {
                if (isExponentNegative)
                {
                    fvalue = (float) (fvalue * Math.Pow(10, -exponentPart));
                }
                else
                {
                    fvalue = (float) (fvalue * Math.Pow(10, exponentPart));
                }
            }

            if (isNegative)
            {
                fvalue = -fvalue;
            }

            return new Literal(fvalue);
        }

        public Expression GetLiteralFromString(Span span)
        {
            var value = span.Value;
            if (value.StartsWith("\""))
                value = value.Substring(1, value.Length - 2);
            else if (value.StartsWith("'"))
                value = value.Substring(1, value.Length - 2);
            return new Literal(value.ToStringValue());
        }

        public Expression GetExpressionFromUnaryop(Span span)
        {
            Expression arg = GetExpressionFromSubexprPart(span.Subspans[1]);

            if (span.Subspans[0].Value == "-")
            {
                arg = new FunctionCall(NegationOperation.Value, arg);
            }

            return arg;
        }

        public VariableAccess GetVariableAccessFromVarref(Span span)
        {
            string varname = span.Subspans[0].Value;

            return new VariableAccess(varname);
        }

        public TensorExpression GetTensorExpressionFromArrayLiteral(Span span)
        {
            var components = new List<List<Expression>>();

            void AddComponent(Expression expr)
            {
                if (components.Count < 1)
                    components.Add(new List<Expression>());
                components[components.Count - 1].Add(expr);
            }

            foreach (var sub in span.Subspans)
            {
                if (sub.Node is DefRefNode defref &&
                    defref.DefRef == _grammar.def_expr)
                    AddComponent(GetExpressionFromExpr(sub));
                if (sub.Node == _grammar.node_array_002D_literal_5__003B_ ||
                    sub.Node == _grammar.node_array_002D_literal_10__003B_)
                    components.Add(new List<Expression>());
            }

            if (components.Count < 1)
                return new VectorExpression(0);
            if (components.Count == 1)
                return new VectorExpression(components[0].Count,
                    components[0].ToArray());

            if (components[components.Count - 1].Count < 1)
                components.RemoveAt(components.Count - 1);
            var columnCount = components.Max(cl => cl.Count);
            var comps = new List<Expression>();
            foreach (var cl in components)
            {
                while (cl.Count < columnCount)
                    cl.Add(new Literal(0));
                comps.AddRange(cl);
            }

            return new MatrixExpression(components.Count,
                columnCount, comps.ToArray());
        }

        public Expression GetExpressionFromFunctionCallOrComponentAccess(
            Span span)
        {
            var expr = GetExpressionFromCallCompSubexpr(span.Subspans[0]);
            foreach (var sub in span.Subspans.Skip(1))
            {
                if (sub.DefRef == _grammar.def_array_002D_index)
                {
                    var indexes = GetExpressionsFromArrayIndex(sub);
                    expr = new ComponentAccess(expr, indexes);
                }
                else
                {
                    var args = GetExpressionsFromCallArgs(sub);
                    expr = new FunctionCall(expr, args);
                }
            }

            return expr;
        }

        public Expression GetExpressionFromCallCompSubexpr(Span span)
        {
            return GetExpressionFromSubexprPart(span.Subspans[0]);
        }

        public IEnumerable<Expression> GetExpressionsFromArrayIndex(Span span)
        {
            var indexes = new List<Expression>();
            foreach (var sub in span.Subspans)
            {
                if (sub.Node == _grammar.node_array_002D_index_1_expr ||
                    sub.Node == _grammar.node_array_002D_index_3_expr)
                {
                    indexes.Add(GetExpressionFromExpr(sub));
                }
            }

            return indexes;
        }

        public IEnumerable<Expression> GetExpressionsFromCallArgs(Span span)
        {
            var args = new List<Expression>();
            foreach (var sub in span.Subspans)
            {
                if (sub.Node == _grammar.node_call_002D_args_1_arg ||
                    sub.Node == _grammar.node_call_002D_args_3_arg)
                {
                    args.Add(GetExpressionFromExpr(sub));
                }
            }

            return args;
        }

        public VarIntervalExpression GetVarIntervalFromVarInterval(Span span)
        {
            if (span.Subspans[0].Node ==
                _grammar.node_var_002D_interval_0_varref)
            {
                // var in [lower,upper)
                var varname = span.Subspans[0].Subspans[0].Value;
                var interval = GetIntervalFromInterval(span.Subspans[2]);
                return new VarIntervalExpression(varname, interval);
            }
            else
            {
                // lower <= var <= upper

                var lower = GetExpressionFromExpr(span.Subspans[0]);

                var openLower = (span.Subspans[1].Value == "<");

                string varname = span.Subspans[2].Subspans[0].Value;

                var openUpper = (span.Subspans[3].Value == "<");

                var upper = GetExpressionFromExpr(span.Subspans[4]);

                return new VarIntervalExpression(
                    varname,
                    new IntervalExpression(lower, openLower,
                        upper, openUpper));
            }
        }

        public IntervalExpression GetIntervalFromInterval(Span span)
        {
            var openLower = (span.Subspans[0].Value == "(");
            var lower = GetExpressionFromExpr(span.Subspans[1]);
            var upper = GetExpressionFromExpr(span.Subspans[3]);
            var openUpper = (span.Subspans[4].Value == ")");

            return new IntervalExpression(
                lower, openLower, upper, openUpper);
        }
    }
}
