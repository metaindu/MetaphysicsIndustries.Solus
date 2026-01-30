
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class GreaterThanOrEqualComparisonOperation : ComparisonOperation, IComparisonOperation
    {
        public static readonly GreaterThanOrEqualComparisonOperation Value = new GreaterThanOrEqualComparisonOperation();

        protected GreaterThanOrEqualComparisonOperation()
        {
        }

        public override string Name => ">=";

        public override bool Compare(float x, float y)
        {
            return x >= y;
        }

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes)
        {
            return Booleans.Value;
        }
        public override IFunctionType FunctionType =>
            Sets.Functions.Get(Booleans.Value, Reals.Value, Reals.Value);
    }
}
