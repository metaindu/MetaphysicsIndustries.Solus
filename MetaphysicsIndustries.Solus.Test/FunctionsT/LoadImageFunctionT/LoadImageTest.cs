
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

using System;
using System.Drawing;
using MetaphysicsIndustries.Solus.Functions;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.LoadImageFunctionT
{
    [TestFixture]
    public class LoadImageTest
    {
        [Test]
        public void LoadsImageIntoMatrix()
        {
            // given
            var mi = new MemoryImage(2, 2);
            mi[0, 0] = Color.Blue;
            mi[0, 1] = Color.Lime;
            mi[1, 0] = Color.Red;
            mi[1, 1] = Color.White;
            mi.CopyPixelsToBitmap();
            Func<string, object> loader = filename => mi.Bitmap;
            // when
            var result = LoadImageFunction.LoadImage("filename", loader);
            // then
            Assert.AreEqual(2, result.RowCount);
            Assert.AreEqual(2, result.ColumnCount);
            Assert.AreEqual(255, result[0, 0].ToNumber().Value);
            Assert.AreEqual(65280, result[0, 1].ToNumber().Value);
            Assert.AreEqual(16711680, result[1, 0].ToNumber().Value);
            Assert.AreEqual(16777215, result[1, 1].ToNumber().Value);
        }
    }
}
