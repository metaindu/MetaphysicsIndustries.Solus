
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

using System.Linq;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus
{
    public static class MathObjectHelper
    {
        public static bool IsIsScalar(this IMathObject mo,
            SolusEnvironment env)
        {
            var iss = mo.IsScalar(env);
            return iss.HasValue && iss.Value;
        }
        public static bool IsIsVector(this IMathObject mo,
            SolusEnvironment env)
        {
            var iss = mo.IsVector(env);
            return iss.HasValue && iss.Value;
        }
        public static bool IsIsMatrix(this IMathObject mo,
            SolusEnvironment env)
        {
            var iss = mo.IsMatrix(env);
            return iss.HasValue && iss.Value;
        }
        public static bool IsIsString(this IMathObject mo,
            SolusEnvironment env)
        {
            var iss = mo.IsString(env);
            return iss.HasValue && iss.Value;
        }
        public static bool IsIsInterval(this IMathObject mo,
            SolusEnvironment env)
        {
            var iss = mo.IsInterval(env);
            return iss.HasValue && iss.Value;
        }
        public static bool IsIsFunction(this IMathObject mo,
            SolusEnvironment env)
        {
            var iss = mo.IsFunction(env);
            return iss.HasValue && iss.Value;
        }
        public static bool IsIsExpression(this IMathObject mo,
            SolusEnvironment env)
        {
            var iss = mo.IsExpression(env);
            return iss.HasValue && iss.Value;
        }

        public static Number ToNumber(this IMathObject mo) => (Number) mo;

        public static StringValue ToStringValue(this IMathObject mo) =>
            (StringValue) mo;

        public static IVector ToVector(this IMathObject mo) => (IVector) mo;
        public static IMatrix ToMatrix(this IMathObject mo) => (IMatrix) mo;

        public static Number ToNumber(this float value) => new Number(value);
        public static Number ToNumber(this int value) => new Number(value);
        public static Number ToNumber(this long value) => new Number(value);

        public static Vector ToVector(this float[] values) =>
            new Vector(values);

        public static Vector ToVector(this int[] values) =>
            new Vector(values.Select(v => (float) v).ToArray());

        public static StringValue ToStringValue(this string value) =>
            new StringValue(value);

        public static StringValue ToStringValue(this char value) =>
            value.ToString().ToStringValue();

        public static float ToFloat(this IMathObject value) =>
            value.ToNumber().Value;

        public static Types GetMathType(this IMathObject mo,
            SolusEnvironment env=null)
        {
            if (mo.IsIsScalar(env)) return Types.Scalar;
            if (mo.IsIsVector(env)) return Types.Vector;
            if (mo.IsIsMatrix(env)) return Types.Matrix;
            if (mo.IsIsString(env)) return Types.String;
            return Types.Unknown;
        }

        public static IMathObject[] ToMathObjects(this float[] values)
        {
            return values.Select(
                v => (IMathObject) v.ToNumber()).ToArray();
        }

        public static IMathObject[,] ToMathObjects(this float[,] values)
        {
            var result = new IMathObject[values.GetLength(0),
                values.GetLength(1)];
            // TODO: faster
            // TODO: row first or column first?
            for (var i = 0; i < values.GetLength(0); i++)
            {
                for (var j = 0; j < values.GetLength(1); j++)
                {
                    result[i, j] = values[i, j].ToNumber();
                }
            }

            return result;
        }
    }
}