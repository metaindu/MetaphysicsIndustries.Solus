
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
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Transformers
{
    public class SubstTransformer : ExpressionTransformer<SubstTransformer.SubstTransformArgs>
    {
        public class SubstTransformArgs : TransformArgs
        {
            public string VariableToReplace;
            public Expression ExpressionToInsert;
        }

        public override bool CanTransform (Expression expr, SubstTransformArgs args)
        {
            throw new System.NotImplementedException ();
        }

        public override Expression Transform(Expression expr, SubstTransformArgs args)
        {
            return Subst(expr, args.VariableToReplace, args.ExpressionToInsert);
        }

        public Expression Subst(Expression exprToTransform, string variableToReplace, Expression exprToInsert)
        {
            if (exprToTransform is FunctionCall)
            {
                List<Expression> args = new List<Expression>(((FunctionCall)exprToTransform).Arguments);
                int i;
                for (i = 0; i < args.Count; i++)
                {
                    args[i] = Subst(args[i], variableToReplace, exprToInsert);
                }

                return new FunctionCall(((FunctionCall)exprToTransform).Function, args);
            }
            else if (exprToTransform is VariableAccess)
            {
                if (((VariableAccess)exprToTransform).VariableName == variableToReplace)
                {
                    return exprToInsert;
                }
                else
                {
                    return exprToTransform;
                }
            }
            else if (exprToTransform is Literal)
            {
                return exprToTransform;
            }
            else
            {
                throw new InvalidOperationException("Unknown expression type or invalid target of substitution");
            }
        }
    }
}

