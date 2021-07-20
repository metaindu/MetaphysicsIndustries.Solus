namespace MetaphysicsIndustries.Solus.Values
{
    public interface IMathObject
    {
        bool IsScalar { get; }
        bool IsVector { get; }
        bool IsMatrix { get; }
        bool IsTensor { get; }
        int TensorRank { get; }
        int GetDimension(int index = 0);
        int[] GetDimensions();
    }
}
