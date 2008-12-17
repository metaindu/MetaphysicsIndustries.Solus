
/*****************************************************************************
 *                                                                           *
 *  FunctionCall.cpp                                                         *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  A function call, providing arguments to the function.                    *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "FunctionCall.h"
#include "Function.h"
#include "ExpressionCollection.h"



;
NAMESPACE_START
;



FunctionCall::FunctionCall(void)
{
	this->Init(nullptr, nullptr);
}

FunctionCall::FunctionCall(Solus::Function^ f, ... array<Expression^>^ a)
{
	if (!f) { throw gcnew ArgumentNullException("f" + __WCODESIG__); }

	this->Init(f, a);
}

FunctionCall::FunctionCall(Solus::Function^ f, ICollection<Expression^>^ a)
{
	if (!f) { throw gcnew ArgumentNullException("f" + __WCODESIG__); }
	if (!a) { throw gcnew ArgumentNullException("a" + __WCODESIG__); }

	array<Expression^>^	a2;
	a2 = gcnew array<Expression^>(a->Count);
	a->CopyTo(a2, 0);
	this->Init(f, a2);
}

void FunctionCall::Init(Solus::Function^ f, array<Expression^>^ a)
{
	this->function = f;
	this->arguments = gcnew ExpressionCollection;
	if (a)
	{
		this->arguments->AddRange(a->AsReadOnly(a));
	}
}

FunctionCall::~FunctionCall(void)
{
	this->arguments->Clear();
	delete this->arguments;
	this->arguments = nullptr;
	this->function = nullptr;
}

Solus::Function^ FunctionCall::Function::get(void)
{
	return this->function;
}
void FunctionCall::Function::set(Solus::Function^ f)
{
	if (this->function != f)
	{
		this->function = f;

		this->OnFunctionChanged(gcnew EventArgs);
	}
}

void FunctionCall::OnFunctionChanged(EventArgs^ e)
{
	this->FunctionChanged(this, e);
}

ExpressionCollection^ FunctionCall::Arguments::get(void)
{
	return this->arguments;
}

Literal^ FunctionCall::Call(void)
{
	return this->Function->Call(this->Arguments->ToArray());
}




;
NAMESPACE_END
;




