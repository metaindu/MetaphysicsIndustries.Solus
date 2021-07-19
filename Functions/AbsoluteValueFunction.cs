
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

namespace MetaphysicsIndustries.Solus.Functions
{
    public class AbsoluteValueFunction : SingleArgumentFunction
    {
        public static readonly AbsoluteValueFunction Value = new AbsoluteValueFunction();

        protected AbsoluteValueFunction()
        {
            Name = "Absolue Value";
        }

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            return new Literal(Math.Abs(args[0].Eval(env).Value));
        }

        public override string DisplayName
        {
            get
            {
                return "abs";
            }
        }

        public override string DocString
        {
            get
            {
                return "The absolute value function\n  abs(x)\n\nReturns the absolute value of x, x for (x >= 0) and -x for (x < 0).";
            }
        }
    }
}
