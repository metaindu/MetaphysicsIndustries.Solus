
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

namespace MetaphysicsIndustries.Solus.Test
{
    public readonly struct MockMathObject : IMathObject
    {
        public MockMathObject(bool isScalar = true, bool isVector = false,
            bool isMatrix = false, int tensorRank = 0, bool isString = false,
            int[] dimensions = null)
        {
            _isScalar = isScalar;
            _isVector = isVector;
            _isMatrix = isMatrix;
            _tensorRank = tensorRank;
            _isString = isString;
            if (dimensions == null)
                dimensions = Enumerable.Repeat(1, tensorRank).ToArray();
            _dimensions = dimensions;
        }

        private readonly bool _isScalar;
        public bool IsScalar() => _isScalar;

        private readonly bool _isVector;
        public bool IsVector() => _isVector;

        private readonly bool _isMatrix;
        public bool IsMatrix() => _isMatrix;

        private readonly int _tensorRank;
        public int GetTensorRank() => _tensorRank;

        private readonly bool _isString;
        public bool IsString() => _isString;

        private readonly int[] _dimensions;
        public int GetDimension(int index = 0) => _dimensions[index];
        public int[] GetDimensions() => _dimensions;
    }
}
