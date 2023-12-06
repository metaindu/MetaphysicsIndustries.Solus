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

using MetaphysicsIndustries.Solus.Exceptions;
using MetaphysicsIndustries.Solus.Functions;
using MetaphysicsIndustries.Solus.Sets;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.FunctionsT.MultiplicationOperationT
{
    [TestFixture]
    public class GetResultTest
    {
        [Test]
        public void ScalarsYieldScalar()
        {
            // given
            var args = new ISet[] { Reals.Value, Reals.Value };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Reals.Value));
        }

        [Test]
        public void ScalarsAndVectorYieldVector1()
        {
            // given
            var args = new ISet[] { Vectors.R3, Reals.Value };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Vectors.R3));
        }

        [Test]
        public void ScalarsAndVectorYieldVector2()
        {
            // given
            var args = new ISet[] { Vectors.R3, Reals.Value, Reals.Value };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Vectors.R3));
        }

        [Test]
        public void ScalarsAndVectorYieldVector3()
        {
            // given
            var args = new ISet[] { Reals.Value, Vectors.R3 };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Vectors.R3));
        }

        [Test]
        public void ScalarsAndVectorYieldVector4()
        {
            // given
            var args = new ISet[] { Reals.Value, Reals.Value, Vectors.R3 };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Vectors.R3));
        }

        [Test]
        public void ScalarsAndVectorYieldVector5()
        {
            // given
            var args = new ISet[] { Reals.Value, Vectors.R3, Reals.Value };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Vectors.R3));
        }

        [Test]
        public void MultipleVectorsRaises()
        {
            // given
            var args = new ISet[] { Vectors.R3, Vectors.R3, };
            // expect
            var value = MultiplicationOperation.Value;
            Assert.Throws<TypeException>(
                () => value.GetResultType(null, args));
        }


        [Test]
        public void ScalarsAndMatrixYieldMatrix1()
        {
            // given
            var args = new ISet[] { Matrices.M3x3, Reals.Value };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Matrices.M3x3));
        }

        [Test]
        public void ScalarsAndMatrixYieldMatrix2()
        {
            // given
            var args = new ISet[] { Matrices.M3x3, Reals.Value, Reals.Value };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Matrices.M3x3));
        }

        [Test]
        public void ScalarsAndMatrixYieldMatrix3()
        {
            // given
            var args = new ISet[] { Reals.Value, Matrices.M3x3 };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Matrices.M3x3));
        }

        [Test]
        public void ScalarsAndMatrixYieldMatrix4()
        {
            // given
            var args = new ISet[] { Reals.Value, Reals.Value, Matrices.M3x3 };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Matrices.M3x3));
        }

        [Test]
        public void ScalarsAndMatrixYieldMatrix5()
        {
            // given
            var args = new ISet[] { Reals.Value, Matrices.M3x3, Reals.Value };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Matrices.M3x3));
        }

        [Test]
        public void MatrixAndMatrixYieldsMatrix1()
        {
            // given
            var args = new ISet[] { Matrices.M3x3, Matrices.M3x3 };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Matrices.M3x3));
        }

        [Test]
        public void MatrixAndMatrixYieldsMatrix2()
        {
            // given
            var args = new ISet[] { Matrices.M2x4, Matrices.M4x3 };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Matrices.M2x3));
        }

        [Test]
        public void IncompatibleMatrixAndMatrixThrows()
        {
            // given
            var args = new ISet[] { Matrices.M2x4, Matrices.M2x4 };
            // expect
            var value = MultiplicationOperation.Value;
            Assert.Throws<TypeException>(
                () => value.GetResultType(null, args));
        }

        [Test]
        public void MatrixAndVectorYieldsVector1()
        {
            // given
            var args = new ISet[] { Matrices.M3x3, Vectors.R3 };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Vectors.R3));
        }

        [Test]
        public void MatrixAndVectorYieldsVector2()
        {
            // given
            var args = new ISet[] { Vectors.R3, Matrices.M3x3 };
            // when
            var value = MultiplicationOperation.Value;
            var result = value.GetResultType(null, args);
            // then
            Assert.That(result, Is.SameAs(Vectors.R3));
        }

        [Test]
        public void IncompatibleMatrixAndVectorThrows1()
        {
            // given
            var args = new ISet[] { Matrices.M3x3, Vectors.R2 };
            // expect
            var value = MultiplicationOperation.Value;
            Assert.Throws<TypeException>(
                () => value.GetResultType(null, args));
        }

        [Test]
        public void IncompatibleMatrixAndVectorThrows2()
        {
            // given
            var args = new ISet[] { Vectors.R2, Matrices.M3x3 };
            // expect
            var value = MultiplicationOperation.Value;
            Assert.Throws<TypeException>(
                () => value.GetResultType(null, args));
        }
    }
}
