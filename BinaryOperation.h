
/*****************************************************************************
 *                                                                           *
 *  BinaryOperation.h                                                        *
 *  17 November 2006                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A specialized function which represents simple arithmetical operations   *
 *    on two arguments.                                                      *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Operation.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Solus { 
;



public ref class BinaryOperation abstract : Operation
{

public:

	BinaryOperation(void);
	virtual ~BinaryOperation(void);

protected:

	virtual void CheckArguments( ... array<Expression^>^ ) override;

private:

};



} } 



