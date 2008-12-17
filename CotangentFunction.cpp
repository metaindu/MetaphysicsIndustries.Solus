
/*****************************************************************************
 *                                                                           *
 *  CotangentFunction.cpp                                                    *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Cotangent function.                           *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "CotangentFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



CotangentFunction::CotangentFunction(void)
{
	this->Name = "Cotangent";
}

CotangentFunction::~CotangentFunction(void)
{
	
}

Literal^ CotangentFunction::InternalCall( ... array<Expression^>^ )
{
	Debug::Fail(__WCODESIG__);
	return gcnew Literal;
}


;
NAMESPACE_END
;


