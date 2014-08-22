
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2014 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License along
 *  with this program; if not, write to the Free Software Foundation, Inc.,
 *  51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public struct STuple<T1, T2>
    {
        public STuple(T1 value1, T2 value2)
        {
            Value1 = value1;
            Value2 = value2;
        }

        public T1 Value1;
        public T2 Value2;
    }

    public struct STuple<T1, T2, T3>
    {
        public STuple(T1 value1, T2 value2, T3 value3)
        {
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }

        public T1 Value1;
        public T2 Value2;
        public T3 Value3;
    }
}
