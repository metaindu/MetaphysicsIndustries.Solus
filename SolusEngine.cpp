
/*****************************************************************************
 *                                                                           *
 *  SolusEngine.cpp                                                          *
 *  17 November 2006                                                         *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  The central core of processing in Solus. Does some rudimentary parsing   *
 *    and evaluation and stuff.                                              *
 *                                                                           *
 *****************************************************************************/



#include "stdafx.h"
#include "SolusEngine.h"
#include "FunctionCall.h"



;
NAMESPACE_START
;



SolusEngine::SolusEngine(void)
{
	
}

SolusEngine::~SolusEngine(void)
{
	
}

Expression^ SolusEngine::Parse(String^ s)
{
	array<String^>^	tokens;
	FunctionCall^	fc;
	int				i;
	int				j;

	fc = gcnew FunctionCall;

	tokens = s->Split(' ');

	j = tokens->Length;
	for (i = 1; i < j; i++)
	{
		float		fl;
		Expression^	e;
		fl = float::Parse(tokens[i]);
		e = gcnew Literal(fl);
		fc->Arguments->Add(e);
	}

	fc->Function = this->GetFunctionByName(tokens[0]);

	return fc;
}

Literal^ SolusEngine::Eval(Expression^ e)
{
	return e->Eval();
}

Function^ SolusEngine::GetFunctionByName(String^ s)
{
	//for each (Reflection::PropertyInfo^ pi in Function::typeid->GetProperties(Reflection::BindingFlags::Static))
	//{
	//	if (Function::typeid->InvokeMember(
	//}
	array<Function^>^	functions = {
										Function::Cosine,
										Function::Sine,
										Function::Tangent,
										Function::Cotangent,
										Function::Secant,
										Function::Cosecant,
										Function::Arccosine,
										Function::Arcsine,
										Function::Arctangent,
										Function::Arccotangent,
										Function::Arcsecant,
										Function::Arccosecant,
										Function::Floor,
										Function::Ceiling,
									};

	for each (Function^ f in functions)
	{
		if (s == f->Name)
		{
			return f;
		}
	}

	return nullptr;
}



;
NAMESPACE_END
;



