
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
using System.Text;

namespace MetaphysicsIndustries.Solus
{
    public class IfFunction : Function
    {
        public static readonly IfFunction Value = new IfFunction();

        protected IfFunction()
            : base("If")
        {
            Types.Clear();
            Types.Add(typeof(Expression));
            Types.Add(typeof(Expression));
            Types.Add(typeof(Expression));
        }

        public override Literal Call(SolusEnvironment env, params Expression[] args)
        {
            CheckArguments(args);

            float value = args[0].Eval(env).Value;

            if (value == 0 || float.IsNaN(value) || float.IsInfinity(value))
            {
                return args[2].Eval(env);
            }
            else
            {
                return args[1].Eval(env);
            }
        }

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            throw new NotSupportedException();
            //float value = args[0].Value;
            //if (value == 0 || float.IsNaN(value) || float.IsInfinity(value))
            //{
            //    return args[2];
            //}
            //else
            //{
            //    return args[1];
            //}
        }

        public override string DisplayName
        {
            get { return "if"; }
        }
    }
}
