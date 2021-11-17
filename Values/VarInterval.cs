
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
    public readonly struct VarInterval
    {
        public VarInterval(string variable, Interval interval)
        {
            Variable = variable;
            Interval = interval;
        }

        public readonly string Variable;
        public readonly Interval Interval;

        public override string ToString()
        {
            return string.Format(
                "{0} {1} {2} {3} {4}",
                Interval.LowerBound,
                (Interval.OpenLowerBound ? "<" : "<="),
                Variable,
                (Interval.OpenUpperBound ? "<" : "<="),
                Interval.UpperBound);
        }
    }
}
