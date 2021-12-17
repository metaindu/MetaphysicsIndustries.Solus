
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Transformers
{
    public class RatioOfPolynomialsTransformer : PolynomialTransformer
    {
        public static readonly RatioOfPolynomialsTransformer DefaultInstance = new RatioOfPolynomialsTransformer();

        //public override bool CanTransform(Expression expr, VariableTransformArgs args)
        //{
        //    if (expr is Literal) return true;
        //    if (expr is VariableAccess) return true;
        //    if (expr is FunctionCall)
        //    {
        //        Function func = ((FunctionCall)expr).Function;
        //        Expression[] fargs = ((FunctionCall)expr).Arguments.ToArray();

        //        if (func is DivisionOperation || func is MultiplicationOperation || func is AdditionOperation)
        //        {
        //            return true;
        //        }
        //        else if (func is ExponentOperation)
        //        {
        //            return fargs[1] is Literal || !(ContainsVariable(fargs[0], args.Variable));
        //        }
        //        else
        //        {
        //            foreach (Expression arg in fargs)
        //            {
        //                if (ContainsVariable(arg, args.Variable))
        //                {
        //                    return false;
        //                }
        //            }
        //            return true;
        //        }
        //    }

        //    return false;
        //}

        public override Expression Transform(Expression expr, VariableTransformArgs args)
        {
            if (expr is FunctionCall fc &&
                fc.Function is Literal literal &&
                literal.Value is DivisionOperation)
            {
                List<Expression> fargs = ((FunctionCall)expr).Arguments;

                return new FunctionCall(DivisionOperation.Value,
                    base.Transform(fargs[0], args),
                    base.Transform(fargs[1], args));
            }
            else
            {
                return base.Transform(expr, args);
            }
        }
    }
}
