
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

namespace MetaphysicsIndustries.Solus.Functions
{
    public class Arctangent2Function : DualArgumentFunction
    {
        public static readonly Arctangent2Function Value = new Arctangent2Function();

        protected Arctangent2Function()
            : base ("Arctangent 2")
        {
        }

        protected override float InternalCall(float arg0, float arg1)
        {
            return (float)Math.Atan2(arg0, arg1);
        }

        public override string DisplayName
        {
            get
            {
                return "atan2";
            }
        }

        public override string DocString
        {
            get
            {
                return "The atan2 function\n  atan(y, x)\n\nReturns the arctangent of y/x.";
            }
        }
    }
}
