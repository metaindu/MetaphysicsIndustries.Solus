
/*****************************************************************************
 *                                                                           *
 *  Function.cs                                                              *
 *  24 September 2006                                                        *
 *  Project: Solus, Ligra                                                    *
 *  Written by: Richard Sartor                                               *
 *  Copyright © 2006 Metaphysics Industries, Inc.                            *
 *                                                                           *
 *  Converted from C++ to C# on 29 October 2007                              *
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
using MetaphysicsIndustries.Collections;

namespace MetaphysicsIndustries.Solus
{
    public abstract partial class Function : IDisposable
	{
        public Function()
            : this(string.Empty)
        {
        }

        public Function(string name)
        {
            _name = name;
        }

		public virtual void Dispose()
		{
			
		}

		public virtual Literal Call(VariableTable varTable, params Expression[] args)
        {
            this.CheckArguments(args);
            List<Literal> evalArgs = new List<Literal>(args.Length);
            foreach (Expression arg in args)
            {
                evalArgs.Add(arg.Eval(varTable));
            }
			return this.InternalCall(varTable, evalArgs.ToArray());
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
			get
			{
				return Name;
			}
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

        protected abstract Literal InternalCall(VariableTable varTable, Literal[] args);

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
	}
}
