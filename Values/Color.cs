
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
    public readonly struct Color : IMathObject
    {
        public Color(byte r, byte g, byte b, byte a = 255)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public readonly byte R;
        public readonly byte G;
        public readonly byte B;
        public readonly byte A;

        public static readonly Color Black = new Color(0, 0, 0);
        public static readonly Color White = new Color(255, 255, 255);
        public static readonly Color Gray = new Color(128, 128, 128);
        public static readonly Color Red = new Color(255, 0, 0);
        public static readonly Color Green = new Color(0, 255, 0);
        public static readonly Color Blue = new Color(0, 0, 255);
        public static readonly Color Yellow = new Color(255, 255, 0);
        public static readonly Color Cyan = new Color(0, 255, 255);
        public static readonly Color Magenta = new Color(255, 0, 255);

        public string DocString => @"An RGB color value.";

        public override bool Equals(object obj)
        {
            if (obj is Color c)
                return Equals(c);
            return false;
        }

        public bool Equals(Color other)
        {
            return R == other.R &&
                   G == other.G &&
                   B == other.B &&
                   A == other.A;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = R.GetHashCode();
                hashCode = (hashCode * 397) ^ G.GetHashCode();
                hashCode = (hashCode * 397) ^ B.GetHashCode();
                hashCode = (hashCode * 397) ^ A.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Color a, Color b) => a.Equals(b);
        public static bool operator !=(Color a, Color b) => !(a == b);

        public int ToArgb()
        {
            return A << 24 |
                   R << 16 |
                   G << 8 |
                   B << 0;
        }

        public static Color FromArgb(int value)
        {
            return new Color(
                (byte)((value >> 16) & 0xff),
                (byte)((value >> 8) & 0xff),
                (byte)((value >> 0) & 0xff),
                (byte)((value >> 24) & 0xff));
        }

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => null;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool? IsSet(SolusEnvironment env) => false;
        public bool IsConcrete => true;
    }

    public struct Size
    {
        public int Width;
        public int Height;
    }
}
