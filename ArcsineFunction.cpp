
/*****************************************************************************
 *                                                                           *
 *  ArcsineFunction.cpp                                                      *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Arcsine function.                             *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "ArcsineFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



ArcsineFunction::ArcsineFunction(void)
{
	this->Name = "Arcsine";
}

ArcsineFunction::~ArcsineFunction(void)
{
	
}

Literal^ ArcsineFunction::InternalCall( ... array<Expression^>^ args)
{
	return gcnew Literal((float)Math::Asin(args[0]->Eval()->Value));
}


;
NAMESPACE_END
;

