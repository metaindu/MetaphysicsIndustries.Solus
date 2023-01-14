
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

/*****************************************************************************
 *                                                                           *
 *  SineFunction.cs                                                          *
 *                                                                           *
 *  The class for the built-in Sine function.                                *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class SineFunction : SingleArgumentFunction
	{
        public static readonly SineFunction Value = new SineFunction();

        protected SineFunction()
		{
			this.Name = "Sine";
		}

        public override string DisplayName
        {
            get
            {
                return "sin";
            }
        }

        public override string DocString
        {
            get
            {
                return "The sine function\n  sin(x)\n\nReturns the sine of x.";
            }
        }

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes) => Reals.Value;
        public override IFunctionType FunctionType => Sets.Functions.RealsToReals;
    }
}
