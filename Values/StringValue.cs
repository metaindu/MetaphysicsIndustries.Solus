
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

namespace MetaphysicsIndustries.Solus.Values
{
    public readonly struct StringValue : IMathObject
    {
        public StringValue(string value)
        {
            Value = value;
        }

        public readonly string Value;

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => 0;

        public bool? IsString(SolusEnvironment env) => true;
        // TODO: IsTuple => true

        public int? GetDimension(SolusEnvironment env, int index)
        {
            if (index < 0) return null;
            if (index > 0) return null;
            return Length;
        }

        public int[] GetDimensions(SolusEnvironment env)
        {
            return new[] {Length};
        }

        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool? IsSet(SolusEnvironment env) => false;
        public bool IsConcrete => true;

        public int Length => Value?.Length ?? 0;
        public string DocString => "";

        public override string ToString()
        {
            return $"\"{Value}\"";
        }
    }
}
