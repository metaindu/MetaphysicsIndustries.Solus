
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

using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.SizeFunctionT
{
    [TestFixture]
    public class CallTest
    {
        private static SolusEnvironment empty = new SolusEnvironment();

        [Test]
        public void VectorYieldsLength()
        {

            // given
            var args = new IMathObject[]
            {
                new float[] {1, 2}.ToVector()
            };
            // when
            var result = SizeFunction.Value.Call(empty, args);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(2, result.ToFloat());
        }

        [Test]
        public void MatrixYieldsRowAndColumnCounts()
        {
            // given
            var args = new IMathObject[]
            {
                new Matrix(new float[,]
                {
                    {1, 2, 3},
                    {4, 5, 6}
                })
            };
            // when
            var result = SizeFunction.Value.Call(empty, args);
            // then
            Assert.IsTrue(result.IsVector(null));
            var v = result.ToVector();
            Assert.AreEqual(2, v.Length);
            Assert.AreEqual(2, v[0].ToFloat());
            Assert.AreEqual(3, v[1].ToFloat());
        }

        [Test]
        public void StringYieldsLength()
        {
            // given
            var args = new IMathObject[] {"abc".ToStringValue()};
            // when
            var result = SizeFunction.Value.Call(empty, args);
            // then
            Assert.IsTrue(result.IsScalar(null));
            Assert.AreEqual(3, result.ToFloat());
        }
    }
}
