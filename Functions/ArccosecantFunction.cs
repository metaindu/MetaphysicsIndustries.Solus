
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
 *  ArccosecantFunction.cs                                                   *
 *                                                                           *
 *  The class for the built-in Arccosecant function.                         *
 *                                                                           *
 *****************************************************************************/

using System;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class ArccosecantFunction : SingleArgumentFunction
	{
        public static readonly ArccosecantFunction Value = new ArccosecantFunction();

		protected ArccosecantFunction()
		{
			this.Name = "Arccosecant";
		}


        protected override IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args)
		{
            return ((float)Math.Asin(1/args[0].ToNumber().Value)).ToNumber();
        }

        public override string DisplayName
        {
            get
            {
                return "acsc";
            }
        }

        public override string DocString
        {
            get
            {
                return "The arccosecant function\n  acsc(x)\n\nReturns the arccosecant of x. That is, if csc(y) = x, then acsc(x) = y.";
            }
        }
    }
}
