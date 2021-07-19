
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
using MetaphysicsIndustries.Solus.Expressions;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class Log2Function : SingleArgumentFunction
    {
        public static readonly Log2Function Value = new Log2Function();

        protected Log2Function()
        {
            Name = "Logarithm of base two";
        }

        protected override Literal InternalCall(SolusEnvironment env, Literal[] args)
        {
            return new Literal((float)Math.Log(args[0].Eval(env).Value, 2));
        }

        public override string DisplayName
        {
            get
            {
                return "log2";
            }
        }
    }
}
