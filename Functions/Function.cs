
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
using System.Collections.ObjectModel;
using System.Linq;
using MetaphysicsIndustries.Solus.Values;
using Expression = MetaphysicsIndustries.Solus.Expressions.Expression;

namespace MetaphysicsIndustries.Solus.Functions
{
    public abstract class Function
    {
        protected Function(Types[] paramTypes, string name="")
        {
            if (paramTypes == null)
                throw new ArgumentNullException(nameof(paramTypes));
            _name = name;
            ParamTypes = Array.AsReadOnly(paramTypes);
        }

		public virtual IMathObject Call(SolusEnvironment env, params IMathObject[] args)
        {
            this.CheckArguments(args);
            return InternalCall(env, args);
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

        protected abstract IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args);

        public virtual void CheckArguments(IMathObject[] args) =>
            CheckArguments(args, ParamTypes, DisplayName);

        public static void CheckArguments(IMathObject[] args,
            IList<Types> paramTypes, string displayName)
        {
            if (args.Length != paramTypes.Count)
            {
                throw new ArgumentException(
                    $"Wrong number of arguments given to " +
                    $"{displayName} (expected {paramTypes.Count} but got " +
                    $"{args.Length})");
            }
            for (var i = 0; i < args.Length; i++)
            {
                var argtype = args[i].GetMathType();
                if (argtype != paramTypes[i])
                {
                    throw new ArgumentException(
                        $"Argument {i} wrong type: expected " +
                        $"{paramTypes[i]} but got {argtype}");
                }
            }
        }

        public ReadOnlyCollection<Types> ParamTypes { get; }
        private string _name;

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

        public abstract IMathObject GetResult(IEnumerable<IMathObject> args);
    }
}
