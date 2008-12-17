
/*****************************************************************************
 *                                                                           *
 *  ArccosecantFunction.cpp                                                  *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Arccosecant function.                         *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "ArccosecantFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



ArccosecantFunction::ArccosecantFunction(void)
{
	this->Name = "Arccosecant";
}

ArccosecantFunction::~ArccosecantFunction(void)
{
	
}

Literal^ ArccosecantFunction::InternalCall( ... array<Expression^>^ )
{
	Debug::Fail(__WCODESIG__);
	return gcnew Literal;
}


;
NAMESPACE_END
;

