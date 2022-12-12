
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

using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Evaluators
{
    public class MatrixStoreOp : StoreOp2
    {
        private IMathObject[,] _values = null;
        private Matrix? _result = null;

        public Matrix GetResult()
        {
            if (!_result.HasValue)
                _result = new Matrix(_values);
            return _result.Value;
        }

        public override void Store(int index0, int index1,
            IMathObject value)
        {
            _values[index0, index1] = value;
        }

        public override void SetMinArraySize(int length0, int length1)
        {
            if (_values == null ||
                _values.GetLength(0) < length0 ||
                _values.GetLength(1) < length1)
                _values = new IMathObject[length0, length1];
        }
    }
}
