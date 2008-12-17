
/*****************************************************************************
 *                                                                           *
 *  ArccosineFunction.cpp                                                    *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Arccosine function.                           *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "ArccosineFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



ArccosineFunction::ArccosineFunction(void)
{
	this->Name = "Arccosine";
}

ArccosineFunction::~ArccosineFunction(void)
{
	
}

Literal^ ArccosineFunction::InternalCall( ... array<Expression^>^ args)
{
	return gcnew Literal((float)Math::Acos(args[0]->Eval()->Value));
}


;
NAMESPACE_END
;

