
/*****************************************************************************
 *                                                                           *
 *  CosecantFunction.cpp                                                     *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Cosecant function.                            *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "CosecantFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



CosecantFunction::CosecantFunction(void)
{
	this->Name = "Cosecant";
}

CosecantFunction::~CosecantFunction(void)
{
	
}

Literal^ CosecantFunction::InternalCall( ... array<Expression^>^ )
{
	Debug::Fail(__WCODESIG__);
	return gcnew Literal;
}



;
NAMESPACE_END
;

