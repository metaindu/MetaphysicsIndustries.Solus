
/*****************************************************************************
 *                                                                           *
 *  CosineFunction.cpp                                                       *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The class for the built-in Cosine function.                              *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "CosineFunction.h"
#include "Literal.h"



;
NAMESPACE_START
;



CosineFunction::CosineFunction(void)
{
	this->Name = "Cosine";
}

CosineFunction::~CosineFunction(void)
{
	
}

Literal^ CosineFunction::InternalCall( ... array<Expression^>^ args)
{
	return gcnew Literal((float)Math::Cos(args[0]->Eval()->Value));
}


;
NAMESPACE_END
;



