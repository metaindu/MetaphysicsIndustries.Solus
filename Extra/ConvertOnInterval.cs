
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

namespace MetaphysicsIndustries.Solus.Extra
{
    public static class ConvertOnInterval
    {
        public static float ConvertNegOneOneToZeroOne(float x)
        {
            //convert a number on the interval of [-1,1] to [0,1]
            return (x + 1) / 2;
        }

        public static float ConvertZeroOneToNegOneOne(float x)
        {
            //convert a number on the interval of [0,1] to [-1,1]
            return x * 2 - 1;
        }

        public static STuple<float, float> ConvertNegOneOneToZeroOne(float x,
            float y)
        {
            return new STuple<float, float>(
                ConvertNegOneOneToZeroOne(x),
                ConvertNegOneOneToZeroOne(y));
        }

        public static STuple<float, float> ConvertZeroOneToNegOneOne(float x,
            float y)
        {
            return new STuple<float, float>(
                ConvertZeroOneToNegOneOne(x),
                ConvertZeroOneToNegOneOne(y));
        }

        public static float IntervalFit(float value, float min, float max)
        {
            return (value - min) / (max - min);
        }
    }
}
