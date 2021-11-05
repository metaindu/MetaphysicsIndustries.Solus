
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

        public override string DocString =>
            @"load_image - load an image from disk

  load_image(filename:string) -> matrix

  filename
    The path to the file to load. Should be a BMP, JPG, GIF, or PNG. Paths are
    relative to the current working directory of the process.

  returns matrix
    The result is a matrix. The matrix has as many rows as the image, and as
    many columns as the image. Each component of the matrix corresponds to the
    pixel at that location in the image. Incoming pixels are interpreted as
    24-bit integers, with blue in the low 8 bits, green in the middle 8 bits,
    and red in the high 8 bits; the value is then cast to a float (32-bit).";

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

        public override IMathObject GetResult(IEnumerable<IMathObject> args)
        {
            throw new NotImplementedException();
        }
    }
}
