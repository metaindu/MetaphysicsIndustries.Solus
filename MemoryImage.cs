
/*
 *  MetaphysicsIndustries.Solus
 *  Copyright (C) 2016 Metaphysics Industries, Inc., Richard Sartor
 *
 *  This program is free software; you can redistribute it and/or modify
 *  it under the terms of the GNU General Public License as published by
 *  the Free Software Foundation; either version 2 of the License, or
 *  (at your option) any later version.
 *
 *  This program is distributed in the hope that it will be useful,
 *  but WITHOUT ANY WARRANTY; without even the implied warranty of
 *  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
 *  GNU General Public License for more details.
 *
 *  You should have received a copy of the GNU General Public License along
 *  with this program; if not, write to the Free Software Foundation, Inc.,
 *  51 Franklin Street, Fifth Floor, Boston, MA 02110-1301 USA.
 * 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;

namespace MetaphysicsIndustries.Solus
{
    /*
     * A managed class that organizes access to a Bitmap object.
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

            _bitmap = bitmap.Clone(new Rectangle(0, 0, bitmap.Width, bitmap.Height), PixelFormat.Format32bppArgb);

            _pixels = new Color[bitmap.Height, bitmap.Width];
        }

        public void Dispose()
        {
            _bitmap.Dispose();
        }

        //takes the values from the pixel array in memory and puts them into the bitmap object
        public void CopyPixelsToBitmap()
        {
            BitmapData data = null;
            IntPtr ptr;
            int[] pixeldata;
            int count;
            int column;
            int row;
            int k;
            int ii;
            int jj;
            ii = _bitmap.Width;
            jj = _bitmap.Height;
            count = ii * jj;

            try
            {
                data = _bitmap.LockBits(new Rectangle(0, 0, Width, Height),
                                                ImageLockMode.WriteOnly,
                                                PixelFormat.Format32bppArgb);
                ptr = data.Scan0;
                pixeldata = new int[count];
                k = 0;
                for (row = 0; row < jj; row++)
                {
                    for (column = 0; column < ii; column++)
                    {
                        pixeldata[k] = _pixels[row, column].ToArgb();
                        k++;
                    }
                }
                System.Runtime.InteropServices.Marshal.Copy(pixeldata, 0, ptr, count);
            }
            finally
            {
                if (data != null)
                {
                    _bitmap.UnlockBits(data);
                }
            }
        }

        //takes the values from the bitmap object and puts them into the pixel array in memory
        public void CopyBitmapToPixels()
        {
            if (Bitmap == null) { throw new InvalidOperationException(); }

            BitmapData data = null;
            IntPtr ptr;
            int[] pixeldata;
            int count;
            int column;
            int row;
            int k;
            int ii;
            int jj;

            count = _bitmap.Width * _bitmap.Height;

            try
            {
                data = _bitmap.LockBits(new Rectangle(0, 0, _bitmap.Width, _bitmap.Height),
                                                ImageLockMode.ReadOnly,
                                                PixelFormat.Format32bppArgb);
                ptr = data.Scan0;
                pixeldata = new int[count];
                System.Runtime.InteropServices.Marshal.Copy(ptr, pixeldata, 0, count);
            }
            finally
            {
                if (data != null)
                {
                    _bitmap.UnlockBits(data);
                }
            }

            k = 0;
            ii = _bitmap.Width;
            jj = _bitmap.Height;
            for (row = 0; row < jj; row++)
            {
                for (column = 0; column < ii; column++)
                {
                    _pixels[row, column] = Color.FromArgb(pixeldata[k]);
                    k++;
                }
            }
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
