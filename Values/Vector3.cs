
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
    public readonly struct Vector3 : IVector
    {
        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public readonly float X;
        public readonly float Y;
        public readonly float Z;

        public static readonly Vector3 Zero = new Vector3(0, 0, 0);
        public static readonly Vector3 One = new Vector3(1, 1, 1);
        public static readonly Vector3 UnitX = new Vector3(1, 0, 0);
        public static readonly Vector3 UnitY = new Vector3(0, 1, 0);
        public static readonly Vector3 UnitZ = new Vector3(0, 0, 1);

        public static Vector3 operator -(Vector3 v)
        {
            return new Vector3(-v.X, -v.Y, -v.Z);
        }

        public static Vector3 operator -(Vector3 x, Vector3 y)
        {
            return new Vector3(x.X - y.X, x.Y - y.Y, x.Z - y.Z);
        }

        public static Vector3 operator +(Vector3 x, Vector3 y)
        {
            return new Vector3(x.X + y.X, x.Y + y.Y, x.Z + y.Z);
        }

        public static Vector3 operator *(Vector3 v, float s)
        {
            return new Vector3(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vector3 operator *(float s, Vector3 v)
        {
            return new Vector3(v.X * s, v.Y * s, v.Z * s);
        }

        public static Vector3 operator /(Vector3 v, float s)
        {
            return new Vector3(v.X / s, v.Y / s, v.Z / s);
        }

        public static bool operator ==(Vector3 u, Vector3 v)
        {
            return u.Equals(v);
        }

        public static bool operator !=(Vector3 u, Vector3 v)
        {
            return !u.Equals(v);
        }

        public bool Equals(Vector3 other)
        {
            return (this.X == other.X &&
                    this.Y == other.Y &&
                    this.Z == other.Z);
        }

        public override bool Equals(object other)
        {
            if (other is Vector3)
            {
                return Equals((Vector3)other);
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            var x = 113 * X.GetHashCode();
            x ^= 127 * Y.GetHashCode();
            return x ^ Z.GetHashCode();
        }

        public float Length()
        {
            return (float)Math.Sqrt(this.LengthSquared());
        }

        public float LengthSquared()
        {
            return Vector3.Dot(this, this);
        }

        public static float Dot(Vector3 a, Vector3 b)
        {
            return a.X * b.X + a.Y * b.Y + a.Z * b.Z;
        }

        public Vector3 Normalized()
        {
            return Normalize(this);
        }

        public static Vector3 Normalize(Vector3 v)
        {
            if (v.LengthSquared() > 0)
            {
                return v / v.Length();
            }
            else
            {
                return Zero;
            }
        }

        public static float Distance(Vector3 u, Vector3 v)
        {
            return (u - v).Length();
        }

        public static float DistanceSquared(Vector3 u, Vector3 v)
        {
            return (u - v).LengthSquared();
        }

        public static Vector3 Max(Vector3 u, Vector3 v)
        {
            return new Vector3(
                Math.Max(u.X, v.X),
                Math.Max(u.Y, v.Y),
                Math.Max(u.Z, v.Z));
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1} Z:{2}}}", X, Y, Z);
        }

        public Vector3 AddX(float dx)
        {
            return new Vector3(X + dx, Y, Z);
        }

        public Vector3 AddY(float dy)
        {
            return new Vector3(X, Y + dy, Z);
        }

        public Vector3 AddZ(float dz)
        {
            return new Vector3(X, Y, Z + dz);
        }

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => true;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => 1;
        public bool? IsString(SolusEnvironment env) => false;

        public int? GetDimension(SolusEnvironment env, int index)
        {
            if (index == 0) return 3;
            return null;
        }

        private static readonly int[] Dimensions = new int[1] { 3 };
        public int[] GetDimensions(SolusEnvironment env) => Dimensions;
        public int? GetVectorLength(SolusEnvironment env) => 3;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool IsConcrete => true;
        public string DocString => "";
        int IVector.Length => 3;

        public IMathObject GetComponent(int index)
        {
            if (index == 0) return X.ToNumber();
            if (index == 1) return Y.ToNumber();
            if (index == 2) return Z.ToNumber();
            throw new IndexOutOfRangeException();
        }

        public IMathObject this[int index] => GetComponent(index);
    }
}
