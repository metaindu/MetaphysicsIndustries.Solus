
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
using System.Linq;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public abstract class AssociativeCommutativeOperation : Operation
    {
        protected AssociativeCommutativeOperation()
            : base(Array.Empty<Parameter>())
        {
        }

        private List<Parameter> _lastParameters;

        //protected override Expression InternalCleanUp(Expression[] args)
        //{
        //    if (args.Length == 1)
        //    {
        //        return args[0];
        //    }

        //    args = CleanUpPartAssociativeOperation(args);

        //    if (args.Length == 1)
        //    {
        //        return args[0];
        //    }

        //    if (Collapses)
        //    {
        //        foreach (Expression arg in args)
        //        {
        //            if (arg is Literal && (arg as Literal).Value == CollapseValue)
        //            {
        //                return new Literal(CollapseValue);
        //            }
        //        }
        //    }

        //    if (Culls)
        //    {
        //        List<Expression> args2 = new List<Expression>(args.Length);
        //        foreach (Expression arg in args)
        //        {
        //            if (!(arg is Literal) || (arg as Literal).Value != CullValue)
        //            {
        //                args2.Add(arg);
        //            }
        //        }

        //        if (args2.Count < args.Length)
        //        {
        //            args = args2.ToArray();
        //        }
        //    }

        //    if (args.Length == 1)
        //    {
        //        return args[0];
        //    }

        //    bool call = true;
        //    foreach (Expression arg in args)
        //    {
        //        if (!(arg is Literal))
        //        {
        //            call = false;
        //            break;
        //        }
        //    }

        //    if (call)
        //    {
        //        return Call(null, args);
        //    }

        //    return new FunctionCall(this, args);
        //}

        //protected override Expression[] InternalCleanUpPartAssociativeOperation(Expression[] args, Literal combinedLiteral, List<Expression> nonLiterals)
        //{
        //    List<Expression> newArgs = new List<Expression>(nonLiterals.Count + 1);
        //    newArgs.Add(combinedLiteral);
        //    newArgs.AddRange(nonLiterals);
        //    return newArgs.ToArray();
        //}

        public override bool IsCommutative
        {
            get
            {
                return true;
            }
        }

        public override bool IsAssociative
        {
            get
            {
                return true;
            }
        }

        //if the operation collapses, then any argument that evaluates to the collapse value will cause the result of the entire operation to be that value
        //e.g. a * 0 = 0

        public virtual bool Collapses
        {
            get { return false; }
        }

        public virtual float CollapseValue
        {
            get { return 0; }
        }

        //if the operation culls, then any argument that evaluates to the cull value should be removed
        //e.g. a + 0 = a

        public virtual bool Culls
        {
            get { return true; }
        }

        public virtual float CullValue 
        {
            get { return IdentityValue; }
        }
    }
}
