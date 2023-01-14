
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

namespace MetaphysicsIndustries.Solus.Sets
{
    public class Matrices : ISet
    {
        protected static readonly List<List<Matrices>> sets =
            new List<List<Matrices>>();

        public static readonly Matrices M2x2 = Get(2, 2);
        public static readonly Matrices M2x3 = Get(2, 3);
        public static readonly Matrices M2x4 = Get(2, 4);
        public static readonly Matrices M3x2 = Get(3, 2);
        public static readonly Matrices M3x3 = Get(3, 3);
        public static readonly Matrices M3x4 = Get(3, 4);
        public static readonly Matrices M4x2 = Get(4, 2);
        public static readonly Matrices M4x3 = Get(4, 3);
        public static readonly Matrices M4x4 = Get(4, 4);

        public static Matrices Get(int rowCount, int columnCount)
        {
            // TODO: make this more efficient in both time and memory for
            //       large row and column counts
            while (sets.Count <= rowCount)
                sets.Add(new List<Matrices>());
            while (sets[rowCount].Count <= columnCount)
                sets[rowCount].Add(null);
            if (sets[rowCount][columnCount] == null)
                sets[rowCount][columnCount] =
                    new Matrices(rowCount, columnCount);
            return sets[rowCount][columnCount];
        }

        protected Matrices(int rowCount, int columnCount)
        {
            RowCount = rowCount;
            ColumnCount = columnCount;
        }

        public readonly int RowCount;
        public readonly int ColumnCount;

        public bool Contains(IMathObject mo)
        {
            if (!mo.IsIsMatrix(null))
                return false;
            var m = mo.ToMatrix();
            // TODO: check for NaN, qNaN, and ±inf
            return m.RowCount == RowCount &&
                   m.ColumnCount == ColumnCount;
        }

        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other == this ||
            other is AllMatrices ||
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
            $"The set of {RowCount}x{ColumnCount} matrices";

        public string DisplayName => "Matrix";
    }

    public class AllMatrices : ISet
    {
        public static readonly AllMatrices Value = new AllMatrices();

        protected AllMatrices()
        {
        }

        public bool Contains(IMathObject mo)
        {
            return mo.IsIsMatrix(null);
        }

        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other is AllMatrices ||
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
            "The set of all matrices of any size";
        public string DisplayName => "Matrix";
    }
}
