
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
    public interface IMathObject
    {
        bool IsScalar { get; }
        bool IsVector { get; }
        bool IsMatrix { get; }
        int TensorRank { get; }
        int GetDimension(int index = 0);
        int[] GetDimensions();
    }

    public static class MathObjectHelper
    {
        public static Number ToNumber(this IMathObject mo) => (Number) mo;
        public static Number ToNumber(this float value) => new Number(value);
        public static Number ToNumber(this int value) => new Number(value);
        public static Number ToNumber(this long value) => new Number(value);
    }
}