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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;
using MetaphysicsIndustries.Solus.Sets;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class MultiplicationOperation : Function, IAssociativeCommutativeOperation
    {
        public static readonly MultiplicationOperation Value = new MultiplicationOperation();

        private MultiplicationOperation()
        {
        }

        public override string Name => "*";

        public override bool IsVariadic => true;
        public override IReadOnlyList<Parameter> Parameters { get; } = new List<Parameter>();

        public OperationPrecedence Precedence => OperationPrecedence.Multiplication;
        public override bool IsCommutative => true;
        public override bool IsAssociative => true;
        public bool HasIdentityValue => true;
        public float IdentityValue => 1;
        public bool Collapses => true;
        public float CollapseValue => 0;
        public bool Culls => true;
        public float CullValue => 1;


        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes)
        {
            var argTypes1 = new List<ISet>(argTypes);
            CheckArguments(env, argTypes1);

            var hasMatrix = false;
            var hasVector = false;
            var hasScalar = false;
            var nonScalars = new List<ISet>();
            ISet scalarType = null;
            foreach (var argType in argTypes1)
            {
                if (argType.IsSubsetOf(Reals.Value))
                {
                    hasScalar = true;
                    scalarType = argType;
                }
                else if (argType.IsSubsetOf(AllVectors.Value))
                {
                    hasVector = true;
                    nonScalars.Add(argType);
                }
                else if (argType.IsSubsetOf(AllMatrices.Value))
                {
                    hasMatrix = true;
                    nonScalars.Add(argType);
                }
            }

            // ss+ -> scalar
            // s+m+s -> matrix with scalar
            // s+vs+ -> vector with scalar
            // s*vs*vs* -> error, unless row*column
            // s*vs*v(s*v)+s* -> error
            // (s*m)+s* -> matmult, possibly w/scalar
            // (s*m)+s*vs* s*v(s*m)+s* -> matvec, possibly w/scalar
            //
            // generally, all scalars can be shifted to the left
            // if there's more than one vector anywhere, probably an error
            // if there are more than two vectors, error
            // vectors and matrices must remain in order
            if (hasScalar && !hasMatrix && !hasVector)
                // simple scalar multiplication
                return scalarType;

            if (hasMatrix)
            {
                // check for more than one vector
                var foundVector = false;
                int vectorIndex = -1;
                for (var i = 0; i < nonScalars.Count; i++)
                {
                    var argType = nonScalars[i];
                    if (argType.IsSubsetOf(AllVectors.Value))
                    {
                        if (foundVector)
                            throw new TypeException("More than one vector");
                        foundVector = true;
                        vectorIndex = i;
                    }
                }

                if (foundVector)
                {
                    if (vectorIndex == 0)
                    {
                        // vm+
                        // vector is 1xM row vector
                        // first matrix is MxN
                        // last matrix is LxK
                        // result is 1xK vector
                        var last = ((Vectors)nonScalars[0]).Dimension;
                        for (var i = 1; i < nonScalars.Count; i++)
                            last = ((Matrices)nonScalars[i]).ColumnCount;
                        return Vectors.Get(last); // Matrices.Get(1, last) ?
                    }

                    if (vectorIndex == nonScalars.Count - 1)
                    {
                        // m+v
                        // first matrix is LxK
                        // last matrix is MxN
                        // vector is Nx1 column vector
                        // result is Lx1 vector
                        var mt0 = (Matrices)nonScalars[0];
                        return Vectors.Get(mt0.RowCount);
                    }

                    // m+vm+
                    // last of the first
                }
                else
                {
                    // m+
                    // first matrix is LxK
                    // last matrix is MxN
                    // result is LxN matrix
                    int i;
                    var mt0 = (Matrices)nonScalars[0];
                    var last = mt0;
                    for (i = 1; i < nonScalars.Count; i++)
                        last = (Matrices)nonScalars[i];

                    return Matrices.Get(mt0.RowCount, last.ColumnCount);
                }
            }
            else if (hasVector)
                // one vector with any number of scalars
                // s+vs*
                // vs+
                return nonScalars[0];

            throw new TypeException("???");
        }

        public static void CheckArguments(SolusEnvironment env, IEnumerable<ISet> argTypes)
        {
            var argTypes1 = new List<ISet>(argTypes);
            if (argTypes1.Count < 2)
            {
                var name = MultiplicationOperation.Value.DisplayName;
                throw new TypeException(
                    null,
                    $"Wrong number of arguments given to " +
                    $"{name} (expected at least " +
                    "2 but " +
                    $"got {argTypes1.Count})");
            }

            bool hasMatrix = false;
            bool hasVector = false;
            bool hasScalar = false;
            var nonScalars = new List<ISet>();
            foreach (var argType in argTypes1)
            {
                if (argType.IsSubsetOf(Reals.Value))
                {
                    hasScalar = true;
                }
                else if (argType.IsSubsetOf(AllVectors.Value))
                {
                    hasVector = true;
                    nonScalars.Add(argType);
                }
                else if (argType.IsSubsetOf(AllMatrices.Value))
                {
                    hasMatrix = true;
                    nonScalars.Add(argType);
                }
                else
                    throw new NotImplementedException(
                        $"Argument type \"{argType}\"");
            }

            // ss+ -> scalar
            // s+m+s -> matrix with scalar
            // s+vs+ -> vector with scalar
            // s*vs*vs* -> error, unless row*column
            // s*vs*v(s*v)+s* -> error
            // (s*m)+s* -> matmult, possibly w/scalar
            // (s*m)+s*vs* s*v(s*m)+s* -> matvec, possibly w/scalar
            //
            // generally, all scalars can be shifted to the left
            // if there's more than one vector anywhere, probably an error
            // if there are more than two vectors, error
            // vectors and matrices must remain in order
            if (hasScalar && !hasMatrix && !hasVector)
                // simple scalar multiplication
                return;

            if (hasMatrix)
            {
                // check for more than one vector
                var foundVector = false;
                int vectorIndex = -1;
                for (var i = 0; i < nonScalars.Count; i++)
                {
                    var argType = nonScalars[i];
                    if (argType.IsSubsetOf(AllVectors.Value))
                    {
                        if (foundVector)
                            throw new TypeException("More than one vector");
                        foundVector = true;
                        vectorIndex = i;
                    }
                }

                if (foundVector)
                {
                    if (vectorIndex == 0)
                    {
                        // vm+
                        // vector is 1xM row vector
                        // first matrix is MxN
                        // last matrix is LxK
                        // result is 1xK vector
                        int i;
                        var vdim = ((Vectors)nonScalars[0]).Dimension;
                        int last = vdim;
                        for (i = 1; i < nonScalars.Count; i++)
                        {
                            var mt = (Matrices)nonScalars[i];
                            if (mt.RowCount != last)
                                throw new TypeException(
                                    $"Matrix dimension does not match, " +
                                    $"{mt.RowCount} vs {last}");
                            last = mt.ColumnCount;
                        }

                        return ;
                    }

                    if (vectorIndex == nonScalars.Count - 1)
                    {
                        // m+v
                        // first matrix is LxK
                        // last matrix is MxN
                        // vector is Nx1 column vector
                        // result is Lx1 vector
                        int i;
                        var vdim = ((Vectors)nonScalars[nonScalars.Count-1]).Dimension;
                        var mt0 = (Matrices)nonScalars[0];
                        var last = mt0;
                        for (i = 1; i < nonScalars.Count - 1; i++)
                        {
                            var mt = (Matrices)nonScalars[i];
                            if (mt.RowCount != last.ColumnCount)
                                throw new TypeException(
                                    $"Matrix dimension does not match, " +
                                    $"{mt.RowCount} vs {last.ColumnCount}");
                            last = mt;
                        }

                        if (vdim != last.ColumnCount)
                            throw new TypeException(
                                $"Vector dimension does not match, " +
                                $"{vdim} vs {last.ColumnCount}");
                    }
                    // m+vm+
                    // last of the first
                }
                else
                {
                    // m+
                    // first matrix is LxK
                    // last matrix is MxN
                    // result is LxN matrix
                    int i;
                    var mt0 = (Matrices)nonScalars[0];
                    var last = mt0;
                    for (i = 1; i < nonScalars.Count; i++)
                    {
                        var mt = (Matrices)nonScalars[i];
                        if (mt.RowCount != last.ColumnCount)
                            throw new TypeException(
                                $"Matrix dimension does not match, " +
                                $"{mt.RowCount} vs {last.ColumnCount}");
                        last = mt;
                    }
                }
            }
            else if (hasVector)
                if (nonScalars.Count > 1)
                    // more than one vector
                    // s*v(s*v)+s*
                    throw new TypeException();
            // one vector with any number of scalars
            // s+vs*
            // vs+
        }

        public override IFunctionType FunctionType =>
            MultiplicationFunctionType.Value;

        public class MultiplicationFunctionType : IFunctionType
        {
            public static readonly MultiplicationFunctionType Value =
                new MultiplicationFunctionType();

            public bool Contains(IMathObject mo)
            {
                // TODO: some other functions could theoretically be in this
                // set
                return ReferenceEquals(mo, MultiplicationOperation.Value);
            }

            public bool IsSupersetOf(ISet other) =>
                throw new NotImplementedException();
            // other == this ||
            // (other is VariadicFunctions vf &&
            //  ((vf.ReturnType == Reals.Value &&
            //    vf.ParameterType == Reals.Value &&
            //    vf.MinimumNumberOfArguments == 2) ||
            //   (vf.ReturnType is Vectors &&
            //    vf.ParameterType is Vectors &&
            //    vf.ParameterType == vf.ReturnType &&
            //    vf.MinimumNumberOfArguments == 2) ||
            //   (vf.ReturnType is Matrices &&
            //    vf.ParameterType is Matrices &&
            //    vf.ParameterType == vf.ReturnType &&
            //    vf.MinimumNumberOfArguments == 2))) ||
            // other.IsSubsetOf(this);

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

            public string DocString => "TODO";
                // "The union of the set of all variadic functions of " +
                // "minimum two real arguments and returning reals, with the " +
                // "set of all variadic functions of minimum two vector " +
                // "arguments of any dimension and returning a vector of that " +
                // "same dimension, with the set of all variadic function of " +
                // "minimum two matrix arguments of any dimensions and " +
                // "returning a matrix of that same dimension";

            public string DisplayName => "Multiplication Function";
        }

        public override string ToString(List<Expression> arguments) =>
            Operation.ToString(this, arguments);
    }
}
