using System;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Test
{
    public readonly struct MockMathObject : IMathObject
    {
        public MockMathObject(bool isScalar = true, bool isVector = false,
            bool isMatrix = false, int tensorRank = 0)
        {
            IsScalar = isScalar;
            IsVector = isVector;
            IsMatrix = isMatrix;
            TensorRank = tensorRank;
        }

        public bool IsScalar { get; }
        public bool IsVector { get; }
        public bool IsMatrix { get; }
        public int TensorRank { get; }

        public int GetDimension(int index = 0)
        {
            throw new NotImplementedException();
        }

        public int[] GetDimensions()
        {
            throw new NotImplementedException();
        }
    }
}