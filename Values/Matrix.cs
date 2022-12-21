
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
                if (components[r, c].GetMathType2() != Reals.Value)
                    throw new TypeException("All components must be reals");

            // TODO: don't clone here?
            _components = (IMathObject[,])components.Clone();
            ComponentType = Reals.Value;
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

        public IMathObject GetComponent(int row, int column) =>
            _components[row, column];

        public ISet ComponentType { get; }

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
    }
}
