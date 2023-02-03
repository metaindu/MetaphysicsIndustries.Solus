
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

using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class AdditionOperation : Function
    {
        public static readonly AdditionOperation
            Value = new AdditionOperation();

        protected AdditionOperation()
            : base(new Parameter("x", Reals.Value), "+")
        {
        }

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes)
        {
            // TODO: tensor arithmetic
            // TODO: string concatenation
            return Reals.Value;
        }

        public override IFunctionType FunctionType =>
            AdditionFunctionType.Value;

        public class AdditionFunctionType : IFunctionType
        {
            public static readonly AdditionFunctionType Value =
                new AdditionFunctionType();

            public bool Contains(IMathObject mo)
            {
                // TODO: some other functions could theoretically be in this
                // set
                return ReferenceEquals(mo, AdditionOperation.Value);
            }

            public bool IsSupersetOf(ISet other) =>
                other == this ||
                (other is VariadicFunctions vf &&
                 ((vf.ReturnType == Reals.Value &&
                   vf.ParameterType == Reals.Value &&
                   vf.MinimumNumberOfArguments == 2) ||
                  (vf.ReturnType is Vectors &&
                   vf.ParameterType is Vectors &&
                   vf.ParameterType == vf.ReturnType &&
                   vf.MinimumNumberOfArguments == 2) ||
                  (vf.ReturnType is Matrices &&
                   vf.ParameterType is Matrices &&
                   vf.ParameterType == vf.ReturnType &&
                   vf.MinimumNumberOfArguments == 2))) ||
                other.IsSubsetOf(this);

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
                "The union of the set of all variadic functions of " +
                "minimum two real arguments and returning reals, with the " +
                "set of all variadic functions of minimum two vector " +
                "arguments of any dimension and returning a vector of that " +
                "same dimension, with the set of all variadic function of " +
                "minimum two matrix arguments of any dimensions and " +
                "returning a matrix of that same dimension";

            public string DisplayName => "Addition Function";
        }

        public override bool IsCommutative => true;
        public override bool IsAssociative => true;
    }
}
