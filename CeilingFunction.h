
/*****************************************************************************
 *                                                                           *
 *  CeilingFunction.h                                                        *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Ceiling function.                             *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Function.h"

using namespace System;
using namespace System::Collections::Generic;



namespace MetaphysicsIndustries { namespace Solus { 
;



ref class Expression;
ref class Literal;
public ref class CeilingFunction : Function
{

public:

	CeilingFunction(void);
	virtual ~CeilingFunction(void);

protected:

	virtual Literal^ InternalCall( ... array<Expression^>^) override;

private:

};



} } 
