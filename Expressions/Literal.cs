
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

namespace MetaphysicsIndustries.Solus.Expressions
{
	public class Literal : Expression
	{
        public static readonly Literal Zero = new Literal(0);
        public static readonly Literal One = new Literal(1);
        public static readonly Literal NegativeOne = new Literal(-1);

		public Literal()
		{
			_value = 0;
		}

		public Literal(float v)
		{
			_value = v;
		}

        public override Expression Clone()
        {
            return new Literal(Value);
        }

        public override Literal Eval(SolusEnvironment env)
		{
			return this;
		}

        public override string ToString()
        {
            if (Math.Abs(Value - (float)Math.E) < 1e-6)
            {
                return "e";
            }
            else if (Math.Abs(Value - (float)Math.PI) < 1e-6)
            {
                return "π";
            }
            else
            {
                return Value.ToString("G");
            }
        }

		public virtual float Value
		{
			get
			{
				return _value;
			}
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

        protected float _value;

        public static bool IsInteger(float p)
        {
            return (Math.Truncate(p) == p);
        }

        public override void AcceptVisitor(IExpressionVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap)
        {
            return new [] { Instruction.LoadConstant(Value) };
        }
    }
}
