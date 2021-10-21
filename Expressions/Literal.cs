
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
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Compiler;
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
			_value = value;
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

        public override IEnumerable<Instruction> ConvertToInstructions(
	        VariableToArgumentNumberMapper varmap)
        {
	        if (Value.IsScalar)
				return new [] { Instruction.LoadConstant(Value.ToFloat()) };
	        throw new NotImplementedException(
		        "currently only implemented for numbers.");
        }

        public override bool IsResultScalar(SolusEnvironment env) =>
            Value.IsScalar;

        public override bool IsResultVector(SolusEnvironment env) =>
            Value.IsVector;

        public override bool IsResultMatrix(SolusEnvironment env) =>
            Value.IsMatrix;

        public override int GetResultTensorRank(SolusEnvironment env) =>
            Value.TensorRank;

        public override bool IsResultString(SolusEnvironment env) =>
            Value.IsString;

        public override int GetResultDimension(SolusEnvironment env,
            int index) => Value.GetDimension(index);

        public override int[] GetResultDimensions(SolusEnvironment env) =>
            Value.GetDimensions();

        public override int GetResultVectorLength(SolusEnvironment env)
        {
            if (Value.IsVector)
                return Value.ToVector().Length;
            throw new InvalidOperationException(
                "The value is not a vector");
        }

        public override int GetResultStringLength(SolusEnvironment env)
        {
            if (Value.IsString)
                return Value.ToStringValue().Length;
            throw new InvalidOperationException(
                "The value is not a string");
        }
    }
}
