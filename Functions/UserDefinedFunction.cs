
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
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class UserDefinedFunction : Function
    {
        public UserDefinedFunction(string name, string[] argnames, Expression expr)
            : base(argnames.Select(_ =>new Parameter("",Reals.Value)).ToArray(), name)
        {
            Name = name;
            Expression = expr;
        }

        public Expression Expression;

        public override void CheckArguments(IMathObject[] args)
        {
            if (args.Length != Parameters.Count)
                throw new ArgumentException(
                    $"Wrong number of arguments given to " +
                    $"{DisplayName} (expected {Parameters.Count} but got " +
                    $"{args.Length})");
        }   

        public override IMathObject GetResultType(SolusEnvironment env,
            IEnumerable<IMathObject> argTypes)
        {
            SolusEnvironment env2;
            if (env != null)
                env2 = env.CreateChildEnvironment();
            else
                env2 = new SolusEnvironment();
            var argValues = argTypes.ToList();
            int i;
            for (i = 0; i < Parameters.Count; i++)
            {
                var param = Parameters[i];
                var argValue = argValues[i];
                env2.SetVariable(param.Name, argValue);
            }

            return Expression.GetResultType(env2);
        }
    }
}

