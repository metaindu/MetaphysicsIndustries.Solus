using System;

namespace MetaphysicsIndustries.Solus.Values
{
    public struct Number : IMathObject
    {
        public Number(float value)
        {
            Value = value;
        }

        public float Value { get; }

        public bool IsScalar => true;
        public bool IsVector => false;
        public bool IsMatrix => false;
        public int TensorRank => 0;

        public int GetDimension(int index = 0)
        {
            throw new InvalidOperationException(
                "Scalars do not have dimensions");
        }

        public int[] GetDimensions()
        {
            throw new InvalidOperationException(
                "Scalars do not have dimensions");
        }
    }
}