
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
using System.Drawing;
using MetaphysicsIndustries.Solus.Values;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class LoadImageFunction : Function
    {
        public static readonly LoadImageFunction Value =
            new LoadImageFunction();

        protected LoadImageFunction()
            : base(new[] {Types.String}, "load_image")
        {
        }

        protected override IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args)
        {
            return CallWithLoader(env, args, null);
        }

        public IMathObject CallWithLoader(SolusEnvironment env,
            IMathObject[] args, Func<string, Image> loader)
        {
            return SolusEngine.LoadImage(args[0].ToStringValue().Value,
                loader);
        }
    }
}
