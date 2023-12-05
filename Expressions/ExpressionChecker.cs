
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
    public partial class ExpressionChecker
    {
        public bool IsWellFormed(Expression expr,
            bool throws = true)
        {
            switch (expr)
            {
                case ColorExpression ce:
                    return IsWellFormed(ce, throws: throws);
                case ComponentAccess ca:
                    return IsWellFormed(ca, throws: throws);
                case DerivativeOfVariable dov:
                    return IsWellFormed(dov, throws: throws);
                case FunctionCall fc:
                    return IsWellFormed(fc, throws: throws);
                case IntervalExpression interval:
                    return IsWellFormed(interval, throws: throws);
                case Literal literal:
                    return IsWellFormed(literal, throws: throws);
                case MatrixExpression me:
                    return IsWellFormed(me, throws: throws);
                case VariableAccess va:
                    return IsWellFormed(va, throws: throws);
                case VectorExpression ve:
                    return IsWellFormed(ve, throws: throws);
            }

            // throw new ArgumentException(
            //     $"Unknown expression type: {expr.GetType().Name}",
            //     nameof(expr));
            return true;
        }

        public bool IsWellFormed(ColorExpression expr,
            bool throws = true) =>
            true;

        public bool IsWellFormed(ComponentAccess expr,
            bool throws = true)
        {
            var rv = IsWellFormed(expr.Expr, throws: throws);
            if (!rv) return false;
            int i;
            for (i = 0; i < expr.Indexes.Count; i++)
            {
                rv = IsWellFormed(expr.Indexes[i], throws: throws);
                if (!rv) return false;
            }

            return true;
        }

        public bool IsWellFormed(DerivativeOfVariable expr,
            SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormed(FunctionCall expr,
            bool throws = true)
        {
            var rv = IsWellFormed(expr.Function, throws: throws);
            if (!rv) return false;

            IMathObject fv = expr.Function; // TODO: expr.GetResultType
            if (fv is Literal literal)
                fv = literal.Value;
            var f = fv as Function;
            if (f == null)
                return true;

            if (f is DeriveOperator d)
            {
                rv = IsWellFormed(expr.Arguments[0], throws: throws);
                if (!rv) return false;
                return true;
            }

            if (f is IfOperator ii)
            {
                rv = IsWellFormed(expr.Arguments[0], throws: throws);
                if (!rv) return false;
                rv = IsWellFormed(expr.Arguments[1], throws: throws);
                if (!rv) return false;
                rv = IsWellFormed(expr.Arguments[2], throws: throws);
                if (!rv) return false;
                return true;
            }

            if (f is SubstFunction)
            {
                rv = IsWellFormed(expr.Arguments[0], throws: throws);
                if (!rv) return false;
                rv = IsWellFormed(expr.Arguments[2], throws: throws);
                if (!rv) return false;
                return true;
            }

            int i;
            for (i = 0; i < expr.Arguments.Count; i++)
            {
                rv = IsWellFormed(expr.Arguments[i], throws: throws);
                if (!rv) return false;
            }

            var args = expr.Arguments;

            if (f is IAssociativeCommutativeOperation)
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

                return true;
            }

            switch (f)
            {
                // case AbsoluteValueFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case AdditionOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArccosecantFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArccosineFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArccotangentFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArcsecantFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArcsineFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case Arctangent2Function ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArctangentFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case BitwiseAndOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case BitwiseOrOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CeilingFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CosecantFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CosineFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CotangentFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case DistFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case DistSqFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case DivisionOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case EqualComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ExponentOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case FactorialFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case FloorFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case GreaterThanComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case GreaterThanOrEqualComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LessThanComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LessThanOrEqualComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LoadImageFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case Log10Function ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case Log2Function ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LogarithmFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LogicalAndOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LogicalOrOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                case MaximumFiniteFunction ff:
                    rv = IsWellFormedFunctionCall(ff, args,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                case MaximumFunction ff:
                    rv = IsWellFormedFunctionCall(ff, args,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                case MinimumFiniteFunction ff:
                    rv = IsWellFormedFunctionCall(ff, args,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                case MinimumFunction ff:
                    rv = IsWellFormedFunctionCall(ff, args,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                // case ModularDivision ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case MultiplicationOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case NaturalLogarithmFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case NegationOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case NotEqualComparisonOperation ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case SecantFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case SineFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                case SizeFunction ff:
                    rv = IsWellFormedFunctionCall(ff, args,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                // case TangentFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case UnitStepFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case UserDefinedFunction ff:
                //     rv = IsWellFormedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
            }

            if (f.FunctionType is VariadicFunctions vf)
            {
                if (args.Count < vf.MinimumNumberOfArguments)
                {
                    if (throws)
                        throw new TypeException(
                            $"Wrong number of arguments given to " +
                            $"{f.DisplayName} (expected aat least " +
                            $"{vf.MinimumNumberOfArguments} but got " +
                            $"{args.Count})");
                    return false;
                }
            }
            if (!f.IsVariadic && 
                args.Count != f.Parameters.Count)
            {
                if (throws)
                    throw new TypeException(
                        $"Wrong number of arguments given to " +
                        $"{f.DisplayName} (expected {f.Parameters.Count} " +
                        $"but got {args.Count})");
                return false;
            }

            return true;
        }

        public bool IsWellFormed(IntervalExpression expr,
            bool throws = true)
        {
            var rv = IsWellFormed(expr.LowerBound, throws: throws);
            if (!rv) return false;

            rv = IsWellFormed(expr.UpperBound, throws: throws);
            if (!rv) return false;

            return true;
        }

        public bool IsWellFormed(Literal expr,
            bool throws = true) => true;

        public bool IsWellFormed(MatrixExpression expr,
            bool throws = true)
        {
            for (int r = 0; r < expr.RowCount; r++)
            for (int c = 0; c < expr.ColumnCount; c++)
            {
                var rv = IsWellFormed(expr[r, c], throws: throws);
                if (!rv) return false;
            }

            return true;
        }

        public bool IsWellFormed(VariableAccess expr,
            bool throws = true) =>
            true;

        public bool IsWellFormed(VectorExpression expr, bool throws = true)
        {
            for (var i = 0; i < expr.Length; i++)
            {
                var rv = IsWellFormed(expr[i], throws: throws);
                if (!rv) return false;
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(AbsoluteValueFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(AdditionOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArccosecantFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArccosineFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArccotangentFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArcsecantFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArcsineFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(Arctangent2Function ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ArctangentFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(BitwiseAndOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(BitwiseOrOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(CeilingFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(CosecantFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(CosineFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(CotangentFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(DistFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(DistSqFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(DivisionOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(EqualComparisonOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(ExponentOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(FactorialFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(FloorFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(GreaterThanComparisonOperation ff,
            List<Expression> args, bool throws = true)
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
            List<Expression> args, bool throws = true)
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
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(Log10Function ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(Log2Function ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(LogarithmFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(LogicalAndOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(LogicalOrOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(MaximumFiniteFunction ff,
            List<Expression> args, bool throws = true)
        {
            if (args.Count < 1)
            {
                if (throws)
                    throw new ArgumentException("No arguments passed");
                return false;
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(MaximumFunction ff,
            List<Expression> args, bool throws = true)
        {
            if (args.Count < 1)
            {
                if (throws)
                    throw new ArgumentException("No arguments passed");
                return false;
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(MinimumFiniteFunction ff,
            List<Expression> args, bool throws = true)
        {
            if (args.Count < 1)
            {
                if (throws)
                    throw new ArgumentException("No arguments passed");
                return false;
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(MinimumFunction ff,
            List<Expression> args, bool throws = true)
        {
            if (args.Count < 1)
            {
                if (throws)
                    throw new ArgumentException("No arguments passed");
                return false;
            }

            return true;
        }

        public bool IsWellFormedFunctionCall(ModularDivision ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(MultiplicationOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(NaturalLogarithmFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(NegationOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(NotEqualComparisonOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(SecantFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(SineFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(SizeFunction ff,
            List<Expression> args, bool throws = true)
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

            return true;
        }

        public bool IsWellFormedFunctionCall(TangentFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(UnitStepFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellFormedFunctionCall(UserDefinedFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }
    }
}
