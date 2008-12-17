
/*****************************************************************************
 *                                                                           *
 *  SecantFunction.cpp                                                       *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Secant function.                              *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "SecantFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



SecantFunction::SecantFunction(void)
{
	this->Name = "Secant";
}

SecantFunction::~SecantFunction(void)
{
	
}

Literal^ SecantFunction::InternalCall( ... array<Expression^>^ )
{
	Debug::Fail(__WCODESIG__);
	return gcnew Literal;
}


;
NAMESPACE_END
;
