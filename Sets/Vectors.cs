
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Exceptions;

namespace MetaphysicsIndustries.Solus.Sets
{
    public class Vectors : ISet
    {
        protected static readonly List<Vectors> sets =
            new List<Vectors>();

        public static readonly Vectors R2 = Get(2);
        public static readonly Vectors R3 = Get(3);

        public static Vectors Get(int dimension)
        {
            // TODO: make this more efficient in both time and memory for
            //       large dimensions
            while (sets.Count <= dimension)
                sets.Add(null);
            if (sets[dimension] == null)
                sets[dimension] = new Vectors(dimension);
            return sets[dimension];
        }

        protected Vectors(int dimension)
        {
            if (dimension < 1)
                throw new ValueException(nameof(dimension),
                    "Must be a positive integer");
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

        public bool IsSupersetOf(ISet other) => other == this;
        public bool IsSubsetOf(ISet other) =>
            other == this ||
            other is AllVectors ||
            other is Tensors ||
            other is MathObjects;

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsBoolean(SolusEnvironment env) => false;
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
        public string DisplayName => "Vector";
    }

    public class AllVectors : ISet
    {
        public static readonly AllVectors Value = new AllVectors();

        protected AllVectors()
        {
        }

        public bool Contains(IMathObject mo)
        {
            return mo.IsIsVector(null);
        }

        public bool IsSupersetOf(ISet other) =>
            other is AllVectors||
            other is Vectors;
        public bool IsSubsetOf(ISet other) =>
            other is AllVectors ||
            other is Tensors ||
            other is MathObjects;

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsBoolean(SolusEnvironment env) => false;
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
        public string DocString => "The set of all vectors of any dimension";
        public string DisplayName => "Vector";
    }
}
