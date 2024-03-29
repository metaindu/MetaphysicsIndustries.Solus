﻿
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

namespace MetaphysicsIndustries.Solus.Macros
{
    public class DeriveMacro : Macro
    {
        public static readonly DeriveMacro Value = new DeriveMacro();

        private DeriveMacro()
        {
        }

        public override string Name => "derive";
        public override int NumArguments => 2;

        public override string DocString
        {
            get
            {
                return "The derive operator\n  derive(f(x), x)\n\nReturns the derivative of f(x) with respect to x.";
            }
        }
    }
}
