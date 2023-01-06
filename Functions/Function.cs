
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
using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Functions
{
    public abstract class Function : IMathObject
    {
        protected Function(Parameter[] parameters, string name = "")
        {
            if (parameters == null)
                throw ValueException.Null(nameof(parameters));
            _name = name;
            Parameters = Array.AsReadOnly(parameters);
        }
        protected Function(Parameter parameter, string name)
        {
            IsVariadic = true;
            Parameters = new ReadOnlyCollection<Parameter>(
                new List<Parameter>(new[] { parameter }));
            _name = name;
        }

        public virtual IMathObject CustomCall(IMathObject[] args,
            SolusEnvironment env) => null;

        public virtual bool ProvidesCustomCall => false;

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

        public ReadOnlyCollection<Parameter> Parameters { get; }
        public bool IsVariadic { get; } = false;
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

        public abstract ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes);

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
        public bool? IsFunction(SolusEnvironment env) => true;
        public bool? IsExpression(SolusEnvironment env) => false;
        public bool? IsSet(SolusEnvironment env) => false;
        public bool IsConcrete => true;

        public virtual bool IsCommutative => false;
        public virtual bool IsAssociative => false;
    }
}
