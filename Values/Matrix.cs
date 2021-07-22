
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
using System.Text;

namespace MetaphysicsIndustries.Solus.Values
{
    public readonly struct Matrix : IMathObject
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

        private readonly IMathObject[,] _components;

        public IMathObject this[int row, int column] =>
            _components[row, column];

        public int RowCount => _components.GetLength(0);
        public int ColumnCount => _components.GetLength(1);
        public Types ComponentType { get; }

        public bool IsScalar => false;
        public bool IsVector => false;
        public bool IsMatrix => true;
        public int TensorRank => 2;
        public bool IsString => false;

        public int GetDimension(int index = 0)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Index must not be negative");
            if (index > 1)
                throw new ArgumentOutOfRangeException(nameof(index),
                    "Matrices only have two dimensions");
            return _components.GetLength(index);
        }

        public int[] GetDimensions() => new[] {RowCount, ColumnCount};

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[ ");
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
