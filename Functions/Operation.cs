
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

/*****************************************************************************
 *                                                                           *
 *  Operation.cs                                                             *
 *                                                                           *
 *  A specialized function which represents simple arithmetical operations   *
 *    such as addition of two numbers.                                       *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public interface IOperation
    {
        OperationPrecedence Precedence { get; }
        bool HasIdentityValue { get; }
        float IdentityValue { get; }
    }

    public abstract class Operation : Function, IOperation
    {
        public abstract OperationPrecedence Precedence
        {
            get;
        }

        public virtual bool HasIdentityValue
        {
            get { return true; }
        }

        public virtual float IdentityValue
        {
            get { return 1; }
        }

        //protected abstract Expression InternalCleanUp(Expression[] args);

        //protected Expression[] CleanUpPartAssociativeOperation(Expression[] args)
        //{
        //    List<FunctionCall> assocOps = new List<FunctionCall>();
        //    (new FunctionCall(this, args)).GatherMatchingFunctionCalls(assocOps);

        //    Set<FunctionCall> assocOpsSet = new Set<FunctionCall>(assocOps);
        //    Literal combinedLiteral = null;

        //    combinedLiteral = new Literal(IdentityValue);

        //    List<Expression> nonLiterals = new List<Expression>(assocOps.Count);

        //    foreach (FunctionCall opToCombine in assocOps)
        //    {
        //        foreach (Expression arg in opToCombine.Arguments)
        //        {
        //            if (!(arg is FunctionCall) ||
        //                !(assocOpsSet.Contains(arg as FunctionCall)))
        //            {
        //                if (arg is Literal)
        //                {
        //                    combinedLiteral = Call(null, combinedLiteral, arg);
        //                }
        //                else
        //                {
        //                    nonLiterals.Add(arg);
        //                }
        //            }
        //        }
        //    }

        //    args = InternalCleanUpPartAssociativeOperation(args, combinedLiteral, nonLiterals);

        //    return args;
        //}

        //protected abstract Expression[] InternalCleanUpPartAssociativeOperation(Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals);

        public override string ToString(List<Expression> arguments)
        {
            return ToString(this, arguments);
        }

        public static string ToString(Function f, List<Expression> arguments)
        {
            var strs = Array.ConvertAll(arguments.ToArray(),
                Expression.ToString);

            int i;
            for (i = 0; i < strs.Length; i++)
            {
                if (arguments[i] is FunctionCall call &&
                    call.Function is Literal literal &&
                    literal.Value is IOperation oper &&
                    f is IOperation foper &&
                    oper.Precedence < foper.Precedence)
                    strs[i] = "(" + strs[i] + ")";
            }

            return string.Join(" " + f.DisplayName + " ", strs);
        }
    }
}
