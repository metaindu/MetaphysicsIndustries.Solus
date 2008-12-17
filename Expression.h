
/*****************************************************************************
 *                                                                           *
 *  Expression.h                                                             *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The basic unit of calculation and parse trees.                           *
 *                                                                           *
 *****************************************************************************/



#pragma once

using namespace System;



namespace MetaphysicsIndustries { namespace Solus { 
;



ref class Literal;
public ref class Expression
{

public:

	Expression(void);
	virtual ~Expression(void);

	virtual Literal^ Eval(void);

protected:

private:

};



} } 

