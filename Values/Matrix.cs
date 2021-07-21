
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

namespace MetaphysicsIndustries.Solus.Values
{
    public readonly struct Matrix : IMathObject
    {
        public Matrix(IMathObject[,] components)
        {
            _components = (IMathObject[,]) components.Clone();
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
    }
}
