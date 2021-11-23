
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

using System.Collections.Generic;
using System.Linq;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Macros
{
    public class IfMacro : Macro
    {
        public static readonly IfMacro Value = new IfMacro();

        protected IfMacro()
        {
            Name = "if";
            NumArguments = 3;
        }

        public override Expression InternalCall(IEnumerable<Expression> args, SolusEnvironment env)
        {
            var args2 = args.ToArray();
            float value = args2[0].Eval(env).ToNumber().Value;

            if (value == 0 || float.IsNaN(value) || float.IsInfinity(value))
            {
                return new Literal(args2[2].Eval(env));
            }
            else
            {
                return new Literal(args2[1].Eval(env));
            }
        }
    }
}
