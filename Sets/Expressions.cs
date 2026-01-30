
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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

namespace MetaphysicsIndustries.Solus.Sets
{
    public class Expressions : ISet
    {
        public static readonly Expressions Value = new Expressions();

        protected Expressions()
        {
        }

        public bool Contains(IMathObject mo) => mo.IsIsExpression(null);
        public string DisplayName => "Expression";
        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other is Expressions ||
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

        public string DocString => "The set of expressions";
    }

    public class ComponentAccesses : ISet
    {
        public static readonly ComponentAccesses
            Value = new ComponentAccesses();

        protected ComponentAccesses()
        {
        }

        public bool Contains(IMathObject mo) => mo.IsIsComponentAccess();
        public string DisplayName => "ComponentAccess";
        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other is ComponentAccesses ||
            other is Expressions ||
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

        public string DocString => "The set of component access expressions";
    }

    public class FunctionCalls : ISet
    {
        public static readonly FunctionCalls Value = new FunctionCalls();

        protected FunctionCalls()
        {
        }

        public bool Contains(IMathObject mo) => mo.IsIsFunctionCall();
        public string DisplayName => "FunctionCall";
        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other is FunctionCalls ||
            other is Expressions ||
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

        public string DocString => "The set of function calls";
    }

    public class IntervalExpressions : ISet
    {
        public static readonly IntervalExpressions Value =
            new IntervalExpressions();

        protected IntervalExpressions()
        {
        }

        public bool Contains(IMathObject mo) => mo.IsIsIntervalExpression();
        public string DisplayName => "IntervalExpression";
        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other is IntervalExpressions ||
            other is Expressions ||
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

        public string DocString => "The set of interval expressions";
    }

    public class Literals : ISet
    {
        public static readonly Literals Value = new Literals();

        protected Literals()
        {
        }

        public bool Contains(IMathObject mo) => mo.IsIsLiteral();
        public string DisplayName => "Literal";
        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other is Literals ||
            other is Expressions ||
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

        public string DocString => "The set of literal expressions";
    }

    public class TensorExpressions : ISet
    {
        public static readonly TensorExpressions
            Value = new TensorExpressions();

        protected TensorExpressions()
        {
        }

        public bool Contains(IMathObject mo) => mo.IsIsTensorExpression();
        public string DisplayName => "TensorExpression";
        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other is TensorExpressions ||
            other is Expressions ||
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

        public string DocString => "The set of tensor expressions";
    }

    public class MatrixExpressions : ISet
    {
        public static readonly MatrixExpressions
            Value = new MatrixExpressions();

        protected MatrixExpressions()
        {
        }

        public bool Contains(IMathObject mo) => mo.IsIsMatrixExpression();
        public string DisplayName => "MatrixExpression";
        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other is TensorExpressions ||
            other is MatrixExpressions ||
            other is Expressions ||
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

        public string DocString => "The set of matrix expressions";
    }

    public class VectorExpressions : ISet
    {
        public static readonly VectorExpressions
            Value = new VectorExpressions();

        protected VectorExpressions()
        {
        }

        public bool Contains(IMathObject mo) => mo.IsIsVectorExpression();
        public string DisplayName => "VectorExpression";
        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other is TensorExpressions ||
            other is VectorExpressions ||
            other is Expressions ||
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

        public string DocString => "The set of vector expressions";
    }

    public class VariableAccesses : ISet
    {
        public static readonly VariableAccesses Value =
            new VariableAccesses();

        protected VariableAccesses()
        {
        }

        public bool Contains(IMathObject mo) => mo.IsIsVariableAccess();
        public string DisplayName => "VariableAccess";
        public bool IsSupersetOf(ISet other) =>
            other == this ||
            other.IsSubsetOf(this);
        public bool IsSubsetOf(ISet other) =>
            other is VariableAccesses ||
            other is Expressions ||
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

        public string DocString => "The set of variable access expressions";
    }
}
