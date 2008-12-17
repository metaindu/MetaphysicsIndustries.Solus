
/*****************************************************************************
 *                                                                           *
 *  Function.h                                                               *
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



#pragma once

#include "Expression.h"
#include "Literal.h"

using namespace System;
using namespace System::Collections::Generic;



namespace MetaphysicsIndustries { namespace Solus { 
;



ref class Expression;
ref class Literal;
public ref class Function abstract
{

public:

	Function(void);
	virtual ~Function(void);

	Literal^ Call(... array<Expression^>^);
	//Literal^ Call(ICollection<Expression^>^);

	//this should be read-only
	property List<Type^>^ Types
	{
		List<Type^>^ get(void);
	}

	property String^ Name
	{
		String^ get(void);
	protected:
		void set(String^);
	}
	virtual property String^ DisplayName
	{
		String^ get(void);
	}

	static property Function^ Cosine
	{
		Function^ get(void);
	}
	static property Function^ Sine
	{
		Function^ get(void);
	}
	static property Function^ Tangent
	{
		Function^ get(void);
	}
	static property Function^ Cotangent
	{
		Function^ get(void);
	}
	static property Function^ Secant
	{
		Function^ get(void);
	}
	static property Function^ Cosecant
	{
		Function^ get(void);
	}
	static property Function^ Arccosine
	{
		Function^ get(void);
	}
	static property Function^ Arcsine
	{
		Function^ get(void);
	}
	static property Function^ Arctangent
	{
		Function^ get(void);
	}
	static property Function^ Arccotangent
	{
		Function^ get(void);
	}
	static property Function^ Arcsecant
	{
		Function^ get(void);
	}
	static property Function^ Arccosecant
	{
		Function^ get(void);
	}
	static property Function^ Floor
	{
		Function^ get(void);
	}
	static property Function^ Ceiling
	{
		Function^ get(void);
	}

protected:

	virtual Literal^ InternalCall( ... array<Expression^>^ ) abstract;
	virtual void CheckArguments( ... array<Expression^>^ );

	property List<Type^>^ InternalTypes
	{
		List<Type^>^ get(void);
	}

private:

	static Function(void);

	static Function^	cosine;
	static Function^	sine;
	static Function^	tangent;
	static Function^	cotangent;
	static Function^	secant;
	static Function^	cosecant;
	static Function^	arccosine;
	static Function^	arcsine;
	static Function^	arctangent;
	static Function^	arccotangent;
	static Function^	arcsecant;
	static Function^	arccosecant;
	static Function^	floor;
	static Function^	ceiling;

	List<Type^>^		internaltypes;
	String^				name;


};



} } 

