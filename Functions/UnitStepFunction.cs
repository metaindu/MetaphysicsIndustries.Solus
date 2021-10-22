
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

using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class UnitStepFunction : SingleArgumentFunction
    {
        public static readonly UnitStepFunction Value = new UnitStepFunction();

        protected UnitStepFunction()
        {
            Name = "UnitStep";
        }

        protected override IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args)
        {
            if (args[0].ToNumber().Value >= 0)
            {
                return new Number(1);
            }
            else
            {
                return new Number(0);
            }
        }

        public override string DisplayName
        {
            get
            {
                return "unitstep";
            }
        }

        public override string DocString
        {
            get
            {
                return "unit step function";
            }
        }
    }
}
