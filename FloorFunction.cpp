
/*****************************************************************************
 *                                                                           *
 *  FloorFunction.cpp                                                        *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Floor function.                               *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "FloorFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



FloorFunction::FloorFunction(void)
{
	this->Name = "Floor";
}

FloorFunction::~FloorFunction(void)
{
	
}

Literal^ FloorFunction::InternalCall( ... array<Expression^>^ )
{
	Debug::Fail(__WCODESIG__);
	return gcnew Literal;
}


;
NAMESPACE_END
;
