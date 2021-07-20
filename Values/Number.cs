
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