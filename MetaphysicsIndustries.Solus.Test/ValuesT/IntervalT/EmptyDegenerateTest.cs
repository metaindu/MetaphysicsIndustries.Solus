using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ValuesT.IntervalT
{
    [TestFixture]
    public class EmptyDegenerateTest
    {
        [Test]
        public void EqualBoundsBothClosedIsDegenerateNotEmpty()
        {
            // given
            var i = new Interval(1, false, 1, false, false);
            // expect
            Assert.IsTrue(i.IsDegenerate);
            Assert.IsFalse(i.IsEmpty);
        }

        [Test]
        public void EqualBoundsSomeOpenIsEmptyNotDegenerate2()
        {
            // given
            var i = new Interval(1, true, 1, false, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsTrue(i.IsEmpty);
        }

        [Test]
        public void EqualBoundsSomeOpenIsEmptyNotDegenerate3()
        {
            // given
            var i = new Interval(1, false, 1, true, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsTrue(i.IsEmpty);
        }

        [Test]
        public void EqualBoundsSomeOpenIsEmptyNotDegenerate4()
        {
            // given
            var i = new Interval(1, true, 1, true, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsTrue(i.IsEmpty);
        }

        [Test]
        public void NonEqualBoundsNeitherEmptyNorDegenerate1()
        {
            // given
            var i = new Interval(1, false, 2, false, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsFalse(i.IsEmpty);
        }

        [Test]
        public void NonEqualBoundsNeitherEmptyNorDegenerate2()
        {
            // given
            var i = new Interval(1, true, 2, false, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsFalse(i.IsEmpty);
        }

        [Test]
        public void NonEqualBoundsNeitherEmptyNorDegenerate3()
        {
            // given
            var i = new Interval(1, false, 2, true, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsFalse(i.IsEmpty);
        }

        [Test]
        public void NonEqualBoundsNeitherEmptyNorDegenerate4()
        {
            // given
            var i = new Interval(1, true, 2, true, false);
            // expect
            Assert.IsFalse(i.IsDegenerate);
            Assert.IsFalse(i.IsEmpty);
        }
    }
}
