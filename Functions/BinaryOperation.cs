
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

/*****************************************************************************
 *                                                                           *
 *  BinaryOperation.cs                                                       *
 *                                                                           *
 *  A specialized function which represents simple arithmetical operations   *
 *    on two arguments.                                                      *
 *                                                                           *
 *****************************************************************************/

using System;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public abstract class BinaryOperation : Operation
    {
        protected BinaryOperation()
        {
            Types.Clear();
            Types.Add(typeof(Expression));
            Types.Add(typeof(Expression));
        }

        protected override void CheckArguments(IMathObject[] args)
        {
            // TODO

            // int i;
            // int j;
            // Type e;
            //
            // if (Types.Count != 2)
            // {
            //     throw new InvalidOperationException("Wrong number of types specified internally to BinaryOperation (given " + Types.Count.ToString() + ", require 2)");
            // }
            // if (args.Length != 2)
            // {
            //     throw new InvalidOperationException("Wrong number of arguments given to " + this.DisplayName + " (given " + args.Length.ToString() + ", require 2)");
            // }
            // e = typeof(Expression);
            // j = 2;
            // for (i = 0; i < j; i++)
            // {
            //     if (!e.IsAssignableFrom(Types[i]))
            //     {
            //         throw new InvalidOperationException("Required argument type " + i.ToString() + " is invalid (given \"" + Types[i].Name + "\", require \"" + e.Name + "\")");
            //     }
            //     if (!Types[i].IsAssignableFrom(args[i].GetType()))
            //     {
            //         throw new InvalidOperationException("Argument " + ((i).ToString()) + " of wrong type (given \"" + args[i].GetType().Name + "\", require \"" + Types[i].Name + "\")");
            //     }
            // }
        }

        protected override sealed IMathObject InternalCall(
            SolusEnvironment env, IMathObject[] args)
        {
            return InternalBinaryCall(args[0].ToNumber().Value,
                args[1].ToNumber().Value).ToNumber();
        }
        protected abstract float InternalBinaryCall(float x, float y);

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