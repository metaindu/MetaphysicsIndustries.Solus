
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

using MetaphysicsIndustries.Solus.Exceptions;

namespace MetaphysicsIndustries.Solus.Sets
{
    public class RealCoordinateSpace : ISet
    {
        public static readonly RealCoordinateSpace R2 =
            new RealCoordinateSpace(2);

        public static readonly RealCoordinateSpace R3 =
            new RealCoordinateSpace(3);

        protected RealCoordinateSpace(int dimension)
        {
            if (dimension <= 1)
                throw new ValueException(nameof(dimension),
                    "Must be a positive integer greater than 1");
            Dimension = dimension;
        }

        public readonly int Dimension;

        public bool Contains(IMathObject mo)
        {
            if (!mo.IsIsVector(null))
                return false;
            var v = mo.ToVector();
            // TODO: check for NaN, qNaN, and ±inf
            return v.Length == Dimension;
        }

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => null;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool? IsSet(SolusEnvironment env) => true;
        public bool IsConcrete => true;

        public string DocString =>
            $"The real coordinate space of dimension {Dimension}, " +
            $"ℝ^{Dimension}";
    }
}
