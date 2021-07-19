
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
 *  CotangentFunction.cs                                                     *
 *                                                                           *
 *  The class for the built-in Cotangent function.                           *
 *                                                                           *
 *****************************************************************************/

using System;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class CotangentFunction : SingleArgumentFunction
	{
        public static readonly CotangentFunction Value = new CotangentFunction();

        protected CotangentFunction()
		{
			this.Name = "Cotangent";
		}


        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
		{
            return new Literal((float)(1 / Math.Tan(args[0].Eval(env).Value)));
        }

        public override string DisplayName
        {
            get
            {
                return "cot";
            }
        }

        public override string DocString
        {
            get
            {
                return "The cotangent function\n  cot(x)\n\nReturns the cotangent of x, which is equal to 1 / tan(x).";
            }
        }
    }
}
