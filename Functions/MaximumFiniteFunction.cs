
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

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class MaximumFiniteFunction : Function
    {
        public static readonly MaximumFiniteFunction Value =
            new MaximumFiniteFunction();

        public override string Name => "maxf";

        public override string DocString =>
            "The maxf function\n  maxf(x1, x2, ..., xn)\n\n" +
            "Returns the maximum of all finite arguments. Infinities and " +
            "NaN are ignored. If no finite numbers are given, NaN is " +
            "returned.";

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes) => Reals.Value;

        public override IReadOnlyList<Parameter> Parameters { get; } =
            new List<Parameter>();
        public override bool IsVariadic => true;
        public override ISet VariadicParameterType => Reals.Value;
    }
}
