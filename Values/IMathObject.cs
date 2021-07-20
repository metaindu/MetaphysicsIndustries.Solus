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
