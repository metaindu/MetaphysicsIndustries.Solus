
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

namespace MetaphysicsIndustries.Solus
{
    public abstract class Macro
    {
        public string Name = string.Empty;
        public int NumArguments = 0;
        public bool HasVariableNumArgs = false;

        public abstract Expression InternalCall(IEnumerable<Expression> args, SolusEnvironment env);

        public virtual Expression Call(IEnumerable<Expression> args, SolusEnvironment env)
        {
            List<Expression> arglist = new List<Expression>(args);
            if (!HasVariableNumArgs &&
                arglist.Count != NumArguments)
            {
                throw new ArgumentException("Incorrect number of arguments.", "arg");
            }

            return InternalCall(args, env);
        }

        public virtual string DocString
        {
            get { return string.Empty; }
        }
    }
}
