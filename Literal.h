
/*****************************************************************************
 *                                                                           *
 *  Literal.h                                                                *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A number. No fancy moving parts.                                         *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Expression.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Solus { 
;



public ref class Literal : Expression
{

public:

	Literal(void);
	Literal(float);
	virtual ~Literal(void);

	virtual Literal^ Eval(void) override;

	virtual property float Value
	{
		float get(void);
		void set(float);
	}

	event EventHandler^	ValueChanged;

protected:

	virtual void OnValueChanged(EventArgs^);

	float	value;

private:

};



} } 


