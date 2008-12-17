
/*****************************************************************************
 *                                                                           *
 *  ArcsecantFunction.cpp                                                    *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Arcsecant function.                           *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "ArcsecantFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



ArcsecantFunction::ArcsecantFunction(void)
{
	this->Name = "Arcsecant";
}

ArcsecantFunction::~ArcsecantFunction(void)
{
	
}

Literal^ ArcsecantFunction::InternalCall( ... array<Expression^>^ )
{
	Debug::Fail(__WCODESIG__);
	return gcnew Literal;
}


;
NAMESPACE_END
;

