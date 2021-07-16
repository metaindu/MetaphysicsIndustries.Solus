
/*****************************************************************************
 *                                                                           *
 *  Operation.cs                                                             *
 *  30 November 2007                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright (c) 2006-2021 Metaphysics Industries, Inc.                     *
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
