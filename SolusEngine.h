
/*****************************************************************************
 *                                                                           *
 *  SolusEngine.h                                                            *
 *  17 November 2006                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The central core of processing in Solus. Does some rudimentary parsing   *
 *    and evaluation and stuff.                                              *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Expression.h"
#include "Literal.h"
#include "Function.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Solus { 
;



public ref class SolusEngine
{

public:

	SolusEngine(void);
	virtual ~SolusEngine(void);

	Expression^ Parse(String^);
	Literal^ Eval(Expression^);

protected:

	Function^ GetFunctionByName(String^);

private:

};



} } 




