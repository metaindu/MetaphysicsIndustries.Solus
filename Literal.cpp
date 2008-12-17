
/*****************************************************************************
 *                                                                           *
 *  Literal.cpp                                                              *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A number. No fancy moving parts.                                         *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "Literal.h"



;
NAMESPACE_START
;



Literal::Literal(void)
{
	this->value = 0;
}

Literal::Literal(float v)
{
	this->value = v;
}

Literal::~Literal(void)
{
	
}

float Literal::Value::get(void)
{
	return this->value;
}
void Literal::Value::set(float f)
{
	if (this->value != f)
	{
		this->value = f;

		this->OnValueChanged(gcnew EventArgs);
	}
}

void Literal::OnValueChanged(EventArgs^ e)
{
	this->ValueChanged(this, e);
}

Literal^ Literal::Eval(void)
{
	return this;
}



;
NAMESPACE_END
;

