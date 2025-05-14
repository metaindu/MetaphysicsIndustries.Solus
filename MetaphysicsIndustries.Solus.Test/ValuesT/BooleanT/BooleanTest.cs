
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

using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ValuesT.BooleanT
{
    [TestFixture]
    public class BooleanTest
    {
        [Test]
        public void CreateSetsValue1()
        {
            // when
            var result = new Boolean(true);
            // then
            Assert.True(result.Value);
        }

        [Test]
        public void CreateSetsValue2()
        {
            // when
            var result = new Boolean(false);
            // then
            Assert.False(result.Value);
        }

        [Test]
        public void EqualsOtherBooleanWithSameValue1()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(true);
            // expect
            Assert.That(a.Equals(b));
            Assert.That(b.Equals(a));
        }

        [Test]
        public void EqualsOtherBooleanWithSameValue2()
        {
            // given
            var a = new Boolean(false);
            var b = new Boolean(false);
            // expect
            Assert.That(a.Equals(b));
            Assert.That(b.Equals(a));
        }

        [Test]
        public void DoesNotEqualOtherBooleanWithDifferentValue()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(false);
            // expect
            Assert.That(!a.Equals(b));
            Assert.That(!b.Equals(a));
        }

        [Test]
        public void ObjectEqualsOtherBooleanWithSameValue1()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(true);
            // expect
            Assert.That(a.Equals((object)b));
            Assert.That(b.Equals((object)a));
        }

        [Test]
        public void ObjectEqualsOtherBooleanWithSameValue2()
        {
            // given
            var a = new Boolean(false);
            var b = new Boolean(false);
            // expect
            Assert.That(a.Equals((object)b));
            Assert.That(b.Equals((object)a));
        }

        [Test]
        public void ObjectDoesNotEqualOtherBooleanWithDifferentValue()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(false);
            // expect
            Assert.That(!a.Equals((object)b));
            Assert.That(!b.Equals((object)a));
        }

        [Test]
        public void EqualsSystemBoolWithSameValue1()
        {
            // given
            var a = new Boolean(true);
            // expect
            Assert.That(a.Equals(true));
        }

        [Test]
        public void EqualsSystemBoolWithSameValue2()
        {
            // given
            var a = new Boolean(false);
            // expect
            Assert.That(a.Equals(false));
        }

        [Test]
        public void DoesNotEqualSystemBoolWithDifferentValue()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(false);
            // expect
            Assert.That(!a.Equals(false));
            Assert.That(!b.Equals(true));
        }

        [Test]
        public void DoesNotEqualNonBool()
        {
            // given
            var a = new Boolean(true);
            // expect
            Assert.That(!a.Equals("abc"));
            Assert.That(!a.Equals("true"));
            Assert.That(!a.Equals(1));
        }

        [Test]
        public void SameValueHashCodeMatches1()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(true);
            // expect
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
        }

        [Test]
        public void SameValueHashCodeMatches2()
        {
            // given
            var a = new Boolean(false);
            var b = new Boolean(false);
            // expect
            Assert.That(a.GetHashCode(), Is.EqualTo(b.GetHashCode()));
        }

        [Test]
        public void DifferentValueHashCodeDoesNotMatch()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(false);
            // expect
            Assert.That(a.GetHashCode(), Is.Not.EqualTo(b.GetHashCode()));
        }

        [Test]
        public void SystemBoolSameValueHashCodeMatches1()
        {
            // given
            var a = new Boolean(true);
            // expect
            Assert.That(a.GetHashCode(), Is.EqualTo(true.GetHashCode()));
        }

        [Test]
        public void SystemBoolSameValueHashCodeMatches2()
        {
            // given
            var a = new Boolean(false);
            // expect
            Assert.That(a.GetHashCode(), Is.EqualTo(false.GetHashCode()));
        }

        [Test]
        public void SystemBoolDifferentValueHashCodeDoesNotMatch()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(false);
            // expect
            Assert.That(a.GetHashCode(), Is.Not.EqualTo(false.GetHashCode()));
            Assert.That(b.GetHashCode(), Is.Not.EqualTo(true.GetHashCode()));
        }

        [Test]
        public void OperatorEqualsOtherBooleanWithSameValue1()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(true);
            // expect
            Assert.That(a == b);
            Assert.That(b == a);
        }

        [Test]
        public void OperatorEqualsOtherBooleanWithSameValue2()
        {
            // given
            var a = new Boolean(false);
            var b = new Boolean(false);
            // expect
            Assert.That(a == b);
            Assert.That(b == a);
        }

        [Test]
        public void OperatorDoesNotEqualOtherBooleanWithDifferentValue()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(false);
            // expect
            Assert.That(!(a == b));
            Assert.That(!(b == a));
        }

        [Test]
        public void OperatorEqualsSystemBoolWithSameValue1()
        {
            // given
            var a = new Boolean(true);
            // expect
            Assert.That(a == true);
        }

        [Test]
        public void OperatorEqualsSystemBoolWithSameValue2()
        {
            // given
            var a = new Boolean(false);
            // expect
            Assert.That(a == false);
        }

        [Test]
        public void OperatorDoesNotEqualSystemBoolWithDifferentValue()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(false);
            // expect
            Assert.That(!(a == false));
            Assert.That(!(b == true));
        }

        [Test]
        public void Operator2EqualsOtherBooleanWithSameValue1()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(true);
            // expect
            Assert.That(!(a != b));
            Assert.That(!(b != a));
        }

        [Test]
        public void Operator2EqualsOtherBooleanWithSameValue2()
        {
            // given
            var a = new Boolean(false);
            var b = new Boolean(false);
            // expect
            Assert.That(!(a != b));
            Assert.That(!(b != a));
        }

        [Test]
        public void Operator2DoesNotEqualOtherBooleanWithDifferentValue()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(false);
            // expect
            Assert.That(a != b);
            Assert.That(b != a);
        }

        [Test]
        public void Operator2EqualsSystemBoolWithSameValue1()
        {
            // given
            var a = new Boolean(true);
            // expect
            Assert.That(!(a != true));
        }

        [Test]
        public void Operator2EqualsSystemBoolWithSameValue2()
        {
            // given
            var a = new Boolean(false);
            // expect
            Assert.That(!(a != false));
        }

        [Test]
        public void Operator2DoesNotEqualSystemBoolWithDifferentValue()
        {
            // given
            var a = new Boolean(true);
            var b = new Boolean(false);
            // expect
            Assert.That(a != false);
            Assert.That(b != true);
        }

        [Test]
        public void CastToSystemBool1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = (bool)a;
            // expect
            Assert.That(a);
        }

        [Test]
        public void CastToSystemBool2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = (bool)a;
            // expect
            Assert.That(!a);
        }

        [Test]
        public void ImplicitCastToSystemBool1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = !!a;
            // expect
            Assert.That(a);
        }

        [Test]
        public void ImplicitCastToSystemBool2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = !!a;
            // expect
            Assert.That(!a);
        }

        [Test]
        public void IsNotScalar1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.IsScalar(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotScalar2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.IsScalar(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsBoolean1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.IsBoolean(null);
            // then
            Assert.That(result.HasValue && result.Value == true);
        }

        [Test]
        public void IsBoolean2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.IsBoolean(null);
            // then
            Assert.That(result.HasValue && result.Value == true);
        }

        [Test]
        public void IsNotVector1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.IsVector(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotVector2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.IsVector(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotMatrix1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.IsMatrix(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotMatrix2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.IsMatrix(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void TensorRankIsZero1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.GetTensorRank(null);
            // then
            Assert.That(result.HasValue && result.Value == 0);
        }

        [Test]
        public void TensorRankIsZero2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.GetTensorRank(null);
            // then
            Assert.That(result.HasValue && result.Value == 0);
        }

        [Test]
        public void IsNotString1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.IsString(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotString2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.IsString(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotDimension1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.GetDimension(null, 0);
            // then
            Assert.That(!result.HasValue);
        }

        [Test]
        public void IsNotDimension2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.GetDimension(null, 0);
            // then
            Assert.That(!result.HasValue);
        }

        [Test]
        public void DoesNotHaveDimensions1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.GetDimensions(null);
            // then
            Assert.IsNull(result);
        }

        [Test]
        public void DoesNotHaveDimensions2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.GetDimensions(null);
            // then
            Assert.IsNull(result);
        }

        [Test]
        public void DoesNotHaveVectorLength1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.GetVectorLength(null);
            // then
            Assert.That(!result.HasValue);
        }

        [Test]
        public void DoesNotHaveVectorLength2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.GetVectorLength(null);
            // then
            Assert.That(!result.HasValue);
        }

        [Test]
        public void IsNotInterval1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.IsInterval(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotInterval2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.IsInterval(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotFunction1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.IsFunction(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotFunction2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.IsFunction(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotExpression1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.IsExpression(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotExpression2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.IsExpression(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotSet1()
        {
            // given
            var a = new Boolean(true);
            // when
            var result = a.IsSet(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsNotSet2()
        {
            // given
            var a = new Boolean(false);
            // when
            var result = a.IsSet(null);
            // then
            Assert.That(result.HasValue && result.Value == false);
        }

        [Test]
        public void IsConcrete1()
        {
            // given
            var a = new Boolean(true);
            // expect
            Assert.That(a.IsConcrete);
        }

        [Test]
        public void IsConcrete2()
        {
            // given
            var a = new Boolean(false);
            // expect
            Assert.That(a.IsConcrete);
        }

        [Test]
        public void HasDocString1()
        {
            // given
            var a = new Boolean(true);
            // expect
            Assert.That(a.DocString, Is.EqualTo("Boolean True value"));
        }

        [Test]
        public void HasDocString2()
        {
            // given
            var a = new Boolean(false);
            // expect
            Assert.That(a.DocString, Is.EqualTo("Boolean False value"));
        }
    }
}
