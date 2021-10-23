
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

/*****************************************************************************
 *                                                                           *
 *  Literal.cs                                                               *
 *                                                                           *
 *  A number. No fancy moving parts.                                         *
 *                                                                           *
 *****************************************************************************/

using System;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Expressions
{
	public class Literal : Expression
	{
        public static readonly Literal Zero = new Literal(0);
        public static readonly Literal One = new Literal(1);
        public static readonly Literal NegativeOne = new Literal(-1);

		public Literal(float v)
			: this(v.ToNumber())
		{
		}
		public Literal(IMathObject value)
        {
            if (!value.IsConcrete)
                throw new ArgumentException(
                    "Literal expressions can only hold concrete values",
                    nameof(value));

			_value = value;
            Result = new ResultC(this);
        }

        public override Expression Clone()
        {
            return new Literal(Value);
        }

        public override IMathObject Eval(SolusEnvironment env)
		{
			return _value;
		}

        public override string ToString()
        {
	        return Value.ToString();
        }

        public virtual IMathObject Value
		{
			get => _value;
			set
			{
                if (_value != value)
				{
                    _value = value;
					this.OnValueChanged(new EventArgs());
				}
			}
		}

		public  event EventHandler ValueChanged;

		protected virtual void OnValueChanged(EventArgs e)
		{
            if (ValueChanged != null)
            {
                ValueChanged(this, e);
            }
		}

        protected IMathObject _value;

        public static bool IsInteger(float p)
        {
            return (Math.Truncate(p) == p);
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override IMathObject Result { get; }

        private class ResultC : IMathObject
        {
            public ResultC(Literal lit) => _lit = lit;
            private readonly Literal _lit;

            public bool IsScalar(SolusEnvironment env) =>
                _lit.Value.IsScalar(env);

            public bool IsVector(SolusEnvironment env) =>
                _lit.Value.IsVector(env);

            public bool IsMatrix(SolusEnvironment env) =>
                _lit.Value.IsMatrix(env);

            public int GetTensorRank(SolusEnvironment env) =>
                _lit.Value.GetTensorRank(env);

            public bool IsString(SolusEnvironment env) =>
                _lit.Value.IsString(env);

            public int GetDimension(SolusEnvironment env,
                int index) => _lit.Value.GetDimension(env, index);

            public int[] GetDimensions(SolusEnvironment env) =>
                _lit.Value.GetDimensions(env);

            public int GetVectorLength(SolusEnvironment env)
            {
                if (_lit.Value.IsVector(env))
                    return _lit.Value.ToVector().Length;
                throw new InvalidOperationException(
                    "The value is not a vector");
            }

            public int GetStringLength(SolusEnvironment env)
            {
                if (_lit.Value.IsString(env))
                    return _lit.Value.ToStringValue().Length;
                throw new InvalidOperationException(
                    "The value is not a string");
            }

            public bool IsConcrete => false;
        }
    }
}
