
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
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Test
{
    public readonly struct MockMathObject : IMathObject
    {
        public MockMathObject(bool isScalar = true, bool isVector = false,
            bool isMatrix = false, int tensorRank = 0, bool isString = false)
        {
            IsScalar = isScalar;
            IsVector = isVector;
            IsMatrix = isMatrix;
            TensorRank = tensorRank;
            IsString = isString;
        }

        public bool IsScalar { get; }
        public bool IsVector { get; }
        public bool IsMatrix { get; }
        public int TensorRank { get; }
        public bool IsString { get; }

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
