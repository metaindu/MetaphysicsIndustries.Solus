
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2006-2025 Metaphysics Industries, Inc., Richard Sartor
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
using System.IO;
using MetaphysicsIndustries.Solus.Sets;
using MetaphysicsIndustries.Solus.Values;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace MetaphysicsIndustries.Solus.Functions
{
    public class LoadImageFunction : Function
    {
        public static readonly LoadImageFunction Value =
            new LoadImageFunction();

        protected LoadImageFunction()
        {
        }

        public override string Name => "load_image";

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

        public override IReadOnlyList<Parameter> Parameters { get; } =
            new[] { new Parameter("arg", Strings.Value) };

        protected IMathObject InternalCall(SolusEnvironment env,
            IMathObject[] args)
        {
            return CallWithReader(env, args, null);
        }

        public IMathObject CallWithReader(SolusEnvironment env,
            IMathObject[] args, Func<string, Stream> reader)
        {
            return LoadImage(args[0].ToStringValue().Value,
                reader);
        }

        public static Matrix LoadViaSystemDrawing(string filename)
        {
            // if (_loader == null)
            //     _loader = Image.FromFile;
            // var fileImage = _loader(filename);
            // if (!(fileImage is Bitmap bitmap))
            //     throw new InvalidOperationException(
            //         "The file is not in the correct format");
            // var image = new MemoryImage(bitmap);
            // image.CopyBitmapToPixels();
            //
            // var values = new float[image.Height, image.Width];
            // for (var c = 0; c < image.Width; c++)
            // for (var r = 0; r < image.Height; r++)
            //     values[r, c] = image[r, c].ToArgb() & 0x00FFFFFF;
            // return new Matrix(values);

            throw new NotImplementedException();
        }

        public static Matrix LoadViaImageSharp(Stream stream)
        {
            var image = Image.Load(stream);
            int w = image.Width;
            int h = image.Height;
            var values = new float[h, w];
            if (image is Image<Rgba32> image32)
            {
                for (var c = 0; c < w; c++)
                for (var r = 0; r < h; r++)
                    values[h - r - 1, c] = image32[c, r].R << 16 |
                                           image32[c, r].G << 8 |
                                           image32[c, r].B << 0;
            }
            else if (image is Image<Rgb24> image24)
            {
                for (var c = 0; c < w; c++)
                for (var r = 0; r < h; r++)
                    values[h - r - 1, c] = image24[c, r].R << 16 |
                                           image24[c, r].G << 8 |
                                           image24[c, r].B << 0;
            }

            return new Matrix(values);
            // throw new NotImplementedException();
        }

        public static Matrix LoadViaMagickNet(Stream stream)
        {
            // var image = new MagickImage(filename);
            // var values = new float[image.Height, image.Width];
            // var pvalues = image.GetPixels().GetValues();
            // int i=0;
            // for (var c = 0; c < image.Width; c++)
            // for (var r = 0; r < image.Height; r++, i += 4)
            //     values[r, c] = pvalues[i + 0] << 16 |
            //                    pvalues[i + 1] << 8 |
            //                    pvalues[i + 2];
            // return new Matrix(values);
            throw new NotImplementedException();
        }

        public static Matrix LoadViaFreeImage(Stream stream)
        {
            // var image = FreeImage.LoadFromStream(stream,
            //     FREE_IMAGE_LOAD_FLAGS.DEFAULT);
            // var w = FreeImage.GetWidth(image);
            // var h = FreeImage.GetHeight(image);
            // var values = new float[h, w];
            // for (uint c = 0; c < w; c++)
            // for (uint r = 0; r < h; r++)
            // {
            //     FreeImage.GetPixelColor(image, c, r,
            //         out var quad);
            //     values[r, c] = quad.uintValue & 0xffffff;
            // }
            // return new Matrix(values);
            throw new NotImplementedException();
        }

        public static Matrix LoadViaSkiaSharp(string filename)
        {
            // var image = SKImage.FromEncodedData(filename);
            // var pixels = image.PeekPixels();
            //
            // var w = pixels.Width;
            // var h = pixels.Height;
            // var values = new float[h, w];
            // for (int c = 0; c < w; c++)
            // for (int r = 0; r < h; r++)
            // {
            //     var color = pixels.GetPixelColor(c, r);
            //     values[r, c] = color.Red << 16 |
            //                    color.Green << 8 |
            //                    color.Blue << 0;
            // }
            // return new Matrix(values);
            throw new NotImplementedException();
        }

        public static Matrix LoadImage(string filename,
            Func<string, Stream> reader = null)
        {
            Stream stream;
            if (reader != null)
                stream = reader(filename);
            else
                stream = new FileStream(filename, FileMode.Open);

            return LoadViaImageSharp(stream);
        }

        public override ISet GetResultType(SolusEnvironment env,
            IEnumerable<ISet> argTypes)
        {
            return AllMatrices.Value;
        }
    }
}
