
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
    public class MockMathObjectF : IMathObject
    {
        public MockMathObjectF(
            Func<SolusEnvironment, bool> isScalarF = null,
            Func<SolusEnvironment, bool> isVectorF = null,
            Func<SolusEnvironment, bool> isMatrixF = null,
            Func<SolusEnvironment, int> getTensorRankF = null,
            Func<SolusEnvironment, bool> isStringF = null,
            Func<SolusEnvironment, int, int> getDimensionF = null,
            Func<SolusEnvironment, int[]> getDimensionsF = null,
            Func<SolusEnvironment, int> getVectorLengthF = null,
            Func<SolusEnvironment, bool?> isIntervalF = null,
            bool isConcreteV = false)
        {
            IsScalarF = isScalarF;
            IsVectorF = isVectorF;
            IsMatrixF = isMatrixF;
            GetTensorRankF = getTensorRankF;
            IsStringF = isStringF;
            GetDimensionF = getDimensionF;
            GetDimensionsF = getDimensionsF;
            GetVectorLengthF = getVectorLengthF;
            IsIntervalF = isIntervalF;
            IsConcreteV = isConcreteV;
        }

        public Func<SolusEnvironment, bool> IsScalarF;
        public bool? IsScalar(SolusEnvironment env)
        {
            if (IsScalarF != null) return IsScalarF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, bool> IsVectorF;
        public bool? IsVector(SolusEnvironment env)
        {
            if (IsVectorF != null) return IsVectorF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, bool> IsMatrixF;
        public bool? IsMatrix(SolusEnvironment env)
        {
            if (IsMatrixF != null) return IsMatrixF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, int> GetTensorRankF;
        public int? GetTensorRank(SolusEnvironment env)
        {
            if (GetTensorRankF != null) return GetTensorRankF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, bool> IsStringF;
        public bool? IsString(SolusEnvironment env)
        {
            if (IsStringF != null) return IsStringF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, int, int> GetDimensionF;
        public int? GetDimension(SolusEnvironment env, int index)
        {
            if (GetDimensionF != null) return GetDimensionF(env, index);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, int[]> GetDimensionsF;
        public int[] GetDimensions(SolusEnvironment env)
        {
            if (GetDimensionsF != null) return GetDimensionsF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, int> GetVectorLengthF;
        public int? GetVectorLength(SolusEnvironment env)
        {
            if (GetVectorLengthF != null) return GetVectorLengthF(env);
            throw new NotImplementedException();
        }

        public Func<SolusEnvironment, bool?> IsIntervalF;
        public bool? IsInterval(SolusEnvironment env)
        {
            if (IsIntervalF != null) return IsIntervalF(env);
            throw new NotImplementedException();
        }

        public bool IsConcreteV;
        public bool IsConcrete => IsConcreteV;
    }
}
