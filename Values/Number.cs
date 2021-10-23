
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
    public readonly struct Number : IMathObject
    {
        public Number(float value)
        {
            Value = value;
        }

        public float Value { get; }

        public bool IsScalar(SolusEnvironment env) => true;
        public bool IsVector(SolusEnvironment env) => false;
        public bool IsMatrix(SolusEnvironment env) => false;
        public int GetTensorRank(SolusEnvironment env) => 0;
        public bool IsString(SolusEnvironment env) => false;

        public int GetDimension(SolusEnvironment env, int index)
        {
            throw new InvalidOperationException(
                "Scalars do not have dimensions");
        }

        public int[] GetDimensions(SolusEnvironment env)
        {
            throw new InvalidOperationException(
                "Scalars do not have dimensions");
        }

        public override string ToString()
        {
            if (Math.Abs(Value - (float) Math.E) < 1e-6)
                return "e";
            if (Math.Abs(Value - (float) Math.PI) < 1e-6)
                return "π";
            return Value.ToString("G");
        }
    }
}
