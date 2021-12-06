
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
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class MinimumFiniteFunction : Function
    {
        public static readonly MinimumFiniteFunction Value =
            new MinimumFiniteFunction();

        public MinimumFiniteFunction() :
            base(new Types[0], "minf")
        {
        }

        public override string DocString =>
            "The minf function\n  minf(x1, x2, ..., xn)\n\n" +
            "Returns the minimum of all finite arguments. Infinities and " +
            "NaN are ignored. If no finite numbers are given, NaN is " +
            "returned.";

        public override void CheckArguments(IMathObject[] args)
        {
            if (args.Length < 1)
                throw new ArgumentException("No arguments passed");
            for (var i = 0; i < args.Length; i++)
            {
                var argtype = args[i].GetMathType();
                if (argtype != Types.Scalar)
                    throw new ArgumentException(
                        $"Argument {i} wrong type: expected " +
                        $"Scalar but got {argtype}");
            }
        }

        protected override IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args)
        {
            int i;
            float current = float.NaN;
            bool first = true;
            for (i = 0; i < args.Length; i++)
            {
                var value = args[i].ToNumber().Value;
                if (!float.IsInfinity(value) &&
                    !float.IsNaN(value))
                {
                    if (first)
                    {
                        current = value;
                        first = false;
                    }
                    else
                        current = Math.Min(current, value);
                }
            }

            return current.ToNumber();
        }

        public override IMathObject GetResult(IEnumerable<IMathObject> args)
        {
            return ScalarMathObject.Value;
        }
    }
}
