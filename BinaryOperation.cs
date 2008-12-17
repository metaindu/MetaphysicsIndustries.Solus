
/*****************************************************************************
 *                                                                           *
 *  BinaryOperation.cs                                                       *
 *  17 November 2006                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  A specialized function which represents simple arithmetical operations   *
 *    on two arguments.                                                      *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
	public abstract class BinaryOperation : Operation
	{
        public BinaryOperation()
        {
            Types.Clear();
            Types.Add(typeof(Expression));
            Types.Add(typeof(Expression));
        }

        private static DivisionOperation _division = new DivisionOperation();
        public static DivisionOperation Division
        {
            get { return _division; }
        }

        private static ExponentOperation _exponent = new ExponentOperation();
        public static ExponentOperation Exponent
        {
            get { return _exponent; }
        }

		protected override void CheckArguments(Expression[] args)
		{
			Expression[] _args = args;
			List<Type>	types;
			int				i;
			int				j;
			
			types = this.Types;
			if (types.Count != 2)
			{
				throw new InvalidOperationException("Wrong number of types specified internally to BinarryOperation (given " + types.Count.ToString() + ", require 2)");
			}
			if (args.Length != 2)
			{
				throw new InvalidOperationException("Wrong number of arguments given to " + this.DisplayName + " (given " + args.Length.ToString() + ", require 2)");
			}
			Type	e;
			e = typeof(Expression);
			j = 2;
			for (i = 0; i < j; i++)
			{
				if (!e.IsAssignableFrom(types[i]))
				{
					
					throw new InvalidOperationException("Required argument type " + i.ToString() + " is invalid (given \"" + types[i].Name + "\", require \"" + e.Name + ")");
				}
				if (!types[i].IsAssignableFrom(args[i].GetType()))
				{
					throw new InvalidOperationException("Argument " + ((i).ToString()) + " of wrong type (given \"" + args.GetType().Name + "\", require \"" + types[i].Name + ")");
				}
			}
		}

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
