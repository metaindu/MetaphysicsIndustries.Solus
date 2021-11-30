
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
using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class UserDefinedFunction : Function
    {
        public UserDefinedFunction(string name, string[] argnames, Expression expr)
            : base(argnames.Select(_ => Types.Scalar).ToArray(), name)
        {
            Name = name;
            Argnames = argnames;
            Expression = expr;
        }

        public string[] Argnames;
        public Expression Expression;

        public override void CheckArguments(IMathObject[] args)
        {
            if (args.Length != Argnames.Length)
                throw new ArgumentException(
                    $"Wrong number of arguments given to " +
                    $"{DisplayName} (expected {Argnames.Length} but got " +
                    $"{args.Length})");
        }

        protected override IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args)
        {
            SolusEnvironment env2 = env.Clone();

            int i;
            for (i = 0; i < Argnames.Length; i++)
            {
                env2.SetVariable(Argnames[i], new Literal(args[i]));
            }

            return Expression.Eval(env2);
        }

        public override IMathObject GetResult(IEnumerable<IMathObject> args)
        {
            return Expression.Result;
        }
    }
}

