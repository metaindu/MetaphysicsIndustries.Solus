
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
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.LoadImageFunctionT
{
    [TestFixture]
    public class LoadImageFunctionTest
    {
        [Test]
        public void LoadsImages()
        {
            // given
            var mi = new MemoryImage(2, 2);
            mi[0, 0] = Color.White;
            mi[0, 1] = Color.Red;
            mi[1, 0] = Color.Green;
            mi[1, 1] = Color.Blue;
            mi.CopyPixelsToBitmap();
            Func<string, object> loader = filename => mi.Bitmap;
            var args = new IMathObject[] {"filename".ToStringValue()};
            // when
            var result =
                LoadImageFunction.Value.CallWithLoader(null, args, null);
            // then
            Assert.IsTrue(result.IsMatrix(null));
            Assert.IsInstanceOf<Matrix>(result);
            var matrix = (Matrix) result;
            Assert.AreEqual(2, matrix.RowCount);
            Assert.AreEqual(2, matrix.ColumnCount);
            Assert.AreEqual(16777215, matrix[0, 0].ToNumber().Value);
            Assert.AreEqual(16711680, matrix[0, 1].ToNumber().Value);
            Assert.AreEqual(65280, matrix[1, 0].ToNumber().Value);
            Assert.AreEqual(255, matrix[1, 1].ToNumber().Value);
        }
    }
}
