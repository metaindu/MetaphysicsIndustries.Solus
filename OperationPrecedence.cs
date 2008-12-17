
/*****************************************************************************
 *                                                                           *
 *  Operation.cs                                                             *
 *  30 November 2007                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public enum OperationPrecedence
    {

        Addition = 50,

        Multiplication = 75,
        
        Exponent = 90,

        Division = 100,

    }
}
