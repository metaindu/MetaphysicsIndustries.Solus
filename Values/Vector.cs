
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
using System.Linq;

namespace MetaphysicsIndustries.Solus.Values
{
    public readonly struct Vector : IMathObject
    {
        public Vector(IMathObject[] components)
        {
            // TODO: don't clone here
            _components = (IMathObject[]) components.Clone();

            var componentType = _components[0].GetMathType();
            foreach (var c in _components)
            {
                if (c.GetMathType() == componentType) continue;
                componentType = Types.Mixed;
                break;
            }

            ComponentType = componentType;
        }
        public Vector(float[] components)
            : this(components.ToMathObjects())
        {
        }

        private readonly IMathObject[] _components;
        public IMathObject this[int index] => _components[index];
        public int Length => _components.Length;
        public Types ComponentType { get; }

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => true;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => 1;
        public bool? IsString(SolusEnvironment env) => false;

        public int? GetDimension(SolusEnvironment env, int index)
        {
            if (index < 0) return null;
            if (index > 0) return null;
            return Length;
        }

        public int[] GetDimensions(SolusEnvironment env) => new[] {Length};
        public int? GetVectorLength(SolusEnvironment env) => Length;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;

        public bool IsConcrete => true;

        public override string ToString()
        {
            var inner = string.Join(", ",
                _components.Select(c => c.ToString()));
            return $"[{inner}]";
        }
    }
}
