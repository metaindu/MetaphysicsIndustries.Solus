
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
 *  ArcsineFunction.cs                                                       *
 *                                                                           *
 *  The class for the built-in Arcsine function.                             *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class ArcsineFunction : SingleArgumentFunction
	{
        public static readonly ArcsineFunction Value = new ArcsineFunction();

		protected ArcsineFunction()
		{
			this.Name = "Arcsine";
		}

        public override string DisplayName
        {
            get
            {
                return "asin";
            }
        }

        public override string DocString
        {
            get
            {
                return "The arcsine function\n  asin(x)\n\nReturns the arcsine of x. That is, if sin(y) = x, then asin(x) = y.";
            }
        }

        public override IMathObject GetResultType(SolusEnvironment env,
            IEnumerable<IMathObject> argTypes)
        {
            return ScalarMathObject.Value;
        }
    }
}
