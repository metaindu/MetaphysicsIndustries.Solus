
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

using System;
using System.Collections.Generic;

namespace MetaphysicsIndustries.Solus
{
    public class PrefixTree<TKey, TValue>
    {
        public PrefixTree(TValue value)
        {
            Value = value;
        }

        public readonly TValue Value;

        public readonly Dictionary<TKey, PrefixTree<TKey, TValue>>
            Subtrees =
                new Dictionary<TKey, PrefixTree<TKey, TValue>>();

        public TValue Get(TKey[] parameterTypes, int index,
            Func<TKey[], int, TValue> allocate)
        {
            var key = parameterTypes[index];
            if (!Subtrees.ContainsKey(key))
            {
                var value = allocate(parameterTypes, index);
                Subtrees[key] = new PrefixTree<TKey, TValue>(value);
            }

            var sub = Subtrees[key];
            if (index + 1 == parameterTypes.Length)
                return sub.Value;
            return sub.Get(parameterTypes, index + 1, allocate);
        }
    }
}