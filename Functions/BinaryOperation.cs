
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

/*****************************************************************************
 *                                                                           *
 *  BinaryOperation.cs                                                       *
 *                                                                           *
 *  A specialized function which represents simple arithmetical operations   *
 *    on two arguments.                                                      *
 *                                                                           *
 *****************************************************************************/

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public abstract class BinaryOperation : Operation
    {
        public override IReadOnlyList<Parameter> Parameters { get; } =
            new[]
            {
                new Parameter("a", Reals.Value),
                new Parameter("b", Reals.Value)
            };

        //protected override Expression InternalCleanUp(Expression[] args)
        //{
        //    if (args[0] is Literal &&
        //                    args[1] is Literal)
        //    {
        //        return Call(null, args);
        //    }

        //    if (IsAssociative)
        //    {
        //        args = CleanUpPartAssociativeOperation(args);
        //    }

        //    if (IsCommutative &&
        //        args[0] is Literal &&
        //        (args[0] as Literal).Value == IdentityValue)
        //    {
        //        return args[1];
        //    }

        //    if (args[1] is Literal &&
        //        (args[1] as Literal).Value == IdentityValue)
        //    {
        //        return args[0];
        //    }


        //    return new FunctionCall(this, args);
        //}

        //protected override Expression[] InternalCleanUpPartAssociativeOperation(Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals)
        //{
        //    FunctionCall ret = new FunctionCall(this, combinedLiteral);
        //    FunctionCall temp = ret;
        //    FunctionCall last = null;

        //    foreach (Expression expr in nonLiterals)
        //    {
        //        //Expression cleanExpr = CleanUp(expr);
        //        last = temp;
        //        temp = new FunctionCall(this, expr);//cleanExpr);
        //        last.Arguments.Add(temp);
        //    }

        //    last.Arguments[1] = temp.Arguments[0];

        //    return ret.Arguments.ToArray();
        //}
    }
}
