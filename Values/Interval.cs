
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

namespace MetaphysicsIndustries.Solus.Values
{
    public readonly struct Interval : IMathObject
    {
        public Interval(float lowerBound, bool openLowerBound,
            float upperBound, bool openUpperBound, bool isIntegerInterval)
        {
            LowerBound = lowerBound;
            OpenLowerBound = openLowerBound;
            UpperBound = upperBound;
            OpenUpperBound = openUpperBound;
            IsIntegerInterval = isIntegerInterval;
        }

        public readonly float LowerBound;
        public readonly float UpperBound;
        public float Length => UpperBound - LowerBound;

        public readonly bool OpenLowerBound;
        public readonly bool OpenUpperBound;

        public readonly bool IsIntegerInterval;

        public static Interval Integer(int lower, int upper)
        {
            return new Interval(
                lower, false,
                upper, false,
                true);
        }

        public Interval Round()
        {
            return Integer(LowerBound.RoundInt(), UpperBound.RoundInt());
        }

        public float CalcDelta(int numSteps) =>
            (UpperBound - LowerBound) / (numSteps - 1);

        // TODO: Contains number
        // TODO: intersects other interval

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => null;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => true;
        public bool IsConcrete => true;
    }
}
