
/*****************************************************************************
 *                                                                           *
 *  Operation.cs                                                             *
 *  30 November 2007                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  See SolusParser.Ex.cs                                                    *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public enum OperationPrecedence
    {
        Equality = 90,

        Comparison = 100,

        Addition = 120,

        Multiplication = 130,
        
        Division = 131,

        Negation = 133,

        Exponent = 135,

    }
}
