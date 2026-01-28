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
        /// <summary>
        /// Determine if an expression is well-defined.
        ///
        /// An expression is well-defined if its definition assigns it a
        /// unique interpretation or value. For our purposes, this means that
        /// the expression could be evaluated to produce a result. For
        /// example, all arguments have the correct type.
        /// </summary>
        /// <param name="expr"></param>
        /// <param name="env"></param>
        /// <param name="throws"></param>
        /// <returns></returns>
        public bool IsWellDefined(Expression expr, SolusEnvironment env,
            bool throws = true)
        {
            switch (expr)
            {
                case ColorExpression ce:
                    return IsWellDefined(ce, env, throws: throws);
                case ComponentAccess ca:
                    return IsWellDefined(ca, env, throws: throws);
                case DerivativeOfVariable dov:
                    return IsWellDefined(dov, env, throws: throws);
                case FunctionCall fc:
                    return IsWellDefined(fc, env, throws: throws);
                case IntervalExpression interval:
                    return IsWellDefined(interval, env, throws: throws);
                case Literal literal:
                    return IsWellDefined(literal, env, throws: throws);
                case MatrixExpression me:
                    return IsWellDefined(me, env, throws: throws);
                case VariableAccess va:
                    return IsWellDefined(va, env, throws: throws);
                case VectorExpression ve:
                    return IsWellDefined(ve, env, throws: throws);
            }

            // throw new ArgumentException(
            //     $"Unknown expression type: {expr.GetType().Name}",
            //     nameof(expr));
            return true;
        }

        public bool IsWellDefined(ColorExpression expr, SolusEnvironment env,
            bool throws = true) =>
            true;

        public bool IsWellDefined(ComponentAccess expr, SolusEnvironment env,
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

            var rv = IsWellDefined(expr.Expr, env, throws: throws);
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

                rv = IsWellDefined(expr.Indexes[i], env, throws: throws);
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

        public bool IsWellDefined(DerivativeOfVariable expr,
            SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefined(FunctionCall expr, SolusEnvironment env,
            bool throws = true)
        {
            var rv = IsWellDefined(expr.Function, env, throws: throws);
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
                rv = IsWellDefined(expr.Arguments[0], env2, throws: throws);
                if (!rv) return false;
                return true;
            }

            if (f is IfOperator ii)
            {
                rv = IsWellDefined(expr.Arguments[0], env, throws: throws);
                if (!rv) return false;
                rv = IsWellDefined(expr.Arguments[1], env, throws: throws);
                if (!rv) return false;
                rv = IsWellDefined(expr.Arguments[2], env, throws: throws);
                if (!rv) return false;
                return true;
            }

            if (f is SubstFunction)
            {
                var varname = expr.Arguments[1].As<VariableAccess>()
                    .VariableName;
                var env2 = env.CreateChildEnvironment();
                env2.SetVariable(varname, new Number(0)); // place-holder
                rv = IsWellFormed(expr.Arguments[0], throws: throws);
                if (!rv) return false;
                rv = IsWellFormed(expr.Arguments[2], throws: throws);
                if (!rv) return false;
                return true;
            }

            var args = expr.Arguments;

            if (!f.IsVariadic &&
                args.Count != f.Parameters.Count)
            {
                if (throws)
                    throw new ArgumentException(
                        $"Wrong number of arguments given to " +
                        $"{f.DisplayName} (expected {f.Parameters.Count} " +
                        $"but got {args.Count})");
                return false;
            }

            int i;
            if (f is MultiplicationOperation)
            {
                var hasMatrix = false;
                var hasVector = false;
                var hasScalar = false;
                var nonScalars = new List<ISet>();
                ISet scalarType = null;
                for (i = 0; i < expr.Arguments.Count; i++)
                {
                    var arg = expr.Arguments[i];
                    var argType = arg.GetResultType(env);
                    if (argType.IsSubsetOf(Reals.Value))
                    {
                        hasScalar = true;
                        scalarType = argType;
                    }
                    else if (argType.IsSubsetOf(AllVectors.Value))
                    {
                        hasVector = true;
                        nonScalars.Add(argType);
                    }
                    else if (argType.IsSubsetOf(AllMatrices.Value))
                    {
                        hasMatrix = true;
                        nonScalars.Add(argType);
                    }
                    else
                    {
                        if (throws)
                            throw new TypeException(
                                null,
                                $"Argument {i} wrong type: " +
                                $"expected {Reals.Value.DisplayName} or " +
                                $"{AllVectors.Value.DisplayName} or " +
                                $"{AllMatrices.Value.DisplayName} but " +
                                $"got {argType.DisplayName}");
                        return false;
                    }
                }
            }

            for (i = 0; i < args.Count; i++)
            {
                var pt = f.GetParameterType(i);
                if (pt != null && !pt.IsSubsetOf(Sets.Expressions.Value))
                {
                    rv = IsWellDefined(expr.Arguments[i], env, throws: throws);
                    if (!rv) return false;
                }
            }

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

                for (i = 0; i < args.Count; i++)
                {
                    var argtype = args[i].GetResultType(env);
                    if (!argtype.IsSubsetOf(Reals.Value))
                    {
                        if (throws)
                            throw new TypeException(
                                null,
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
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case AdditionOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArccosecantFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArccosineFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArccotangentFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArcsecantFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArcsineFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case Arctangent2Function ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ArctangentFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case BitwiseAndOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case BitwiseOrOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CeilingFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CosecantFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CosineFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case CotangentFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case DistFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case DistSqFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case DivisionOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case EqualComparisonOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case ExponentOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case FactorialFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case FloorFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case GreaterThanComparisonOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case GreaterThanOrEqualComparisonOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LessThanComparisonOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LessThanOrEqualComparisonOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LoadImageFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case Log10Function ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case Log2Function ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LogarithmFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LogicalAndOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case LogicalOrOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                case MaximumFiniteFunction ff:
                    rv = IsWellDefinedFunctionCall(ff, args, env,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                case MaximumFunction ff:
                    rv = IsWellDefinedFunctionCall(ff, args, env,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                case MinimumFiniteFunction ff:
                    rv = IsWellDefinedFunctionCall(ff, args, env,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                case MinimumFunction ff:
                    rv = IsWellDefinedFunctionCall(ff, args, env,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                // case ModularDivision ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case MultiplicationOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case NaturalLogarithmFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case NegationOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case NotEqualComparisonOperation ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case SecantFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case SineFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                case SizeFunction ff:
                    rv = IsWellDefinedFunctionCall(ff, args, env,
                        throws: throws);
                    if (!rv) return false;
                    return true;
                // case TangentFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case UnitStepFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
                // case UserDefinedFunction ff:
                //     rv = IsWellDefinedFunctionCall(ff, args, throws: throws);
                //     if (!rv) return false;
                //     return true;
            }

            // for (var i = 0; i < args.Count; i++)
            // {
            //     var argtype = args[i].GetResultType(env);
            //     if (!argtype.IsSubsetOf(f.Parameters[i].Type))
            //     {
            //         if (throws)
            //             throw new TypeException(
            //                 $"Argument {i} wrong type: expected " +
            //                 $"{f.Parameters[i].Type.DisplayName} but got " +
            //                 $"{argtype.DisplayName}");
            //         return false;
            //     }
            // }

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
                {
                    if (throws)
                        throw new TypeException(
                            null,
                            $"Wrong number of arguments given to " +
                            $"{f.DisplayName} (expected " +
                            $"{f.Parameters.Count} but got {args.Count})");
                    return false;
                }

                for (i = 0; i < argResultTypes.Length; i++)
                    if (!argResultTypes[i].IsSubsetOf(
                            ftf.ParameterTypes[i]))
                    {
                        var p = f.Parameters[i];
                        if (throws)
                            throw new TypeException(
                                p.Name,
                                $"Argument {i} wrong type: " +
                                $"expected {p.Type.DisplayName} but got " +
                                $"{argResultTypes[i].DisplayName}");
                        return false;
                    }
            }
            else if (ft is VariadicFunctions vf)
            {
                if (argResultTypes.Length < vf.MinimumNumberOfArguments)
                {
                    if (throws)
                        throw new TypeException(
                            null,
                            $"Wrong number of arguments given to " +
                            $"{f.DisplayName} (expected at least " +
                            $"{vf.MinimumNumberOfArguments} but " +
                            $"got {args.Count})");
                    return false;
                }

                for (i = 0; i < argResultTypes.Length; i++)
                    if (!argResultTypes[i].IsSubsetOf(vf.ParameterType))
                    {
                        if (throws)
                            throw new TypeException(
                                null,
                                $"Argument {i} wrong type: expected " +
                                $"{vf.ParameterType.DisplayName} " +
                                $"but got {argResultTypes[i].DisplayName}");
                        return false;
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
            else if (ft is MultiplicationOperation.MultiplicationFunctionType mft)
            {
                MultiplicationOperation.CheckArguments(env, argResultTypes);
            }
            // TODO: AllVectorFunctions ?
            // TODO: AllRealFunctions ?
            // TODO: AllFunctions ?
            else
            {
                if (!CanReceive(ft, argResultTypes))
                {
                    if (throws)
                        throw new TypeException(
                            null,
                            $"Wrong number or type of arguments given " +
                            $"to {f.DisplayName}");
                    return false;
                }
            }

            return true;
        }

        public bool CanReceive(IFunctionType ft, ISet[] argTypes)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefined(IntervalExpression expr, SolusEnvironment env,
            bool throws = true)
        {
            var rv = IsWellDefined(expr.LowerBound, env, throws: throws);
            if (!rv) return false;
            var lower = expr.LowerBound.GetResultType(env);
            if (!lower.IsSubsetOf(Reals.Value))
            {
                if (throws)
                    throw new TypeException(null,
                        "Lower bound is not a scalar");
                return false;
            }

            rv = IsWellDefined(expr.UpperBound, env, throws: throws);
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

        public bool IsWellDefined(Literal expr, SolusEnvironment env,
            bool throws = true) => true;

        public bool IsWellDefined(MatrixExpression expr, SolusEnvironment env,
            bool throws = true)
        {
            for (int r = 0; r < expr.RowCount; r++)
            for (int c = 0; c < expr.ColumnCount; c++)
            {
                if (!expr[r, c].GetResultType(env).IsSubsetOf(Reals.Value))
                {
                    if (throws)
                        throw new TypeException(
                            null,
                            "All components must be reals");
                    return false;
                }

                var rv = IsWellDefined(expr[r, c], env, throws: throws);
                if (!rv) return false;
            }

            return true;
        }

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
                var rv = IsWellDefined((Expression)target, env, throws: throws);
                if (!rv) return false;
            }

            return true;
        }

        public bool IsWellDefined(VectorExpression expr, SolusEnvironment env,
            bool throws = true)
        {
            for (var i = 0; i < expr.Length; i++)
            {
                if (!expr[i].GetResultType(env).IsSubsetOf(Reals.Value))
                {
                    if (throws)
                        throw new TypeException(
                            null,
                            "All components must be reals");
                    return false;
                }

                var rv = IsWellDefined(expr[i], env, throws: throws);
                if (!rv) return false;
            }

            return true;
        }

        public bool IsWellDefinedFunctionCall(AbsoluteValueFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(AdditionOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(ArccosecantFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(ArccosineFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(ArccotangentFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(ArcsecantFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(ArcsineFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(Arctangent2Function ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(ArctangentFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(BitwiseAndOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(BitwiseOrOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(CeilingFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(CosecantFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(CosineFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(CotangentFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(DistFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(DistSqFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(DivisionOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(EqualComparisonOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(ExponentOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(FactorialFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(FloorFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(GreaterThanComparisonOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(
            GreaterThanOrEqualComparisonOperation ff, List<Expression> args,
            SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(LessThanComparisonOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(
            LessThanOrEqualComparisonOperation ff, List<Expression> args,
            SolusEnvironment env, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(LoadImageFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(Log10Function ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(Log2Function ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(LogarithmFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(LogicalAndOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(LogicalOrOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(MaximumFiniteFunction ff,
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

        public bool IsWellDefinedFunctionCall(MaximumFunction ff,
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

        public bool IsWellDefinedFunctionCall(MinimumFiniteFunction ff,
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

        public bool IsWellDefinedFunctionCall(MinimumFunction ff,
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

        public bool IsWellDefinedFunctionCall(ModularDivision ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(MultiplicationOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(NaturalLogarithmFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(NegationOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(NotEqualComparisonOperation ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(SecantFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(SineFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(SizeFunction ff,
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

        public bool IsWellDefinedFunctionCall(TangentFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(UnitStepFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }

        public bool IsWellDefinedFunctionCall(UserDefinedFunction ff,
            List<Expression> args, bool throws = true)
        {
            throw new NotImplementedException();
        }
    }
}
