
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
 *  CeilingFunction.cs                                                       *
 *                                                                           *
 *  The class for the built-in Ceiling function.                             *
 *                                                                           *
 *****************************************************************************/

using System;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class CeilingFunction : SingleArgumentFunction
	{
        public static readonly CeilingFunction Value = new CeilingFunction();

        protected CeilingFunction()
		{
			this.Name = "Ceiling";
		}


        protected override IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args)
        {
            return ((float)Math.Ceiling(args[0].ToNumber().Value)).ToNumber();
        }

        public override string DisplayName
        {
            get
            {
                return "ceil";
            }
        }

        public override string DocString
        {
            get
            {
                return "The ceiling function\n  ceil(x)\n\nReturns the lowest integer that is greater than or equal to x.\n";
            }
        }
    }
}
