
/*****************************************************************************
 *                                                                           *
 *  Function.cs                                                              *
 *  30 January 2008                                                          *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2008 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  A mathematical function that can be evaluated with a set of              *
 *    parameters. This serves as a base class that is inherited by other,    *
 *    specialized classes, each representing a different mathematical        *
 *    function (e.g. "SineFunction : Function"). This base class performs    *
 *    all necessary type checking on given arguments based on information    *
 *    specified by the derived class.                                        *
 *                                                                           *
 *  This file list several static proerties for accessing functions by       *
 *    name.                                                                  *
 *                                                                           *
 *****************************************************************************/


using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public abstract partial class Function
    {

        private static ArccosecantFunction _arccosecant = new ArccosecantFunction();
        public static Function Arccosecant
        {
            get { return _arccosecant; }
        }
        private static ArccosineFunction _arccosine = new ArccosineFunction();
        public static Function Arccosine
        {
            get { return _arccosine; }
        }
        private static ArccotangentFunction _arccotangent = new ArccotangentFunction();
        public static Function Arccotangent
        {
            get { return _arccotangent; }
        }
        private static ArcsecantFunction _arcsecant = new ArcsecantFunction();
        public static Function Arcsecant
        {
            get { return _arcsecant; }
        }
        private static ArcsineFunction _arcsine = new ArcsineFunction();
        public static Function Arcsine
        {
            get { return _arcsine; }
        }
        private static ArctangentFunction _arctangent = new ArctangentFunction();
        public static Function Arctangent
        {
            get { return _arctangent; }
        }
        private static CeilingFunction _ceiling = new CeilingFunction();
        public static Function Ceiling
        {
            get { return _ceiling; }
        }
        private static CosecantFunction _cosecant = new CosecantFunction();
        public static Function Cosecant
        {
            get { return _cosecant; }
        }
        private static CosineFunction _cosine = new CosineFunction();
        public static Function Cosine
        {
            get { return _cosine; }
        }
        private static CotangentFunction _cotangent = new CotangentFunction();
        public static Function Cotangent
        {
            get { return _cotangent; }
        }
        private static FloorFunction _floor = new FloorFunction();
        public static Function Floor
        {
            get { return _floor; }
        }
        private static SecantFunction _secant = new SecantFunction();
        public static Function Secant
        {
            get { return _secant; }
        }
        private static SineFunction _sine = new SineFunction();
        public static Function Sine
        {
            get { return _sine; }
        }
        private static TangentFunction _tangent = new TangentFunction();
        public static Function Tangent
        {
            get { return _tangent; }
        }
        private static NaturalLogarithmFunction _naturalLogarithm = new NaturalLogarithmFunction();
        public static NaturalLogarithmFunction NaturalLogarithm
        {
            get { return _naturalLogarithm; }
        }
        private static UnitStepFunction _unitStep = new UnitStepFunction();
        public static UnitStepFunction UnitStep
        {
            get { return _unitStep; }
        }

        private static Log10Function _log10 = new Log10Function();
        public static Log10Function Log10
        {
            get { return _log10; }
        }
        private static Log2Function _log2 = new Log2Function();
        public static Log2Function Log2
        {
            get { return _log2; }
        }
        private static LogarithmFunction _logarithm = new LogarithmFunction();
        public static LogarithmFunction Logarithm
        {
            get { return _logarithm; }
        }
        private static AbsoluteValueFunction _absoluteValue = new AbsoluteValueFunction();
        public static AbsoluteValueFunction AbsoluteValue
        {
            get { return _absoluteValue; }
        }
        private static Arctangent2Function _arctangent2 = new Arctangent2Function();
        public static Arctangent2Function Arctangent2
        {
            get { return _arctangent2; }
        }

    }
}