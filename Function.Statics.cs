
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
        static Function()
        {
            RegisterFunctions();
        }


        private static Dictionary<Type, Function> _registeredFunctions = new Dictionary<Type, Function>();
        protected static void RegisterFunctionType(Function function)
        {
            if (function == null) throw new ArgumentNullException("function");

            _registeredFunctions[function.GetType()] = function;
        }
        protected static T GetRegisteredFunction<T>()
            where T : Function
        {
            Type type = typeof(T);
            if (!_registeredFunctions.ContainsKey(type)) return null;
            return (T)_registeredFunctions[type];
        }

        public static ArccosecantFunction Arccosecant
        {
            get { return GetRegisteredFunction<ArccosecantFunction>(); }
        }
        public static ArccosineFunction Arccosine
        {
            get { return GetRegisteredFunction<ArccosineFunction>(); }
        }
        public static ArccotangentFunction Arccotangent
        {
            get { return GetRegisteredFunction<ArccotangentFunction>(); }
        }
        public static ArcsecantFunction Arcsecant
        {
            get { return GetRegisteredFunction<ArcsecantFunction>(); }
        }
        public static ArcsineFunction Arcsine
        {
            get { return GetRegisteredFunction<ArcsineFunction>(); }
        }
        public static ArctangentFunction Arctangent
        {
            get { return GetRegisteredFunction<ArctangentFunction>(); }
        }
        public static CeilingFunction Ceiling
        {
            get { return GetRegisteredFunction<CeilingFunction>(); }
        }
        public static CosecantFunction Cosecant
        {
            get { return GetRegisteredFunction<CosecantFunction>(); }
        }
        public static CosineFunction Cosine
        {
            get { return GetRegisteredFunction<CosineFunction>(); }
        }
        public static CotangentFunction Cotangent
        {
            get { return GetRegisteredFunction<CotangentFunction>(); }
        }
        public static FloorFunction Floor
        {
            get { return GetRegisteredFunction<FloorFunction>(); }
        }
        public static SecantFunction Secant
        {
            get { return GetRegisteredFunction<SecantFunction>(); }
        }
        public static SineFunction Sine
        {
            get { return GetRegisteredFunction<SineFunction>(); }
        }
        public static TangentFunction Tangent
        {
            get { return GetRegisteredFunction<TangentFunction>(); }
        }
        public static NaturalLogarithmFunction NaturalLogarithm
        {
            get { return GetRegisteredFunction<NaturalLogarithmFunction>(); }
        }
        public static UnitStepFunction UnitStep
        {
            get { return GetRegisteredFunction<UnitStepFunction>(); }
        }
        public static Log10Function Log10
        {
            get { return GetRegisteredFunction<Log10Function>(); }
        }
        public static Log2Function Log2
        {
            get { return GetRegisteredFunction<Log2Function>(); }
        }
        public static LogarithmFunction Logarithm
        {
            get { return GetRegisteredFunction<LogarithmFunction>(); }
        }
        public static AbsoluteValueFunction AbsoluteValue
        {
            get { return GetRegisteredFunction<AbsoluteValueFunction>(); }
        }
        public static Arctangent2Function Arctangent2
        {
            get { return GetRegisteredFunction<Arctangent2Function>(); }
        }
        public static IfFunction If
        {
            get { return GetRegisteredFunction<IfFunction>(); }
        }
        public static DistFunction Dist
        {
            get { return GetRegisteredFunction<DistFunction>(); }
        }
        public static DistSqFunction DistSq
        {
            get { return GetRegisteredFunction<DistSqFunction>(); }
        }

        public static readonly NegationOperation Negation = new NegationOperation();
    }
}