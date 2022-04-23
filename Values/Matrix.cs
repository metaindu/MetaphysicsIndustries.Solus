
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

using System.Text;

namespace MetaphysicsIndustries.Solus.Values
{
    public readonly struct Matrix : IMatrix
    {
        public Matrix(IMathObject[,] components)
        {
            _components = (IMathObject[,]) components.Clone();

            Types GetComponentType(IMathObject[,] comps)
            {
                var componentType = comps[0, 0].GetMathType();
                for (int i = 0; i < comps.GetLength(0); i++)
                for (int j = 0; j < comps.GetLength(1); j++)
                    if (comps[i, j].GetMathType() != componentType)
                        return Types.Mixed;

                return componentType;
            }

            ComponentType = GetComponentType(_components);
        }
        public Matrix(float[,] components)
            : this(components.ToMathObjects())
        {
        }

        public static Matrix Zero(int rowCount, int columnCount) =>
            new Matrix(new float[rowCount, columnCount]);

        private readonly IMathObject[,] _components;

        public IMathObject this[int row, int column] =>
            _components[row, column];

        public int RowCount => _components.GetLength(0);
        public int ColumnCount => _components.GetLength(1);

        public IMathObject GetComponent(int row, int column) =>
            _components[row, column];

        public Types ComponentType { get; }

        public bool? IsScalar(SolusEnvironment env) => false;
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
    }
}
