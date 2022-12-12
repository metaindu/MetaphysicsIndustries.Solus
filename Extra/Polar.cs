
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

using System;

namespace MetaphysicsIndustries.Solus.Extra
{
    public class Polar
    {
        public static STuple<float, float> ConvertEuclideanToPolar(float x,
            float y)
        {
            STuple<float, float> pair = new STuple<float, float>();
            pair.Value1 = (float)Math.Sqrt(x * x + y * y);
            pair.Value2 = (float)Math.Atan2(y, x);
            return pair;
        }

        public static STuple<float, float> ConvertPolarToEuclidean(float r,
            float theta)
        {
            STuple<float, float> pair = new STuple<float, float>();
            pair.Value1 = (float)(r * Math.Cos(theta));
            pair.Value2 = (float)(r * Math.Sin(theta));
            return pair;
        }

        public static float ConvertDegreesToRadians(float degrees)
        {
            return (float)(Math.PI * degrees / 180.0);
        }

        public static float ConvertRadiansToDegrees(float radians)
        {
            return (float)(180.0 * radians / Math.PI);
        }
    }
}
