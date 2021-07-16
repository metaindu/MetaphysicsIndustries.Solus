
/*****************************************************************************
 *                                                                           *
 *  Literal.cs                                                               *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006-2021 Metaphysics Industries, Inc.                       *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
 *                                                                           *
 *  A number. No fancy moving parts.                                         *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;

using System.Linq;

namespace MetaphysicsIndustries.Solus
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
