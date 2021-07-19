
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
 *  Function.cs                                                              *
 *                                                                           *
 *  A mathematical function that can be evaluated with a set of              *
 *    parameters. This serves as a base class that is inherited by other,    *
 *    specialized classes, each representing a different mathematical        *
 *    function (e.g. "SineFunction : Function"). This base class performs    *
 *    all necessary type checking on given arguments based on information    *
 *    specified by the derived class.                                        *
 *                                                                           *
 *****************************************************************************/

using System;
using System.Collections.Generic;
using MetaphysicsIndustries.Solus.Compiler;
using MetaphysicsIndustries.Solus.Expressions;
using Expression = MetaphysicsIndustries.Solus.Expressions.Expression;

namespace MetaphysicsIndustries.Solus.Functions
{
    public abstract partial class Function
	{
        protected Function()
            : this(string.Empty)
        {
        }

        protected Function(string name)
        {
            _name = name;
        }

		public virtual Literal Call(SolusEnvironment env, params Expression[] args)
        {
            this.CheckArguments(args);
            List<Literal> evalArgs = new List<Literal>(args.Length);
            foreach (Expression arg in args)
            {
                evalArgs.Add(arg.Eval(env));
            }
			return this.InternalCall(env, evalArgs.ToArray());
		}

        //public virtual Expression CleanUp(Expression[] args)
        //{
        //    bool call = true;
        //    foreach (Expression arg in args)
        //    {
        //        if (!(arg is Literal))
        //        {
        //            call = false;
        //            break;
        //        }
        //    }

        //    if (call)
        //    {
        //        return Call(null, args);
        //    }

        //    return new FunctionCall(this, args);
        //}

		public virtual string DisplayName
		{
            get { return Name; }
		}

		public string Name
		{
			get
			{
				return _name;
			}
			protected set
			{
				if (_name != value)
				{
					_name = value;
				}
			}
		}

		public List<Type> Types
		{
			get
			{
				return InternalTypes;
			}
		}

        protected abstract Literal InternalCall(SolusEnvironment env, Literal[] args);

		protected virtual void CheckArguments(Expression[] args)
		{
			Expression[] _args = args;
			List<Type>	types;
			int				i;
			int				j;
			
			types = Types;
			if (args.Length != types.Count)
			{
				throw new InvalidOperationException("Wrong number of arguments given to " + DisplayName + " (given " + args.Length.ToString() + ", require " + Types.Count.ToString() + ")");
			}
			Type	e;
			e = typeof(Expression);
			j = args.Length;
			for (i = 0; i < j; i++)
			{
				if (!e.IsAssignableFrom(types[i]))
				{
					
					throw new InvalidOperationException("Required argument type " + i.ToString() + " is invalid (given \"" + types[i].Name + "\", require \"" + e.Name + ")");
				}
				if (!types[i].IsAssignableFrom(args[i].GetType()))
				{
					throw new InvalidOperationException("Argument " + ((i).ToString()) + " of wrong type (given \"" + args.GetType().Name + "\", require \"" + types[i].Name + ")");
				}
			}
		}

		protected List<Type> InternalTypes
		{
			get
			{
				return _internaltypes;
			}
		}

		private  List<Type> _internaltypes = new List<Type>();
		private  string     _name;

        public virtual string ToString(List<Expression> arguments)
        {
            Expression[] exprs = arguments.ToArray();

            string[] strs = Array.ConvertAll<Expression, string>(exprs, Expression.ToString);

            return DisplayName + "(" + string.Join(", ", strs) + ")";
        }

        public T As<T>()
            where T : Function
        {
            return this as T;
        }

        public virtual string DocString
        {
            get { return string.Empty; }
        }

        public virtual IEnumerable<Instruction> ConvertToInstructions(VariableToArgumentNumberMapper varmap, List<Expression> arguments)
        {
            throw new NotImplementedException();
        }
    }
}
