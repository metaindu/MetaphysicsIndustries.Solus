
/*****************************************************************************
 *                                                                           *
 *  ArctangentFunction.cpp                                                   *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Arctangent function.                          *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "ArctangentFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



ArctangentFunction::ArctangentFunction(void)
{
	this->Name = "Arctangent";
}

ArctangentFunction::~ArctangentFunction(void)
{
	
}

Literal^ ArctangentFunction::InternalCall( ... array<Expression^>^ args)
{
	return gcnew Literal((float)Math::Atan(args[0]->Eval()->Value));
}


;
NAMESPACE_END
;



