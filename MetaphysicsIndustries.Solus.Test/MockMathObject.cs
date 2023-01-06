
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
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

namespace MetaphysicsIndustries.Solus.Test
{
    public readonly struct MockMathObject : IMathObject
    {
        public MockMathObject(
            bool isScalar = true,
            bool isBoolean = false,
            bool isVector = false,
            bool isMatrix = false,
            int tensorRank = 0,
            bool isString = false,
            int[] dimensions = null,
            bool isInterval = false,
            bool isFunction = false,
            bool isExpression = false,
            bool isSet = false,
            bool isConcrete = false,
            string docString = "")
        {
            _isScalar = isScalar;
            _isBoolean = isBoolean;
            _isVector = isVector;
            _isMatrix = isMatrix;
            _tensorRank = tensorRank;
            _isString = isString;
            if (dimensions == null)
                dimensions = Enumerable.Repeat(1, tensorRank).ToArray();
            _dimensions = dimensions;
            _isInterval = isInterval;
            _isFunction = isFunction;
            _isExpression = isExpression;
            _isSet = isSet;
            _isConcrete = isConcrete;
            _docString = docString;
        }

        private readonly bool _isScalar;
        public bool? IsScalar(SolusEnvironment env) => _isScalar;

        private readonly bool _isBoolean;
        public bool? IsBoolean(SolusEnvironment env) => _isBoolean;

        private readonly bool _isVector;
        public bool? IsVector(SolusEnvironment env) => _isVector;

        private readonly bool _isMatrix;
        public bool? IsMatrix(SolusEnvironment env) => _isMatrix;

        private readonly int _tensorRank;
        public int? GetTensorRank(SolusEnvironment env) => _tensorRank;

        private readonly bool _isString;
        public bool? IsString(SolusEnvironment env) => _isString;

        private readonly int[] _dimensions;

        public int? GetDimension(SolusEnvironment env, int index)
        {
            if (_dimensions == null) return null;
            if (index < 0 || index >= _dimensions.Length) return null;
            return _dimensions?[index];
        }

        public int[] GetDimensions(SolusEnvironment env) => _dimensions;

        public int? GetVectorLength(SolusEnvironment env)
        {
            if (_dimensions == null) return null;
            if (_dimensions.Length != 1) return null;
            return _dimensions[0];
        }

        private readonly bool? _isInterval;
        public bool? IsInterval(SolusEnvironment env) => _isInterval;

        private readonly bool? _isFunction;
        public bool? IsFunction(SolusEnvironment env) => _isFunction;

        private readonly bool? _isExpression;
        public bool? IsExpression(SolusEnvironment env) => _isExpression;

        private readonly bool? _isSet;
        public bool? IsSet(SolusEnvironment env) => _isSet;

        private readonly bool _isConcrete;
        public bool IsConcrete => _isConcrete;

        private readonly string _docString;
        public string DocString => _docString;
    }
}
