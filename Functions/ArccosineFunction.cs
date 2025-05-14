
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

/*****************************************************************************
 *                                                                           *
 *  ArccosineFunction.cs                                                     *
 *                                                                           *
 *  The class for the built-in Arccosine function.                           *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class ArccosineFunction : SingleArgumentFunction
	{
        public static readonly ArccosineFunction Value = new ArccosineFunction();

		protected ArccosineFunction()
		{
		}

        public override string DisplayName => "acos";
        public override string Name => "Arccosine";

        public override string DocString
        {
            get
            {
                return "The arccosine function\n  acos(x)\n\nReturns the arccosine of x. That is, if cos(y) = x, then acos(x) = y.";
            }
        }

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes) => Reals.Value;
    }
}
