
/*****************************************************************************
 *                                                                           *
 *  ArccotangentFunction.cpp                                                 *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Arccotangent function.                        *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "ArccotangentFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



ArccotangentFunction::ArccotangentFunction(void)
{
	this->Name = "Arccotangent";
}

ArccotangentFunction::~ArccotangentFunction(void)
{
	
}

Literal^ ArccotangentFunction::InternalCall( ... array<Expression^>^ )
{
	Debug::Fail(__WCODESIG__);
	return gcnew Literal;
}


;
NAMESPACE_END
;


