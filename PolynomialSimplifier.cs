
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
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus
{
    public class PolynomialSimplifier : ExpressionTransformer
    {
        public override bool CanTransform(Expression expr)
        {
            throw new NotImplementedException();
        }

        public override Expression Transform(Expression expr)
        {
            if (expr is Literal) return expr;
            if (expr is VariableAccess) return expr;

            if (expr is FunctionCall)
            {
                TransformFunctionCall((FunctionCall)expr);
            }

            throw new NotImplementedException();
        }

        private void TransformFunctionCall(FunctionCall fc)
        {
            if (fc.Function is ExponentOperation)
            {
                if (fc.Arguments[1] is Literal && Literal.IsInteger(((Literal)fc.Arguments[1]).Value) &&
                    fc.Arguments[0] is FunctionCall)
                {
                    FunctionCall fcarg = (FunctionCall)fc.Arguments[0];
                    if (fcarg.Function is AdditionOperation ||
                        fcarg.Function is MultiplicationOperation)
                    {
                        //this is all wrong

                        List<Expression> terms = new List<Expression>();
                        foreach (Expression e in fcarg.Arguments)
                        {
                            terms.Add(Transform(e));
                        }

                        FunctionCall newfc = new FunctionCall();
                        newfc.Function = fcarg.Function;

                        int n = (int)(((Literal)fc.Arguments[1]).Value);
                        int i;
                        for (i = 0; i < n; i++)
                        {
                            newfc.Arguments.AddRange(terms);
                        }
                    }
                }
            }
        }
    }
}
