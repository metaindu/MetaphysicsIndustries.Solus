
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
using MetaphysicsIndustries.Solus.Exceptions;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class LogarithmFunction : DualArgumentFunction
    {
        public static readonly LogarithmFunction Value = new LogarithmFunction();

        protected LogarithmFunction()
            : base("Logarithm")
        {
        }

        public override string DisplayName
        {
            get
            {
                return "log";
            }
        }

        protected override float InternalCall(float arg0, float arg1)
        {
            if (arg0 <= 0)
                throw new OperandException("Argument must be positive");
            if (arg1 <= 0)
                throw new OperandException("Base must be positive");
            if (arg1 <= 1 && arg1 >= 1)
                throw new OperandException("Base must not be one");

            var rv = (float)Math.Log(arg0, arg1);
            return rv;
        }

        public override IMathObject GetResult(IEnumerable<IMathObject> args)
        {
            return ScalarMathObject.Value;
        }
    }
}
