
/*****************************************************************************
 *                                                                           *
 *  SineFunction.cpp                                                         *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Sine function.                                *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "SineFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



SineFunction::SineFunction(void)
{
	this->Name = "Sine";
}

SineFunction::~SineFunction(void)
{
	
}

Literal^ SineFunction::InternalCall( ... array<Expression^>^ args)
{
	return gcnew Literal((float)Math::Sin(args[0]->Eval()->Value));
}


;
NAMESPACE_END
;


