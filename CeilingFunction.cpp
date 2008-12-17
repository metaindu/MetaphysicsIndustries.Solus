
/*****************************************************************************
 *                                                                           *
 *  CeilingFunction.cpp                                                      *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Ceiling function.                             *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "CeilingFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



CeilingFunction::CeilingFunction(void)
{
	this->Name = "Ceiling";
}

CeilingFunction::~CeilingFunction(void)
{
	
}

Literal^ CeilingFunction::InternalCall( ... array<Expression^>^ )
{
	Debug::Fail(__WCODESIG__);
	return gcnew Literal;
}


;
NAMESPACE_END
;
