
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class ExpressionChecker
    {
        public bool IsWellFormed(Expression expr, SolusEnvironment env,
            bool throws = true)
        {
            switch (expr)
            {
                case ColorExpression ce:
                    return IsWellFormed(ce, env, throws: throws);
                case ComponentAccess ca:
                    return IsWellFormed(ca, env, throws: throws);
                case DerivativeOfVariable dov:
                    return IsWellFormed(dov, env, throws: throws);
                case FunctionCall fc:
                    return IsWellFormed(fc, env, throws: throws);
                case IntervalExpression interval:
                    return IsWellFormed(interval, env, throws: throws);
                case Literal literal:
                    return IsWellFormed(literal, env, throws: throws);
                case MatrixExpression me:
                    return IsWellFormed(me, env, throws: throws);
                case VariableAccess va:
                    return IsWellFormed(va, env, throws: throws);
                case VectorExpression ve:
                    return IsWellFormed(ve, env, throws: throws);
            }

            // throw new ArgumentException(
            //     $"Unknown expression type: {expr.GetType().Name}",
            //     nameof(expr));
            return true;
        }

        public bool IsWellFormed(ColorExpression expr, SolusEnvironment env,
            bool throws = true) =>
            true;

        public bool IsWellFormed(ComponentAccess expr, SolusEnvironment env,
            bool throws = true)
        {
            var exprResultType = expr.Expr.GetResultType(env);
            if (!exprResultType.IsSubsetOf(Tensors.Value) &&
                !exprResultType.IsSubsetOf(Strings.Value))
            {
                if (throws)
                    throw new TypeException(null,
                        "The expression should result in a type with components");
                return false;
            }

            var rv = IsWellFormed(expr.Expr, env, throws: throws);
            if (!rv) return false;
            int i;
            for (i = 0; i < expr.Indexes.Count; i++)
            {
                var indexType = expr.Indexes[i].GetResultType(env);
                if (!indexType.IsSubsetOf(Reals.Value))
                {
                    if (throws)
                        throw new TypeException($"index {i}",
                            "Index must be a real number");
                    return false;
                }

                rv = IsWellFormed(expr.Indexes[i], env, throws: throws);
                if (!rv) return false;
            }

            if (exprResultType.IsSubsetOf(Strings.Value))
            {
                if (expr.Indexes.Count != 1)
                {
                    if (throws)
                        throw new TypeException(null,
                            "Wrong number of indexes for the expression");
                    return false;
                }
            }
            else if (exprResultType.IsSubsetOf(AllVectors.Value))
            {
                if (expr.Indexes.Count != 1)
                {
                    if (throws)
                        throw new TypeException(null,
                            "Wrong number of indexes for the expression");
                    return false;
                }
            }
            else if (exprResultType.IsSubsetOf(AllMatrices.Value))
            {
                if (expr.Indexes.Count != 2)
                {
                    if (throws)
                        throw new TypeException(null,
                            "Wrong number of indexes for the expression");
                    return false;
                }
            }
            else
            {
                throw new NotImplementedException(
                    "Not implemented for high-rank tensors");
            }

            IMathObject exprValue = null;
            if (expr.Expr is Literal ee)
                exprValue = ee.Value;

            for (i = 0; i < expr.Indexes.Count; i++)
            {
                var si = expr.Indexes[i].GetResultType(env);
                if (si == null) continue;
                if (!si.IsSubsetOf(Reals.Value))
                {
                    if (throws)
                        throw new TypeException(null,
                            "Indexes must be scalar");
                    return false;
                }

                if (!(expr.Indexes[i] is Literal literal))
                    continue;
                var vv = literal.Value;
                if (!vv.IsIsScalar(env))
                {
                    if (throws)
                        throw new TypeException(null,
                            "Indexes must be scalar");
                    return false;
                }

                var vi = vv.ToNumber().Value;
                if (!vi.IsInteger())
                {
                    if (throws)
                        throw new TypeException(null,
                            "Indexes must be integers");
                    return false;
                }

                if (vi < 0)
                {
                    if (throws)
                        throw new TypeException(null,
                            "Indexes must not be negative");
                    return false;
                }

                if (i == 0 && exprResultType is Strings)
                    if (vi >= exprValue.ToStringValue().Length)
                    {
                        if (throws)
                            throw new IndexException(
                                "Index exceeds the size of the string");
                        return false;
                    }

                if (i == 0 && exprResultType is Vectors vs)
                    if (vi >= vs.Dimension)
                    {
                        if (throws)
                            throw new IndexException(
                                "Index exceeds the size of the vector");
                        return false;
                    }

                if (exprResultType is Matrices ms)
                    switch (i)
                    {
                        case 0 when vi >= ms.RowCount:
                        {
                            if (throws)
                                throw new IndexException(
                                    "Index exceeds number of rows of the" +
                                    " matrix");
                            return false;
                        }
                        case 1 when vi >= ms.ColumnCount:
                        {
                            if (throws)
                                throw new IndexException(
                                    "Index exceeds number of columns of" +
                                    " the matrix");
                            return false;
                        }
                    }
            }

            return true;
        }

        public bool IsWellFormed(DerivativeOfVariable expr,
            SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormed(FunctionCall expr, SolusEnvironment env,
            bool throws = true)
        {
            var rv = IsWellFormed(expr.Function, env, throws: throws);
            if (!rv) return false;

            IMathObject fv = expr.Function; // TODO: expr.GetResultType
            if (fv is VariableAccess va)
                fv = va.GetFinalReferencedValue(env);
            if (fv is Literal literal)
                fv = literal.Value;
            var f = fv as Function;
            if (f == null)
                throw new ArgumentException(
                    "Can't check non-function target.");

            if (f is DeriveOperator d)
            {
                var varname = expr.Arguments[1].As<VariableAccess>()
                    .VariableName;
                var env2 = env.CreateChildEnvironment();
                env2.SetVariable(varname, new Number(0)); // place-holder
                rv = IsWellFormed(expr.Arguments[0], env2, throws: throws);
                if (!rv) return false;
                return true;
            }

            if (f is IfOperator ii)
            {
                rv = IsWellFormed(expr.Arguments[0], env, throws: throws);
                if (!rv) return false;
                rv = IsWellFormed(expr.Arguments[1], env, throws: throws);
                if (!rv) return false;
                rv = IsWellFormed(expr.Arguments[2], env, throws: throws);
                if (!rv) return false;
                return true;
            }

            if (f is SubstFunction)
            {
                var varname = expr.Arguments[1].As<VariableAccess>()
                    .VariableName;
                var env2 = env.CreateChildEnvironment();
                env2.SetVariable(varname, new Number(0)); // place-holder
                rv = IsWellFormed(expr.Arguments[0], env2, throws: throws);
                if (!rv) return false;
                rv = IsWellFormed(expr.Arguments[2], env2, throws: throws);
                if (!rv) return false;
                return true;
            }

            for (var i = 0; i < expr.Arguments.Count; i++)
            {
                rv = IsWellFormed(expr.Arguments[i], env, throws: throws);
                if (!rv) return false;
            }

            var args = expr.Arguments;

            if (f is AssociativeCommutativeOperation)
            {
                if (args.Count < 2)
                {
                    if (throws)
                        throw new ArgumentException(
                            $"Wrong number of arguments given to " +
                            $"{f.DisplayName} (given {args.Count}, require " +
                            $"at least 2)");
                    return false;
                }

                for (var i = 0; i < args.Count; i++)
                {
                    var argtype = args[i].GetResultType(env);
                    if (!argtype.IsSubsetOf(Reals.Value))
                    {
                        if (throws)
                            throw new TypeException(
                                $"Argument {i} wrong type: " +
                                $"expected {Reals.Value.DisplayName} but " +
                                $"got {argtype.DisplayName}");
                        return false;
                    }
                }

                return true;
            }

            switch (f)
            {
                // case AbsoluteValueFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case AdditionOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArccosecantFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArccosineFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArccotangentFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArcsecantFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArcsineFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case Arctangent2Function ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArctangentFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case BitwiseAndOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case BitwiseOrOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CeilingFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CosecantFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CosineFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CotangentFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case DistFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case DistSqFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case DivisionOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case EqualComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ExponentOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case FactorialFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case FloorFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case GreaterThanComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case GreaterThanOrEqualComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LessThanComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LessThanOrEqualComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LoadImageFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case Log10Function ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case Log2Function ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LogarithmFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LogicalAndOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LogicalOrOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                case MaximumFiniteFunction ff:
                    rv = IsWellFormedFunctionCall(ff, args, env,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                case MaximumFunction ff:
                    rv = IsWellFormedFunctionCall(ff, args, env,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                case MinimumFiniteFunction ff:
                    rv = IsWellFormedFunctionCall(ff, args, env,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                case MinimumFunction ff:
                    rv = IsWellFormedFunctionCall(ff, args, env,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                // case ModularDivision ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case MultiplicationOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case NaturalLogarithmFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case NegationOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case NotEqualComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case SecantFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case SineFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                case SizeFunction ff:
                    rv = IsWellFormedFunctionCall(ff, args, env,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                // case TangentFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case UnitStepFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case UserDefinedFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, env, throws: throws);
                //     if (!rv) return false;
                //     return true;
            }

            if (args.Count != f.Parameters.Count)
            {
                if (throws)
                    throw new ArgumentException(
                        $"Wrong number of arguments given to " +
                        $"{f.DisplayName} (expected {f.Parameters.Count} " +
                        $"but got {args.Count})");
                return false;
            }

            for (var i = 0; i < args.Count; i++)
            {
                var argtype = args[i].GetResultType(env);
                if (!argtype.IsSubsetOf(f.Parameters[i].Type))
                {
                    if (throws)
                        throw new TypeException(
                            $"Argument {i} wrong type: expected " +
                            $"{f.Parameters[i].Type.DisplayName} but got " +
                            $"{argtype.DisplayName}");
                    return false;
                }
            }

            return true;
        }

        public bool IsWellFormed(IntervalExpression expr, SolusEnvironment env,
            bool throws = true)
        {
            var rv = IsWellFormed(expr.LowerBound, env, throws: throws);
            if (!rv) return false;
            var lower = expr.LowerBound.GetResultType(env);
            if (!lower.IsSubsetOf(Reals.Value))
            {
                if (throws)
                    throw new TypeException(null,
                        "Lower bound is not a scalar");
                return false;
            }

            rv = IsWellFormed(expr.UpperBound, env, throws: throws);
            if (!rv) return false;
            var upper = expr.UpperBound.GetResultType(env);
            if (!upper.IsSubsetOf(Reals.Value))
            {
                if (throws)
                    throw new TypeException(null,
                        "Upper bound is not a scalar");
                return false;
            }

            return true;
        }

        public bool IsWellFormed(Literal expr, SolusEnvironment env,
            bool throws = true) => true;

        public bool IsWellFormed(MatrixExpression expr, SolusEnvironment env,
            bool throws = true)
        {
            for (int r = 0; r < expr.RowCount; r++)
            for (int c = 0; c < expr.ColumnCount; c++)
            {
                if (!expr[r, c].GetResultType(env).IsSubsetOf(Reals.Value))
                {
                    if (throws)
                        throw new TypeException(
                            "All components must be reals");
                    return false;
                }

                var rv = IsWellFormed(expr[r, c], env, throws: throws);
                if (!rv) return false;
            }

            return true;
        }

        public bool IsWellFormed(VariableAccess expr, SolusEnvironment env,
            bool throws = true) =>
            true;

        public bool IsWellDefined(VariableAccess expr, SolusEnvironment env,
            bool throws = true)
        {
            if (!env.ContainsVariable(expr.VariableName))
            {
                if (throws)
                    throw new NameException(
                        $"Variable not found: {expr.VariableName}");
                return false;
            }

            var target = env.GetVariable(expr.VariableName);
            if (target.IsIsExpression(env))
            {
                var rv = IsWellFormed((Expression)target, env, throws: throws);
                if (!rv) return false;
            }

            return true;
        }

        public bool IsWellFormed(VectorExpression expr, SolusEnvironment env,
            bool throws = true)
        {
            for (var i = 0; i < expr.Length; i++)
            {
                if (!expr[i].GetResultType(env).IsSubsetOf(Reals.Value))
                {
                    if (throws)
                        throw new TypeException(
                            "All components must be reals");
                    return false;
                }

                var rv = IsWellFormed(expr[i], env, throws: throws);
                if (!rv) return false;
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(AbsoluteValueFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(AdditionOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArccosecantFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArccosineFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArccotangentFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArcsecantFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArcsineFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(Arctangent2Function ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArctangentFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(BitwiseAndOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(BitwiseOrOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(CeilingFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(CosecantFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(CosineFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(CotangentFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(DistFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(DistSqFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(DivisionOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(EqualComparisonOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ExponentOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(FactorialFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(FloorFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(GreaterThanComparisonOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(
            GreaterThanOrEqualComparisonOperation ff, List<Expression> args,
            SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(LessThanComparisonOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(
            LessThanOrEqualComparisonOperation ff, List<Expression> args,
            SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(LoadImageFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(Log10Function ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(Log2Function ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(LogarithmFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(LogicalAndOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(LogicalOrOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(MaximumFiniteFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            if (args.Count < 1)
            {
                if (throws)
                    throw new ArgumentException("No arguments passed");
                return false;
            }

            for (var i = 0; i < args.Count; i++)
            {
                var argtype = args[i].GetResultType(env);
                if (!argtype.IsSubsetOf(Reals.Value))
                {
                    if (throws)
                        throw new ArgumentException(
                            $"Argument {i} wrong type: expected " +
                            $"Scalar but got {argtype}");
                    return false;
                }
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(MaximumFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            if (args.Count < 1)
            {
                if (throws)
                    throw new ArgumentException("No arguments passed");
                return false;
            }

            for (var i = 0; i < args.Count; i++)
            {
                var argtype = args[i].GetResultType(env);
                if (!argtype.IsSubsetOf(Reals.Value))
                {
                    if (throws)
                        throw new ArgumentException(
                            $"Argument {i} wrong type: expected " +
                            $"Scalar but got {argtype}");
                    return false;
                }
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(MinimumFiniteFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            if (args.Count < 1)
            {
                if (throws)
                    throw new ArgumentException("No arguments passed");
                return false;
            }

            for (var i = 0; i < args.Count; i++)
            {
                var argtype = args[i].GetResultType(env);
                if (!argtype.IsSubsetOf(Reals.Value))
                {
                    if (throws)
                        throw new ArgumentException(
                            $"Argument {i} wrong type: expected " +
                            $"Scalar but got {argtype}");
                    return false;
                }
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(MinimumFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            if (args.Count < 1)
            {
                if (throws)
                    throw new ArgumentException("No arguments passed");
                return false;
            }

            for (var i = 0; i < args.Count; i++)
            {
                var argtype = args[i].GetResultType(env);
                if (!argtype.IsSubsetOf(Reals.Value))
                {
                    if (throws)
                        throw new ArgumentException(
                            $"Argument {i} wrong type: expected " +
                            $"Scalar but got {argtype}");
                    return false;
                }
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(ModularDivision ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(MultiplicationOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(NaturalLogarithmFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(NegationOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(NotEqualComparisonOperation ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(SecantFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(SineFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(SizeFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            if (args.Count != 1)
            {
                if (throws)
                    throw new ArgumentException(
                        $"Wrong number of arguments given to " +
                        $"{ff.DisplayName} (expected 1 but got " +
                        $"{args.Count})");
                return false;
            }

            var argtype = args[0].GetResultType(env);
            if (!(argtype.IsSubsetOf(AllVectors.Value) ||
                  argtype.IsSubsetOf(AllMatrices.Value) ||
                  argtype.IsSubsetOf(Strings.Value)))
            {
                if (throws)
                    throw new ArgumentException(
                        "Argument wrong type: expected Vector " +
                        $"or Matrix or String but got {argtype.DisplayName}");
                return false;
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(TangentFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(UnitStepFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(UserDefinedFunction ff,
            List<Expression> args, SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }
    }
}
