
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

using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class UserDefinedFunction : Function
    {
        public UserDefinedFunction(string name, string[] argnames,
            Expression expr)
        {
            _name = name;
            Expression = expr;
            var parameters = new List<Parameter>();
            foreach (var argname in argnames)
                parameters.Add(new Parameter(argname, Reals.Value));
            Parameters = parameters;
        }
        public UserDefinedFunction(string name,
            IEnumerable<Parameter> parameters, Expression expr)
        {
            _name = name;
            Expression = expr;
            Parameters = new List<Parameter>(parameters);
        }

        private readonly string _name;
        public override string Name => _name;

        public Expression Expression;

        public override IReadOnlyList<Parameter> Parameters { get; }

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes)
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
                env2.SetVariableType(param.Name, argValue);
            }

            return Expression.GetResultType(env2);
        }
    }
}
