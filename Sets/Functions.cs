
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2022 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This library is free software; you can redistribute it and/or
 *  modify it under the terms of the GNU Lesser General Public
 *  License as published by the Free Software Foundation; either
 *  version 3 of the License, or (at your option) any later version.
 *
 *  This library is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 *  Lesser General Public License for more details.
 *
 *  You should have received a copy of the GNU Lesser General Public
 *  License along with this library; if not, write to the Free Software
 *  Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
 *  USA
 *
 */

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Sets
{
    public interface IFunctionType : ISet
    {
    }

    /// <summary>
    /// The set of functions of fixed arity, and whose parameters' types are
    /// supersets of the types given in `this.ParameterTypes`, and whose
    /// return type is a subset of `this.ReturnType`. A function can be a
    /// member of more than one function type, as the `ParameterTypes` and
    /// `ReturnType` do not require strict equality for a function to be
    /// considered a member.
    ///
    /// In brief, the `ParameterTypes` field answers the question "Can the
    /// function accept arguments that are members of these sets?". The
    /// `ReturnType` field answers the question "Will the output of the
    /// function be a member of this set?".
    ///
    /// Example: A function having the reals as both its domain and
    /// codomain is a member of `Functions.RealsToReals`.
    ///
    /// Example: A function having the integers as both its domain and
    /// codomain is considered a member of
    /// `Functions.Get(Integers, Integers)`.
    ///
    /// Example: A function having the integers as both its domain and
    /// codomain is also considered a member of
    /// `Functions.Get(Reals.Value, Integers)`.
    ///
    /// Example: A function having the reals as its domain and the real
    /// interval [0, +inf) as its codomain is considered a member of both
    /// `Functions.RealsToReals` and `Functions.Get([0, +inf), Reals.Values)`.
    ///
    /// Example: A function having the real interval [0, +inf) as its domain
    /// and the reals as its codomain is considered a member of
    /// `Functions.Get(Reals.Values, [0, +inf))` but NOT a member of
    /// `Functions.RealsToReals`.
    ///
    /// Example: A function having the reals as its domain and X as its
    /// codomain would be a member of both `Functions.Get(X, Reals.Value)` and
    /// `Functions.Get(X, Integers.Value)`, because the integers are a subset
    /// of the reals.
    ///
    /// Example: A function having the the integers as its domain and X as its
    /// codomain would be considered a member of
    /// `Functions.Get(X, Integers.Value)`, but not a member of
    /// `Functions.Get(X, Reals.Value)`.
    ///
    /// Example: A function having X as its domain and the reals as its
    /// codomain would be considered a member of
    /// `Functions.Get(Reals.Value, X)`, but not a member of
    /// `Functions.Get(Integers.Value, X)`.
    ///
    /// Example: A function having X as its domain and the integers as its
    /// codomain would be considered a member of both
    /// `Functions.Get(Reals.Value, X)` and
    /// `Functions.Get(Integers.Value, X)`.
    ///
    /// Counter-example: A function having the reals as both its domain and
    /// codomain would not be considered a member of
    /// `Functions.Get(Integers.Value, Integers.Value)`.
    /// </summary>
    public class Functions : IFunctionType
    {
        protected static Dictionary<ISet, PrefixTree<ISet, Functions>> sets2 =
            new Dictionary<ISet, PrefixTree<ISet, Functions>>();

        public static Functions Get(ISet returnType,
            params ISet[] parameterTypes)
        {
            if (!sets2.ContainsKey(returnType))
                sets2[returnType] =
                    new PrefixTree<ISet, Functions>(new Functions(returnType,
                        Array.Empty<ISet>()));
            if (parameterTypes.Length == 0)
                return sets2[returnType].Value;
            var x = sets2[returnType].Get(parameterTypes, 0,
                (ptypes, index) => Allocate(returnType, ptypes, index));
            return x;
        }

        public static Functions Allocate(ISet returnType,
            ISet[] parameterTypes, int index)
        {
            var ptypes2 = new ISet[index + 1];
            int i;
            for (i = 0; i <= index; i++)
                ptypes2[i] = parameterTypes[i];
            return new Functions(returnType, ptypes2);
        }

        public static readonly Functions RealsToReals =
            Get(Reals.Value, Reals.Value);

        private static int __id = 1;
        public readonly int ID;

        protected Functions(ISet returnType, ISet[] parameterTypes)
        {
            ID = __id++;
            ReturnType = returnType;
            // TODO: replace AsReadOnly with something that makes a copy. A
            //       simple wrapper doesn't guarantee that the underlying
            //       array can't change.
            ParameterTypes = Array.AsReadOnly(parameterTypes);
        }

        public readonly ISet ReturnType;
        public readonly ReadOnlyCollection<ISet> ParameterTypes;

        public static bool FunctionHasFixedTypes(Function f)
        {
            if (f is IAssociativeCommutativeOperation)
                // + * |
                return false;
            if (f is SizeFunction)
                return false;
            if (f is MaximumFiniteFunction ||
                f is MaximumFunction ||
                f is MinimumFiniteFunction ||
                f is MinimumFunction)
                return false;
            if (f is NegationOperation)
                return false;
            // TODO: other functions without fixed types
            // TODO: equality and non-equality comparisons
            // TODO: other comparisons
            // TODO: udf?
            // TODO: division with vectors and matrices
            // TODO: exponent with square matrices
            return true;
        }

        public bool Contains(IMathObject mo)
        {
            if (!mo.IsIsFunction(null))
                return false;
            var f = mo.ToFunction();
            if (!FunctionHasFixedTypes(f))
                return false;
            if (f.IsVariadic)
                return false;
            return IsSupersetOf(f.FunctionType);
        }

        public bool IsSupersetOf(ISet other) =>
            this == other ||
            other.IsSubsetOf(this);

        public bool IsSubsetOf(ISet other)
        {
            if (other == this ||
                other is AllFunctions ||
                other is MathObjects)
                return true;

            if (!(other is IFunctionType))
                return false;

            if (other is Functions ft)
            {
                if (ft.ReturnType != this.ReturnType)
                    return false;
                if (ft.ParameterTypes.Count != this.ParameterTypes.Count)
                    return false;
                int i;
                for (i = 0; i < ParameterTypes.Count; i++)
                    if (ft.ParameterTypes[i] != this.ParameterTypes[i])
                        return false;
                return true;
            }

            if (other is VariadicFunctions vf)
                return false;

            throw new NotImplementedException();
        }

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsBoolean(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => null;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool? IsSet(SolusEnvironment env) => true;
        public bool IsConcrete => true;

        private string _docString = null;
        public string DocString
        {
            get
            {
                if (_docString == null)
                    _docString = FormatDocString(ReturnType, ParameterTypes);

                return _docString;
            }
        }

        public static string FormatDocString(ISet returnType,
            ISet[] parameterTypes)
        {
            return FormatDocString(returnType,
                Array.AsReadOnly(parameterTypes));
        }

        public static string FormatDocString(ISet returnType,
            ReadOnlyCollection<ISet> parameterTypes)
        {
            var pieces = new List<string>();
            pieces.Add("The set of all functions taking ");
            int i;
            if (parameterTypes.Count < 1)
                pieces.Add("no arguments");
            for (i = 0; i < parameterTypes.Count; i++)
            {
                pieces.Add(parameterTypes[i].DisplayName);
                if (i < parameterTypes.Count - 2)
                {
                    pieces.Add(", ");
                }
                else if (i < parameterTypes.Count - 1)
                {
                    pieces.Add(" and ");
                }
            }

            pieces.Add(" and returning ");
            pieces.Add(returnType.DisplayName);
            return string.Join("", pieces);
        }

        public string DisplayName => "Function";

        public override string ToString()
        {
            var items = new string[ParameterTypes.Count + 1];
            items[0] = ReturnType.DisplayName;
            int i;
            for (i = 0; i < ParameterTypes.Count; i++)
                items[i + 1] = ParameterTypes[i].DisplayName;
            var itemList = string.Join(", ", items);
            return $"Functions({itemList})";
        }
    }

    /// <summary>
    /// Functions of varying arity, having all arguments of the same type
    /// </summary>
    /// <summary>
    /// The set of functions of varying arity starting from minimum, and whose
    /// parameters' types are all subsets of `this.ParameterType`, and whose
    /// return types are subsets of `this.ReturnType`. Each function in this
    /// set has a domain that is the union of all sets of tuples of all sizes
    /// greater than or equal to `this.MinimumNumberOfArguments`. For example,
    /// a function that is a member of `VariadicFunctions.RealsToReals` can
    /// accept zero or more real arguments, or zero or more arguments of a
    /// subset of the reals.
    ///
    /// Example: A variadic function having the reals as its parameter type
    /// and return type is a member of `VariadicFunctions.RealsToReals`.
    ///
    /// Counter-example: A variadic function having the integers as its
    /// parameter type and return type is not a member of
    /// `VariadicFunctions.RealsToReals`.
    ///
    /// Counter-example: A function that takes any number of arguments of any
    /// type is a member of `VariadicFunctions.Get(A, MathObjects, 0)`.
    ///
    /// Counter-example: A variadic function taking any alternating sequence
    /// of real and boolean arguments (f(R, B), f(R, B, R, B)...) is not a
    /// member of any instance of `VariadicFunctions` (not even of
    /// `VariadicFunctions.Get(A, MathObjects, 0)`), because it only takes
    /// even numbers of arguments.
    ///
    /// Counter-example: A variadic function taking at least 2 arguments is
    /// not a member of VariadicFunctions.Get(A, A, 1)
    ///
    /// Counter-example: A variadic function taking at least 2 arguments is
    /// not a member of VariadicFunctions.Get(A, A, 3)
    /// </summary>
    public class VariadicFunctions : IFunctionType
    {
        protected static Dictionary<STuple<ISet, ISet, int>, VariadicFunctions>
            sets = new Dictionary<STuple<ISet, ISet, int>,
                VariadicFunctions>();

        public static VariadicFunctions Get(ISet returnType,
            ISet parameterType, int? minimumNumberOfArguments=null)
        {
            if (!minimumNumberOfArguments.HasValue ||
                minimumNumberOfArguments < 0)
                minimumNumberOfArguments = 0;
            var stuple = new STuple<ISet, ISet, int>(returnType,
                parameterType, minimumNumberOfArguments.Value);
            if (!sets.ContainsKey(stuple))
                sets[stuple] = new VariadicFunctions(returnType,
                    parameterType, minimumNumberOfArguments.Value);
            return sets[stuple];
        }

        public static VariadicFunctions RealsToReals = Get(Reals.Value,
            Reals.Value);

        protected VariadicFunctions(ISet returnType, ISet parameterType,
            int minimumNumberOfArguments)
        {
            ReturnType = returnType;
            ParameterType = parameterType;
            MinimumNumberOfArguments = minimumNumberOfArguments;
        }

        public readonly ISet ReturnType;
        public readonly ISet ParameterType;
        public readonly int MinimumNumberOfArguments;

        public bool Contains(IMathObject mo)
        {
            if (!mo.IsIsFunction(null))
                return false;
            var f = mo.ToFunction();
            var ft = f.FunctionType;
            if (!(ft is VariadicFunctions vf))
                // What kind of variadic function type isn't an instance of
                // VariadicFunctions?
                throw new TypeException(null,
                    $"Unknown function type for variadic function: {ft}");
            return vf.IsSubsetOf(this);
        }

        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);

        public bool IsSubsetOf(ISet other) =>
            other == this ||
            other is AllFunctions ||
            other is MathObjects ||
            (other is VariadicFunctions vf &&
             vf.ReturnType == ReturnType &&
             vf.ParameterType == ParameterType &&
             vf.MinimumNumberOfArguments == MinimumNumberOfArguments);

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsBoolean(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => null;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool? IsSet(SolusEnvironment env) => true;
        public bool IsConcrete => true;

        private string _docString = null;

        public string DocString =>
            "The set of all variadic functions taking parameters of " +
            $"{ParameterType.DisplayName} and returning " +
            $"{ReturnType.DisplayName}";

        public string DisplayName => "Function";
    }

    /// <summary>
    /// All functions of any arity and type
    /// </summary>
    public class AllFunctions : IFunctionType
    {
        public static readonly AllFunctions Value = new AllFunctions();

        protected AllFunctions()
        {
        }

        public bool Contains(IMathObject mo) => mo.IsIsFunction(null);

        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other == this ||
            other is MathObjects;

        public bool? IsScalar(SolusEnvironment env) => false;
        public bool? IsBoolean(SolusEnvironment env) => false;
        public bool? IsVector(SolusEnvironment env) => false;
        public bool? IsMatrix(SolusEnvironment env) => false;
        public int? GetTensorRank(SolusEnvironment env) => null;
        public bool? IsString(SolusEnvironment env) => false;
        public int? GetDimension(SolusEnvironment env, int index) => null;
        public int[] GetDimensions(SolusEnvironment env) => null;
        public int? GetVectorLength(SolusEnvironment env) => null;
        public bool? IsInterval(SolusEnvironment env) => false;
        public bool? IsFunction(SolusEnvironment env) => false;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool? IsSet(SolusEnvironment env) => true;
        public bool IsConcrete => true;

        public string DocString => "The set of all functions";
        public string DisplayName => "Function";
    }
}
