
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2007-2021 Metaphysics Industries, Inc., Richard Sartor
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

/*****************************************************************************
 *                                                                           *
 *  Operation.cs                                                             *
 *                                                                           *
 *  See SolusParser.Ex.cs                                                    *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;


namespace MetaphysicsIndustries.Solus
{
    public enum OperationPrecedence
    {
        Factorial = 150,

        Exponent = 140,

        Negation = 133,

        Division = 131,

        Multiplication = 130,

        Bitwise = 125,

        Addition = 120,



        Comparison = 100,

        Equality = 90,

        LogicalAnd = 82,
        LogicalOr = 80,

        Assignment = 20,
    }

    //we don't think about operator precedence the way c++ and c# 
    //do, because we're doing math, and we have to deal with 
    //things like rendering parentheses and division bars. 
    //nevertheless, here's the precedences for those languages:

    //c++
    //
    //n/a     ::
    //LtR     . -> [array index] (function call) (member init) postfix++ postfix-- typeid() const_cast dynamic_cast reinterpret_cast static_cast
    //RtL     sizeof ++prefix --prefix ~ ! -unary +unary &addressof *indirection new delete (cast)
    //LtR     .* ->*
    //LtR     * / %
    //LtR     + - 
    //LtR     << >>
    //LtR     < > <= >=
    //LtR     == !=
    //LtR     &
    //LtR     ^
    //LtR     |
    //LtR     &&
    //LtR     ||
    //RtL     ?:
    //RtL     = *= /= %= += -= <<= >>= &= |= ^=
    //RtL     throw
    //LtR     ,

    //c#
    //
    //primary   x.y f(x) a[x] x++ x-- new typeof checked unchecked
    //unary     + - ! ~ ++x --x (T)x
    //mult      * / %
    //add       + - 
    //shift     << >>
    //relation  < > <= >= is as
    //equ       == !=
    //bit and   &
    //bit xor   ^
    //bit or    |
    //log and   &&
    //log or    ||
    //coalesce  ??      <RtL>
    //condition ?:      <RtL>
    //assign    = *= /= %= += -= <<= >>= &= ^= |=       <RtL>
    //
    //
    //
    //


}
