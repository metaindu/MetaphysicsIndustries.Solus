
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
using MetaphysicsIndustries.Solus.Compiler.IlExpressions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Compiler
{
    public partial class ILCompiler
    {
        public IlExpression ConvertToIlExpression(
            Function func,
            NascentMethod nm,
            SolusEnvironment env,
            VariableIdentityMap variables,
            List<Expression> arguments)
        {
            switch (func)
            {
                case AbsoluteValueFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case AdditionOperation ao:
                    return ConvertToIlExpression(ao, nm, env, variables,
                        arguments);
                case ArccosecantFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case ArccosineFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case ArccotangentFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case ArcsecantFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case ArcsineFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case Arctangent2Function ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case ArctangentFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case BitwiseAndOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case BitwiseOrOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case CeilingFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case CosecantFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case CosineFunction c:
                    return ConvertToIlExpression(c, nm, env, variables,
                        arguments);
                case CotangentFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case DistFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case DistSqFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case DivisionOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case EqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case ExponentOperation eo:
                    return ConvertToIlExpression(eo, nm, env, variables,
                        arguments);
                case FactorialFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case FloorFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case GreaterThanComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case GreaterThanOrEqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case LessThanComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case LessThanOrEqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case LoadImageFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case Log10Function ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case Log2Function ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case LogarithmFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case LogicalAndOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case LogicalOrOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case MaximumFiniteFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case MaximumFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case MinimumFiniteFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case MinimumFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case ModularDivision ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case MultiplicationOperation mo:
                    return ConvertToIlExpression(mo, nm, env, variables,
                        arguments);
                case NaturalLogarithmFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case NegationOperation no:
                    return ConvertToIlExpression(no, nm, env, variables,
                        arguments);
                case NotEqualComparisonOperation ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case SecantFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case SineFunction s:
                    return ConvertToIlExpression(s, nm, env, variables,
                        arguments);
                case SizeFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case TangentFunction ff:
                    return ConvertToIlExpression(ff, nm, env, variables,
                        arguments);
                case UnitStepFunction usf:
                    return ConvertToIlExpression(usf, nm, env, variables,
                        arguments);
                case UserDefinedFunction udf:
                    return ConvertToIlExpression(udf, nm, env, variables,
                        arguments);
                default:
                    // if (func.ProvidesCustomCall)
                    // {
                    //     var args2 = new IlExpression[arguments.Count];
                    //     int i;
                    //     for (i = 0; i < arguments.Count; i++)
                    //         args2[i] = ConvertToIlExpression(arguments[i],
                    //             nm);
                    //     var expr = new CallIlExpression(
                    //         func.CustomCall, args2);
                    //
                    //     var expr = ConvertToIlExpression(
                    //         arguments[0], nm);
                    //     int i;
                    //     for (i = 1; i < arguments.Count; i++)
                    //     {
                    //         expr = new CallIlExpression(
                    //             new Func<float, float, float>(Math.Max),
                    //             expr,
                    //             ConvertToIlExpression(arguments[i], nm));
                    //     }
                    //
                    //     return expr;
                    //
                    //
                    //     var expr = new CallIlExpression(
                    //         new Func<double, double>(Math.Tan),
                    //         ConvertToIlExpression(arguments[0], nm));
                    //     return expr;
                    // }
                    throw new ArgumentException(
                        $"Unsupported function type: \"{func}\"",
                        nameof(func));
            }
        }
    }
}