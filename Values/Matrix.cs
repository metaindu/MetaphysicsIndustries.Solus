
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

using System;
using System.Text;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Values
{
    public readonly struct Matrix : IMatrix
    {
        public Matrix(IMathObject[,] components)
        {
            for (var r = 0; r < components.GetLength(0); r++)
            for (var c = 0; c < components.GetLength(1); c++)
                if (!components[r, c].GetMathType().IsSubsetOf(Reals.Value))
                    throw new TypeException("All components must be reals");

            // TODO: don't clone here?
            _components = (IMathObject[,])components.Clone();
            ComponentType = Reals.Value;
        }
        public Matrix(float[,] components)
            : this(components.ToMathObjects())
        {
        }

        public static readonly Matrix Identity2 =
            new Matrix(new float[,]
            {
                { 1, 0 },
                { 0, 1 }
            });

        public static readonly Matrix Identity3 =
            new Matrix(new float[,]
            {
                { 1, 0, 0 },
                { 0, 1, 0 },
                { 0, 0, 1 }
            });

        public static readonly Matrix Identity4 =
            new Matrix(new float[,]
            {
                { 1, 0, 0, 0 },
                { 0, 1, 0, 0 },
                { 0, 0, 1, 0 },
                { 0, 0, 0, 1 }
            });

        public static Matrix Zero(int rowCount, int columnCount) =>
            new Matrix(new float[rowCount, columnCount]);

        public static Matrix M22(
            float m11, float m12,
            float m21, float m22)
        {
            return new Matrix(new[,]
            {
                { m11, m12 },
                { m21, m22 }
            });
        }

        public static Matrix M23(
            float m11, float m12, float m13,
            float m21, float m22, float m23)
        {
            return new Matrix(new[,]
            {
                { m11, m12, m13 },
                { m21, m22, m23 }
            });
        }

        public static Matrix M24(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24)
        {
            return new Matrix(new[,]
            {
                { m11, m12, m13, m14 },
                { m21, m22, m23, m24 }
            });
        }

        public static Matrix M32(
            float m11, float m12,
            float m21, float m22,
            float m31, float m32)
        {
            return new Matrix(new[,]
            {
                { m11, m12 },
                { m21, m22 },
                { m31, m32 }
            });
        }

        public static Matrix M33(
            float m11, float m12, float m13,
            float m21, float m22, float m23,
            float m31, float m32, float m33)
        {
            return new Matrix(new[,]
            {
                { m11, m12, m13 },
                { m21, m22, m23 },
                { m31, m32, m33 }
            });
        }

        public static Matrix M34(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34)
        {
            return new Matrix(new[,]
            {
                { m11, m12, m13, m14 },
                { m21, m22, m23, m24 },
                { m31, m32, m33, m34 }
            });
        }

        public static Matrix M42(
            float m11, float m12,
            float m21, float m22,
            float m31, float m32,
            float m41, float m42)
        {
            return new Matrix(new[,]
            {
                { m11, m12 },
                { m21, m22 },
                { m31, m32 },
                { m41, m42 }
            });
        }

        public static Matrix M43(
            float m11, float m12, float m13,
            float m21, float m22, float m23,
            float m31, float m32, float m33,
            float m41, float m42, float m43)
        {
            return new Matrix(new[,]
            {
                { m11, m12, m13 },
                { m21, m22, m23 },
                { m31, m32, m33 },
                { m41, m42, m43 }
            });
        }

        public static Matrix M44(
            float m11, float m12, float m13, float m14,
            float m21, float m22, float m23, float m24,
            float m31, float m32, float m33, float m34,
            float m41, float m42, float m43, float m44)
        {
            return new Matrix(new[,]
            {
                { m11, m12, m13, m14 },
                { m21, m22, m23, m24 },
                { m31, m32, m33, m34 },
                { m41, m42, m43, m44 }
            });
        }

        private readonly IMathObject[,] _components;

        public IMathObject this[int row, int column] =>
            _components[row, column];

        public int RowCount => _components.GetLength(0);
        public int ColumnCount => _components.GetLength(1);

        public IMathObject GetComponent(int row, int column) =>
            _components[row, column];

        public ISet ComponentType { get; }

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsBoolean(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => true;
        public int? GetTensorRank(SolusEnvironment env) => 2;
        public bool? IsString(SolusEnvironment env) => false;

        public int? GetDimension(SolusEnvironment env, int index)
        {
            if (index < 0) return null;
            if (index > 1) return null;
            return _components.GetLength(index);
        }

        public int[] GetDimensions(SolusEnvironment env) =>
            new[] { RowCount, ColumnCount };

        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool? IsSet(SolusEnvironment env) => false;

        public bool IsConcrete => true;
        public string DocString => "";

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[");
            for (var r = 0; r < RowCount; r++)
            {
                if (r > 0) sb.Append("; ");
                for (var c = 0; c < ColumnCount; c++)
                {
                    if (c > 0) sb.Append(", ");
                    sb.Append(_components[r, c]);
                }
            }

            sb.Append("]");
            return sb.ToString();
        }

        public override bool Equals(object obj)
        {
            if (!(obj is IMatrix m))
                return false;
            if (m.RowCount != RowCount ||
                m.ColumnCount != ColumnCount)
                return false;
            int r, c;
            for (r = 0; r < RowCount; r++)
            for (c = 0; c < ColumnCount; c++)
                if (!m[r, c].Equals(this[r, c]))
                    return false;
            return true;
        }

        public override int GetHashCode()
        {
            const uint s_seed = 399891796U;
            const uint Prime1 = 2654435761U;
            const uint Prime2 = 2246822519U;
            const uint Prime3 = 3266489917U;
            const uint Prime4 = 668265263U;
            const uint Prime5 = 374761393U;
            var primes = new[] { Prime1, Prime2, Prime3, Prime4, Prime5 };
            uint hash = s_seed + Prime5;
            hash += 8;

            int r, c, k;
            k = 0;
            for (r = 0; r < RowCount; r++)
            {
                for (c = 0; c < ColumnCount; c++, k++)
                {
                    var h = this[r, c].GetHashCode();
                    var x = (uint)(hash + h * primes[k % 5]);
                    var y = (x << 17) | (x >> (32 - 17));
                    var z = y * primes[(k + 1) % 5];
                    hash = z;
                }

                k++;
            }

            return (int)hash;
        }
    }
}
