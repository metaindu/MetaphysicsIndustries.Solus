using System;
using MetaphysicsIndustries.Solus.Values;
using NUnit.Framework;

namespace MetaphysicsIndustries.Solus.Test.ValuesT.NumberT
{
    [TestFixture]
    public class NumberTest
    {
        [Test]
        public void CreateWithoutArgYieldsDefaultValues()
        {
            // when
            var result = new Number();
            // then
            Assert.AreEqual(0, result.Value);
            Assert.IsTrue(result.IsScalar);
            Assert.IsFalse(result.IsVector);
            Assert.IsFalse(result.IsMatrix);
            Assert.AreEqual(0, result.TensorRank);
            Assert.Throws<InvalidOperationException>(
                () => result.GetDimension(0));
            Assert.Throws<InvalidOperationException>(
                () => result.GetDimensions());
        }

        [Test]
        public void CreateWithArgYieldsThatValue()
        {
            // when
            var result = new Number(123);
            // then
            Assert.AreEqual(123, result.Value);
        }
    }
}
