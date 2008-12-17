
/*****************************************************************************
 *                                                                           *
 *  Function.cpp                                                             *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A mathematical function that can be evaluated with a set of              *
 *    parameters. This serves as a base class that is inherited by other,    *
 *    specialized classes, each representing a different mathematical        *
 *    function (e.g. SineFunction : Function). This base class performs      *
 *    all necessary type checking on given arguments based on information    *
 *    specified by the derived class.                                        *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "Function.h"
#include "Literal.h"
#include "CosineFunction.h"
#include "SineFunction.h"
#include "TangentFunction.h"
#include "CotangentFunction.h"
#include "SecantFunction.h"
#include "CosecantFunction.h"
#include "ArccosineFunction.h"
#include "ArcsineFunction.h"
#include "ArctangentFunction.h"
#include "ArccotangentFunction.h"
#include "ArcsecantFunction.h"
#include "ArccosecantFunction.h"
#include "FloorFunction.h"
#include "CeilingFunction.h"




;
NAMESPACE_START
;



Function::Function(void)
{
	this->internaltypes = gcnew List<Type^>;
	this->internaltypes->Add(Expression::typeid);
}

Function::~Function(void)
{
	
}

Function^ Function::Cosine::get(void)
{
	return Function::cosine;
}
Function^ Function::Sine::get(void)
{
	return Function::sine;
}
Function^ Function::Tangent::get(void)
{
	return Function::tangent;
}
Function^ Function::Cotangent::get(void)
{
	return Function::cotangent;
}
Function^ Function::Secant::get(void)
{
	return Function::secant;
}
Function^ Function::Cosecant::get(void)
{
	return Function::cosecant;
}
Function^ Function::Arccosine::get(void)
{
	return Function::arccosine;
}
Function^ Function::Arcsine::get(void)
{
	return Function::arcsine;
}
Function^ Function::Arctangent::get(void)
{
	return Function::arctangent;
}
Function^ Function::Arccotangent::get(void)
{
	return Function::arccotangent;
}
Function^ Function::Arcsecant::get(void)
{
	return Function::arcsecant;
}
Function^ Function::Arccosecant::get(void)
{
	return Function::arccosecant;
}
Function^ Function::Floor::get(void)
{
	return Function::floor;
}
Function^ Function::Ceiling::get(void)
{
	return Function::ceiling;
}

static Function::Function(void)
{
	Function::cosine = gcnew CosineFunction;
	Function::sine = gcnew SineFunction;
	Function::tangent = gcnew TangentFunction;
	Function::cotangent = gcnew CotangentFunction;
	Function::secant = gcnew SecantFunction;
	Function::cosecant = gcnew CosecantFunction;
	Function::arccosine = gcnew ArccosineFunction;
	Function::arcsine = gcnew ArcsineFunction;
	Function::arctangent = gcnew ArctangentFunction;
	Function::arccotangent = gcnew ArccotangentFunction;
	Function::arcsecant = gcnew ArcsecantFunction;
	Function::arccosecant = gcnew ArccosecantFunction;
	Function::floor = gcnew FloorFunction;
	Function::ceiling = gcnew CeilingFunction;
}

Literal^ Function::Call( ... array<Expression^>^ args)
{
	this->CheckArguments(args);

	return this->InternalCall(args);
}

//Literal^ Function::Call(ICollection<Expression^>^ )
//{
//	return gcnew Literal(0);
//}

List<Type^>^ Function::Types::get(void)
{
	return this->InternalTypes;
}
List<Type^>^ Function::InternalTypes::get(void)
{
	return this->internaltypes;
}

String^ Function::Name::get(void)
{
	return this->name;
}
void Function::Name::set(String^ s)
{
	if (this->name != s)
	{
		this->name = s;
	}
}

String^ Function::DisplayName::get(void)
{
	return this->Name;
}

void Function::CheckArguments( ... array<Expression^>^ args)
{
	array<Expression^>^ _args = args;

	List<Type^>^	types;
	int				i;
	int				j;
	//Type^			t;

	types = this->Types;

	if (args->Length != types->Count)
	{
		throw gcnew InvalidOperationException("Wrong number of arguments given to " + this->DisplayName + " (given " + args->Length.ToString() + ", require " + this->Types->Count.ToString() + ") " + __WCODESIG__);
	}

	Type^	e;

	e = Expression::typeid;

	j = args->Length;
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


