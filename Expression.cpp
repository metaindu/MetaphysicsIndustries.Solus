
/*****************************************************************************
 *                                                                           *
 *  Expression.cpp                                                           *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The basic unit of calculation and parse trees.                           *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "Expression.h"
#include "Literal.h"



;
NAMESPACE_START
;



Expression::Expression(void)
{
	
}

Expression::~Expression(void)
{
	
}


Literal^ Expression::Eval(void)
{
	return gcnew Literal(0);
}


;
NAMESPACE_END
;




