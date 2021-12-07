
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
    public readonly struct Vector2 : IVector
    {
        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public readonly float X;
        public readonly float Y;

        public static readonly Vector2 Zero = new Vector2(0, 0);
        public static readonly Vector2 One = new Vector2(1, 1);
        public static readonly Vector2 UnitX = new Vector2(1, 0);
        public static readonly Vector2 UnitY = new Vector2(0, 1);

        public static Vector2 operator -(Vector2 v)
        {
            return new Vector2(-v.X, -v.Y);
        }
        public static Vector2 operator -(Vector2 x, Vector2 y)
        {
            return new Vector2(x.X - y.X, x.Y - y.Y);
        }
        public static Vector2 operator +(Vector2 x, Vector2 y)
        {
            return new Vector2(x.X + y.X, x.Y + y.Y);
        }
        public static Vector2 operator *(Vector2 v, float s)
        {
            return new Vector2(v.X * s, v.Y * s);
        }
        public static Vector2 operator *(float s, Vector2 v)
        {
            return new Vector2(v.X * s, v.Y * s);
        }
        public static Vector2 operator /(Vector2 v, float s)
        {
            return new Vector2(v.X / s, v.Y / s);
        }
        public static bool operator ==(Vector2 u, Vector2 v)
        {
            return u.Equals(v);
        }
        public static bool operator !=(Vector2 u, Vector2 v)
        {
            return !u.Equals(v);
        }

        public bool Equals(Vector2 other)
        {
            return (this.X == other.X &&
                this.Y == other.Y);
        }
        public override bool Equals(object other)
        {
            if (other is Vector2)
            {
                return Equals((Vector2)other);
            }
            else
            {
                return false;
            }
        }
        public override int GetHashCode()
        {
            var x = 101 * X.GetHashCode();
            return x ^ Y.GetHashCode();
        }

        public float Length()
        {
            return (float)Math.Sqrt(this.LengthSquared());
        }

        public float LengthSquared()
        {
            return Vector2.Dot(this, this);
        }

        public static float Dot(Vector2 a, Vector2 b)
        {
            return a.X * b.X + a.Y * b.Y;
        }

        public float ToAngle()
        {
            if (this.LengthSquared() > 0)
            {
                return (float)Math.Atan2(this.Y, this.X);
            }
            else
            {
                return 0;
            }
        }
        public static Vector2 FromAngle(float angle)
        {
            return FromAngle((double)angle);
        }
        public static Vector2 FromAngle(double angle)
        {
            return new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
        }

        public Vector2 Normalized()
        {
            return Normalize(this);
        }

        public static Vector2 Normalize(Vector2 v)
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

        public static float Distance(Vector2 u, Vector2 v)
        {
            return (u - v).Length();
        }

        public static float DistanceSquared(Vector2 u, Vector2 v)
        {
            return (u - v).LengthSquared();
        }

        public static Vector2 Max(Vector2 u, Vector2 v)
        {
            return new Vector2(
                Math.Max(u.X, v.X),
                Math.Max(u.Y, v.Y));
        }

        public override string ToString()
        {
            return string.Format("{{X:{0} Y:{1}}}", X, Y);
        }

        public Vector2 AddX(float dx)
        {
            return new Vector2(X + dx, Y);
        }
        public Vector2 AddY(float dy)
        {
            return new Vector2(X, Y + dy);
        }

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => true;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => 1;
        public bool? IsString(SolusEnvironment env) => false;

        public int? GetDimension(SolusEnvironment env, int index)
        {
            if (index == 0) return 2;
            return null;
        }

        private static readonly int[] Dimensions = new int[1] { 2 };
        public int[] GetDimensions(SolusEnvironment env) => Dimensions;
        public int? GetVectorLength(SolusEnvironment env) => 2;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool IsConcrete => true;
        public string DocString => "";
        int IVector.Length => 2;

        public IMathObject GetComponent(int index)
        {
            if (index == 0) return X.ToNumber();
            if (index == 1) return Y.ToNumber();
            throw new IndexOutOfRangeException();
        }

        public IMathObject this[int index] => GetComponent(index);
    }
}

