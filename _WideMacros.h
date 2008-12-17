
/*****************************************************************************
 *                                                                           *
 *  WideMacros.h                                                             *
 *  14 April 2006                                                            *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries                                  *
 *                                                                           *
 *  Preprocessor macros for wchar_t versions of __FILE__ etc., and other     *
 *    helpful stuff.                                                         *
 *                                                                           *
 *****************************************************************************/


#pragma once


#define __WIDEN2(x) L ## x
#define __WIDEN(x) __WIDEN2(x)
#define __WIDEN0(x) __WIDEN(x)

#define __WFILE__ __WIDEN(__FILE__)
#define __WFUNCTION__ __WIDEN(__FUNCTION__)

#define __STRINGIZE2(x) #x
#define __STRINGIZE(x) __STRINGIZE2(x)
#define __CLINE__ __STRINGIZE(__LINE__)
#define __AFILESIG__ " - " __FILE__ ", line " __CLINE__
#define __WFILESIG__ L" - " __WFILE__ L", line " __WIDEN0(__CLINE__)

#define __ACODESIG__ __AFILESIG__ " : " __FUNCTION__
#define __WCODESIG__ __WFILESIG__ L" : " __WFUNCTION__



