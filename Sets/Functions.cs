
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
using MetaphysicsIndustries.Solus.Functions;

namespace MetaphysicsIndustries.Solus.Sets
{
    public interface IFunctionType : ISet
    {
    }

    /// <summary>
    /// Functions of fixed arity and defined types
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
            ParameterTypes = Array.AsReadOnly(parameterTypes);
        }

        public readonly ISet ReturnType;
        public readonly ReadOnlyCollection<ISet> ParameterTypes;

        public static bool FunctionHasFixedTypes(Function f)
        {
            if (f is AssociativeCommutativeOperation)
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
            // TODO: is subset/superset, rather than "!="
            // what's the best way to do it? exact type match or
            // f.GetResultType is subset of ReturnType?
            if (f.GetResultType(null, null) != ReturnType)
                return false;
            if (f.Parameters.Count != ParameterTypes.Count)
                return false;
            for (var i = 0; i < ParameterTypes.Count; i++)
            {
                // TODO: is subset/superset, rather than "!="
                if (f.Parameters[i].Type != ParameterTypes[i])
                    return false;
            }

            return true;
        }

        public bool IsSupersetOf(ISet other) => this == other;
        public bool IsSubsetOf(ISet other) =>
            other == this ||
            other is AllFunctions ||
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
    }

    /// <summary>
    /// Functions of varying arity, having all arguments of the same type
    /// </summary>
    public class VariadicFunctions : IFunctionType
    {
        protected static Dictionary<STuple<ISet, ISet>, VariadicFunctions>
            sets = new Dictionary<STuple<ISet, ISet>, VariadicFunctions>();
        public static VariadicFunctions Get(ISet returnType,
            ISet parameterType)
        {
            var stuple = new STuple<ISet, ISet>(returnType, parameterType);
            if (!sets.ContainsKey(stuple))
                sets[stuple] = new VariadicFunctions(returnType,
                    parameterType);
            return sets[stuple];
        }

        public static VariadicFunctions RealsToReals = Get(Reals.Value,
            Reals.Value);

        protected VariadicFunctions(ISet returnType, ISet parameterType)
        {
            ReturnType = returnType;
            ParameterType = parameterType;
        }

        public readonly ISet ReturnType;
        public readonly ISet ParameterType;

        public bool Contains(IMathObject mo)
        {
            if (!mo.IsIsFunction(null))
                return false;
            var f = mo.ToFunction();
            if (!f.IsVariadic)
                return false;
            if (f.GetResultType(null, null) != ReturnType)
                return false;
            if (f.Parameters.Count != 1)
                // this should not even be possible, since the function is
                // marked variadic
                return false;
            if (f.Parameters[0].Type != ParameterType)
                return false;
            return true;
        }

        public bool IsSupersetOf(ISet other) => this == other;
        public bool IsSubsetOf(ISet other) =>
            other == this ||
            other is AllFunctions ||
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

        private string _docString = null;

        public string DocString =>
            "The set of all variadic functions taking parameters of " +
            $"{ParameterType.DisplayName} and returning " +
            $"{ReturnType.DisplayName}";

        public string DisplayName => "Function";
    }

    /// <summary>
    /// All functions taking a (positive) fixed or varying number of
    /// arguments, each argument being a vector, all arguments having the
    /// same dimension, and returning a vector of that dimension.
    ///
    /// i.e. union( VF(V(n), V(n)) for all n in NaturalNumbers )
    /// </summary>
    public class AllVectorFunctions : IFunctionType
    {
        public static readonly AllVectorFunctions Value =
            new AllVectorFunctions();

        public bool Contains(IMathObject mo)
        {
            if (!mo.IsIsFunction(null))
                return false;
            var f = mo.ToFunction();
            if (f.Parameters.Count < 1)
                // nullary functions not included
                return false;
            if (!f.Parameters[0].Type.IsSubsetOf(AllVectors.Value))
                return false;
            var t = f.Parameters[0].Type;
            if (!(t is Vectors))
                return false;
            if (f.GetResultType(null, null) != t)
                return false;
            int i;
            // this will also work for variadic functions
            for (i = 0; i < f.Parameters.Count; i++)
                if (f.Parameters[i].Type != t)
                    return false;
            return true;
        }

        public bool IsSupersetOf(ISet other) => this == other;
        public bool IsSubsetOf(ISet other) =>
            // TODO: VariadicFunctions ?
            other == this ||
            other is AllFunctions ||
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

        private string _docString = null;

        public string DocString =>
            "The set of all functions taking any number of parameters of a " +
            "vector type and returning that same vector type";

        public string DisplayName => "VectorFunction";
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
            other is Functions ||
            other is AllFunctions;

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
