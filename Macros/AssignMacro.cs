
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

namespace MetaphysicsIndustries.Solus
{
    public class AssignMacro : Macro
    {
        public static readonly AssignMacro Value = new AssignMacro();
        protected AssignMacro()
        {
            Name = "assign";
            HasVariableNumArgs = true;
        }


        public override Expression InternalCall(IEnumerable<Expression> args, SolusEnvironment env)
        {
            var args2 = args.ToList();
            var v = ((VariableAccess)args2[0]).VariableName;
            var value = args2[args2.Count-1].PreliminaryEval(env);
            if (args2.Count > 2)
            {
                var funcargs = args2.Skip(1).Take(args2.Count - 2).Select(e => ((VariableAccess)e).VariableName);
                env.Functions[v] = new UserDefinedFunction(v, funcargs.ToArray(), value);
            }
            else
            {
                env.Variables[v] = value;
            }

            return value;
        }

        public override string DocString
        {
            get
            {
                return "The derive operator\n  derive(f(x), x)\n\nReturns the derivative of f(x) with respect to x.";
            }
        }
    }
}

