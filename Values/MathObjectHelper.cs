using System.Linq;

namespace MetaphysicsIndustries.Solus.Values
{
    public static class MathObjectHelper
    {
        public static Number ToNumber(this IMathObject mo) => (Number) mo;

        public static StringValue ToStringValue(this IMathObject mo) =>
            (StringValue) mo;

        public static Vector ToVector(this IMathObject mo) => (Vector) mo;
        public static Matrix ToMatrix(this IMathObject mo) => (Matrix) mo;

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

        public static Types GetMathType(this IMathObject mo)
        {
            if (mo.IsScalar) return Types.Scalar;
            if (mo.IsVector) return Types.Vector;
            if (mo.IsMatrix) return Types.Matrix;
            if (mo.IsString) return Types.String;
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