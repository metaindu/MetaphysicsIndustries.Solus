
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
    public class Functions : ISet
    {
        protected static Dictionary<ISet, Dictionary<ISet[], Functions>> sets =
            new Dictionary<ISet, Dictionary<ISet[], Functions>>();
        public static Functions Get(ISet returnType,
            params ISet[] parameterTypes)
        {
            if (!sets.ContainsKey(returnType))
                sets[returnType] = new Dictionary<ISet[], Functions>();
            if (!sets[returnType].ContainsKey(parameterTypes))
                sets[returnType][parameterTypes] =
                    new Functions(returnType, parameterTypes);
            return sets[returnType][parameterTypes];
        }

        public static Functions RealsToReals = Get(Reals.Value, Reals.Value);

        protected Functions(ISet returnType, ISet[] parameterTypes)
        {
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

    public class AllFunctions : ISet
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
