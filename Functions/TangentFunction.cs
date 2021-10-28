
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
 *  TangentFunction.cs                                                       *
 *                                                                           *
 *  The class for the built-in Tangent function.                             *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class TangentFunction : SingleArgumentFunction
	{
        public static readonly TangentFunction Value = new TangentFunction();

        protected TangentFunction()
		{
			this.Name = "Tangent";
		}


        protected override IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args)
		{
            return ((float)Math.Tan(args[0].ToNumber().Value)).ToNumber();
		}

        public override string DisplayName
        {
            get
            {
                return "tan";
            }
        }

        public override IMathObject GetResult(IEnumerable<IMathObject> args)
        {
            return ScalarMathObject.Value;
        }
    }
}
