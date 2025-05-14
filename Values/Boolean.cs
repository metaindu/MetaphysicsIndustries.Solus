
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
    public readonly struct Boolean : IMathObject
    {
        public Boolean(bool value) =>
            Value = value;

        public readonly bool Value;

        public override bool Equals(object obj)
        {
            if (obj is Boolean b)
                return Equals(b);
            if (obj is bool bb)
                return Value == bb;
            return false;
        }

        public override string ToString()
        {
            return Value ? "true" : "false";
        }

        public bool Equals(Boolean other) => Value == other.Value;
        public override int GetHashCode() => Value.GetHashCode();
        public static bool operator ==(Boolean a, Boolean b) =>
            a.Value == b.Value;
        public static bool operator !=(Boolean a, Boolean b) => !(a == b);
        public static implicit operator bool(Boolean a) => a.Value;

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsBoolean(SolusEnvironment env) => true;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => 0;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool? IsSet(SolusEnvironment env) => false;

        public bool IsConcrete => true;

        public string DocString => $"Boolean {Value} value";
    }
}