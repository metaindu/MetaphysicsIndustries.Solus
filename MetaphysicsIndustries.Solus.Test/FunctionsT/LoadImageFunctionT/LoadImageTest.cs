
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
using System.IO;
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
            Func<string, Stream> reader = filename =>
                new MemoryStream(TinyTestPatternPng);
            // when
            var result = LoadImageFunction.LoadImage("filename", reader);
            // then
            Assert.That(result.RowCount, Is.EqualTo(4));
            Assert.That(result.ColumnCount, Is.EqualTo(4));
            Assert.That(result[0, 0].ToNumber().Value, Is.EqualTo(0x0000ff));
            Assert.That(result[0, 1].ToNumber().Value, Is.EqualTo(0x00007f));
            Assert.That(result[0, 2].ToNumber().Value, Is.EqualTo(0x000000));
            Assert.That(result[0, 3].ToNumber().Value, Is.EqualTo(0x7f7f7f));
            Assert.That(result[1, 0].ToNumber().Value, Is.EqualTo(0x00ff00));
            Assert.That(result[1, 1].ToNumber().Value, Is.EqualTo(0x007f00));
            Assert.That(result[1, 2].ToNumber().Value, Is.EqualTo(0xffff00));
            Assert.That(result[1, 3].ToNumber().Value, Is.EqualTo(0x7f7f00));
            Assert.That(result[2, 0].ToNumber().Value, Is.EqualTo(0xff0000));
            Assert.That(result[2, 1].ToNumber().Value, Is.EqualTo(0x7f0000));
            Assert.That(result[2, 2].ToNumber().Value, Is.EqualTo(0xff00ff));
            Assert.That(result[2, 3].ToNumber().Value, Is.EqualTo(0x7f007f));
            Assert.That(result[3, 0].ToNumber().Value, Is.EqualTo(0xffffff));
            Assert.That(result[3, 1].ToNumber().Value, Is.EqualTo(0x7f7f7f));
            Assert.That(result[3, 2].ToNumber().Value, Is.EqualTo(0x00ffff));
            Assert.That(result[3, 3].ToNumber().Value, Is.EqualTo(0x007f7f));
        }

        [Test]
        public void LoadsImageIntoMatrix2()
        {
            // given
            Func<string, Stream> reader = filename =>
                new MemoryStream(TinyTestPatternPng);
            var args = new IMathObject[] {"filename".ToStringValue()};
            // when
            var result0 = LoadImageFunction.Value.CallWithReader(
                null,args, reader);
            // then
            Assert.IsTrue(result0.IsIsMatrix(null));
            var result = result0.ToMatrix();
            Assert.That(result.RowCount, Is.EqualTo(4));
            Assert.That(result.ColumnCount, Is.EqualTo(4));
            Assert.That(result[0, 0].ToNumber().Value, Is.EqualTo(0x0000ff));
            Assert.That(result[0, 1].ToNumber().Value, Is.EqualTo(0x00007f));
            Assert.That(result[0, 2].ToNumber().Value, Is.EqualTo(0x000000));
            Assert.That(result[0, 3].ToNumber().Value, Is.EqualTo(0x7f7f7f));
            Assert.That(result[1, 0].ToNumber().Value, Is.EqualTo(0x00ff00));
            Assert.That(result[1, 1].ToNumber().Value, Is.EqualTo(0x007f00));
            Assert.That(result[1, 2].ToNumber().Value, Is.EqualTo(0xffff00));
            Assert.That(result[1, 3].ToNumber().Value, Is.EqualTo(0x7f7f00));
            Assert.That(result[2, 0].ToNumber().Value, Is.EqualTo(0xff0000));
            Assert.That(result[2, 1].ToNumber().Value, Is.EqualTo(0x7f0000));
            Assert.That(result[2, 2].ToNumber().Value, Is.EqualTo(0xff00ff));
            Assert.That(result[2, 3].ToNumber().Value, Is.EqualTo(0x7f007f));
            Assert.That(result[3, 0].ToNumber().Value, Is.EqualTo(0xffffff));
            Assert.That(result[3, 1].ToNumber().Value, Is.EqualTo(0x7f7f7f));
            Assert.That(result[3, 2].ToNumber().Value, Is.EqualTo(0x00ffff));
            Assert.That(result[3, 3].ToNumber().Value, Is.EqualTo(0x007f7f));
        }

        public byte[] TinyTestPatternPng =
        {
            0x89, 0x50, 0x4e, 0x47, 0x0d, 0x0a, 0x1a, 0x0a,
            0x00, 0x00, 0x00, 0x0d, 0x49, 0x48, 0x44, 0x52,
            0x00, 0x00, 0x00, 0x04, 0x00, 0x00, 0x00, 0x04,
            0x08, 0x06, 0x00, 0x00, 0x00, 0xa9, 0xf1, 0x9e,
            0x7e, 0x00, 0x00, 0x04, 0x0e, 0x69, 0x43, 0x43,
            0x50, 0x6b, 0x43, 0x47, 0x43, 0x6f, 0x6c, 0x6f,
            0x72, 0x53, 0x70, 0x61, 0x63, 0x65, 0x47, 0x65,
            0x6e, 0x65, 0x72, 0x69, 0x63, 0x52, 0x47, 0x42,
            0x00, 0x00, 0x38, 0x8d, 0x8d, 0x55, 0x5d, 0x68,
            0x1c, 0x55, 0x14, 0x3e, 0x9b, 0xb9, 0xb3, 0x2b,
            0x24, 0xce, 0x83, 0xd4, 0xa6, 0xa6, 0x92, 0x0e,
            0xfe, 0x35, 0x94, 0xb4, 0x6c, 0x52, 0xd1, 0x84,
            0xda, 0xe8, 0xfe, 0x65, 0xb3, 0x6d, 0xdc, 0x2c,
            0x93, 0x6c, 0xb4, 0x41, 0x90, 0xc9, 0xec, 0xdd,
            0x9d, 0x69, 0x26, 0x33, 0xe3, 0xfc, 0xa4, 0x69,
            0x29, 0x3e, 0x14, 0x41, 0x10, 0xc1, 0xa8, 0xe0,
            0x93, 0xe0, 0xff, 0x5b, 0xc1, 0x27, 0x21, 0x6a,
            0xab, 0xed, 0x8b, 0x2d, 0xa2, 0xb4, 0x50, 0xa2,
            0x04, 0x83, 0x28, 0xf8, 0xd0, 0xfa, 0x47, 0xa1,
            0xd2, 0x17, 0x09, 0xeb, 0xb9, 0x33, 0xb3, 0xbb,
            0x93, 0xb8, 0x6b, 0xbd, 0xcb, 0xdc, 0xf9, 0xe6,
            0x9c, 0xef, 0x7e, 0xe7, 0xde, 0x73, 0xee, 0xde,
            0x0b, 0x90, 0xb8, 0x2c, 0x5b, 0x96, 0xde, 0x25,
            0x02, 0x2c, 0x1a, 0xae, 0x2d, 0xe5, 0xd3, 0xe2,
            0xb3, 0xc7, 0xe6, 0xc4, 0xc4, 0x3a, 0x74, 0xc1,
            0x7d, 0xd0, 0x0d, 0x7d, 0xd0, 0x2d, 0x2b, 0x8e,
            0x95, 0x2a, 0x95, 0x26, 0x01, 0x1b, 0xe3, 0xc2,
            0xbf, 0xda, 0xed, 0xef, 0x20, 0xc6, 0xde, 0xd7,
            0xf6, 0xb7, 0xf7, 0xff, 0x67, 0xeb, 0xae, 0x50,
            0x47, 0x01, 0x88, 0xdd, 0x85, 0xd8, 0xac, 0x38,
            0xca, 0x22, 0xe2, 0x65, 0x00, 0xfe, 0x15, 0xc5,
            0xb2, 0x5d, 0x80, 0x04, 0x41, 0xfb, 0xc8, 0x09,
            0xd7, 0x62, 0xf8, 0x09, 0xc4, 0x3b, 0x6c, 0x9c,
            0x20, 0xe2, 0x12, 0xc3, 0xb5, 0x00, 0x57, 0x18,
            0x9e, 0x0f, 0xf0, 0xb2, 0xcf, 0x99, 0x91, 0x32,
            0x88, 0x5f, 0x45, 0x2c, 0x28, 0xaa, 0x8c, 0xfe,
            0xc4, 0xdb, 0x88, 0x07, 0xe7, 0x23, 0xf6, 0x5a,
            0x04, 0x07, 0x73, 0xf0, 0xdb, 0x8e, 0x3c, 0x35,
            0xa8, 0xad, 0x29, 0x22, 0xcb, 0x45, 0xc9, 0x36,
            0xab, 0x9a, 0x4e, 0x23, 0xd3, 0xbd, 0x83, 0xfb,
            0x7f, 0xb6, 0x45, 0xdd, 0x6b, 0xc4, 0xdb, 0x83,
            0x4f, 0x8f, 0xb3, 0x30, 0x7d, 0x14, 0xdf, 0x03,
            0xb8, 0xf6, 0x97, 0x2a, 0x72, 0x96, 0xe1, 0x87,
            0x10, 0x9f, 0x55, 0xe4, 0xdc, 0x74, 0x88, 0xaf,
            0x2e, 0x69, 0xb3, 0xc5, 0x10, 0xff, 0x65, 0xb9,
            0x69, 0x09, 0xf1, 0x23, 0x00, 0x5d, 0xbb, 0xbc,
            0x85, 0x72, 0x0a, 0xf1, 0x3e, 0xc4, 0x63, 0x55,
            0x7b, 0xbc, 0x1c, 0xe8, 0x74, 0xa9, 0xaa, 0x37,
            0xd1, 0xc0, 0x2b, 0xa7, 0xd4, 0x99, 0x67, 0x10,
            0xdf, 0x83, 0x78, 0x75, 0xc1, 0x3c, 0xca, 0xc6,
            0xee, 0x44, 0xfc, 0xb5, 0x31, 0x5f, 0x9c, 0x0a,
            0x75, 0x7e, 0x52, 0x9c, 0x0c, 0xe6, 0x0f, 0x1e,
            0x00, 0xe0, 0xe2, 0x2a, 0x2d, 0xb0, 0x7a, 0xf7,
            0x23, 0x1e, 0xb0, 0x4d, 0x69, 0x2a, 0x88, 0xcb,
            0x8d, 0x57, 0x68, 0x36, 0xc7, 0xf2, 0x88, 0xf8,
            0xb8, 0xe6, 0x16, 0x66, 0x02, 0x7d, 0xee, 0x2d,
            0x67, 0x69, 0x3a, 0xd7, 0xd0, 0x39, 0xa5, 0x66,
            0x8a, 0x41, 0x2c, 0xee, 0x8b, 0xe3, 0xf2, 0x11,
            0x56, 0xa7, 0x3e, 0xc4, 0x57, 0xa9, 0x9e, 0x97,
            0x42, 0xfd, 0x5f, 0x2d, 0xb7, 0x14, 0xc6, 0x25,
            0x3d, 0x86, 0x5e, 0x9c, 0x0c, 0x74, 0xc8, 0x30,
            0x75, 0xfc, 0xf5, 0xfa, 0x76, 0x57, 0x9d, 0x99,
            0x08, 0xe2, 0x92, 0x39, 0x17, 0x0b, 0x1a, 0x8c,
            0x25, 0x2f, 0x56, 0xb5, 0xf1, 0x42, 0xc8, 0x7f,
            0x57, 0xb5, 0x27, 0xa4, 0x10, 0x5f, 0xb6, 0x74,
            0x7f, 0x8f, 0xe2, 0xdc, 0xc8, 0x4d, 0xdb, 0x93,
            0xca, 0x01, 0x9f, 0xbf, 0x9f, 0x1a, 0xe5, 0x50,
            0x93, 0x1f, 0x93, 0xed, 0x5c, 0x3e, 0xc4, 0x15,
            0x98, 0x8d, 0xc9, 0x40, 0xc1, 0x84, 0x79, 0xec,
            0x15, 0x30, 0x60, 0x13, 0x44, 0x90, 0x20, 0x0f,
            0x69, 0x7c, 0x5b, 0x60, 0xa3, 0xa7, 0x0a, 0x1a,
            0xe8, 0x68, 0xa1, 0xe8, 0xa5, 0x68, 0xa1, 0xf8,
            0xd5, 0xe0, 0xec, 0xdf, 0xc2, 0x29, 0xf9, 0xfe,
            0x00, 0xb7, 0x54, 0x6a, 0xfe, 0xc8, 0xeb, 0xe8,
            0xd1, 0x30, 0x42, 0xa6, 0xff, 0x23, 0xf0, 0xd0,
            0xaa, 0xc2, 0xef, 0x68, 0x55, 0x23, 0xbc, 0x0c,
            0x7e, 0x79, 0x68, 0xab, 0x75, 0xd0, 0x09, 0x66,
            0x70, 0x23, 0xd4, 0x31, 0x49, 0x2f, 0x49, 0x92,
            0x83, 0xf8, 0x1c, 0x22, 0x93, 0xe4, 0x30, 0x19,
            0x21, 0xa3, 0x20, 0x92, 0x27, 0xc9, 0x53, 0x64,
            0x8c, 0x64, 0xd1, 0x3a, 0x4a, 0x0e, 0x35, 0xc7,
            0x96, 0x22, 0x73, 0x64, 0xf3, 0xb9, 0xd1, 0xd4,
            0x79, 0x01, 0x23, 0x52, 0x9f, 0x37, 0x8b, 0xbc,
            0x8b, 0xe8, 0x77, 0x41, 0xc6, 0xfe, 0x67, 0x64,
            0x98, 0xe0, 0xb4, 0xcf, 0xc5, 0x4a, 0x9f, 0x37,
            0x10, 0xd8, 0xcf, 0xd8, 0xcf, 0x6b, 0xca, 0x95,
            0xd7, 0x6e, 0x5e, 0x3a, 0x7d, 0x6e, 0x57, 0x8b,
            0xbb, 0x46, 0x56, 0x9f, 0xbb, 0xd6, 0x73, 0xe9,
            0x74, 0x24, 0x67, 0x1a, 0x6a, 0x2d, 0x74, 0xc8,
            0xda, 0xd4, 0x9d, 0x72, 0xcf, 0xff, 0xc2, 0x5f,
            0xe7, 0xd7, 0xb0, 0x5f, 0xe7, 0x37, 0x5a, 0x0c,
            0xfe, 0x07, 0x7e, 0x03, 0x7f, 0xeb, 0xdb, 0x56,
            0xb7, 0x35, 0xeb, 0x34, 0xcc, 0x56, 0x0a, 0x7d,
            0xba, 0x6f, 0x5b, 0xc4, 0x47, 0xf3, 0x3d, 0x4e,
            0x64, 0x3e, 0xa5, 0x2d, 0x55, 0x6c, 0x61, 0xb3,
            0xa3, 0xa2, 0x59, 0x35, 0x56, 0xfa, 0x02, 0x1f,
            0xcb, 0x00, 0x7d, 0xb9, 0x78, 0xbb, 0x08, 0x67,
            0x06, 0x5b, 0xfc, 0xe4, 0xf7, 0xc9, 0x3f, 0x92,
            0x6b, 0xc9, 0xf7, 0x92, 0x1f, 0x26, 0x7f, 0xe3,
            0xde, 0xe4, 0x3e, 0xe1, 0xce, 0x73, 0x9f, 0x72,
            0x9f, 0x71, 0xdf, 0x80, 0xc8, 0x5d, 0xe0, 0x2e,
            0x72, 0x5f, 0x72, 0x5f, 0x71, 0x1f, 0x73, 0x9f,
            0x47, 0x6a, 0xd4, 0x79, 0xef, 0x34, 0x6b, 0xee,
            0xaf, 0xa3, 0xb1, 0x0a, 0xe6, 0x69, 0x97, 0x51,
            0xdc, 0x9f, 0x42, 0x5a, 0xd8, 0x2d, 0x3c, 0x28,
            0x64, 0x85, 0x3d, 0xc2, 0xc3, 0xc2, 0x64, 0x4b,
            0x4f, 0xe8, 0x15, 0x86, 0x84, 0x09, 0x61, 0x2f,
            0x7a, 0x76, 0x37, 0xab, 0x13, 0x8d, 0x17, 0x5d,
            0xbb, 0x06, 0xc7, 0xb0, 0x6f, 0x64, 0xab, 0x7d,
            0xac, 0x80, 0x17, 0xa9, 0x73, 0xec, 0x6e, 0xac,
            0xb3, 0xd6, 0xe1, 0x3f, 0x54, 0x46, 0x96, 0x06,
            0x27, 0x7c, 0xa6, 0xe3, 0xef, 0x33, 0x03, 0x4e,
            0x6e, 0xe3, 0x84, 0x23, 0x49, 0x3f, 0x19, 0x22,
            0x85, 0x6d, 0xbb, 0x7a, 0x84, 0xed, 0xf5, 0xa6,
            0x76, 0x7e, 0x4b, 0x3d, 0xda, 0xaf, 0x81, 0xc6,
            0x73, 0xf1, 0x6c, 0x3c, 0x05, 0x62, 0x7c, 0x5f,
            0x7c, 0x34, 0x3e, 0x14, 0x3f, 0xc2, 0x70, 0x83,
            0x19, 0xdf, 0x8b, 0xbe, 0x51, 0xec, 0xf1, 0xd4,
            0x72, 0xe9, 0xb2, 0xcb, 0x0e, 0xe2, 0x8c, 0x69,
            0x9d, 0xb4, 0xb5, 0x9a, 0xea, 0x8a, 0xc3, 0xc9,
            0xe4, 0xe3, 0x62, 0x0a, 0xaf, 0x32, 0x2a, 0x16,
            0x0c, 0xe5, 0xc0, 0xa0, 0x28, 0xeb, 0xba, 0xe8,
            0xbb, 0x1c, 0xd1, 0xa6, 0x0e, 0xb5, 0x97, 0x68,
            0xe5, 0x00, 0xb0, 0x7b, 0x32, 0x38, 0xc2, 0x6f,
            0x49, 0xfe, 0xfd, 0x17, 0xdb, 0x79, 0xa5, 0x65,
            0x73, 0x9f, 0x06, 0x38, 0xfc, 0x27, 0x3b, 0xf7,
            0x5a, 0xb6, 0x39, 0x0f, 0xe0, 0xac, 0x03, 0xd0,
            0xfb, 0x68, 0xcb, 0x36, 0x80, 0x67, 0xe3, 0xbd,
            0xef, 0x00, 0xac, 0x3e, 0xa6, 0x78, 0xf6, 0x52,
            0x78, 0x27, 0xc4, 0x62, 0xdf, 0x02, 0x38, 0xd5,
            0x83, 0xc3, 0xc1, 0x57, 0x4f, 0x1a, 0xcf, 0xab,
            0x1f, 0xeb, 0xf5, 0x5b, 0x78, 0x6e, 0x25, 0xde,
            0x00, 0xd8, 0x7c, 0xbd, 0x5e, 0xff, 0xfb, 0xfd,
            0x7a, 0x7d, 0xf3, 0x03, 0xd4, 0xdf, 0x00, 0xb8,
            0xa0, 0xff, 0x03, 0xff, 0x25, 0x78, 0x0b, 0xc5,
            0x63, 0xd6, 0xcb, 0x00, 0x00, 0x00, 0x38, 0x65,
            0x58, 0x49, 0x66, 0x4d, 0x4d, 0x00, 0x2a, 0x00,
            0x00, 0x00, 0x08, 0x00, 0x01, 0x87, 0x69, 0x00,
            0x04, 0x00, 0x00, 0x00, 0x01, 0x00, 0x00, 0x00,
            0x1a, 0x00, 0x00, 0x00, 0x00, 0x00, 0x02, 0xa0,
            0x02, 0x00, 0x04, 0x00, 0x00, 0x00, 0x01, 0x00,
            0x00, 0x00, 0x04, 0xa0, 0x03, 0x00, 0x04, 0x00,
            0x00, 0x00, 0x01, 0x00, 0x00, 0x00, 0x04, 0x00,
            0x00, 0x00, 0x00, 0xe6, 0xa4, 0xdc, 0x38, 0x00,
            0x00, 0x00, 0x33, 0x49, 0x44, 0x41, 0x54, 0x08,
            0x1d, 0x2d, 0x8b, 0xc1, 0x11, 0x00, 0x20, 0x08,
            0xc3, 0x82, 0xba, 0x77, 0x61, 0x33, 0x9c, 0x0c,
            0xf1, 0xb4, 0x9f, 0xf4, 0x91, 0xcc, 0xea, 0x01,
            0xbe, 0x33, 0xfd, 0x72, 0x61, 0xd6, 0x84, 0x0a,
            0xc7, 0xdf, 0xa3, 0x10, 0xd7, 0x2b, 0x35, 0x07,
            0xd6, 0x45, 0xe8, 0x8b, 0xe2, 0x00, 0x2f, 0xd9,
            0x15, 0x40, 0xdd, 0x34, 0xd2, 0x54, 0x00, 0x00,
            0x00, 0x00, 0x49, 0x45, 0x4e, 0x44, 0xae, 0x42,
            0x60, 0x82
        };
    }
}
