
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
    public abstract class UnaryOperation : Operation
    {
        protected UnaryOperation()
        {
            Types.Clear();
            Types.Add(typeof(Expression));
        }

        protected override void CheckArguments(Expression[] args)
        {
            if (Types.Count != 1)
            {
                throw new InvalidOperationException("Wrong number of types specified internally to UnaryOperation (given " + Types.Count.ToString() + ", require 1)");
            }
            if (args.Length != 1)
            {
                throw new InvalidOperationException("Wrong number of arguments given to " + this.DisplayName + " (given " + args.Length.ToString() + ", require 1)");
            }

            Type e = typeof(Expression);

            if (!e.IsAssignableFrom(Types[0]))
            {
                throw new InvalidOperationException("Required argument type is invalid (given \"" + Types[0].Name + "\", require \"" + e.Name + "\")");
            }
            if (!Types[0].IsAssignableFrom(args[0].GetType()))
            {
                throw new InvalidOperationException("Argument of wrong type (given \"" + args[0].GetType().Name + "\", require \"" + Types[0].Name + "\")");
            }
        }

        public override bool HasIdentityValue
        {
            get { return false; }
        }
    }
}
