
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
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
using System.Text;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;


namespace MetaphysicsIndustries.Solus
{
    public class PolynomialTransformer : ExpressionTransformer<VariableTransformArgs>
    {
        public static bool ContainsVariable(Expression expr, string variable)
        {
            if (expr is Literal) return false;
            if (expr is VariableAccess) return ((VariableAccess)expr).VariableName == variable;
            if (expr is FunctionCall)
            {
                foreach (Expression arg in ((FunctionCall)expr).Arguments)
                {
                    if (ContainsVariable(arg, variable)) return true;
                }
                return false;
            }
            return false;
        }

        public override bool CanTransform(Expression expr, VariableTransformArgs args)
        {
            if (expr is Literal) return true;
            if (expr is VariableAccess) return true;
            if (expr is FunctionCall)
            {
                Function func = ((FunctionCall)expr).Function;
                Expression[] fargs = ((FunctionCall)expr).Arguments.ToArray();

                if (func is DivisionOperation || func is MultiplicationOperation || func is AdditionOperation)
                {
                    return true;
                }
                else if (func is ExponentOperation)
                {
                    return fargs[1] is Literal || !(ContainsVariable(fargs[0], args.Variable));
                }
                else
                {
                    foreach (Expression arg in fargs)
                    {
                        if (ContainsVariable(arg, args.Variable))
                        {
                            return false;
                        }
                    }
                    return true;
                }
            }

            return false;
        }

        public override Expression Transform(Expression expr, VariableTransformArgs args)
        {
            throw new NotImplementedException();

            Dictionary<Literal, HashSet<Expression>> coeffs = new Dictionary<Literal, HashSet<Expression>>();

            if (expr.IsFunction(DivisionOperation.Value))
            {

            }
            else if (expr.IsFunction(MultiplicationOperation.Value))
            {
                FunctionCall call = expr.As<FunctionCall>();

                
                HashSet<Expression> adds = new HashSet<Expression>();
                foreach (Expression arg in call.Arguments)
                {
                    if (arg.IsFunction(AdditionOperation.Value) && ContainsVariable(arg, args.Variable))
                    {
                        adds.Add(arg);
                    }
                }

                if (adds.Count > 0)
                {
                    List<Expression> fargs = new List<Expression>(call.Arguments);
                    foreach (Expression arg in adds)
                    {
                        fargs.Remove(arg);
                    }
                }
            }

            throw new NotImplementedException();
        }
    }
}
