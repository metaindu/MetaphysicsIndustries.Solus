
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

using System.Linq;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Values
{
    public readonly struct Vector : IVector
    {
        public Vector(IMathObject[] components)
        {
            for (var i=0;i<components.Length;i++)
                if (!components[i].GetMathType().IsSubsetOf(Reals.Value))
                    throw new TypeException("All components must be reals");
            // TODO: don't clone here
            _components = (IMathObject[]) components.Clone();
            ComponentType = Reals.Value;
        }
        public Vector(float[] components)
            : this(components.ToMathObjects())
        {
        }

        private readonly IMathObject[] _components;
        public IMathObject this[int index] => _components[index];
        public int Length => _components.Length;
        public IMathObject GetComponent(int index) => _components[index];

        public ISet ComponentType { get; }

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsBoolean(SolusEnvironment env) => false;
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
        public bool? IsSet(SolusEnvironment env) => false;

        public bool IsConcrete => true;
        public string DocString => "";

        public override string ToString()
        {
            var inner = string.Join(", ",
                _components.Select(c => c.ToString()));
            return $"[{inner}]";
        }
    }
}
