
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

using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Evaluators
{
    public class VectorStoreOp : StoreOp1
    {
        private IMathObject[] _values = null;
        private Vector? _result = null;

        public Vector GetResult()
        {
            if (!_result.HasValue)
                _result = new Vector(_values);
            return _result.Value;
        }

        public override void Store(int index, IMathObject value)
        {
            _values[index] = value;
        }

        public override void SetMinArraySize(int length)
        {
            if (_values == null || _values.Length != length)
                _values = new IMathObject[length];
        }
    }
}
