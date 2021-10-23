
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2021 Metaphysics Industries, Inc., Richard Sartor
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
using System.Linq;
using System.Text;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Transformers;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
{
    public class VectorExpression : TensorExpression
    {
        private static SolusEngine _engine = new SolusEngine();

        public static VectorExpression FromUniformSequence(float value,
            int length)
        {
            return FromUniformSequence(new Literal(value), length);
        }

        public static VectorExpression FromUniformSequence(Expression value,
            int length)
        {
            var ret = new VectorExpression(length);

            int i;
            for (i = 0; i < length; i++)
            {
                ret[i] = value;
            }

            return ret;
        }

        public VectorExpression(int length)
        {
            _array = new Expression[length];

            int i;

            for (i = 0; i < length; i++)
            {
                _array[i] = Literal.Zero;
            }

            Result = new ResultC(this);
        }

        public VectorExpression(int length, params float[] initialContents)
            : this(length)
        {
            int i;
            int j = Math.Min(length, initialContents.Length);
            for (i = 0; i < j; i++)
            {
                _array[i] = new Literal(initialContents[i]);
            }
        }

        public VectorExpression(int length,
            params Expression[] initialContents)
            : this(length)
        {
            int i;
            int j = Math.Min(length, initialContents.Length);
            for (i = 0; i < j; i++)
            {
                _array[i] = initialContents[i];
            }
        }

        public override int TensorRank => 1;

        private Expression[] _array;
        public int Length => _array.Length;

        public override IMathObject Eval(SolusEnvironment env)
        {
            var values = new IMathObject[Length];
            for (int i = 0; i < Length; i++)
                values[i] = this[i].Eval(env);
            return new Vector(values);
        }

        public override Expression Clone()
        {
            var ret = new VectorExpression(Length);

            int i;
            for (i = 0; i < Length; i++)
            {
                ret[i] = this[i];//.Clone();
            }

            return ret;
        }

        //public override Expression CleanUp()
        //{
        //    var ret = new VectorExpression(Length);

        //    int i;
        //    for (i = 0; i < Length; i++)
        //    {
        //        ret[i] = this[i].CleanUp();
        //    }

        //    return ret;
        //}

        public Expression this[int index]
        {
            get
            {
                if (index < 0 || index >= Length) { throw new IndexOutOfRangeException("index"); }

                return _array[index];
            }
            set
            {
                if (index < 0 || index >= Length) { throw new IndexOutOfRangeException("index"); }

                _array[index] = value;
            }
        }

        #region IEnumerable<Expression> Members

        public override IEnumerator<Expression> GetEnumerator()
        {
            IList<Expression> list = _array;
            return list.GetEnumerator();
        }

        #endregion

        //methods and overloaded operators

        public VectorExpression Convolution(VectorExpression convolvee)
        {
            return AdvancedConvolution(convolvee, MultiplicationOperation.Value, AdditionOperation.Value);
        }

        public VectorExpression AdvancedConvolution(VectorExpression convolvee,
            Operation firstOp, AssociativeCommutativeOperation secondOp)
        {
            int r = Length + convolvee.Length - 1;

            var ret = new VectorExpression(r);

            List<Expression> group = new List<Expression>();

            int n;
            int k;
            for (n = 0; n < r; n++)
            {
                group.Clear();

                for (k = 0; k < Length; k++)
                {
                    if (n - k < 0) { break; }
                    if (n - k >= convolvee.Length) { continue; }

                    group.Add(
                        new FunctionCall(
                        firstOp,
                        this[k],
                        convolvee[n - k]));
                }

                CleanUpTransformer cleanup = new CleanUpTransformer();
                ret[n] = cleanup.CleanUp(new FunctionCall(secondOp, group.ToArray()));
            }

            return ret;
        }

        protected override void InternalApplyToExpressionTree(SolusAction action, bool applyToChildrenBeforeParent)
        {
            foreach (Expression expr in this)
            {
                expr.ApplyToExpressionTree(action, applyToChildrenBeforeParent);
            }
        }
        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);

            foreach (Expression expr in this)
            {
                expr.AcceptVisitor(visitor);
            }
        }

        public override Expression PreliminaryEval(SolusEnvironment env)
        {
            var values = _array.Select(
                e => e.PreliminaryEval(env)).ToArray();
            if (values.All(e => e is Literal))
            {
                return new Literal(
                    new Vector(
                        values.Select(
                            e => ((Literal) e).Value).ToArray()));
            }

            return new VectorExpression(values.Length, values);
        }

        public override void ApplyToAll(Modulator mod)
        {
            int i;
            for (i = 0; i < Length; i++)
            {
                this[i] = new Literal(
                    mod(((Literal)this[i]).Value.ToFloat()));
            }
        }

        public VectorExpression GetSlice(int startIndex, int length)
        {
            var ret = new VectorExpression(length);

            int i;
            int j = Math.Min(length, Length - startIndex);
            for (i = 0; i < j; i++)
            {
                ret[i] = this[i + startIndex];
            }

            return ret;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("[ ");
            for (var i = 0; i < Length; i++)
            {
                if (i > 0) sb.Append(", ");
                sb.Append(this[i]);
            }
            sb.Append("]");
            return sb.ToString();
        }

        public override IEnvMathObject Result { get; }

        private class ResultC : IEnvMathObject
        {
            public ResultC(VectorExpression ve) => _ve = ve;
            private readonly VectorExpression _ve;
            public bool IsScalar(SolusEnvironment env) => false;
            public bool IsVector(SolusEnvironment env) => true;
            public bool IsMatrix(SolusEnvironment env) => false;
            public int GetTensorRank(SolusEnvironment env) => 1;
            public bool IsString(SolusEnvironment env) => false;

            public int GetDimension(SolusEnvironment env, int index)
            {
                if (index == 0) return _ve.Length;
                throw new IndexOutOfRangeException(
                    "The index must be zero for a vector");
            }

            private int[] __GetDimensions;

            public int[] GetDimensions(SolusEnvironment env)
            {
                if (__GetDimensions == null)
                    __GetDimensions = new[] { _ve.Length };
                return __GetDimensions;
            }

            public int GetVectorLength(SolusEnvironment env) => _ve.Length;

            public int GetStringLength(SolusEnvironment env)
            {
                throw new InvalidOperationException(
                    "A vector is not a string");
            }
        }
    }
}
