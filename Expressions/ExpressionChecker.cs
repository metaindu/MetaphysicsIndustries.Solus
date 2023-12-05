
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
using MetaphysicsIndustries.Solus.Macros;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class ExpressionChecker
    {
        public void Check(Expression expr, SolusEnvironment env)
        {
            switch (expr)
            {
                case ColorExpression ce:
                    Check(ce, env);
                    break;
                case ComponentAccess ca:
                    Check(ca, env);
                    break;
                case DerivativeOfVariable dov:
                    Check(dov, env);
                    break;
                case FunctionCall fc:
                    Check(fc, env);
                    break;
                case IntervalExpression interval:
                    Check(interval, env);
                    break;
                case Literal literal:
                    Check(literal, env);
                    break;
                case MatrixExpression me:
                    Check(me, env);
                    break;
                case RandomExpression re:
                    Check(re, env);
                    break;
                case VariableAccess va:
                    Check(va, env);
                    break;
                case VectorExpression ve:
                    Check(ve, env);
                    break;
                // default:
                //     throw new ArgumentException(
                //         $"Unknown expression type: {expr.GetType().Name}",
                //         nameof(expr));
            }
        }

        public void Check(ColorExpression expr, SolusEnvironment env) { }

        public void Check(ComponentAccess expr, SolusEnvironment env)
        {
            var exprResultType = expr.Expr.GetResultType(env);
            if (!exprResultType.IsSubsetOf(Tensors.Value) &&
                !exprResultType.IsSubsetOf(Strings.Value))
                throw new TypeException(null,
                    "The expression should result in a type with components");

            Check(expr.Expr, env);
            int i;
            for (i = 0; i < expr.Indexes.Count; i++)
            {
                var indexType = expr.Indexes[i].GetResultType(env);
                if (!indexType.IsSubsetOf(Reals.Value))
                    throw new TypeException($"index {i}",
                        "Index must be a real number");
                Check(expr.Indexes[i], env);
            }

            if (exprResultType.IsSubsetOf(Strings.Value))
            {
                if (expr.Indexes.Count != 1)
                    throw new TypeException(null,
                        "Wrong number of indexes for the expression");
            }
            else if (exprResultType.IsSubsetOf(AllVectors.Value))
            {
                if (expr.Indexes.Count != 1)
                    throw new TypeException(null,
                        "Wrong number of indexes for the expression");
            }
            else if (exprResultType.IsSubsetOf(AllMatrices.Value))
            {
                if (expr.Indexes.Count != 2)
                    throw new TypeException(null,
                        "Wrong number of indexes for the expression");
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
                    throw new TypeException(null,
                        "Indexes must be scalar");
                if (!(expr.Indexes[i] is Literal literal))
                    continue;
                var vv = literal.Value;
                if (!vv.IsIsScalar(env))
                    throw new TypeException(null,
                        "Indexes must be scalar");
                var vi = vv.ToNumber().Value;
                if (!vi.IsInteger())
                    throw new TypeException(null,
                        "Indexes must be integers");
                if (vi < 0)
                    throw new TypeException(null,
                        "Indexes must not be negative");

                if (i == 0 && exprResultType is Strings)
                    if (vi >= exprValue.ToStringValue().Length)
                        throw new IndexException(
                            "Index exceeds the size of the string");

                if (i == 0 && exprResultType is Vectors vs)
                    if (vi >= vs.Dimension)
                        throw new IndexException(
                            "Index exceeds the size of the vector");

                if (exprResultType is Matrices ms)
                    switch (i)
                    {
                        case 0 when vi >= ms.RowCount:
                            throw new IndexException(
                                "Index exceeds number of rows of the" +
                                " matrix");
                        case 1 when vi >= ms.ColumnCount:
                            throw new IndexException(
                                "Index exceeds number of columns of" +
                                " the matrix");
                    }
            }
        }

        public void Check(DerivativeOfVariable expr, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void Check(FunctionCall expr, SolusEnvironment env)
        {
            Check(expr.Function, env);

            IMathObject fv = expr.Function;  // TODO: expr.GetResultType
            if (fv is VariableAccess va)
                fv = va.GetFinalReferencedValue(env);
            if (fv is Literal literal)
                fv = literal.Value;
            if (fv is Macro) return;
            var f = fv as Function;
            if (f == null)
                throw new ArgumentException(
                    "Can't check non-function target.");

            int i;
            for (i = 0; i < expr.Arguments.Count; i++)
                Check(expr.Arguments[i], env);

            var args = expr.Arguments;

            switch (f)
            {
                // case AbsoluteValueFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case AdditionOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case ArccosecantFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case ArccosineFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case ArccotangentFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case ArcsecantFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case ArcsineFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case Arctangent2Function ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case ArctangentFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case BitwiseAndOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case BitwiseOrOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case CeilingFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case CosecantFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case CosineFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case CotangentFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case DistFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case DistSqFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case DivisionOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case EqualComparisonOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case ExponentOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case FactorialFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case FloorFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case GreaterThanComparisonOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case GreaterThanOrEqualComparisonOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case LessThanComparisonOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case LessThanOrEqualComparisonOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case LoadImageFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case Log10Function ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case Log2Function ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case LogarithmFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case LogicalAndOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case LogicalOrOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                case MaximumFiniteFunction ff:
                    CheckFunctionCall(ff, args, env); return;
                case MaximumFunction ff:
                    CheckFunctionCall(ff, args, env); return;
                case MinimumFiniteFunction ff:
                    CheckFunctionCall(ff, args, env); return;
                case MinimumFunction ff:
                    CheckFunctionCall(ff, args, env); return;
                // case ModularDivision ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case MultiplicationOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case NaturalLogarithmFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case NegationOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case NotEqualComparisonOperation ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case SecantFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case SineFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                case SizeFunction ff:
                    CheckFunctionCall(ff, args, env); return;
                // case TangentFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case UnitStepFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
                // case UserDefinedFunction ff:
                //     CheckFunctionCall(ff, args, env); return;
            }

            var ft = f.FunctionType;
            var argResultTypes = new ISet[args.Count];
            for (i = 0; i < args.Count; i++)
                argResultTypes[i] = args[i].GetResultType(env);

            // If the function type is one of the simpler kinds, we can
            // make use of what we know to provide helpful feedback to the
            // user.
            if (ft is Sets.Functions ftf)
            {
                if (argResultTypes.Length != ftf.ParameterTypes.Count)
                    throw new TypeException(
                        null,
                        $"Wrong number of arguments given to " +
                        $"{f.DisplayName} (expected " +
                        $"{f.Parameters.Count} but got {args.Count})");
                for (i = 0; i < argResultTypes.Length; i++)
                    if (!argResultTypes[i].IsSubsetOf(
                            ftf.ParameterTypes[i]))
                    {
                        var p = f.Parameters[i];
                        throw new TypeException(
                            p.Name,
                            $"Argument {i} wrong type: " +
                            $"expected {p.Type.DisplayName} but got " +
                            $"{argResultTypes[i].DisplayName}");
                    }
            }
            else if (ft is VariadicFunctions vf)
            {
                if (argResultTypes.Length < vf.MinimumNumberOfArguments)
                    throw new TypeException(
                        null,
                        $"Wrong number of arguments given to " +
                        $"{f.DisplayName} (expected at least " +
                        $"{vf.MinimumNumberOfArguments} but " +
                        $"got {args.Count})");
                for (i = 0; i < argResultTypes.Length; i++)
                    if (!argResultTypes[i].IsSubsetOf(vf.ParameterType))
                    {
                        throw new TypeException(
                            null,
                            $"Argument {i} wrong type: expected " +
                            $"{vf.ParameterType.DisplayName} " +
                            $"but got {argResultTypes[i].DisplayName}");
                    }
            }
            else if (ft is AdditionOperation.AdditionFunctionType aft)
            {
                if (argResultTypes.Length < 2)
                    throw new TypeException(
                        null,
                        $"Wrong number of arguments given to " +
                        $"{f.DisplayName} (expected at least " +
                        $"2 but got {args.Count})");
                var argType = argResultTypes[0];
                if (!argType.IsSubsetOf(Reals.Value) &&
                    !(argType is Vectors) &&
                    // is subset of any Vectors instance? what about a subset
                    // of a Vectors instance?
                    //
                    // for now we cheat and rely on the fact that AllVectors
                    // and Vectors are the only vector types, and AllMatrices
                    // and Matrices are the only matrix types.
                    !(argType is Matrices))
                    throw new TypeException(
                        null,
                        $"Wrong argument type at index 0: expected " +
                        $"Real or Vector or Matrix, but got " +
                        $"{argType.DisplayName}");
                for (i = 0; i < argResultTypes.Length; i++)
                    // again, if each individual argument reported a type that
                    // was a different subset of some vector space, that would
                    // be ok. but we only have Vectors, so we don't have to
                    // deal with that now. In the future, we'll have to make
                    // the decision below based on the subset of a broader,
                    // containing type, such as R^N
                    if (argResultTypes[i] != argType)
                    {
                        throw new TypeException(
                            null,
                            $"Wrong argument type at index {i}: " +
                            $"expected {argType.DisplayName} " +
                            $"but got {argResultTypes[i].DisplayName}");
                    }
            }
            // TODO: AllVectorFunctions ?
            // TODO: AllRealFunctions ?
            // TODO: AllFunctions ?
            else
            {
                if (!CanReceive(ft, argResultTypes))
                    throw new TypeException(
                        null,
                        $"Wrong number or type of arguments given " +
                        $"to {f.DisplayName}");
            }
        }

        public bool CanReceive(IFunctionType ft, ISet[] argTypes)
        {
            throw new NotImplementedException();
        }

        public void Check(IntervalExpression expr, SolusEnvironment env)
        {
            Check(expr.LowerBound, env);
            var lower = expr.LowerBound.GetResultType(env);
            if (!lower.IsSubsetOf(Reals.Value))
                throw new TypeException(null,
                    "Lower bound is not a scalar");

            Check(expr.UpperBound, env);
            var upper = expr.UpperBound.GetResultType(env);
            if (!upper.IsSubsetOf(Reals.Value))
                throw new TypeException(null,
                    "Upper bound is not a scalar");
        }

        public void Check(Literal expr, SolusEnvironment env) { }

        public void Check(MatrixExpression expr, SolusEnvironment env)
        {
            for (int r = 0; r < expr.RowCount; r++)
            for (int c = 0; c < expr.ColumnCount; c++)
            {
                if (!expr[r, c].GetResultType(env).IsSubsetOf(Reals.Value))
                    throw new TypeException(
                        null,
                        "All components must be reals");
                Check(expr[r, c], env);
            }
        }

        public void Check(RandomExpression expr, SolusEnvironment env) { }

        public void Check(VariableAccess expr, SolusEnvironment env)
        {
            if (!env.ContainsVariable(expr.VariableName))
                throw new NameException(
                    $"Variable not found: {expr.VariableName}");
            var target = env.GetVariable(expr.VariableName);
            if (target.IsIsExpression(env))
                Check((Expression)target, env);
        }

        public void Check(VectorExpression expr, SolusEnvironment env)
        {
            for (var i = 0; i < expr.Length; i++)
            {
                if (!expr[i].GetResultType(env).IsSubsetOf(Reals.Value))
                    throw new TypeException(
                        null,
                        "All components must be reals");
                Check(expr[i], env);
            }
        }

        public void CheckFunctionCall(AbsoluteValueFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(AdditionOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(ArccosecantFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(ArccosineFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(ArccotangentFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(ArcsecantFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(ArcsineFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(Arctangent2Function ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(ArctangentFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(BitwiseAndOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(BitwiseOrOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(CeilingFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(CosecantFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(CosineFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(CotangentFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(DistFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(DistSqFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(DivisionOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(EqualComparisonOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(ExponentOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(FactorialFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(FloorFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(GreaterThanComparisonOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(GreaterThanOrEqualComparisonOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(LessThanComparisonOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(LessThanOrEqualComparisonOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(LoadImageFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(Log10Function ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(Log2Function ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(LogarithmFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(LogicalAndOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(LogicalOrOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(MaximumFiniteFunction ff, List<Expression> args, SolusEnvironment env)
        {
            if (args.Count < 1)
                throw new ArgumentException("No arguments passed");
            for (var i = 0; i < args.Count; i++)
            {
                var argtype = args[i].GetResultType(env);
                if (!argtype.IsSubsetOf(Reals.Value))
                    throw new ArgumentException(
                        $"Argument {i} wrong type: expected " +
                        $"Scalar but got {argtype}");
            }
        }

        public void CheckFunctionCall(MaximumFunction ff, List<Expression> args, SolusEnvironment env)
        {
            if (args.Count < 1)
                throw new ArgumentException("No arguments passed");
            for (var i = 0; i < args.Count; i++)
            {
                var argtype = args[i].GetResultType(env);
                if (!argtype.IsSubsetOf(Reals.Value))
                    throw new ArgumentException(
                        $"Argument {i} wrong type: expected " +
                        $"Scalar but got {argtype}");
            }
        }

        public void CheckFunctionCall(MinimumFiniteFunction ff, List<Expression> args, SolusEnvironment env)
        {
            if (args.Count < 1)
                throw new ArgumentException("No arguments passed");
            for (var i = 0; i < args.Count; i++)
            {
                var argtype = args[i].GetResultType(env);
                if (!argtype.IsSubsetOf(Reals.Value))
                    throw new ArgumentException(
                        $"Argument {i} wrong type: expected " +
                        $"Scalar but got {argtype}");
            }
        }

        public void CheckFunctionCall(MinimumFunction ff, List<Expression> args, SolusEnvironment env)
        {
            if (args.Count < 1)
                throw new ArgumentException("No arguments passed");
            for (var i = 0; i < args.Count; i++)
            {
                var argtype = args[i].GetResultType(env);
                if (!argtype.IsSubsetOf(Reals.Value))
                    throw new ArgumentException(
                        $"Argument {i} wrong type: expected " +
                        $"Scalar but got {argtype}");
            }
        }

        public void CheckFunctionCall(ModularDivision ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(MultiplicationOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(NaturalLogarithmFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(NegationOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(NotEqualComparisonOperation ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(SecantFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(SineFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(SizeFunction ff, List<Expression> args, SolusEnvironment env)
        {
            if (args.Count != 1)
                throw new ArgumentException(
                    $"Wrong number of arguments given to " +
                    $"{ff.DisplayName} (expected 1 but got " +
                    $"{args.Count})");
            var argtype = args[0].GetResultType(env);
            if (!(argtype.IsSubsetOf(AllVectors.Value) ||
                  argtype.IsSubsetOf(AllMatrices.Value) ||
                  argtype.IsSubsetOf(Strings.Value)))
            {
                throw new ArgumentException(
                    "Argument wrong type: expected Vector " +
                    $"or Matrix or String but got {argtype.DisplayName}");
            }
        }

        public void CheckFunctionCall(TangentFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(UnitStepFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }

        public void CheckFunctionCall(UserDefinedFunction ff, List<Expression> args, SolusEnvironment env)
        {
            throw new NotImplementedException();
        }
    }
}
