
/*****************************************************************************
 *                                                                           *
 *  BinaryOperation.cpp                                                      *
 *  17 November 2006                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A specialized function which represents simple arithmetical operations   *
 *    on two arguments.                                                      *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "BinaryOperation.h"



;
NAMESPACE_START
;



BinaryOperation::BinaryOperation(void)
{
	
}

BinaryOperation::~BinaryOperation(void)
{
	
}

void BinaryOperation::CheckArguments( ... array<Expression^>^ args )
{
	array<Expression^>^ _args = args;

	List<Type^>^	types;
	int				i;
	int				j;
	//Type^			t;

	types = this->Types;

	if (types->Count != 2)
	{
		throw gcnew InvalidOperationException("Wrong number of types specified internally to BinarryOperation (given " + types->Count.ToString() + ", require 2) " + __WCODESIG__);
	}
	if (args->Length != 2)
	{
		throw gcnew InvalidOperationException("Wrong number of arguments given to " + this->DisplayName + " (given " + args->Length.ToString() + ", require 2) " + __WCODESIG__);
	}

	Type^	e;

	e = Expression::typeid;

	j = 2;
	for (i = 0; i < j; i++)
	{
		if (!e->IsAssignableFrom(types[i]))
		{
			//change this so it's only checked when the types are set by the Function's constructor
			throw gcnew InvalidOperationException("Required argument type " + i.ToString() + " is invalid (given \"" + types[i]->Name + "\", require \"" + e->Name + ") " + __WCODESIG__);
		}
		if (!types[i]->IsAssignableFrom(args[i]->GetType()))
		{
			throw gcnew InvalidOperationException("Argument " + ((i).ToString()) + " of wrong type (given \"" + args->GetType()->Name + "\", require \"" + types[i]->Name + ") " + __WCODESIG__);
		}
	}
}



;
NAMESPACE_END
;

