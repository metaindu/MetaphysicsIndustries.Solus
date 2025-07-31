
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
using System.Linq;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class SizeFunction : Function
    {
        public static readonly SizeFunction Value = new SizeFunction();

        public override string Name => "size";

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes)
        {
            var argType = argTypes.First();
            if (argType is Strings)
                return Vectors.Get(1);
            if (argType is Vectors)
                return Vectors.Get(1);
            if (argType is Matrices)
                return Vectors.R2;
            throw new NotImplementedException();
        }

        public override IReadOnlyList<Parameter> Parameters { get; } =
            new List<Parameter>() { new Parameter("arg", MathObjects.Value) };
    }
}
