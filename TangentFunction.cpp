
/*****************************************************************************
 *                                                                           *
 *  TangentFunction.cpp                                                      *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Tangent function.                             *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "TangentFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



TangentFunction::TangentFunction(void)
{
	this->Name = "Tangent";
}

TangentFunction::~TangentFunction(void)
{
	
}

Literal^ TangentFunction::InternalCall( ... array<Expression^>^ args)
{
	return gcnew Literal((float)Math::Tan(args[0]->Eval()->Value));
}



;
NAMESPACE_END
;


