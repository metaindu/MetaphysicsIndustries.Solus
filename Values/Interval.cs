
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

using MetaphysicsIndustries.Solus.Exceptions;

namespace MetaphysicsIndustries.Solus.Values
{
    public readonly struct Interval : IMathObject
    {
        public Interval(float lowerBound, bool openLowerBound,
            float upperBound, bool openUpperBound, bool isIntegerInterval)
        {
            if (float.IsNaN(lowerBound))
                throw new ValueException(
                    nameof(lowerBound), "Not a number");
            if (float.IsNaN(upperBound))
                throw new ValueException(
                    nameof(upperBound), "Not a number");

            if (lowerBound > upperBound)
            {
                var tempf = upperBound;
                upperBound = lowerBound;
                lowerBound = tempf;

                var tempb = openUpperBound;
                openUpperBound = openLowerBound;
                openLowerBound = tempb;
            }

            LowerBound = lowerBound;
            OpenLowerBound = openLowerBound;
            UpperBound = upperBound;
            OpenUpperBound = openUpperBound;
            IsIntegerInterval = isIntegerInterval;
        }

        public Interval(float lower, float upper)
            : this(lower, false, upper, false, false)
        {
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

        public bool Contains(float value)
        {
            if (float.IsNaN(value)) return false;
            if (float.IsInfinity(value)) return false;
            if (value < LowerBound) return false;
            if (value > UpperBound) return false;
            if (value > LowerBound && value < UpperBound) return true;
            if (LowerBound < UpperBound) // not degenerate
            {
                if (value < UpperBound) // equal to lower
                    return !OpenLowerBound;
                // equal to upper
                return !OpenUpperBound;
            }
            // interval is degenerate, lower == upper
            return !OpenLowerBound || !OpenUpperBound;
        }

        public bool IsEmpty
        {
            get
            {
                // assuming that neither bound is NaN
                if (UpperBound < LowerBound) return true;
                if (LowerBound < UpperBound) return false;
                return OpenLowerBound || OpenUpperBound;
            }
        }

        public bool IsDegenerate
        {
            get
            {
                // assuming that neither bound is NaN
                if (UpperBound < LowerBound) return false;
                if (LowerBound < UpperBound) return false;
                return !OpenLowerBound && !OpenUpperBound;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is Interval interval)
                return Equals(interval);
            return false;
        }

        public bool Equals(Interval other)
        {
            return LowerBound <= other.LowerBound &&
                   LowerBound >= other.LowerBound &&
                   OpenLowerBound == other.OpenLowerBound &&
                   UpperBound <= other.UpperBound &&
                   UpperBound >= other.UpperBound &&
                   OpenUpperBound == other.OpenUpperBound &&
                   IsIntegerInterval == other.IsIntegerInterval;
        }

        public override int GetHashCode()
        {
            var s1 = (19 * LowerBound).GetHashCode();
            s1 ^= (23 * UpperBound).GetHashCode();
            float s2;
            if (OpenLowerBound && OpenUpperBound && IsIntegerInterval)
                s2 = 29;
            else if (OpenLowerBound && OpenUpperBound)
                s2 = 31;
            else if (OpenLowerBound && IsIntegerInterval)
                s2 = 37;
            else if (OpenUpperBound && IsIntegerInterval)
                s2 = 41;
            else if (OpenLowerBound)
                s2 = 43;
            else if (OpenUpperBound)
                s2 = 47;
            else if (IsIntegerInterval)
                s2 = 53;
            else
                s2 = 59;
            s1 ^= s2.GetHashCode();
            return s1;
        }

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => null;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => true;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool IsConcrete => true;
        public string DocString => "";
    }
}
