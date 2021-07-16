
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2016-2021 Metaphysics Industries, Inc., Richard Sartor
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
using System.Drawing.Imaging;

namespace MetaphysicsIndustries.Solus
{
    /*
     * A 2D array of pixel color data.
     * 
     */

    public class MemoryImage : IDisposable
    {
        public MemoryImage()
            : this(1, 1)
        {
        }

        public MemoryImage(int width, int height)
            : this(new Bitmap(width, height, PixelFormat.Format32bppArgb))
        {
        }

        public MemoryImage(Bitmap bitmap)
        {
            if (bitmap == null) { throw new ArgumentNullException("bitmap"); }

            _bitmap = bitmap.Clone(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                PixelFormat.Format32bppArgb);

            _pixels = new Color[bitmap.Height, bitmap.Width];
        }

        public void Dispose()
        {
            _bitmap.Dispose();
        }

        public int[] AllocateArrayForPixels()
        {
            return new int[Width * Height];
        }

        public void CopyPixelsToArray(int[] pixeldata)
        {
            int k = 0;
            for (int row = 0; row < Height; row++)
            {
                for (int column = 0; column < Width; column++)
                {
                    pixeldata[k] = _pixels[row, column].ToArgb();
                    k++;
                }
            }
        }

        public byte[] AllocateByteArrayForPixels()
        {
            return new byte[4 * Width * Height];
        }

        public void CopyPixelsToArray(byte[] pixelbytes)
        {
            int k = 0;
            for (int row = 0; row < Height; row++)
            {
                for (int column = 0; column < Width; column++)
                {
                    pixelbytes[k + 0] = _pixels[row, column].R;
                    pixelbytes[k + 1] = _pixels[row, column].G;
                    pixelbytes[k + 2] = _pixels[row, column].B;
                    pixelbytes[k + 3] = _pixels[row, column].A;
                    k += 4;
                }
            }
        }

        // takes the values from the pixel array in memory and puts them into
        // the bitmap object
        public void CopyPixelsToBitmap()
        {
            BitmapData data = null;

            try
            {
                data = _bitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                                ImageLockMode.WriteOnly,
                                                PixelFormat.Format32bppArgb);
                IntPtr ptr = data.Scan0;
                int[] pixeldata = AllocateArrayForPixels();
                CopyPixelsToArray(pixeldata);
                System.Runtime.InteropServices.Marshal.Copy(pixeldata, 0, ptr,
                    Width * Height);
            }
            finally
            {
                if (data != null)
                {
                    _bitmap.UnlockBits(data);
                }
            }
        }

        public void CopyArrayToPixels(int[] pixeldata)
        {
            int k = 0;
            for (int row = 0; row < Height; row++)
            {
                for (int column = 0; column < Width; column++)
                {
                    _pixels[row, column] = Color.FromArgb(pixeldata[k]);
                    k++;
                }
            }
        }

        // takes the values from the bitmap object and puts them into the pixel
        // array in memory
        public void CopyBitmapToPixels()
        {
            if (Bitmap == null) { throw new InvalidOperationException(); }

            BitmapData data = null;
            int[] pixeldata;

            try
            {
                data = _bitmap.LockBits(
                    new Rectangle(0, 0, _bitmap.Width, _bitmap.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);
                IntPtr ptr = data.Scan0;
                pixeldata = AllocateArrayForPixels();
                System.Runtime.InteropServices.Marshal.Copy(ptr, pixeldata, 0,
                    Width * Height);
            }
            finally
            {
                if (data != null)
                {
                    _bitmap.UnlockBits(data);
                }
            }

            CopyArrayToPixels(pixeldata);
        }

        public Size Size
        {
            get { return _bitmap.Size; }
            //set
            //{
            //    if (value.Width <= 0 || value.Height <= 0)
            //    {
            //        return;
            //    }
            //    if (value.Width > _pixels.GetLength(0) ||
            //        value.Height > _pixels.GetLength(1))
            //    {
            //        int w;
            //        int h;
            //        int i;
            //        int j;
            //        Color[,] newpixels;
            //        w = System.Math.Max(value.Width, _pixels.GetLength(0));
            //        h = System.Math.Max(value.Height, _bitmap.Height);
            //        newpixels = new Color[w, h];
            //        w = System.Math.Min(value.Width, _pixels.GetLength(0));
            //        h = System.Math.Min(value.Height, _pixels.GetLength(1));
            //        for (i = 0; i < w; i++)
            //        {
            //            for (j = 0; j < h; j++)
            //            {
            //                newpixels[i, j] = _pixels[i, j];
            //            }
            //        }
            //        _pixels = newpixels;
            //    }
            //    if (value.Width > _bitmap.Width || value.Height > _bitmap.Height)
            //    {
            //        int w;
            //        int h;
            //        w = System.Math.Max(value.Width, _bitmap.Width);
            //        h = System.Math.Max(value.Height, _bitmap.Height);
            //        _bitmap = new Bitmap(_bitmap, w, h);
            //    }

            //    _size = value;
            //}
        }

        public Color this[int row, int column]
        {
            get
            {
                return _pixels[row, column];
            }
            set
            {
                _pixels[row, column] = value;
            }
        }

        public Bitmap Bitmap
        {
            get
            {
                return _bitmap;
            }
        }

        public int Width
        {
            get { return _bitmap.Width; }
        }

        public int Height
        {
            get { return _bitmap.Height; }
        }

        private readonly Color[,] _pixels;
        private readonly Bitmap _bitmap;
    }
}
