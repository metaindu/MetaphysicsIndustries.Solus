
/*****************************************************************************
 *                                                                           *
 *  FunctionCall.h                                                           *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A function call, providing arguments to the function.                    *
 *                                                                           *
 *****************************************************************************/



#pragma once

#include "Expression.h"
#include "ExpressionCollection.h"

using namespace System;



namespace MetaphysicsIndustries { namespace Solus { 
;



ref class ExpressionCollection;
ref class Function;
ref class Literal;
public ref class FunctionCall : Expression
{

public:

	FunctionCall(void);
	FunctionCall(Solus::Function^, ... array<Expression^>^);
	FunctionCall(Solus::Function^, ICollection<Expression^>^);
	virtual ~FunctionCall(void);

	virtual Literal^ Call(void) = Expression::Eval;

	property Solus::Function^ Function
	{
		Solus::Function^ get(void);
		void set(Solus::Function^);
	}

	virtual property ExpressionCollection^ Arguments
	{
		ExpressionCollection^ get(void);
	}

	event EventHandler^	FunctionChanged;

protected:

	void Init(Solus::Function^, array<Expression^>^);

	virtual void OnFunctionChanged(EventArgs^);

	Solus::Function^		function;
	ExpressionCollection^	arguments;

private:

};



} } 


